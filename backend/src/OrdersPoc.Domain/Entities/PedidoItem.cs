using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.Domain.Entities;

public class PedidoItem : Entity
{
    public Guid PedidoId { get; private set; }
    public string Produto { get; private set; }
    public int Quantidade { get; private set; }
    public Money PrecoUnitario { get; private set; }
    public Money Subtotal { get; private set; }

    public Pedido Pedido { get; private set; } = null!;

    private PedidoItem() { }

    private PedidoItem(string produto, int quantidade, Money precoUnitario)
    {
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        Subtotal = precoUnitario.Multiply(quantidade);

        ValidarItem();
    }

    public static PedidoItem Criar(string produto, int quantidade, decimal precoUnitario)
    {
        var preco = Money.Create(precoUnitario);
        return new PedidoItem(produto, quantidade, preco);
    }

    public void AtualizarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantidade));

        Quantidade = quantidade;
        Subtotal = PrecoUnitario.Multiply(quantidade);
        Atualizar();
    }

    public void AtualizarPreco(decimal precoUnitario)
    {
        PrecoUnitario = Money.Create(precoUnitario);
        Subtotal = PrecoUnitario.Multiply(Quantidade);
        Atualizar();
    }

    private void ValidarItem()
    {
        if (string.IsNullOrWhiteSpace(Produto))
            throw new ArgumentException("Produto é obrigatório", nameof(Produto));

        if (Quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero", nameof(Quantidade));

        if (PrecoUnitario.Amount <= 0)
            throw new ArgumentException("Preço unitário deve ser maior que zero");
    }
}