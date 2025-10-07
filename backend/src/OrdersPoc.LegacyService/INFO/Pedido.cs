namespace OrdersPoc.LegacyService.INFO;

public class PedidoInfo
{
    public string? ClienteCpfCnpj { get; set; }
    public string? ClienteNome { get; set; }
    public string? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string? Observacoes { get; set; }
}