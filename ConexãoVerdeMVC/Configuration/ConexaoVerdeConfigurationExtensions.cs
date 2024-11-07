using ConexãoVerdeMVC.Business.Interfaces;
using ConexãoVerdeMVC.Business.Services;

namespace ConexãoVerdeMVC.Configuration;

public static class ConexaoVerdeConfigurationExtensions
{
    public static void AddConexaoVerdeServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
    }
}