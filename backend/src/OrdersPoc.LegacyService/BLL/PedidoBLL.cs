using OrdersPoc.LegacyService.DAL;
using OrdersPoc.LegacyService.INFO;

namespace OrdersPoc.LegacyService.BLL;

public class PedidoBLL
{
    private readonly PedidoDAL _dal;

    public PedidoBLL(string connectionString)
    {
        _dal = new PedidoDAL(connectionString);
    }

    public (bool Success, string Message) ImportarPedidoFromCSV(PedidoInfo pedidoInfo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(pedidoInfo.ClienteCpfCnpj))
            {
                return (false, "CPF/CNPJ do cliente é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(pedidoInfo.Produto))
            {
                return (false, "Produto é obrigatório");
            }

            if (pedidoInfo.Quantidade <= 0)
            {
                return (false, "Quantidade deve ser maior que zero");
            }

            if (pedidoInfo.PrecoUnitario <= 0)
            {
                return (false, "Preço deve ser maior que zero");
            }

            var clienteId = _dal.GetClienteIdByCpfCnpj(pedidoInfo.ClienteCpfCnpj);

            if (clienteId == null)
            {
                return (false, $"Cliente com CPF/CNPJ {pedidoInfo.ClienteCpfCnpj} não encontrado");
            }

            var total = pedidoInfo.Quantidade * pedidoInfo.PrecoUnitario;

            _dal.InsertPedido(
                clienteId.Value,
                pedidoInfo.Observacoes ?? "Importado via CSV",
                total,
                out var pedidoId,
                out var numeroPedido);

            _dal.InsertPedidoItem(
                pedidoId,
                pedidoInfo.Produto,
                pedidoInfo.Quantidade,
                pedidoInfo.PrecoUnitario);

            return (true, $"Pedido {numeroPedido} importado com sucesso!");
        }
        catch (Exception ex)
        {
            return (false, $"Erro ao importar: {ex.Message}");
        }
    }
}
