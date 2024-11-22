using ConexaoVerde.AppData.Entities;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IProdutoBusiness
{
    Task<Produto> CriarProduto(Produto produto);
    Task<Produto> AtualizarProduto(int id, Produto produto);
    Task<bool> DeletarProduto(int id);
    Task<Produto> ObterProdutoPorId(int id);
    Task<IEnumerable<Produto>> ListarProdutos();
}