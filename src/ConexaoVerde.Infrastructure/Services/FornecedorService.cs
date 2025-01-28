using ConexaoVerde.Domain.Models;

namespace ConexaoVerde.Infrastructure.Services;

public class FornecedorService
{
    public static string ProcessarMensagem(string mensagem, FornecedorModel fornecedor, UsuarioModel usuario)
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

        return mensagem.Contains("email", StringComparison.OrdinalIgnoreCase) ? $"Meu e-mail de contato é: {usuario.Email}." : "Desculpe, não entendi sua mensagem. Tente novamente!";
    }
}