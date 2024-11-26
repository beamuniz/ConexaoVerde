using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.AppData.Entities;

public class Avaliacao
{
    public int Id { get; set; }
    [Required] public string Comentario { get; set; }
    [Required] public int Nota { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
}