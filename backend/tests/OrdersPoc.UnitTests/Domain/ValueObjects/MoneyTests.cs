using FluentAssertions;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Create_ComValorValido_DeveRetornarMoney()
    {
        var valor = 100.50m;

        var money = Money.Create(valor);

        money.Should().NotBeNull();
        money.Amount.Should().Be(100.50m);
        money.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Create_ComValorNegativo_DeveLancarExcecao()
    {
        var valorNegativo = -10m;

        var act = () => Money.Create(valorNegativo);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Valor não pode ser negativo*");
    }

    [Fact]
    public void Create_DeveArredondarParaDuasCasas()
    {
        var valor = 100.999m;

        var money = Money.Create(valor);

        money.Amount.Should().Be(101.00m);
    }

    [Fact]
    public void Add_ComMesmaMoeda_DeveSomar()
    {
        var money1 = Money.Create(100m);
        var money2 = Money.Create(50m);

        var resultado = money1.Add(money2);

        resultado.Amount.Should().Be(150m);
    }

    [Fact]
    public void Add_ComMoedasDiferentes_DeveLancarExcecao()
    {
        var moneyBrl = Money.Create(100m, "BRL");
        var moneyUsd = Money.Create(50m, "USD");

        var act = () => moneyBrl.Add(moneyUsd);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*moedas diferentes*");
    }

    [Fact]
    public void Multiply_DeveMultiplicar()
    {
        var money = Money.Create(10m);

        var resultado = money.Multiply(5);

        resultado.Amount.Should().Be(50m);
    }

    [Fact]
    public void Zero_DeveRetornarMoneyZerado()
    {
        var money = Money.Zero();

        money.Amount.Should().Be(0m);
        money.Currency.Should().Be("BRL");
    }
}
