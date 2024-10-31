using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConexãoVerdeMVC.Business.Interfaces;
using ConexãoVerdeAppData.Entities;
using ConexãoVerdeMVC.Models;

namespace ConexãoVerdeMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBusiness _usuario;

        public UsuarioController(IUsuarioBusiness usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                _usuario.RegistrarUsuario(usuario);
                return RedirectToAction(nameof(Registrar));
            }
            return View(usuario);
        }

    }
}