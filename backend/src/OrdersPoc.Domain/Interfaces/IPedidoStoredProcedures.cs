namespace OrdersPoc.Domain.Interfaces;

public interface IPedidoStoredProcedures
{
    Task<ResultadoStoredProcedure> AtualizarStatusPedidoAsync(
        string numeroPedido,
        int novoStatus);
}

public class ResultadoStoredProcedure
{
    public int Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}