using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Business.Services;

public class ClienteBusiness(DbContextConfig dbContextConfig) : IClienteBusiness
{
    public async Task RegistrarCliente(ClienteModel clienteModel)
    {
        var usuario = new Cliente
        {
            CPF = clienteModel.CPF,
            NomeCompleto = clienteModel.NomeCompleto,
        };

        await dbContextConfig.Clientes.AddAsync(usuario);
        await dbContextConfig.SaveChangesAsync();
    }
}