using Microsoft.AspNetCore.Mvc;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Controllers;

public class UsuarioController(IUsuarioBusiness usuario) : Controller
{
    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioModel usuarioModel)
    {
        if (!ModelState.IsValid)
            return View(usuarioModel); 

        if (usuarioModel.Perfil == "Cliente")
            await usuario.RegistrarCliente(usuarioModel);
        else
            await usuario.RegistrarFornecedor(usuarioModel);

        return RedirectToAction(nameof(Cadastro)); 
    }
}