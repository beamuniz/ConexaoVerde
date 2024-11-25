using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoVerde.Web.Controllers;

public class EnderecoCOntroller(IEnderecoBusiness endereco) : Controller
{
    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(EnderecoModel enderecoModel)
    {
        if (ModelState.IsValid)
        {
            await endereco.CriarEndereco(enderecoModel);
            return RedirectToAction(nameof(Cadastro));
        }

        return View(enderecoModel);
    }
}