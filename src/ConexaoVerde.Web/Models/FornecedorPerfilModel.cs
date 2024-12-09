namespace ConexaoVerde.Web.Models;

public class FornecedorPerfilModel
{
    public FornecedorModel Fornecedor { get; set; }
    public List<ProdutoModel> Produtos { get; set; }
    public UsuarioModel Usuario { get; set; }
}