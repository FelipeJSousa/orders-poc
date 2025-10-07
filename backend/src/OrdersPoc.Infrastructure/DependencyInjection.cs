using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersPoc.Domain.Interfaces;
using OrdersPoc.Infrastructure.Data;
using OrdersPoc.Infrastructure.Repositories;
using OrdersPoc.Infrastructure.StoredProcedures;

namespace OrdersPoc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });

            var enableSensitiveDataLogging = configuration
                .GetSection("Database:EnableSensitiveDataLogging")
                .Get<bool>();

            if (enableSensitiveDataLogging)
            {
                options.EnableSensitiveDataLogging();
            }

            var enableDetailedErrors = configuration
                .GetSection("Database:EnableDetailedErrors")
                .Get<bool>();

            if (enableDetailedErrors)
            {
                options.EnableDetailedErrors();
            }
        });

        services.RegisterStoreProcedures();

        services.RegisterRepositories();

        return services;
    }

    private static void RegisterStoreProcedures(this IServiceCollection services)
    {
        services.AddScoped<IPedidoStoredProcedures, PedidoStoredProcedures>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
    }
}