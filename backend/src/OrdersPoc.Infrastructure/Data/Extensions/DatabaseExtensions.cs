using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersPoc.Infrastructure.Data.Seeds;

namespace OrdersPoc.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            await DatabaseSeeder.SeedAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao aplicar migrations: {ex.Message}");
            throw;
        }

        return host;
    }
}