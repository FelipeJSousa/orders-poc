using FluentAssertions;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.UnitTests.Entities;

public class ClienteTests
{
    [Fact]
    public void Criar_ComDadosValidos_DeveCriarCliente()
    {
        var nome = "João Silva";
        var email = "joao@email.com";
        var cpf = "47717411075";

        var cliente = Cliente.Criar(nome, email, TipoPessoa.Fisica, cpf);

        cliente.Should().NotBeNull();
        cliente.Nome.Should().Be(nome);
        cliente.Email.Address.Should().Be(email);
        cliente.TipoPessoa.Should().Be(TipoPessoa.Fisica);
        cliente.CpfCnpj.Should().Be(cpf);
        cliente.Ativo.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("AB")]
    public void Criar_ComNomeInvalido_DeveLancarExcecao(string nomeInvalido)
    {
        var act = () => Cliente.Criar(nomeInvalido, "teste@email.com", TipoPessoa.Fisica);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Criar_ComEmailInvalido_DeveLancarExcecao()
    {
        var act = () => Cliente.Criar("João Silva", "emailinvalido", TipoPessoa.Fisica);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AtualizarDados_DeveAtualizarCliente()
    {
        var cliente = Cliente.Criar("João Silva", "joao@email.com", TipoPessoa.Fisica);
        var novoNome = "João Silva Santos";
        var novoEmail = "joao.santos@email.com";

        cliente.AtualizarDados(novoNome, novoEmail, "11999999999", null, null, null, null);

        cliente.Nome.Should().Be(novoNome);
        cliente.Email.Address.Should().Be(novoEmail);
        cliente.Telefone.Should().Be("11999999999");
        cliente.AtualizadoEm.Should().NotBeNull();
    }

    [Fact]
    public void Desativar_DeveMarcarComoInativo()
    {
        var cliente = Cliente.Criar("João Silva", "joao@email.com", TipoPessoa.Fisica);

        cliente.Desativar();

        cliente.Ativo.Should().BeFalse();
        cliente.AtualizadoEm.Should().NotBeNull();
    }

    [Fact]
    public void Ativar_DeveMarcarComoAtivo()
    {
        var cliente = Cliente.Criar("João Silva", "joao@email.com", TipoPessoa.Fisica);
        cliente.Desativar();

        cliente.Ativar();

        cliente.Ativo.Should().BeTrue();
    }
}
