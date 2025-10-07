using Microsoft.EntityFrameworkCore;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.Interfaces;
using OrdersPoc.Infrastructure.Data;

namespace OrdersPoc.Infrastructure.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> GetByEmailAsync(string email)
    {
        var emailLower = email.ToLowerInvariant();

        return await _dbSet
            .Where(c => c.Email.Address == emailLower && c.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente?> GetByCpfCnpjAsync(string cpfCnpj)
    {
        return await _dbSet
            .Where(c => c.CpfCnpj == cpfCnpj && c.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Cliente>> GetByTipoPessoaAsync(TipoPessoa tipoPessoa)
    {
        return await _dbSet
            .Where(c => c.TipoPessoa == tipoPessoa && c.Ativo)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var emailLower = email.ToLowerInvariant();

        return await _dbSet
            .AnyAsync(c => c.Email.Address == emailLower && c.Ativo);
    }

    public async Task<bool> CpfCnpjExistsAsync(string cpfCnpj)
    {
        return await _dbSet
            .AnyAsync(c => c.CpfCnpj == cpfCnpj && c.Ativo);
    }
}