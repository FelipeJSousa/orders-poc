using OrdersPoc.Application.DTOs;

namespace OrdersPoc.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoDto?> GetByIdAsync(Guid id);
    Task<List<PedidoDto>> GetAll();
    Task<PedidoDto> CreateAsync(CreatePedidoDto dto);
    Task<PedidoDto> UpdateStatusAsync(Guid id, int novoStatus);
    Task<PedidoDto> CancelAsync(Guid id);
}