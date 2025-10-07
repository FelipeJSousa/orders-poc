using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.Domain.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<Pedido?> GetByNumeroAsync(string numeroPedido);
    Task<Pedido?> GetByIdWithItensAsync(Guid id);
    Task<List<Pedido>> GetByClienteIdAsync(Guid clienteId);
    Task<List<Pedido>> GetByStatusAsync(StatusPedido status);
    Task<List<Pedido>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    Task<bool> NumeroExistsAsync(string numeroPedido);
}