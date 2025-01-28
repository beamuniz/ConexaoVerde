using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.Domain.Models;

public class AlterarSenhaModel
{
    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    [DataType(DataType.Password)]
    public string? SenhaAtual { get; init; }

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string? NovaSenha { get; init; }

    [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
    public string? ConfirmarSenha { get; init; }
}