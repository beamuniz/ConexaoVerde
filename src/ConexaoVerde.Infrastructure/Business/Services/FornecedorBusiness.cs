using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Business.Interfaces;
using ConexaoVerde.Infrastructure.Context;
using ConexaoVerde.Web.Business.Interfaces;
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

    public async Task<List<FornecedorModel>> ListarFornecedores(string searchTerm = null)
    {
        var query = dbContextConfig.Fornecedores
            .Join(dbContextConfig.Usuarios,
                f => f.Id,
                u => u.Id,
                (f, u) => new FornecedorModel
                {
                    Id = f.Id,
                    RazaoSocial = f.RazaoSocial,
                    NomeFantasia = f.NomeFantasia,
                    Cnpj = f.Cnpj,
                    Endereco = f.Endereco,
                    FotoPerfil = u.FotoPerfil
                });

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(f => f.NomeFantasia.Contains(searchTerm));

        return await query.ToListAsync();
    }

    public async Task AdicionarAvaliacao(int avaliacao, string comentario, int fornecedorId, int clienteId)
    {
        var novaAvaliacao = new Avaliacao
        {
            Comentario = comentario,
            Nota = avaliacao,
            FornecedorId = fornecedorId,
            ClienteId = clienteId,
            DataCriacao = DateTime.Now
        };

        await dbContextConfig.Avaliacoes.AddAsync(novaAvaliacao);
        await dbContextConfig.SaveChangesAsync();
    }

    public async Task<List<Avaliacao>> ObterAvaliacoesPorFornecedor(int fornecedorId)
    {
        return await dbContextConfig.Avaliacoes
            .Where(a => a.FornecedorId == fornecedorId)
            .Include(a => a.Cliente)
            .Include(a => a.Fornecedor) 
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> ListaDeFornecedores()
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

    public async Task<FornecedorModel> ObterFornecedorPorId(int id)
    {
        var fornecedor = await dbContextConfig.Fornecedores
            .Where(f => f.Id == id)
            .Select(f => new FornecedorModel
            {
                Id = f.Id,
                RazaoSocial = f.RazaoSocial,
                NomeFantasia = f.NomeFantasia,
                Cnpj = f.Cnpj,
                Endereco = f.Endereco,
                Descricao = f.Descricao
            })
            .FirstOrDefaultAsync();

        return fornecedor;
    }
}