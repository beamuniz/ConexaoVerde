using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Services;

public class FornecedorService
{
    public string ProcessarMensagem(string mensagem, FornecedorModel fornecedor, UsuarioModel usuario)
    {
        if (string.IsNullOrEmpty(mensagem))
            return "Por favor, envie uma mensagem.";

        if (mensagem.Contains("Olá", StringComparison.OrdinalIgnoreCase))
            return $"Olá! Eu sou o fornecedor {fornecedor.NomeFantasia}. Como posso ajudar?";

        if (mensagem.Contains("endereço", StringComparison.OrdinalIgnoreCase))
        {
            var endereco =
                $"{fornecedor.Endereco!.Rua}, {fornecedor.Endereco.Numero}, {fornecedor.Endereco.Cidade} - {fornecedor.Endereco.Estado}, {fornecedor.Endereco.CEP}";
            return $"O meu endereço é: {endereco}.";
        }

        if (mensagem.Contains("telefone", StringComparison.OrdinalIgnoreCase))
            return $"Meu telefone de contato é: {usuario.Telefone}.";

        if (mensagem.Contains("email", StringComparison.OrdinalIgnoreCase))
            return $"Meu e-mail de contato é: {usuario.Email}.";
        
        return "Desculpe, não entendi sua mensagem. Tente novamente!";
    }
}