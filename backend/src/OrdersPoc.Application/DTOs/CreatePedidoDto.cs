namespace OrdersPoc.Application.DTOs;

public class CreatePedidoDto
{
    public Guid ClienteId { get; set; }
    public string? Observacoes { get; set; }
    public List<CreatePedidoItemDto> Itens { get; set; } = new();
}

public class CreatePedidoItemDto
{
    public string Produto { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}