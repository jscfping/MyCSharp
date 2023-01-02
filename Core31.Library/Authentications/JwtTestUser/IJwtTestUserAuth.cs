using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core31.Library.Authentications.JwtTestUser
{
    public interface IJwtTestUserAuth
    {
        IJwtTestUser GetJwtTestUser();
        string GetJwt(int id, string userName);
        JwtTestUserData GetJwtTestUserData(AuthorizationFilterContext context, string authValue);
    }
}