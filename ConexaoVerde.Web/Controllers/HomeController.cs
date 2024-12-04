using System.Diagnostics;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ConexaoVerde.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Página inicial (Index)
    public IActionResult Index()
    {
        // Verificar se o usuário está autenticado
        if (User.Identity?.IsAuthenticated == true)
        {
            // Passar informações para a View
            ViewData["IsLoggedIn"] = true;
            ViewData["UserName"] = User.Identity.Name; // Nome do usuário logado
        }
        else
        {
            ViewData["IsLoggedIn"] = false;
        }

        return View();
    }

    // Página de privacidade
    public IActionResult Privacy()
    {
        return View();
    }

    // Página de erro
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
