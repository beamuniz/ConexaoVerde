using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConexaoVerde.Infrastructure.Business.Interfaces;

public interface ICategoriaBusiness
{
    Task<List<SelectListItem>> ListarCategorias();
}