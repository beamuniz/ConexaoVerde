using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Senha { get; set; }
    public string Telefone { get; set; }
    public byte[]? FotoPerfil { get; set; }
    public string Perfil { get; set; }
    public Cliente? Cliente { get; set; }
}