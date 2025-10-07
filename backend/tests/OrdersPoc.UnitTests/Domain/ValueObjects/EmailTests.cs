using FluentAssertions;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_ComEmailValido_DeveRetornarEmail()
    {
        var emailValido = "teste@email.com";

        var email = Email.Create(emailValido);

        email.Should().NotBeNull();
        email.Address.Should().Be(emailValido);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_ComEmailVazio_DeveLancarExcecao(string emailInvalido)
    {
        var act = () => Email.Create(emailInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Email não pode ser vazio*");
    }

    [Theory]
    [InlineData("emailsemarroba.com")]
    [InlineData("@semdominio.com")]
    [InlineData("email@")]
    [InlineData("email sem espaco@teste.com")]
    public void Create_ComEmailInvalido_DeveLancarExcecao(string emailInvalido)
    {
        var act = () => Email.Create(emailInvalido);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Email inválido*");
    }

    [Fact]
    public void Create_DeveConverterParaLowerCase()
    {
        var emailMaiusculo = "TESTE@EMAIL.COM";

        var email = Email.Create(emailMaiusculo);

        email.Address.Should().Be("teste@email.com");
    }

    [Fact]
    public void Equals_EmailsIguais_DeveRetornarTrue()
    {
        var email1 = Email.Create("teste@email.com");
        var email2 = Email.Create("teste@email.com");

        email1.Should().Be(email2);
    }

    [Fact]
    public void ToString_DeveRetornarAddress()
    {
        var emailString = "teste@email.com";
        var email = Email.Create(emailString);

        var resultado = email.ToString();

        resultado.Should().Be(emailString);
    }
}
