using Npgsql;
using OrdersPoc.LegacyService.INFO;

namespace OrdersPoc.LegacyService.DAL;

public class PedidoDAL
{
    private readonly string _connectionString;

    public PedidoDAL(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertPedido(Guid clienteId, string observacoes, decimal total, out Guid pedidoId, out string numeroPedido)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var transaction = conn.BeginTransaction();

        try
        {
            var countSql = "SELECT COUNT(*) FROM \"Pedidos\"";
            using var countCmd = new NpgsqlCommand(countSql, conn, transaction);
            var count = (long)(countCmd.ExecuteScalar() ?? 0L);
            numeroPedido = $"PED-{DateTime.Now.Year}-{(count + 1):D6}";

            var insertSql = @"
                INSERT INTO ""Pedidos"" (""Id"", ""NumeroPedido"", ""ClienteId"", ""ValorTotal"", ""DataPedido"", ""Status"", ""Observacoes"", ""CriadoEm"")
                VALUES (@id, @numero, @clienteId, @total, @data, @status, @obs, @criado)";

            pedidoId = Guid.NewGuid();

            using var cmd = new NpgsqlCommand(insertSql, conn, transaction);
            cmd.Parameters.AddWithValue("id", pedidoId);
            cmd.Parameters.AddWithValue("numero", numeroPedido);
            cmd.Parameters.AddWithValue("clienteId", clienteId);
            cmd.Parameters.AddWithValue("total", total);
            cmd.Parameters.AddWithValue("data", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("status", 1);
            cmd.Parameters.AddWithValue("obs", (object?)observacoes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("criado", DateTime.UtcNow);

            cmd.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public Guid? GetClienteIdByCpfCnpj(string cpfCnpj)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var sql = "SELECT \"Id\" FROM \"Clientes\" WHERE \"CpfCnpj\" = @cpfCnpj AND \"Ativo\" = true LIMIT 1";

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("cpfCnpj", cpfCnpj);

        var result = cmd.ExecuteScalar();
        return result != null ? (Guid)result : null;
    }


    public void InsertPedidoItem(Guid pedidoId, string produto, int quantidade, decimal precoUnitario)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var sql = @"
            INSERT INTO ""PedidoItens"" (""Id"", ""PedidoId"", ""Produto"", ""Quantidade"", ""PrecoUnitario"", ""Subtotal"", ""CriadoEm"")
            VALUES (@id, @pedidoId, @produto, @quantidade, @preco, @subtotal, @criado)";

        var subtotal = quantidade * precoUnitario;

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", Guid.NewGuid());
        cmd.Parameters.AddWithValue("pedidoId", pedidoId);
        cmd.Parameters.AddWithValue("produto", produto);
        cmd.Parameters.AddWithValue("quantidade", quantidade);
        cmd.Parameters.AddWithValue("preco", precoUnitario);
        cmd.Parameters.AddWithValue("subtotal", subtotal);
        cmd.Parameters.AddWithValue("criado", DateTime.UtcNow);

        cmd.ExecuteNonQuery();
    }
}
