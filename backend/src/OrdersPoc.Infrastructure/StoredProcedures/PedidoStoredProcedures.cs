using Microsoft.EntityFrameworkCore;
using Npgsql;
using OrdersPoc.Domain.Interfaces;
using OrdersPoc.Infrastructure.Data;

namespace OrdersPoc.Infrastructure.StoredProcedures;

public class PedidoStoredProcedures : IPedidoStoredProcedures
{
    private readonly ApplicationDbContext _context;

    public PedidoStoredProcedures(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultadoStoredProcedure> AtualizarStatusPedidoAsync(
        string numeroPedido,
        int novoStatus)
    {
        var numeroPedidoParam = new NpgsqlParameter("p_numero_pedido", numeroPedido);
        var novoStatusParam = new NpgsqlParameter("p_novo_status", novoStatus);

        var resultado = await _context.Database
            .SqlQueryRaw<ResultadoStoredProcedure>(
                @"SELECT 
                    sucesso AS ""Sucesso"",
                    mensagem AS ""Mensagem""
                FROM sp_atualizar_status_pedido(@p_numero_pedido, @p_novo_status)",
                numeroPedidoParam,
                novoStatusParam)
            .FirstOrDefaultAsync();

        if (resultado == null)
        {
            return new ResultadoStoredProcedure
            {
                Sucesso = 0,
                Mensagem = "Erro ao executar procedure"
            };
        }

        return resultado;
    }
}