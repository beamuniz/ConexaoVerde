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
            usuarioExistente.Telefone = usuarioModel.Telefone;
            usuarioExistente.FotoPerfil = usuarioModel.FotoPerfil;

            if (usuarioExistente.Cliente != null)
            {
                usuarioExistente.Cliente.Cpf = usuarioModel.ClienteModel.Cpf;
                usuarioExistente.Cliente.Nome = usuarioModel.ClienteModel.Nome;
                usuarioExistente.Cliente.Sobrenome = usuarioModel.ClienteModel.Sobrenome;
            }

            if (usuarioExistente is Fornecedor fornecedor)
            {
                fornecedor.Cnpj = usuarioModel.FornecedorModel.Cnpj;
                fornecedor.RazaoSocial = usuarioModel.FornecedorModel.RazaoSocial;
                fornecedor.NomeFantasia = usuarioModel.FornecedorModel.NomeFantasia;
                fornecedor.Descricao = usuarioModel.FornecedorModel.Descricao;
            }

            dbContextConfig.Usuarios.Update(usuarioExistente);

            await dbContextConfig.SaveChangesAsync();
        }

        return usuarioExistente;
    }

    public List<UsuarioModel> Favoritos(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UsuarioModel> ObterUsuariosPorFornecedor(int id)
    {
        var usuarioFornecedor = await (from fornecedor in dbContextConfig.Fornecedores
                join usuario in dbContextConfig.Usuarios
                    on fornecedor.Id equals usuario.Id
                where fornecedor.Id == id
                select usuario)
            .FirstOrDefaultAsync();

        if (usuarioFornecedor == null)
            return null;

        return new UsuarioModel
        {
            Id = usuarioFornecedor.Id,
            Telefone = usuarioFornecedor.Telefone,
            Email = usuarioFornecedor.Email,
            FotoPerfil = usuarioFornecedor.FotoPerfil
        };
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

    public async Task<UsuarioModel> ObterIdUsuario(int id)
    {
        var usuario = await dbContextConfig.Usuarios
            .Where(u => u.Id == id)
            .Select(u => new UsuarioModel
            {
                Id = u.Id,
                Email = u.Email,
                Telefone = u.Telefone,
                FotoPerfil = u.FotoPerfil,
                Perfil = u.Perfil,
                Senha = u.Senha
            })
            .FirstOrDefaultAsync();

        return usuario;
    }

    public async Task<UsuarioModel> ObterPorEmail(string email)
    {
        var usuario = await dbContextConfig.Usuarios
            .Where(u => u.Email == email)
            .Select(u => new UsuarioModel
            {
                Id = u.Id,
                Email = u.Email,
                Telefone = u.Telefone,
                FotoPerfil = u.FotoPerfil,
                Perfil = u.Perfil
            })
            .FirstOrDefaultAsync();

        return usuario;
    }

    public async Task<bool> AtualizarSenha(UsuarioModel usuario, string novaSenha)
    {
        var usuarioExistente = await dbContextConfig.Usuarios.FindAsync(usuario.Id);
        
        if (usuarioExistente == null)
            return false;

        // Criptografa a nova senha
        usuarioExistente.Senha = BCrypt.Net.BCrypt.HashPassword(novaSenha);

        dbContextConfig.Usuarios.Update(usuarioExistente);
        await dbContextConfig.SaveChangesAsync();

        return true;
    }
}