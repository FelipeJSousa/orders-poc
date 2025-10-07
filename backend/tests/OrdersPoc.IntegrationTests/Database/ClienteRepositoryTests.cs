using FluentAssertions;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;
using OrdersPoc.Infrastructure.Repositories;
using OrdersPoc.IntegrationTests.Helpers;

namespace OrdersPoc.IntegrationTests.Database;

public class ClienteRepositoryTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly ClienteRepository _repository;

    public ClienteRepositoryTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
        _repository = new ClienteRepository(_fixture.Context);
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarClientePessoaFisica()
    {
        var cliente = Cliente.Criar("João Silva", "joao@teste.com", TipoPessoa.Fisica);

        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();

        var clienteSalvo = await _repository.GetByIdAsync(cliente.Id);
        clienteSalvo.Should().NotBeNull();
        clienteSalvo!.Nome.Should().Be("João Silva");
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarClientePessoaJuridica()
    {
        var cliente = Cliente.Criar("Empresa XYZ", "empresa@teste.com", TipoPessoa.Juridica, "01571120000101");

        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();
        var clienteSalvo = await _repository.GetByIdAsync(cliente.Id);
        clienteSalvo.Should().NotBeNull();
        clienteSalvo!.Nome.Should().Be("Empresa XYZ");
        clienteSalvo!.CpfCnpj.Should().Be("01571120000101");
    }

    [Fact]
    public async Task GetByEmailAsync_DeveRetornarCliente()
    {
        var cliente = Cliente.Criar("Maria Santos", "maria@teste.com", TipoPessoa.Fisica);
        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();

        var resultado = await _repository.GetByEmailAsync("maria@teste.com");

        resultado.Should().NotBeNull();
        resultado!.Email.Address.Should().Be("maria@teste.com");
    }

    [Fact]
    public async Task GetByCpfCnpjAsync_DeveRetornarCliente()
    {
        var cliente = Cliente.Criar("Pedro Oliveira", "pedro@teste.com", TipoPessoa.Fisica, "27600992027");
        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();

        var resultado = await _repository.GetByCpfCnpjAsync("27600992027");

        resultado.Should().NotBeNull();
        resultado!.CpfCnpj.Should().Be("27600992027");
    }

    [Fact]
    public async Task EmailExistsAsync_DeveRetornarTrue_QuandoEmailExiste()
    {
        var cliente = Cliente.Criar("Ana Silva", "ana@teste.com", TipoPessoa.Fisica);
        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();

        var existe = await _repository.EmailExistsAsync("ana@teste.com");

        existe.Should().BeTrue();
    }

    [Fact]
    public async Task Delete_DeveDesativarCliente()
    {
        var cliente = Cliente.Criar("Carlos Santos", "carlos@teste.com", TipoPessoa.Fisica);
        await _repository.AddAsync(cliente);
        await _fixture.Context.SaveChangesAsync();

        _repository.Delete(cliente);
        await _fixture.Context.SaveChangesAsync();

        var clienteDesativado = await _repository.GetByIdAsync(cliente.Id);
        clienteDesativado!.Ativo.Should().BeFalse();
    }
}
