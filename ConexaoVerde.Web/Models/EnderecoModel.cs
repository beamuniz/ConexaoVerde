using ConexaoVerde.AppData.Entities;

namespace ConexaoVerde.Web.Models;

public class EnderecoModel
{
    public int Id { get; set; }
    
    public string Rua { get; set; }
    
    public int Numero { get; set; }
    
    public string Cidade { get; set; }
   
    public string Estado { get; set; }
  
    public string CEP { get; set; }

    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
}