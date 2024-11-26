using ConexaoVerde.AppData.Context;
using ConexaoVerde.Web.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class CategoriaBusiness(DbContextConfig dbContextConfig) : ICategoriaBusiness
{
    public async Task<List<SelectListItem>> ListarCategorias()
    {
        var categorias = await dbContextConfig.Categorias
            .Select(c => new SelectListItem
            {
                Text = c.NomeCategoria,  
                Value = c.Id.ToString()  
            })
            .ToListAsync();

        return categorias;
    }
}