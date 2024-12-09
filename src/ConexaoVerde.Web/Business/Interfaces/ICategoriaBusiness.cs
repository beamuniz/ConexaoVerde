using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Web.Business.Interfaces;

public interface ICategoriaBusiness
{
    Task<List<SelectListItem>> ListarCategorias();
}