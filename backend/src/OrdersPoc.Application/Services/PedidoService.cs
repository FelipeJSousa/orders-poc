using OrdersPoc.Application.DTOs;
using OrdersPoc.Application.Interfaces;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;
using OrdersPoc.Domain.Interfaces;

namespace OrdersPoc.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPedidoStoredProcedures _pedidoStoredProcedures;
    private readonly IUnitOfWork _unitOfWork;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IPedidoStoredProcedures pedidoStoredProcedures,
        IUnitOfWork unitOfWork)
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
        _pedidoStoredProcedures = pedidoStoredProcedures;
        _unitOfWork = unitOfWork;
    }

    public async Task<PedidoDto?> GetByIdAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdWithItensAsync(id);
        return pedido == null ? null : MapToDto(pedido);
    }

    public async Task<List<PedidoDto>> GetByClienteIdAsync(Guid clienteId)
    {
        var pedidos = await _pedidoRepository.GetByClienteIdAsync(clienteId);
        return pedidos.Select(MapToDto).ToList();
    }

    public async Task<List<PedidoDto>> GetByStatusAsync(int status)
    {
        var pedidos = await _pedidoRepository.GetByStatusAsync((StatusPedido)status);
        return pedidos.Select(MapToDto).ToList();
    }

    public async Task<PedidoDto> CreateAsync(CreatePedidoDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
        if (cliente == null)
        {
            throw new InvalidOperationException("Cliente não encontrado");
        }

        if (dto.Itens == null || dto.Itens.Count == 0)
        {
            throw new InvalidOperationException("Pedido deve ter pelo menos um item");
        }

        var pedido = Pedido.Criar(cliente, dto.Observacoes);

        foreach (var itemDto in dto.Itens)
        {
            pedido.AdicionarItem(
                itemDto.Produto,
                itemDto.Quantidade,
                itemDto.PrecoUnitario
            );
        }

        await _pedidoRepository.AddAsync(pedido);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(pedido);
    }

    public async Task<PedidoDto> UpdateStatusAsync(Guid id, int novoStatus)
    {
        if (novoStatus == (int) StatusPedido.Cancelado)
            return await CancelAsync(id);

        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido == null)
        {
            throw new InvalidOperationException("Pedido não encontrado");
        }

        var resultado = await _pedidoStoredProcedures.AtualizarStatusPedidoAsync(
            pedido.NumeroPedido,
            novoStatus);

        if (resultado.Sucesso == 0)
        {
            throw new InvalidOperationException(resultado.Mensagem);
        }

        var pedidoAtualizado = await _pedidoRepository.GetByIdWithItensAsync(id);

        if (pedidoAtualizado == null)
        {
            throw new InvalidOperationException("Erro ao buscar pedido atualizado");
        }

        return MapToDto(pedidoAtualizado);
    }

    public async Task<PedidoDto> CancelAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido == null)
        {
            throw new InvalidOperationException("Pedido não encontrado");
        }

        var resultado = await _pedidoStoredProcedures.AtualizarStatusPedidoAsync(
            pedido.NumeroPedido, (int) StatusPedido.Cancelado);

        if (resultado.Sucesso == 0)
        {
            throw new InvalidOperationException(resultado.Mensagem);
        }

        var pedidoAtualizado = await _pedidoRepository.GetByIdWithItensAsync(id);
        if (pedidoAtualizado == null)
        {
            throw new InvalidOperationException("Erro ao buscar pedido atualizado");
        }
        return MapToDto(pedidoAtualizado);
    }

    private static PedidoDto MapToDto(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            NumeroPedido = pedido.NumeroPedido,
            ClienteId = pedido.ClienteId,
            ClienteNome = pedido.Cliente?.Nome ?? string.Empty,
            DataPedido = pedido.DataPedido,
            Status = (int)pedido.Status,
            StatusDescricao = pedido.Status.ToString(),
            ValorTotal = pedido.ValorTotal.Amount,
            Observacoes = pedido.Observacoes,
            Itens = pedido.Itens.Select(MapItemToDto).ToList(),
            CriadoEm = pedido.CriadoEm
        };
    }

    private static PedidoItemDto MapItemToDto(PedidoItem item)
    {
        return new PedidoItemDto
        {
            Id = item.Id,
            Produto = item.Produto,
            Quantidade = item.Quantidade,
            PrecoUnitario = item.PrecoUnitario.Amount,
            Subtotal = item.Subtotal.Amount
        };
    }
}