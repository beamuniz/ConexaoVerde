using Microsoft.EntityFrameworkCore;
using ConexaoVerde.AppData.Context;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;

namespace ConexaoVerde.Web.Business.Services;

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
            produtoExistente.CategoriaId = produto.CategoriaId;
            produtoExistente.FornecedorId = produto.FornecedorId;

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

    public Task<Produto> ObterProdutoPorId(int id)
    {
        return dbContextConfig.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Fornecedor)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Produto>> ListarProdutos()
    {
        return await dbContextConfig.Produtos
            .Include(p => p.Categoria)  
            .Include(p => p.Fornecedor) 
            .ToListAsync();
    }
}