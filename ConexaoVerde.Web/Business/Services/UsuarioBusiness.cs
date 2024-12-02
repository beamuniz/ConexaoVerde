using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class UsuarioBusiness(DbContextConfig dbContextConfig) : IUsuarioBusiness
{
    public async Task<Usuario> AtualizarUsuario(UsuarioModel usuarioModel)
    {
        var usuarioExistente = await dbContextConfig.Usuarios.FindAsync(usuarioModel.Id);

        if (usuarioExistente != null)
        {
            usuarioExistente.Email = usuarioModel.Email;
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);
            usuarioExistente.Senha = senhaHash;
            usuarioExistente.Telefone = usuarioModel.Telefone;
            usuarioExistente.FotoPerfil = usuarioModel.FotoPerfil;

            dbContextConfig.Usuarios.Update(usuarioExistente);

            await dbContextConfig.SaveChangesAsync();
        }

        return usuarioExistente;
    }

    public List<UsuarioModel> Favoritos(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> Login(UsuarioModel usuarioModel)
    {
        var usuario = await dbContextConfig.Usuarios
            .FirstOrDefaultAsync(u => u.Email == usuarioModel.Email);

        if (usuario != null && BCrypt.Net.BCrypt.Verify(usuarioModel.Senha, usuario.Senha))
        {
            return usuario;
        }

        return null;
    }

    public UsuarioModel Logout(string email, string senha)
    {
        throw new NotImplementedException();
    }
}