using FluentAssertions;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.UnitTests.Domain.ValueObjects;

public class CnpjTests
{
    [Theory]
    [InlineData("14177000000176")]
    [InlineData("14.177.000/0001-76")]
    public void Create_ComCnpjValido_DeveRetornarCnpj(string cnpjValido)
    {
        var cnpj = Cnpj.Create(cnpjValido);

        cnpj.Should().NotBeNull();
        cnpj.Numero.Should().Be("14177000000176");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ComCnpjVazio_DeveLancarExcecao(string cnpjInvalido)
    {
        var act = () => Cnpj.Create(cnpjInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CNPJ não pode ser vazio*");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("141770000001761234")]
    public void Create_ComTamanhoInvalido_DeveLancarExcecao(string cnpjInvalido)
    {
        var act = () => Cnpj.Create(cnpjInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CNPJ deve conter 14 dígitos*");
    }

    [Theory]
    [InlineData("11111111111111")]
    [InlineData("00000000000000")]
    [InlineData("12345678000100")]
    public void Create_ComCnpjInvalido_DeveLancarExcecao(string cnpjInvalido)
    {
        var act = () => Cnpj.Create(cnpjInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CNPJ inválido*");
    }

    [Fact]
    public void FormatarCnpj_DeveRetornarFormatado()
    {
        var cnpj = Cnpj.Create("14177000000176");

        var formatado = cnpj.FormatarCnpj();

        formatado.Should().Be("14.177.000/0001-76");
    }

    [Fact]
    public void Create_DeveRemoverFormatacao()
    {
        var cnpjFormatado = "14.177.000/0001-76";

        var cnpj = Cnpj.Create(cnpjFormatado);

        cnpj.Numero.Should().Be("14177000000176");
    }

    [Fact]
    public void Equals_CnpjsIguais_DeveRetornarTrue()
    {
        var cnpj1 = Cnpj.Create("14177000000176");
        var cnpj2 = Cnpj.Create("14.177.000/0001-76");

        cnpj1.Should().Be(cnpj2);
    }

    [Fact]
    public void ToString_DeveRetornarFormatado()
    {
        var cnpj = Cnpj.Create("14177000000176");

        var resultado = cnpj.ToString();

        resultado.Should().Be("14.177.000/0001-76");
    }
}
