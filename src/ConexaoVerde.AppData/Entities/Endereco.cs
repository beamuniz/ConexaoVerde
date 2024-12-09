using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Endereco
{
    public int Id { get; set; }
    [Required] public string Rua { get; set; }
    [Required] public int Numero { get; set; }
    [Required] public string Cidade { get; set; }
    [Required] public string Estado { get; set; }
    [Required] public string CEP { get; set; }

    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
}