using OrdersPoc.Domain.Entities;

namespace OrdersPoc.Domain.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<Pedido?> GetByIdWithItensAsync(Guid id);
}