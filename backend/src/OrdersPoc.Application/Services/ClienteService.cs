using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.Interfaces;

namespace OrdersPoc.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(
        IClienteRepository clienteRepository,
        IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClienteDto?> GetByIdAsync(Guid id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        return cliente == null ? null : MapToDto(cliente);
    }

    public async Task<List<ClienteDto>> GetActiveAsync()
    {
        var clientes = await _clienteRepository.GetActiveAsync();
        return clientes.Select(MapToDto).ToList();
    }

    public async Task<ClienteDto?> GetByEmailAsync(string email)
    {
        var cliente = await _clienteRepository.GetByEmailAsync(email);
        return cliente == null ? null : MapToDto(cliente);
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto dto)
    {
        if (await _clienteRepository.EmailExistsAsync(dto.Email))
        {
            throw new InvalidOperationException("Email já cadastrado");
        }

        if (!string.IsNullOrWhiteSpace(dto.CpfCnpj))
        {
            if (await _clienteRepository.CpfCnpjExistsAsync(dto.CpfCnpj))
            {
                throw new InvalidOperationException("CPF/CNPJ já cadastrado");
            }
        }

        var cliente = Cliente.Criar(
            dto.Nome,
            dto.Email,
            (TipoPessoa)dto.TipoPessoa,
            dto.CpfCnpj,
            dto.Telefone,
            dto.Endereco,
            dto.Cidade,
            dto.Estado,
            dto.Cep
        );

        await _clienteRepository.AddAsync(cliente);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(cliente);
    }

    public async Task<ClienteDto> UpdateAsync(Guid id, UpdateClienteDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente == null)
        {
            throw new InvalidOperationException("Cliente não encontrado");
        }

        var clienteComEmail = await _clienteRepository.GetByEmailAsync(dto.Email);
        if (clienteComEmail != null && clienteComEmail.Id != id)
        {
            throw new InvalidOperationException("Email já cadastrado para outro cliente");
        }

        cliente.AtualizarDados(
            dto.Nome,
            dto.Email,
            dto.Telefone,
            dto.Endereco,
            dto.Cidade,
            dto.Estado,
            dto.Cep
        );

        _clienteRepository.Update(cliente);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(cliente);
    }

    public async Task DeleteAsync(Guid id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente == null)
        {
            throw new InvalidOperationException("Cliente não encontrado");
        }

        _clienteRepository.Delete(cliente);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _clienteRepository.EmailExistsAsync(email);
    }

    public async Task<bool> CpfCnpjExistsAsync(string cpfCnpj)
    {
        return await _clienteRepository.CpfCnpjExistsAsync(cpfCnpj);
    }

    private static ClienteDto MapToDto(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email.Address,
            TipoPessoa = (int)cliente.TipoPessoa,
            CpfCnpj = cliente.CpfCnpj,
            Telefone = cliente.Telefone,
            Endereco = cliente.Endereco,
            Cidade = cliente.Cidade,
            Estado = cliente.Estado,
            Cep = cliente.Cep,
            Ativo = cliente.Ativo,
            CriadoEm = cliente.CriadoEm
        };
    }
}
