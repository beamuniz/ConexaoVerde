﻿using Microsoft.AspNetCore.Mvc;
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
        var produtos = await produtoBusiness.ListarProdutos();

        foreach (var produto in produtos.Where(produto => produto.ImgProduto != null))
        {
            produto.ImgProdutoBase64 = Convert.ToBase64String(produto.ImgProduto);
        }

        return View(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterProdutoPorId(int id)
    {
        var produto = await produtoBusiness.ObterProdutoPorId(id);
        if (produto == null) return NotFound();

        return Ok(produto);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var produtoAtualizado = await produtoBusiness.AtualizarProduto(id, produto);
        if (produtoAtualizado == null) return NotFound();

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletarProduto(int id)
    {
        var sucesso = await produtoBusiness.DeletarProduto(id);
        if (!sucesso) return NotFound();

        return NoContent();
    }
}