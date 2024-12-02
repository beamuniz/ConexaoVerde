using ConexaoVerde.Web.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoVerde.Web.Controllers;

public class FornecedorController(IFornecedorBusiness fornecedorBusiness) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarFornecedor()
    {
        var fornecedores = await fornecedorBusiness.ListarFornecedores();

        return View(fornecedores); 
    }
}