using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Usuario
{
    public int Id { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Senha { get; set; }
    [Required] public string Telefone { get; set; }
    public byte[]? FotoPerfil { get; set; }
    [Required] public string Perfil { get; set; }
}