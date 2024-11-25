using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Fornecedor : Usuario
{
    [Required] public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    [Required] public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
    public List<Produto> Produtos { get; set; }
}