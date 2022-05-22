using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCore31.Filters.Action
{
    public class VIPNeedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var vipCookie = context.HttpContext.Request.Cookies["VIP"];

            if (string.IsNullOrEmpty(vipCookie))
            {
                throw new ForbiddenException("not VIP!");
            }
        }
    }
}