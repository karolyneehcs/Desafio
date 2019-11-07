using Desafio.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Desafio.Filters
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"];

            
            var db =  context.HttpContext.RequestServices.GetRequiredService<DesafioContext>();

            //validar token
            if (db.Accesses.Any(a => a.Token.Contains(token)))
            {
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
