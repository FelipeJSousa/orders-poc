using OrdersPoc.Application.DTOs;

namespace OrdersPoc.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoDto?> GetByIdAsync(Guid id);
    Task<List<PedidoDto>> GetByClienteIdAsync(Guid clienteId);
    Task<PedidoDto> CreateAsync(CreatePedidoDto dto);
    Task<PedidoDto> UpdateStatusAsync(Guid id, int novoStatus);
    Task<PedidoDto> CancelAsync(Guid id);
}