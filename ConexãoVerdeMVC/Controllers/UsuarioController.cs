using Microsoft.AspNetCore.Mvc;
using ConexãoVerdeMVC.Business.Interfaces;
using ConexãoVerdeMVC.Models;

namespace ConexãoVerdeMVC.Controllers;

public class UsuarioController(IUsuarioBusiness usuario) : Controller
{
    [HttpGet]
    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioModel usuarioModel)
    {
        if (ModelState.IsValid)
        {
            await usuario.RegistrarUsuario(usuarioModel);
            return RedirectToAction(nameof(Registrar));
        }

        return View(usuarioModel);
    }
}