using FluentAssertions;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.UnitTests.Entities;

public class PedidoTests
{
    private Cliente CriarClienteValido()
    {
        return Cliente.Criar("João Silva", "joao@email.com", TipoPessoa.Fisica);
    }

    [Fact]
    public void Criar_ComClienteValido_DeveCriarPedido()
    {
        var cliente = CriarClienteValido();

        var pedido = Pedido.Criar(cliente);

        pedido.Should().NotBeNull();
        pedido.ClienteId.Should().Be(cliente.Id);
        pedido.Status.Should().Be(StatusPedido.Pendente);
        pedido.NumeroPedido.Should().NotBeNullOrEmpty();
        pedido.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Criar_ComClienteNulo_DeveLancarExcecao()
    {
        var act = () => Pedido.Criar(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Criar_ComClienteInativo_DeveLancarExcecao()
    {
        var cliente = CriarClienteValido();
        cliente.Desativar();

        var act = () => Pedido.Criar(cliente);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*cliente inativo*");
    }

    [Fact]
    public void AdicionarItem_DeveSomarValorTotal()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);

        pedido.AdicionarItem("Produto A", 2, 50.00m);
        pedido.AdicionarItem("Produto B", 1, 100.00m);

        pedido.Itens.Should().HaveCount(2);
        pedido.ValorTotal.Amount.Should().Be(200.00m);
    }

    [Fact]
    public void AdicionarItem_EmPedidoNaoPendente_DeveLancarExcecao()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);
        pedido.AdicionarItem("Produto", 1, 100m);
        pedido.Confirmar();

        var act = () => pedido.AdicionarItem("Outro Produto", 1, 50m);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*não está pendente*");
    }

    [Fact]
    public void Confirmar_PedidoPendente_DeveConfirmar()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);
        pedido.AdicionarItem("Produto", 1, 100m);

        pedido.Confirmar();

        pedido.Status.Should().Be(StatusPedido.Confirmado);
    }

    [Fact]
    public void Confirmar_PedidoSemItens_DeveLancarExcecao()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);

        var act = () => pedido.Confirmar();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*ao menos um item*");
    }

    [Fact]
    public void Cancelar_PedidoValido_DeveCancelar()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);
        pedido.AdicionarItem("Produto", 1, 100m);
        pedido.Confirmar();

        pedido.Cancelar();

        pedido.Status.Should().Be(StatusPedido.Cancelado);
    }

    [Fact]
    public void Cancelar_PedidoEntregue_DeveLancarExcecao()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);
        pedido.AdicionarItem("Produto", 1, 100m);
        pedido.Confirmar();
        pedido.AtualizarStatus(StatusPedido.EmProcessamento);
        pedido.AtualizarStatus(StatusPedido.Enviado);
        pedido.AtualizarStatus(StatusPedido.Entregue);

        var act = () => pedido.Cancelar();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*já entregue*");
    }

    [Fact]
    public void AtualizarStatus_ComTransicaoInvalida_DeveLancarExcecao()
    {
        var cliente = CriarClienteValido();
        var pedido = Pedido.Criar(cliente);

        var act = () => pedido.AtualizarStatus(StatusPedido.Entregue);

        act.Should().Throw<InvalidOperationException>();
    }
}
