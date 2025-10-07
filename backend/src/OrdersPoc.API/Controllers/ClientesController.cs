using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;

namespace OrdersPoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(
        IClienteService clienteService,
        ILogger<ClientesController> logger)
    {
        _clienteService = clienteService;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos os clientes ativos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Listando todos os clientes ativos");

        var clientes = await _clienteService.GetActiveAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Busca cliente por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Buscando cliente {ClienteId}", id);

        var cliente = await _clienteService.GetByIdAsync(id);

        if (cliente == null)
        {
            _logger.LogWarning("Cliente {ClienteId} não encontrado", id);
            return NotFound(new { message = "Cliente não encontrado" });
        }

        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto dto)
    {
        _logger.LogInformation("Criando novo cliente: {Nome}", dto.Nome);

        try
        {
            var cliente = await _clienteService.CreateAsync(dto);

            _logger.LogInformation("Cliente {ClienteId} criado com sucesso", cliente.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = cliente.Id },
                cliente);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Erro ao criar cliente: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Dados inválidos ao criar cliente: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza dados de um cliente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteDto dto)
    {
        _logger.LogInformation("Atualizando cliente {ClienteId}", id);

        try
        {
            var cliente = await _clienteService.UpdateAsync(id, dto);

            _logger.LogInformation("Cliente {ClienteId} atualizado com sucesso", id);

            return Ok(cliente);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Erro ao atualizar cliente {ClienteId}: {Message}", id, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Dados inválidos ao atualizar cliente {ClienteId}: {Message}", id, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deleta (soft delete) um cliente
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deletando cliente {ClienteId}", id);

        try
        {
            await _clienteService.DeleteAsync(id);

            _logger.LogInformation("Cliente {ClienteId} deletado com sucesso", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Erro ao deletar cliente {ClienteId}: {Message}", id, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}
