namespace ConexaoVerde.AppData.Entities;

public class Cliente : Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public Usuario Usuario { get; set; }
}