namespace OrdersPoc.Domain.Events;

public class PedidoCriadoMessage
{
    public Guid PedidoId { get; set; }
    public string NumeroPedido { get; set; } = string.Empty;
    public Guid ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public DateTime DataCriacao { get; set; }
}