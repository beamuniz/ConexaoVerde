using ConexaoVerde.Web.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConexaoVerde.Web.Configuration;

    public class UserProfileActionFilter(IUsuarioBusiness userService) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.Identity?.Name;
            if (userId != null)
            {
                var usuario = userService.ObterPorEmail(userId).Result;
                if (usuario != null)
                {
                    context.HttpContext.Items["FotoPerfil"] = usuario.FotoPerfil; 
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

