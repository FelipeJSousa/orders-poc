using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using OrdersPoc.LegacyService.BLL;
using OrdersPoc.LegacyService.INFO;

Console.WriteLine("╔════════════════════════════════════════╗");
Console.WriteLine("║   OrdersPoc - Legacy CSV Importer     ║");
Console.WriteLine("║   Simulando Sistema Legado WinForms   ║");
Console.WriteLine("╚════════════════════════════════════════╝");
Console.WriteLine();

var connectionString = "Host=localhost;Port=5432;Database=ordersPocDb_legado;Username=postgres;Password=postgres";

var pedidoBLL = new PedidoBLL(connectionString);

var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pedidos.csv");

if (!File.Exists(csvPath))
{
    Console.WriteLine($"Arquivo não encontrado: {csvPath}");
    Console.WriteLine("Crie o arquivo Data/pedidos.csv com os dados de exemplo");
    return;
}

Console.WriteLine($"Lendo arquivo: {csvPath}");
Console.WriteLine();

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true,
    Delimiter = ",",
    TrimOptions = TrimOptions.Trim
};

try
{
    using var reader = new StreamReader(csvPath);
    using var csv = new CsvReader(reader, config);

    var records = csv.GetRecords<PedidoInfo>().ToList();

    Console.WriteLine($"{records.Count} registros encontrados no CSV");
    Console.WriteLine();
    Console.WriteLine("Iniciando importação...");
    Console.WriteLine("═══════════════════════════════════════");
    Console.WriteLine();

    int sucesso = 0;
    int erro = 0;

    foreach (var record in records)
    {
        Console.Write($"Importando: {record.Produto} para {record.ClienteNome}... ");

        var (success, message) = pedidoBLL.ImportarPedidoFromCSV(record);

        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            sucesso++;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            erro++;
        }
    }

    Console.WriteLine();
    Console.WriteLine("═══════════════════════════════════════");
    Console.WriteLine($"Importação concluída:");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Sucesso: {sucesso}");
    Console.ResetColor();

    if (erro > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Erros: {erro}");
        Console.ResetColor();
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erro fatal: {ex.Message}");
    Console.ResetColor();
}

Console.WriteLine();
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();
