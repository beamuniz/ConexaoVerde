using ConexaoVerde.Domain.Models;

namespace ConexaoVerde.Infrastructure.Business.Interfaces;

public interface IClienteBusiness
{
    Task RegistrarCliente(UsuarioModel usuarioModel);
    Task<ClienteModel> ObterClientePorId(int id);
}