using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Business.Services;
using ConexaoVerde.Web.Services;

namespace ConexaoVerde.Web.Configuration;

public static class ConfigurationExtensions
{
    public static void AddConexaoVerdeServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
        services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
        services.AddScoped<ICategoriaBusiness, CategoriaBusiness>();
        services.AddScoped<IFornecedorBusiness, FornecedorBusiness>();
        services.AddScoped<IClienteBusiness, ClienteBusiness>();
        services.AddScoped<LoginService>();
    }
}