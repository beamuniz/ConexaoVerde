using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class UsuarioBusiness(DbContextConfig dbContextConfig, IClienteBusiness clienteBusiness) : IUsuarioBusiness
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
            .Where(a => a.Email == usuarioModel.Email && a.Senha == usuarioModel.Senha)
            .FirstOrDefaultAsync();

        return usuario;
    }

    public UsuarioModel Logout(string email, string senha)
    {
        throw new NotImplementedException();
    }

    public async Task RegistrarUsuario(UsuarioModel usuarioModel)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);
        var usuario = new Usuario
        {
            Email = usuarioModel.Email,
            Senha = senhaHash,
            Telefone = usuarioModel.Telefone,
            FotoPerfil = usuarioModel.FotoPerfil,
            Perfil = usuarioModel.Perfil
        };

        if (usuarioModel.Perfil == "Cliente")
        {
            var clienteModel = new ClienteModel
            {
                CPF = usuarioModel.clienteModel.CPF,
                NomeCompleto = usuarioModel.clienteModel.NomeCompleto
            };

            await clienteBusiness.RegistrarCliente(clienteModel);
        }

        await dbContextConfig.Usuarios.AddAsync(usuario);
        await dbContextConfig.SaveChangesAsync();
    }
}