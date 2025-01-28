using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Infrastructure.Business.Interfaces;

public interface IFornecedorBusiness
{
    Task RegistrarFornecedor(UsuarioModel usuarioModel);

    Task<List<FornecedorModel>> ListarFornecedores(string searchTerm );
    Task<List<SelectListItem>> ListaDeFornecedores();
    Task<FornecedorModel> ObterFornecedorPorId(int id);
    Task AdicionarAvaliacao(int avaliacao, string comentario, int fornecedorId, int clienteId);
    Task<List<Avaliacao>> ObterAvaliacoesPorFornecedor(int fornecedorId);
}