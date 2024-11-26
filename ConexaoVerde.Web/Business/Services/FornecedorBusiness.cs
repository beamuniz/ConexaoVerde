using ConexaoVerde.AppData.Context;
using ConexaoVerde.Web.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConexaoVerde.Web.Business.Services;

public class FornecedorBusiness(DbContextConfig dbContextConfig) : IFornecedorBusiness
{
    public async Task<List<SelectListItem>> ListarFornecedores()
    {
        var fornecedores = await dbContextConfig.Fornecedores
            .Select(f => new SelectListItem
            {
                Text = f.RazaoSocial,  
                Value = f.Id.ToString() 
            })
            .ToListAsync();

        return fornecedores;
    }
}