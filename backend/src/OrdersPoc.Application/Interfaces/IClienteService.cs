using OrdersPoc.Application.DTOs;

namespace OrdersPoc.Application.Interfaces;

public interface IClienteService
{
    Task<ClienteDto?> GetByIdAsync(Guid id);
    Task<List<ClienteDto>> GetActiveAsync();
    Task<ClienteDto> CreateAsync(CreateClienteDto dto);
    Task<ClienteDto> UpdateAsync(Guid id, UpdateClienteDto dto);
    Task DeleteAsync(Guid id);
}