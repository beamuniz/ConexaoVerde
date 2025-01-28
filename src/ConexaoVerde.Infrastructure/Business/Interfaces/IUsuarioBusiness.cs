using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;

namespace ConexaoVerde.Infrastructure.Business.Interfaces;

public interface IUsuarioBusiness
{
    Task<Usuario> Login(UsuarioModel usuarioModel);
    Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel);
    Task<UsuarioModel> ObterUsuariosPorFornecedor(int id);
    Task<UsuarioModel> ObterIdUsuario(int id);
    Task<UsuarioModel> ObterPorEmail(string email);
    Task<bool> AtualizarSenha(UsuarioModel usuario, string novaSenha);
}