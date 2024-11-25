using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IClienteBusiness
{
    Task RegistrarCliente(ClienteModel clienteModel);
}