using Microsoft.AspNetCore.Mvc;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Extensions;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Controllers;

public class ProdutoController(
    IProdutoBusiness produtoBusiness,
    ICategoriaBusiness categoriaBusiness,
    IFornecedorBusiness fornecedorBusiness) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarProduto()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim);
        var perfilClaim = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;

        List<ProdutoModel> produtos;

        if (perfilClaim == "Fornecedor")
            produtos = await produtoBusiness.ObterProdutosPorFornecedor(userId);

        else
            produtos = await produtoBusiness.ListarProdutos();

        foreach (var produto in produtos.Where(produto => produto.ImgProduto != null))
        {
            produto.ImgProdutoBase64 = Convert.ToBase64String(produto.ImgProduto);
        }

        return View(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> CriarProduto()
    {
        ViewBag.Categorias = await categoriaBusiness.ListarCategorias();
        ViewBag.Fornecedores = await fornecedorBusiness.ListaDeFornecedores();

        return View(new ProdutoModel());
    }


    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoModel produtoModel, IFormFile fotoProduto)
    {
        if (!ModelState.IsValid)
            return View(produtoModel);

        var fornecedorId = User.FindFirst("UserId")?.Value;

        var imgProduto = fotoProduto is { Length: > 0 }
            ? await fotoProduto.OpenReadStream().ReadToEndAsync()
            : null;

        var produto = new Produto
        {
            NomeProduto = produtoModel.NomeProduto,
            Preco = produtoModel.Preco,
            Descricao = produtoModel.Descricao,
            CategoriaId = produtoModel.Categoria.Id,
            FornecedorId = int.Parse(fornecedorId!),
            ImgProduto = imgProduto
        };

        await produtoBusiness.CriarProduto(produto);

        return RedirectToAction(nameof(ListarProduto));
    }

    public async Task<IActionResult> EditarProduto(int id)
    {
        var produto = await produtoBusiness.ObterProdutoPorId(id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> EditarProduto(Produto produtoModel, IFormFile fotoProduto)
    {
        var produtoAtual = await produtoBusiness.ObterProdutoPorId(produtoModel.Id);
        if (produtoAtual == null)
        {
            ModelState.AddModelError("", "Produto não encontrado.");
            return View(produtoModel);
        }

        produtoModel.FornecedorId = produtoAtual.FornecedorId;

        var imgProduto = fotoProduto is { Length: > 0 }
            ? await fotoProduto.OpenReadStream().ReadToEndAsync()
            : null;

        var produtoAtualizado = new Produto
        {
            Id = produtoModel.Id,
            NomeProduto = produtoModel.NomeProduto,
            Preco = produtoModel.Preco,
            Descricao = produtoModel.Descricao,
            FornecedorId = produtoModel.FornecedorId,
            ImgProduto = imgProduto ?? produtoModel.ImgProduto
        };

        var sucesso = await produtoBusiness.AtualizarProduto(produtoModel.Id, produtoAtualizado);

        if (sucesso != null)
        {
            ModelState.AddModelError("", "Erro ao atualizar o produto.");
            return View(produtoModel);
        }

        return RedirectToAction(nameof(ListarProduto));
    }

    [HttpPost]
    public async Task<IActionResult> DeletarProduto(int id)
    {
        var sucesso = await produtoBusiness.DeletarProduto(id);
        if (!sucesso)
            return NotFound();

        return Json(new { sucesso = true, mensagem = "Produto excluído com sucesso!" });
    }
}