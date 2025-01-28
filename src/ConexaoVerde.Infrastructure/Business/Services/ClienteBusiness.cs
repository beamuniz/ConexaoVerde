using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Context;
using ConexaoVerde.Web.Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Infrastructure.Business.Services;

public class ClienteBusiness (DbContextConfig dbContextConfig) : IClienteBusiness
{
    public async Task RegistrarCliente(UsuarioModel usuarioModel)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);

        if (usuarioModel.Perfil == "Cliente")
        {
            var clienteModel = usuarioModel.ClienteModel;

            var cliente = new Cliente
            {
                Nome = clienteModel.Nome,
                Sobrenome = clienteModel.Sobrenome,
                Cpf = clienteModel.Cpf,
                Id = usuarioModel.Id,
                Email = usuarioModel.Email,
                Senha = senhaHash,
                Telefone = usuarioModel.Telefone,
                FotoPerfil = usuarioModel.FotoPerfil,
                Perfil = usuarioModel.Perfil
            };
            await dbContextConfig.Clientes.AddAsync(cliente);
        }

        await dbContextConfig.SaveChangesAsync();
    }

    public async Task<ClienteModel> ObterClientePorId(int id)
    {
        var cliente = await dbContextConfig.Clientes
            .Where(c => c.Id == id)
            .Select(c => new ClienteModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Sobrenome = c.Sobrenome,
                Cpf = c.Cpf,
            })
            .FirstOrDefaultAsync();

        return cliente;
    }
}