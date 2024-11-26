using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface IFornecedorBusiness
{
    Task<List<SelectListItem>> ListarFornecedores();
}