using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Business.Services;

namespace ConexaoVerde.Web.Configuration;

public static class ConexaoVerdeConfigurationExtensions
{
    public static void AddConexaoVerdeServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
    }
}