using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Business.Interfaces;
using ConexaoVerde.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Infrastructure.Business.Services;

public class ProdutoBusiness(DbContextConfig dbContextConfig) : IProdutoBusiness
{
    public async Task<Produto> CriarProduto(Produto produto)
    {
        await dbContextConfig.Produtos.AddAsync(produto);
        await dbContextConfig.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> AtualizarProduto(int id, Produto produto)
    {
        var produtoExistente = await dbContextConfig.Produtos.FindAsync(id);

        if (produtoExistente != null)
        {
            produtoExistente.NomeProduto = produto.NomeProduto;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.FornecedorId = produto.FornecedorId;
            produtoExistente.ImgProduto = produto.ImgProduto;

            dbContextConfig.Produtos.Update(produtoExistente);
            await dbContextConfig.SaveChangesAsync();
        }

        return produtoExistente;
    }

    public async Task<bool> DeletarProduto(int id)
    {
        var produto = await dbContextConfig.Produtos.FindAsync(id);
        if (produto != null)
        {
            dbContextConfig.Produtos.Remove(produto);
            await dbContextConfig.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<Produto> ObterProdutoPorId(int id)
    {
        var produto = await dbContextConfig.Produtos
            .Include(p => p.Categoria)   
            .Include(p => p.Fornecedor)   
            .FirstOrDefaultAsync(p => p.Id == id);

        return produto;
    }


    public async Task<List<ProdutoModel>> ListarProdutos()
    {
        return await dbContextConfig.Produtos
            .Include(p => p.Categoria)  
            .Include(p => p.Fornecedor)
            .Select(p => new ProdutoModel
            {
                Id = p.Id,
                NomeProduto = p.NomeProduto,
                Preco = p.Preco,
                ImgProduto = p.ImgProduto,
                Descricao = p.Descricao
            })
            .ToListAsync();
    }

    public async Task<List<ProdutoModel>> ObterProdutosPorFornecedor(int fornecedorId)
    {
        return await dbContextConfig.Produtos
            .Where(p => p.FornecedorId == fornecedorId)
            .Select(p => new ProdutoModel
            {
                Id = p.Id,
                NomeProduto = p.NomeProduto,
                Preco = p.Preco,
                Descricao = p.Descricao,
                ImgProdutoBase64 = p.ImgProduto != null ? Convert.ToBase64String(p.ImgProduto) : null
            })
            .ToListAsync();
    }
}