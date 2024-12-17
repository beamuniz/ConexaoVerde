using System.Diagnostics;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoVerde.Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    // Página inicial (Index)
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
