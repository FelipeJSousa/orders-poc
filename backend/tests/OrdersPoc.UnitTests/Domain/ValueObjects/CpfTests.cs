using FluentAssertions;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.UnitTests.Domain.ValueObjects;

public class CpfTests
{
    [Theory]
    [InlineData("47717411075")]
    [InlineData("477.174.110-75")]
    public void Create_ComCpfValido_DeveRetornarCpf(string cpfValido)
    {
        var cpf = Cpf.Create(cpfValido);

        cpf.Should().NotBeNull();
        cpf.Numero.Should().Be("47717411075");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ComCpfVazio_DeveLancarExcecao(string cpfInvalido)
    {
        var act = () => Cpf.Create(cpfInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CPF não pode ser vazio*");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("12345678901234")]
    public void Create_ComTamanhoInvalido_DeveLancarExcecao(string cpfInvalido)
    {
        var act = () => Cpf.Create(cpfInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CPF deve conter 11 dígitos*");
    }

    [Theory]
    [InlineData("11111111111")]
    [InlineData("00000000000")]
    [InlineData("12345678900")]
    public void Create_ComCpfInvalido_DeveLancarExcecao(string cpfInvalido)
    {
        var act = () => Cpf.Create(cpfInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CPF inválido*");
    }

    [Fact]
    public void FormatarCpf_DeveRetornarFormatado()
    {
        var cpf = Cpf.Create("47717411075");

        var formatado = cpf.FormatarCpf();

        formatado.Should().Be("477.174.110-75");
    }
}