using Microsoft.AspNetCore.Mvc;
using ConexaoVerde.AppData.Entities;
using ConexaoVerde.Web.Business.Interfaces;

namespace ConexaoVerde.Web.Controllers;

public class ProdutoController(IProdutoBusiness produtoBusiness) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarProduto()
    {
        var produtos = await produtoBusiness.ListarProdutos();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterProdutoPorId(int id)
    {
        var produto = await produtoBusiness.ObterProdutoPorId(id);
        if (produto == null) return NotFound();

        return Ok(produto);
    }
    
    [HttpGet]
    public IActionResult CriarProduto()
    {
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto([FromBody] Produto produto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var novoProduto = await produtoBusiness.CriarProduto(produto);
        return CreatedAtAction(nameof(ObterProdutoPorId), new { id = novoProduto.Id }, novoProduto);
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