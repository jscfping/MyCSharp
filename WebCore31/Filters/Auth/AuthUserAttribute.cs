using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebNet6.Filters.Auth
{
    public class AuthUserAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var vipCookie = context.HttpContext.Request.Cookies["User"];

            if (string.IsNullOrEmpty(vipCookie))
            {
                throw new UnAuthorizedException();
            }
        }
    }
}