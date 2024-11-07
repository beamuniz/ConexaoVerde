using ConexaoVerdeAppData.Entities;
using ConexaoVerdeMVC.Models;

namespace ConexaoVerdeMVC.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<Usuario> Login(UsuarioModel usuarioModel);
    UsuarioModel Logout(string email, string senha);
    Task RegistrarUsuario(UsuarioModel usuarioModel);
    Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel);
    List<UsuarioModel> Favoritos(int id);
}