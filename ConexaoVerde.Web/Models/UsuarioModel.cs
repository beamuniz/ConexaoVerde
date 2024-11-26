using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.Web.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public byte[]? FotoPerfil { get; set; }
    public string Perfil { get; set; }
    public ClienteModel ClienteModel { get; set; }
    public FornecedorModel FornecedorModel { get; set; }
}