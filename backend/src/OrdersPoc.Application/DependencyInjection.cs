using Microsoft.Extensions.DependencyInjection;
using OrdersPoc.Application.Interfaces;
using OrdersPoc.Application.Services;

namespace OrdersPoc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IPedidoService, PedidoService>();

        return services;
    }
}