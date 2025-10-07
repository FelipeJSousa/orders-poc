using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.Domain.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente?> GetByEmailAsync(string email);
    Task<Cliente?> GetByCpfCnpjAsync(string cpfCnpj);
    Task<List<Cliente>> GetByTipoPessoaAsync(TipoPessoa tipoPessoa);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> CpfCnpjExistsAsync(string cpfCnpj);
}