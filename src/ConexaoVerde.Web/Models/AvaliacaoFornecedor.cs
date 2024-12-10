namespace ConexaoVerde.Web.Models;

public class AvaliacaoFornecedorModel
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public int UsuarioId { get; set; }
    public int Nota { get; set; } // Nota de 1 a 5
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
        
    public FornecedorModel Fornecedor { get; set; }
    public UsuarioModel Usuario { get; set; }
}