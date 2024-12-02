using System.Security.Claims;
using ConexaoVerde.AppData.Context;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Controllers;

public class LoginController(DbContextConfig dbContextConfig, IUsuarioBusiness usuarioBusiness) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioModel usuarioModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Dados inválidos. Tente novamente.";
            return View("Login");
        }

        var usuario = await usuarioBusiness.Login(usuarioModel);
        
        if (usuario == null)
        {
            ViewBag.ErrorMessage = "E-mail ou senha incorretos.";
            return View("Login");
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, usuario.Email),
            new("UserId", usuario.Id.ToString()), 
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult EsqueceuSenha()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EsqueceuSenha(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            ViewBag.ErrorMessage = "Por favor, informe seu e-mail.";
            return View();
        }

        var usuario = await dbContextConfig.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null)
        {
            ViewBag.ErrorMessage = "E-mail não encontrado.";
            return View();
        }

        ViewBag.Sucess = "E-mail enviado com sucesso.";

        return View();
    }
}