using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<Usuario> Login(UsuarioModel usuarioModel);
    Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel);
    List<UsuarioModel> Favoritos(int id);
    Task<UsuarioModel> ObterUsuariosPorFornecedor(int id);
    Task<UsuarioModel> ObterIdUsuario(int id);
    Task<UsuarioModel> ObterPorEmail(string email);
    Task<bool> AtualizarSenha(UsuarioModel usuario, string novaSenha);
}