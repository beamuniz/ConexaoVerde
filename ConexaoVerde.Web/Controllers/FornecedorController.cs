using ConexaoVerde.Web.Business.Interfaces;
using ConexaoVerde.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoVerde.Web.Controllers;

public class FornecedorController(
    IFornecedorBusiness fornecedorBusiness,
    IProdutoBusiness produtoBusiness,
    IUsuarioBusiness usuarioBusiness) : Controller
{
    [HttpGet]
    public async Task<IActionResult> ListarFornecedor()
    {
        var fornecedores = await fornecedorBusiness.ListarFornecedores();

        return View(fornecedores);
    }

    [HttpGet]
    public async Task<IActionResult> PerfilFornecedor(int id)
    {
        var fornecedor = await fornecedorBusiness.ObterFornecedorPorId(id);

        if (fornecedor == null)
            return NotFound();

        var produtos = await produtoBusiness.ObterProdutosPorFornecedor(id);
        var usuarios = await usuarioBusiness.ObterUsuariosPorFornecedor(id);

        var viewModel = new FornecedorPerfilModel
        {
            Fornecedor = fornecedor,
            Produtos = produtos,
            Usuario = usuarios
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ChatFornecedor(int id)
    {
        var fornecedor = await fornecedorBusiness.ObterFornecedorPorId(id);

        if (fornecedor == null)
            return NotFound();

        var produtos = await produtoBusiness.ObterProdutosPorFornecedor(id);
        var usuarios = await usuarioBusiness.ObterUsuariosPorFornecedor(id);

        // Cria o modelo de visão
        var viewModel = new FornecedorPerfilModel
        {
            Fornecedor = fornecedor,
            Produtos = produtos,
            Usuario = usuarios
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ChatResposta([FromBody] ChatRequest request)
    {
        var fornecedor = fornecedorBusiness.ObterFornecedorPorId(request.FornecedorId).Result;
        var usuario = usuarioBusiness.ObterUsuariosPorFornecedor(request.FornecedorId).Result;
        if (fornecedor == null)
        {
            return Json(new { resposta = "Fornecedor não encontrado." });
        }

        var respostaBot = ProcessarMensagem(request.Mensagem, fornecedor, usuario);

        return Json(new { resposta = respostaBot });
    }

    private static string ProcessarMensagem(string mensagem, FornecedorModel fornecedor, UsuarioModel usuario)
    {
        if (string.IsNullOrEmpty(mensagem))
        {
            return "Por favor, envie uma mensagem.";
        }

        if (mensagem.Contains("Olá", StringComparison.OrdinalIgnoreCase))
        {
            return $"Olá! Eu sou o fornecedor {fornecedor.NomeFantasia}. Como posso ajudar?";
        }

        if (mensagem.Contains("endereço", StringComparison.OrdinalIgnoreCase))
        {
            var endereco =
                $"{fornecedor.Endereco.Rua}, {fornecedor.Endereco.Numero}, {fornecedor.Endereco.Cidade} - {fornecedor.Endereco.Estado}, {fornecedor.Endereco.CEP}";
            return $"O meu endereço é: {endereco}.";
        }

        if (mensagem.Contains("telefone", StringComparison.OrdinalIgnoreCase))
        {
            return $"Meu telefone de contato é: {usuario.Telefone}.";
        }

        if (mensagem.Contains("email", StringComparison.OrdinalIgnoreCase))
        {
            return $"Meu e-mail de contato é: {usuario.Email}.";
        }

        return "Desculpe, não entendi sua mensagem. Tente novamente!";
    }

    public class ChatRequest
    {
        public int FornecedorId { get; set; }
        public string Mensagem { get; set; }
    }
}