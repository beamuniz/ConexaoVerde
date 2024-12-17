using System.ComponentModel.DataAnnotations;

namespace ConexaoVerde.Web.Models;

public class ProdutoModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public string NomeProduto { get; set; }

    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public CategoriaModel Categoria { get; set; }
    public byte[] ImgProduto { get; set; }
    public string? ImgProdutoBase64 { get; set; }

    public FornecedorModel Fornecedor { get; set; }
}