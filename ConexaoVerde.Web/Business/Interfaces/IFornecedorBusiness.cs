using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IFornecedorBusiness
{
    Task RegistrarFornecedor(UsuarioModel usuarioModel);
    Task<List<SelectListItem>> ListarFornecedores();
    Task AtualizarFornecedor(FornecedorModel fornecedorModel);
    Task ExcluirFornecedor(FornecedorModel fornecedorModel);
    Task<Fornecedor> ObterIdFornecedor(string cnpj);
}