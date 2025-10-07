namespace OrdersPoc.Application.DTOs;

public class PedidoItemDto
{
    public Guid Id { get; set; }
    public string Produto { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}