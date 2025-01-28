using ConexaoVerde.Domain.Models;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IClienteBusiness
{
    Task RegistrarCliente(UsuarioModel usuarioModel);
    Task<ClienteModel> ObterClientePorId(int id);
}