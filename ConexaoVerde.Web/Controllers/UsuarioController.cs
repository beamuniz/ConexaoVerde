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
        if (ModelState.IsValid && !usuarioModel.Perfil)
        {
            await usuario.RegistrarCliente(usuarioModel);
            return RedirectToAction(nameof(Cadastro));
        }

        return View(usuarioModel);
    }
}