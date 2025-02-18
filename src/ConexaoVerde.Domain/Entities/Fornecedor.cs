using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.Domain.Entities;

public class Fornecedor : Usuario
{
    public int Id { get; set; }
    [Required] public string RazaoSocial { get; set; }
    public string? Descricao { get; set; }
    public string NomeFantasia { get; set; }
    [Required] public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
}