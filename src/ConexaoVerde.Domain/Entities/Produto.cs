using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.Domain.Entities;

public class Produto
{
    public int Id { get; set; }

    [Required] public string NomeProduto { get; set; }

    [Required] public decimal Preco { get; set; }

    [Required] public string Descricao { get; set; }

    [Required] public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }

    public byte[] ImgProduto { get; set; }

    [Required] public int? FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
}