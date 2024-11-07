using ConexaoVerdeMVC.Business.Interfaces;
using ConexaoVerdeMVC.Business.Services;

namespace ConexaoVerdeMVC.Configuration;

public static class ConexaoVerdeConfigurationExtensions
{
    public static void AddConexaoVerdeServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
    }
}