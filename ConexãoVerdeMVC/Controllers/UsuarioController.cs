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
    public async Task<IActionResult> Registrar(UsuarioModel usuario1)
    {
        if (ModelState.IsValid)
        {
            await usuario.RegistrarUsuario(usuario1);
            return RedirectToAction(nameof(Registrar));
        }

        return View(usuario1);
    }
}