using Microsoft.EntityFrameworkCore;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Interfaces;
using OrdersPoc.Infrastructure.Data;

namespace OrdersPoc.Infrastructure.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Pedido?> GetByIdWithItensAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.Id == id && p.Ativo)
            .FirstOrDefaultAsync();
    }

    public override async Task<List<Pedido>> GetActiveAsync()
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.Ativo)
            .ToListAsync();
    }
}
