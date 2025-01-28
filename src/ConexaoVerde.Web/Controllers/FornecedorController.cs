using ConexaoVerde.Domain.Models;
using ConexaoVerde.Infrastructure.Business.Interfaces;
using ConexaoVerde.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace ConexaoVerde.Web.Controllers;

public class FornecedorController(
    IFornecedorBusiness fornecedorBusiness,
    IProdutoBusiness produtoBusiness,
    IUsuarioBusiness usuarioBusiness,
    FornecedorService fornecedorService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarFornecedor(string searchTerm, int? page)
    {
        const int pageSize = 5;
        var pageNumber = page ?? 1;

        var fornecedores = await fornecedorBusiness.ListarFornecedores(searchTerm);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            fornecedores = fornecedores
                .Where(f => f.NomeFantasia.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var pagedFornecedores = fornecedores
            .OrderBy(f => f.NomeFantasia)
            .ToPagedList(pageNumber, pageSize);

        return View(pagedFornecedores);
    }

    [HttpGet]
    public async Task<IActionResult> PerfilFornecedor(int id)
    {
        var fornecedor = await fornecedorBusiness.ObterFornecedorPorId(id);

        if (fornecedor == null)
            return NotFound();

        var produtos = await produtoBusiness.ObterProdutosPorFornecedor(id);
        var usuarios = await usuarioBusiness.ObterUsuariosPorFornecedor(id);

        var avaliacoes = await fornecedorBusiness.ObterAvaliacoesPorFornecedor(id) ?? [];

        var viewModel = new FornecedorPerfilModel
        {
            Fornecedor = fornecedor,
            Produtos = produtos,
            Usuario = usuarios,
            Avaliacoes = avaliacoes
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AvaliarFornecedor(int avaliacao, string comentario, int fornecedorId)
    {
        if (User.Identity!.IsAuthenticated)
        {
            var clienteId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(clienteId))
            {
                return Json(new { success = false, message = "Usuário não autenticado" });
            }

            try
            {
                await fornecedorBusiness.AdicionarAvaliacao(avaliacao, comentario, fornecedorId, int.Parse(clienteId));
                return Json(new { success = true, message = "Obrigado pela sua avaliação!" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false, message = "Ocorreu um erro ao salvar sua avaliação. Tente novamente mais tarde."
                });
            }
        }

        return Json(new { success = false, message = "Usuário não autenticado" });
    }


    [HttpPost]
    public IActionResult ChatResposta([FromBody] ChatRequestModel request)
    {
        var fornecedor = fornecedorBusiness.ObterFornecedorPorId(request.FornecedorId).Result;
        var usuario = usuarioBusiness.ObterUsuariosPorFornecedor(request.FornecedorId).Result;

        if (fornecedor == null)
            return Json(new { resposta = "Fornecedor não encontrado." });


        var respostaBot = FornecedorService.ProcessarMensagem(request.Mensagem, fornecedor, usuario);

        return Json(new { resposta = respostaBot });
    }
}