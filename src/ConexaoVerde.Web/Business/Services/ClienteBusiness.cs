using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

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

    public async Task<Cliente> ObterIdCliente(string cpf)
    {
        var cliente = await dbContextConfig.Clientes
            .FirstOrDefaultAsync(c => c.Cpf == cpf);

        if (cliente == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        return cliente;
    }

    public async Task ExcluirCliente(ClienteModel clienteModel)
    {
        var clienteExistente = await ObterIdCliente(clienteModel.Cpf);

        if (clienteExistente == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        dbContextConfig.Clientes.Remove(clienteExistente);
        await dbContextConfig.SaveChangesAsync();
    }
}