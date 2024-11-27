using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Fornecedor : Usuario
{
    public int Id { get; set; }
    [Required] public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    [Required] public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
}