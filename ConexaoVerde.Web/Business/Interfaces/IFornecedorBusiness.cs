using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IFornecedorBusiness
{
    Task RegistrarFornecedor(UsuarioModel usuarioModel);

    Task<List<FornecedorModel>> ListarFornecedores();

    // Task AtualizarFornecedor(FornecedorModel fornecedorModel);
    // Task ExcluirFornecedor(FornecedorModel fornecedorModel);
    Task<FornecedorModel> ObterFornecedorPorId(int id);
}