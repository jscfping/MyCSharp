using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Core31.Library.Authentications.JwtTestUser
{
    public class JwtTestUserAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authValue)) throw new UnAuthorizedException();

            var jwtTestUserAuth = context.HttpContext.RequestServices.GetService<IJwtTestUserAuth>();
            context.HttpContext.Items[nameof(JwtTestUser)] = jwtTestUserAuth.GetJwtTestUserData(context, authValue.ToString());
        }

    }
}