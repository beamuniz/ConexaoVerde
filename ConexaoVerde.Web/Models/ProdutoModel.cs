namespace ConexaoVerde.Web.Models;

public class ProdutoModel
{
    public int Id { get; set; }

    public string NomeProduto { get; set; }

    public decimal Preco { get; set; }

    public string Descricao { get; set; }
    
    public int CategoriaId { get; set; }

    public string CategoriaNome { get; set; }

    public List<byte[]> ImgProduto { get; set; }
    
    public int FornecedorId { get; set; }

    public string FornecedorNome { get; set; }
}