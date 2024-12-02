using Microsoft.AspNetCore.Mvc;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Controllers;

public class UsuarioController(IFornecedorBusiness fornecedor, IClienteBusiness cliente) : Controller
{
    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(UsuarioModel usuarioModel,  IFormFile fotoPerfil)
    {
        if (!ModelState.IsValid)
            return View(usuarioModel); 
        
        if (fotoPerfil is { Length: > 0 })
        {
            using var memoryStream = new MemoryStream();
            await fotoPerfil.CopyToAsync(memoryStream);
            usuarioModel.FotoPerfil = memoryStream.ToArray();
        }
        
        if (usuarioModel.Perfil == "Cliente")
            await cliente.RegistrarCliente(usuarioModel);
        else
            await fornecedor.RegistrarFornecedor(usuarioModel);

        return RedirectToAction("Login", "Login");
    }
}