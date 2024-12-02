using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class FornecedorBusiness(DbContextConfig dbContextConfig) : IFornecedorBusiness
{
    public async Task RegistrarFornecedor(UsuarioModel usuarioModel)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha);

        if (usuarioModel.Perfil == "Fornecedor")
        {
            var fornecedorModel = usuarioModel.FornecedorModel;

            var fornecedor = new Fornecedor
            {
                RazaoSocial = fornecedorModel.RazaoSocial,
                Cnpj = fornecedorModel.Cnpj,
                NomeFantasia = fornecedorModel.NomeFantasia,
                Endereco = fornecedorModel.Endereco,
                Id = usuarioModel.Id,
                Email = usuarioModel.Email,
                Senha = senhaHash,
                Telefone = usuarioModel.Telefone,
                FotoPerfil = usuarioModel.FotoPerfil,
                Perfil = usuarioModel.Perfil
            };
            await dbContextConfig.Fornecedores.AddAsync(fornecedor);
        }

        await dbContextConfig.SaveChangesAsync();
    }

    public async Task<List<SelectListItem>> ListarFornecedores()
    {
        var fornecedores = await dbContextConfig.Fornecedores
            .Select(f => new SelectListItem
            {
                Text = f.RazaoSocial,
                Value = f.Id.ToString()
            })
            .ToListAsync();

        return fornecedores;
    }

    public async Task AtualizarFornecedor(FornecedorModel fornecedorModel)
    {
        var fornecedorExistente = await ObterIdFornecedor(fornecedorModel.Cnpj);

        if (fornecedorExistente == null)
            throw new KeyNotFoundException("Fornecedor não encontrado.");

        fornecedorExistente.NomeFantasia = fornecedorModel.NomeFantasia;
        fornecedorExistente.RazaoSocial = fornecedorModel.RazaoSocial;
        fornecedorExistente.Endereco = fornecedorModel.Endereco;

        dbContextConfig.Fornecedores.Update(fornecedorExistente);
        await dbContextConfig.SaveChangesAsync();
    }

    public async Task ExcluirFornecedor(FornecedorModel fornecedorModel)
    {
        var fornecedorExistente = await ObterIdFornecedor(fornecedorModel.Cnpj);

        if (fornecedorExistente == null)
            throw new KeyNotFoundException("Fornecedor não encontrado.");

        dbContextConfig.Fornecedores.Remove(fornecedorExistente);
        await dbContextConfig.SaveChangesAsync();
    }

    public async Task<Fornecedor> ObterIdFornecedor(string cnpj)
    {
        var fornecedor = await dbContextConfig.Fornecedores
            .FirstOrDefaultAsync(f => f.Cnpj == cnpj);

        if (fornecedor == null)
            throw new KeyNotFoundException("Fornecedor não encontrado.");

        return fornecedor;
    }
}