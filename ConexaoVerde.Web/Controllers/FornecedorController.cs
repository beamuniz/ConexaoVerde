using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoVerde.Web.Controllers;

public class FornecedorController(IFornecedorBusiness fornecedorBusiness, IProdutoBusiness produtoBusiness, IUsuarioBusiness usuarioBusiness) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarFornecedor()
    {
        var fornecedores = await fornecedorBusiness.ListarFornecedores();

        return View(fornecedores); 
    }

    [HttpGet]
    public async Task<IActionResult> PerfilFornecedor(int id)
    {
        var fornecedor = await fornecedorBusiness.ObterFornecedorPorId(id);
        if (fornecedor == null)
        {
            return NotFound();
        }

        var produtos = await produtoBusiness.ObterProdutosPorFornecedor(id);
        var usuarios = await usuarioBusiness.ObterUsuariosPorFornecedor(id);

        var viewModel = new FornecedorPerfilModel
        {
            Fornecedor = fornecedor,
            Produtos = produtos,
            Usuario = usuarios
        };

        return View(viewModel);
    }
}