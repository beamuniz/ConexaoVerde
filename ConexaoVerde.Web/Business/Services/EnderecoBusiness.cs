using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class EnderecoBusiness(DbContextConfig dbContextConfig) : IEnderecoBusiness
{
    public async Task<Endereco> CriarEndereco(Endereco endereco)
    {
        await dbContextConfig.Enderecos.AddAsync(endereco);
        await dbContextConfig.SaveChangesAsync();
        return endereco;
    }

    public async Task<Endereco> AtualizarEndereco(int id, Endereco endereco)
    {
        var enderecoExistente = await dbContextConfig.Enderecos.FindAsync(id);

        if (enderecoExistente != null)
        {
            enderecoExistente.Rua = endereco.Rua;
            enderecoExistente.Numero = endereco.Numero;
            enderecoExistente.Cidade = endereco.Cidade;
            enderecoExistente.Estado = endereco.Estado;
            enderecoExistente.CEP = endereco.CEP;
            enderecoExistente.FornecedorId = endereco.FornecedorId;

            dbContextConfig.Enderecos.Update(enderecoExistente);
            await dbContextConfig.SaveChangesAsync();
        }

        return enderecoExistente;
    }

    public Task<Endereco> CriarEndereco(EnderecoModel endereco)
    {
        throw new NotImplementedException();
    }

    public Task<Endereco> AtualizarEndereco(int id, EnderecoModel endereco)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletarEndereco(int id)
    {
        var endereco = await dbContextConfig.Produtos.FindAsync(id);
        if (endereco != null)
        {
            dbContextConfig.Produtos.Remove(endereco);
            await dbContextConfig.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public Task<Endereco> ObterEnderecoPorId(int id)
    {
        return dbContextConfig.Enderecos
            .Include(p => p.Fornecedor)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Endereco> ObterEnderecoPorBairro(string bairro)
    {
        throw new NotImplementedException();
    }

    public Task<Endereco> ObterEnderecoPorCidade(string cidade)
    {
        throw new NotImplementedException();
    }
}