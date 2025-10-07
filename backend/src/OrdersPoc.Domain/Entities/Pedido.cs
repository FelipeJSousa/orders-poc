using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.Domain.Entities;

public class Pedido : Entity
{
    public string NumeroPedido { get; private set; }
    public Guid ClienteId { get; private set; }
    public DateTime DataPedido { get; private set; }
    public StatusPedido Status { get; private set; }
    public Money ValorTotal { get; private set; }
    public string? Observacoes { get; private set; }

    public Cliente Cliente { get; private set; } = null!;

    private readonly List<PedidoItem> _itens = new();
    public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

    private Pedido() { }

    private Pedido(Cliente cliente, string? observacoes = null)
    {
        ClienteId = cliente.Id;
        Cliente = cliente;
        DataPedido = DateTime.UtcNow;
        Status = StatusPedido.Pendente;
        ValorTotal = Money.Zero();
        Observacoes = observacoes;
        NumeroPedido = GerarNumeroPedido();
    }

    public static Pedido Criar(Cliente cliente, string? observacoes = null)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente), "Cliente é obrigatório");

        if (!cliente.Ativo)
            throw new InvalidOperationException("Não é possível criar pedido para cliente inativo");

        return new Pedido(cliente, observacoes);
    }

    public void AdicionarItem(string produto, int quantidade, decimal precoUnitario)
    {
        if (Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Não é possível adicionar itens a um pedido que não está pendente");

        var item = PedidoItem.Criar(produto, quantidade, precoUnitario);
        _itens.Add(item);

        RecalcularTotal();
        Atualizar();
    }

    public void RemoverItem(Guid itemId)
    {
        if (Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Não é possível remover itens de um pedido que não está pendente");

        var item = _itens.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _itens.Remove(item);
            RecalcularTotal();
            Atualizar();
        }
    }

    public void AtualizarStatus(StatusPedido novoStatus)
    {
        if (!PodeAlterarStatus(novoStatus))
            throw new InvalidOperationException($"Não é possível alterar status de {Status} para {novoStatus}");

        Status = novoStatus;
        Atualizar();
    }

    public void Confirmar()
    {
        if (Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Apenas pedidos pendentes podem ser confirmados");

        if (_itens.Count == 0)
            throw new InvalidOperationException("Pedido deve ter ao menos um item");

        Status = StatusPedido.Confirmado;
        Atualizar();
    }

    public void Cancelar()
    {
        if (Status == StatusPedido.Entregue)
            throw new InvalidOperationException("Não é possível cancelar pedido já entregue");

        if (Status == StatusPedido.Cancelado)
            throw new InvalidOperationException("Pedido já está cancelado");

        Status = StatusPedido.Cancelado;
        Atualizar();
    }

    private void RecalcularTotal()
    {
        var total = Money.Zero();
        foreach (var item in _itens)
        {
            total = total.Add(item.Subtotal);
        }
        ValorTotal = total;
    }

    private bool PodeAlterarStatus(StatusPedido novoStatus)
    {
        return (Status, novoStatus) switch
        {
            (StatusPedido.Pendente, StatusPedido.Confirmado) => true,
            (StatusPedido.Pendente, StatusPedido.Cancelado) => true,
            (StatusPedido.Confirmado, StatusPedido.EmProcessamento) => true,
            (StatusPedido.Confirmado, StatusPedido.Cancelado) => true,
            (StatusPedido.EmProcessamento, StatusPedido.Enviado) => true,
            (StatusPedido.EmProcessamento, StatusPedido.Cancelado) => true,
            (StatusPedido.Enviado, StatusPedido.Entregue) => true,
            _ => false
        };
    }

    private static string GerarNumeroPedido()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var random = new Random().Next(1000, 9999);
        return $"PED-{timestamp}-{random}";
    }
}
