using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Extensions;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ConexaoVerde.Web.Controllers;

public class UsuarioController(IFornecedorBusiness fornecedor, IClienteBusiness cliente, IUsuarioBusiness usuario)
    : Controller
{
    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioModel usuarioModel, IFormFile fotoPerfil)
    {
        if (!ModelState.IsValid)
            return View(usuarioModel);

        if (fotoPerfil is { Length: > 0 })
            usuarioModel.FotoPerfil = await fotoPerfil.OpenReadStream().ReadToEndAsync();

        if (usuarioModel.Perfil == "Cliente")
            await cliente.RegistrarCliente(usuarioModel);
        else
            await fornecedor.RegistrarFornecedor(usuarioModel);

        return RedirectToAction("Login", "Login");
    }

    // Exibir perfil do usuário logado
    [HttpGet]
    public async Task<IActionResult> Perfil()
    {
        var usuarioLogado = await GetUsuarioLogado();

        if (usuarioLogado == null)
            return RedirectToAction("Login", "Login");

        switch (usuarioLogado.Perfil)
        {
            case "Cliente":
                usuarioLogado.ClienteModel = await cliente.ObterClientePorId(usuarioLogado.Id);
                break;
            case "Fornecedor":
                usuarioLogado.FornecedorModel = await fornecedor.ObterFornecedorPorId(usuarioLogado.Id);
                break;
        }

        return View(usuarioLogado);
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarPerfil(UsuarioModel usuarioModel, IFormFile fotoPerfil)
    {
        if (!ModelState.IsValid)
            return View("Perfil", usuarioModel);

        if (fotoPerfil is { Length: > 0 })
            usuarioModel.FotoPerfil = await fotoPerfil.OpenReadStream().ReadToEndAsync();
        
        await usuario.AtualizarUsuario(usuarioModel);

        await AtualizarClaims(usuarioModel);

        return RedirectToAction("Perfil");
    }

    private async Task<UsuarioModel> GetUsuarioLogado()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        if (userIdClaim == null)
            return null;

        var user = await usuario.ObterIdUsuario(int.Parse(userIdClaim));
        return user;
    }

    private async Task AtualizarClaims(UsuarioModel usuarioModel)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, usuarioModel.Email),
            new("UserId", usuarioModel.Id.ToString()),
            new("Perfil", usuarioModel.Perfil)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }
}