using FluentAssertions;
using OrdersPoc.Domain.Entities;

namespace OrdersPoc.UnitTests.Entities;

public class PedidoItemTests
{
    [Fact]
    public void Criar_ComDadosValidos_DeveCriarItem()
    {
        var produto = "Notebook";
        var quantidade = 2;
        var precoUnitario = 3000m;

        var item = PedidoItem.Criar(produto, quantidade, precoUnitario);

        item.Should().NotBeNull();
        item.Produto.Should().Be(produto);
        item.Quantidade.Should().Be(quantidade);
        item.PrecoUnitario.Amount.Should().Be(precoUnitario);
        item.Subtotal.Amount.Should().Be(6000m);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Criar_ComProdutoInvalido_DeveLancarExcecao(string produtoInvalido)
    {
        var act = () => PedidoItem.Criar(produtoInvalido, 1, 100m);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Criar_ComQuantidadeInvalida_DeveLancarExcecao(int quantidadeInvalida)
    {
        var act = () => PedidoItem.Criar("Produto", quantidadeInvalida, 100m);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AtualizarQuantidade_DeveRecalcularSubtotal()
    {
        var item = PedidoItem.Criar("Produto", 2, 50m);

        item.AtualizarQuantidade(5);

        item.Quantidade.Should().Be(5);
        item.Subtotal.Amount.Should().Be(250m);
    }

    [Fact]
    public void AtualizarPreco_DeveRecalcularSubtotal()
    {
        var item = PedidoItem.Criar("Produto", 2, 50m);

        item.AtualizarPreco(75m);

        item.PrecoUnitario.Amount.Should().Be(75m);
        item.Subtotal.Amount.Should().Be(150m);
    }
}