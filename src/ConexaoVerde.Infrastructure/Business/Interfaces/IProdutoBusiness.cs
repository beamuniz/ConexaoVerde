using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IProdutoBusiness
{
    Task<Produto> CriarProduto(Produto produto);
    Task<Produto> AtualizarProduto(int id, Produto produto);
    Task<bool> DeletarProduto(int id);
    Task<Produto> ObterProdutoPorId(int id);
    Task<List<ProdutoModel>> ListarProdutos();
    Task<List<ProdutoModel>> ObterProdutosPorFornecedor(int id);
}