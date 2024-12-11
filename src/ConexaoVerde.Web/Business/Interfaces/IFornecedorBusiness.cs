using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IFornecedorBusiness
{
    Task RegistrarFornecedor(UsuarioModel usuarioModel);

    Task<List<FornecedorModel>> ListarFornecedores(string searchTerm );
    Task<List<SelectListItem>> ListaDeFornecedores();

    // Task ExcluirFornecedor(FornecedorModel fornecedorModel);
    Task<FornecedorModel> ObterFornecedorPorId(int id);

    Task AdicionarAvaliacao(int avaliacao, string comentario, int fornecedorId, int clienteId);
    Task<List<Avaliacao>> ObterAvaliacoesPorFornecedor(int fornecedorId);
}