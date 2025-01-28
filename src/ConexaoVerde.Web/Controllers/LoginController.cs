using System.Security.Claims;
using ConexaoVerde.Domain.Entities;
using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Business.Interfaces;
using ConexaoVerde.Infrastructure.Context;
using ConexaoVerde.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Controllers;

public class LoginController(DbContextConfig dbContextConfig, IUsuarioBusiness usuarioBusiness, LoginService loginService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioModel usuarioModel, string? returnUrl = null)
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

        await AuthenticateUser(usuario);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return usuario.Perfil switch
        {
            "Cliente" => RedirectToAction("ListarFornecedor", "Fornecedor"),
            _ => RedirectToAction("Index", "Home")
        };
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

    [HttpGet]
    public IActionResult AlterarSenha()
    {
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var usuarioAtualAsync = await GetUsuarioLogado();

        if (!LoginService.VerificarSenhaAtual(usuarioAtualAsync, model.SenhaAtual))
        {
            ModelState.AddModelError("SenhaAtual", "A senha atual está incorreta.");
            return View(model);
        }

        if (model.NovaSenha != model.ConfirmarSenha)
        {
            ModelState.AddModelError("NovaSenha", "A nova senha e a confirmação não coincidem.");
            return View(model);
        }

        var resultado = await usuarioBusiness.AtualizarSenha(usuarioAtualAsync, model.NovaSenha);

        if (resultado)
            return RedirectToAction("Perfil", "Usuario");

        ModelState.AddModelError("", "Ocorreu um erro ao alterar a senha.");
        return View(model);
    }

    private async Task<UsuarioModel> GetUsuarioLogado()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        if (userIdClaim == null)
            return null;

        var user = await usuarioBusiness.ObterIdUsuario(int.Parse(userIdClaim));
        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home"); 
    }

    private async Task AuthenticateUser(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, usuario.Email),
            new("UserId", usuario.Id.ToString()),
            new("Perfil", usuario.Perfil)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }
}