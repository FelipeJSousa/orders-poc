using Microsoft.EntityFrameworkCore;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.Infrastructure.Data.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Clientes.AnyAsync())
        {
            return;
        }

        var clientes = CriarClientes();
        await context.Clientes.AddRangeAsync(clientes);
        await context.SaveChangesAsync();

        var pedidos = CriarPedidos(clientes);
        await context.Pedidos.AddRangeAsync(pedidos);
        await context.SaveChangesAsync();
    }

    private static List<Cliente> CriarClientes()
    {
        var clientes = new List<Cliente>();

        var cliente1 = Cliente.Criar(
            nome: "João Silva",
            email: "joao.silva@email.com",
            tipoPessoa: TipoPessoa.Fisica,
            cpfCnpj: "26018488043",
            telefone: "(11) 98765-4321",
            endereco: "Rua das Flores, 123",
            cidade: "São Paulo",
            estado: "SP",
            cep: "01234567"
        );
        clientes.Add(cliente1);

        var cliente2 = Cliente.Criar(
            nome: "Maria Santos",
            email: "maria.santos@email.com",
            tipoPessoa: TipoPessoa.Fisica,
            cpfCnpj: "35430209007",
            telefone: "(21) 91234-5678",
            endereco: "Avenida Paulista, 1000",
            cidade: "Rio de Janeiro",
            estado: "RJ",
            cep: "20000000"
        );
        clientes.Add(cliente2);

        var cliente3 = Cliente.Criar(
            nome: "Empresa Tech LTDA",
            email: "contato@empresatech.com",
            tipoPessoa: TipoPessoa.Juridica,
            cpfCnpj: "66520951000108",
            telefone: "(11) 3000-0000",
            endereco: "Av. Brigadeiro Faria Lima, 1500",
            cidade: "São Paulo",
            estado: "SP",
            cep: "01452000"
        );
        clientes.Add(cliente3);

        var cliente4 = Cliente.Criar(
            nome: "Pedro Oliveira",
            email: "pedro.oliveira@email.com",
            tipoPessoa: TipoPessoa.Fisica,
            cpfCnpj: "63491785057",
            telefone: "(31) 99999-8888",
            endereco: "Rua da Bahia, 500",
            cidade: "Belo Horizonte",
            estado: "MG",
            cep: "30160010"
        );
        clientes.Add(cliente4);

        var cliente5 = Cliente.Criar(
            nome: "Comércio ABC S.A.",
            email: "financeiro@comercioabc.com.br",
            tipoPessoa: TipoPessoa.Juridica,
            cpfCnpj: "53303216000192",
            telefone: "(41) 3333-4444",
            endereco: "Rua XV de Novembro, 800",
            cidade: "Curitiba",
            estado: "PR",
            cep: "80020310"
        );
        clientes.Add(cliente5);

        return clientes;
    }

    private static List<Pedido> CriarPedidos(List<Cliente> clientes)
    {
        var pedidos = new List<Pedido>();

        var pedido1 = Pedido.Criar(clientes[0], "Pedido urgente - entregar pela manhã");
        pedido1.AdicionarItem("Notebook Dell Inspiron 15", 1, 3500.00m);
        pedido1.AdicionarItem("Mouse Logitech MX Master", 1, 450.00m);
        pedido1.AdicionarItem("Teclado Mecânico Keychron", 1, 650.00m);
        pedido1.Confirmar();
        pedidos.Add(pedido1);

        var pedido2 = Pedido.Criar(clientes[1], "Cliente preferencial - desconto aplicado");
        pedido2.AdicionarItem("iPhone 15 Pro Max", 1, 8999.00m);
        pedido2.AdicionarItem("AirPods Pro 2", 1, 2299.00m);
        pedido2.AdicionarItem("Capinha de Silicone", 2, 150.00m);
        pedido2.Confirmar();
        pedido2.AtualizarStatus(StatusPedido.EmProcessamento);
        pedidos.Add(pedido2);

        var pedido3 = Pedido.Criar(clientes[2], "Pedido corporativo - 10 estações de trabalho");
        pedido3.AdicionarItem("Monitor LG UltraWide 34\"", 10, 2800.00m);
        pedido3.AdicionarItem("Notebook ThinkPad X1 Carbon", 10, 9500.00m);
        pedido3.AdicionarItem("Dock Station Lenovo", 10, 1200.00m);
        pedido3.AdicionarItem("Webcam Logitech C920", 10, 550.00m);
        pedido3.Confirmar();
        pedido3.AtualizarStatus(StatusPedido.EmProcessamento);
        pedido3.AtualizarStatus(StatusPedido.Enviado);
        pedidos.Add(pedido3);

        var pedido4 = Pedido.Criar(clientes[3], "Aguardando confirmação de pagamento");
        pedido4.AdicionarItem("PlayStation 5", 1, 4199.00m);
        pedido4.AdicionarItem("Controle DualSense Extra", 1, 499.00m);
        pedido4.AdicionarItem("God of War Ragnarök", 1, 299.00m);
        pedidos.Add(pedido4);

        var pedido5 = Pedido.Criar(clientes[4], "Revenda - nota fiscal em anexo");
        pedido5.AdicionarItem("Smart TV Samsung 55\" 4K", 5, 3200.00m);
        pedido5.AdicionarItem("Soundbar JBL", 5, 1899.00m);
        pedido5.Confirmar();
        pedido5.AtualizarStatus(StatusPedido.EmProcessamento);
        pedido5.AtualizarStatus(StatusPedido.Enviado);
        pedido5.AtualizarStatus(StatusPedido.Entregue);
        pedidos.Add(pedido5);

        var pedido6 = Pedido.Criar(clientes[0], "Cliente solicitou cancelamento");
        pedido6.AdicionarItem("Tablet Samsung Galaxy Tab", 1, 1899.00m);
        pedido6.Confirmar();
        pedido6.Cancelar();
        pedidos.Add(pedido6);

        return pedidos;
    }
}