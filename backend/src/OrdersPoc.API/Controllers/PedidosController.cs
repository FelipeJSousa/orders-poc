using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;

namespace OrdersPoc.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly ILogger<PedidosController> _logger;

    public PedidosController(
        IPedidoService pedidoService,
        ILogger<PedidosController> logger)
    {
        _pedidoService = pedidoService;
        _logger = logger;
    }

    /// <summary>
    /// Busca pedido por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Buscando pedido {PedidoId}", id);

        var pedido = await _pedidoService.GetByIdAsync(id);

        if (pedido == null)
        {
            _logger.LogWarning("Pedido {PedidoId} não encontrado", id);
            return NotFound(new { message = "Pedido não encontrado" });
        }

        return Ok(pedido);
    }

    /// <summary>
    /// Lista pedidos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Listando todos pedidos");

        var pedidos = await _pedidoService.GetAll();
        return Ok(pedidos);
    }

    /// <summary>
    /// Cria um novo pedido
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreatePedidoDto dto)
    {
        _logger.LogInformation("Criando novo pedido para cliente {ClienteId}", dto.ClienteId);

        try
        {
            var pedido = await _pedidoService.CreateAsync(dto);

            _logger.LogInformation("Pedido {PedidoId} criado com sucesso", pedido.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = pedido.Id },
                pedido);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Erro ao criar pedido: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Dados inválidos ao criar pedido: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza status do pedido usando stored procedure
    /// </summary>
    [HttpPut("{id}/status/{status}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateStatus(Guid id, int status)
    {
        _logger.LogInformation("Atualizando status do pedido {PedidoId} para {NovoStatus}", id, status);

        try
        {
            var pedido = await _pedidoService.UpdateStatusAsync(id, status);

            _logger.LogInformation("Status do pedido {PedidoId} atualizado com sucesso", id);

            return Ok(pedido);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Erro ao atualizar status do pedido {PedidoId}: {Message}", id, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}