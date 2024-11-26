using ConexaoVerde.AppData.Entities;

namespace ConexaoVerde.Web.Models;

public class FornecedorModel
{
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
}