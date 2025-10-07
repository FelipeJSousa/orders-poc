using Microsoft.EntityFrameworkCore;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.Interfaces;
using OrdersPoc.Infrastructure.Data;

namespace OrdersPoc.Infrastructure.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Pedido?> GetByNumeroAsync(string numeroPedido)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.NumeroPedido == numeroPedido && p.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<Pedido?> GetByIdWithItensAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.Id == id && p.Ativo)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Pedido>> GetByClienteIdAsync(Guid clienteId)
    {
        return await _dbSet
            .Include(p => p.Itens)
            .Where(p => p.ClienteId == clienteId && p.Ativo)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<List<Pedido>> GetByStatusAsync(StatusPedido status)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.Status == status && p.Ativo)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<List<Pedido>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.DataPedido >= dataInicio
                     && p.DataPedido <= dataFim
                     && p.Ativo)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<bool> NumeroExistsAsync(string numeroPedido)
    {
        return await _dbSet
            .AnyAsync(p => p.NumeroPedido == numeroPedido && p.Ativo);
    }
}
