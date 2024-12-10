namespace ConexaoVerde.AppData.Entities;

public class AvaliacaoFornecedor
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public int UsuarioId { get; set; }
    public int Nota { get; set; } // Nota de 1 a 5
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
    
    public Fornecedor Fornecedor { get; set; }
    public Usuario Usuario { get; set; }
}