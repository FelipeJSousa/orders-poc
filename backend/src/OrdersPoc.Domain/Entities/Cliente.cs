using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.ValueObjects;

namespace OrdersPoc.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public TipoPessoa TipoPessoa { get; private set; }
    public string? CpfCnpj { get; private set; }
    public string? Telefone { get; private set; }
    public string? Endereco { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? Cep { get; private set; }

    // Relacionamentos
    private readonly List<Pedido> _pedidos = new();
    public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

    // Construtor privado para EF Core
    private Cliente() { }

    private Cliente(
        string nome,
        Email email,
        TipoPessoa tipoPessoa,
        string? cpfCnpj,
        string? telefone,
        string? endereco,
        string? cidade,
        string? estado,
        string? cep)
    {
        Nome = nome;
        Email = email;
        TipoPessoa = tipoPessoa;
        CpfCnpj = cpfCnpj;
        Telefone = telefone;
        Endereco = endereco;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;

        ValidarCliente();
    }

    public static Cliente Criar(
        string nome,
        string email,
        TipoPessoa tipoPessoa,
        string? cpfCnpj = null,
        string? telefone = null,
        string? endereco = null,
        string? cidade = null,
        string? estado = null,
        string? cep = null)
    {
        var emailVo = Email.Create(email);

        // Validar CPF/CNPJ baseado no tipo de pessoa
        if (!string.IsNullOrWhiteSpace(cpfCnpj))
        {
            if (tipoPessoa == TipoPessoa.Fisica)
            {
                var cpf = Cpf.Create(cpfCnpj);
                cpfCnpj = cpf.Numero;
            }
            else
            {
                var cnpj = Cnpj.Create(cpfCnpj);
                cpfCnpj = cnpj.Numero;
            }
        }

        return new Cliente(nome, emailVo, tipoPessoa, cpfCnpj, telefone, endereco, cidade, estado, cep);
    }

    public void AtualizarDados(
        string nome,
        string email,
        string? telefone,
        string? endereco,
        string? cidade,
        string? estado,
        string? cep)
    {
        Nome = nome;
        Email = Email.Create(email);
        Telefone = telefone;
        Endereco = endereco;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;

        ValidarCliente();
        Atualizar();
    }

    public void AdicionarPedido(Pedido pedido)
    {
        _pedidos.Add(pedido);
        Atualizar();
    }

    private void ValidarCliente()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("Nome é obrigatório", nameof(Nome));

        if (Nome.Length < 3)
            throw new ArgumentException("Nome deve ter no mínimo 3 caracteres", nameof(Nome));

        if (Nome.Length > 200)
            throw new ArgumentException("Nome deve ter no máximo 200 caracteres", nameof(Nome));
    }
}
