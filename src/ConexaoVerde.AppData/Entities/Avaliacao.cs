namespace ConexaoVerde.AppData.Entities;

public class Avaliacao
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public int ClienteId { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; }
    public DateTime DataCriacao { get; set; }

    public Fornecedor Fornecedor { get; set; }
    public Cliente Cliente { get; set; }
}