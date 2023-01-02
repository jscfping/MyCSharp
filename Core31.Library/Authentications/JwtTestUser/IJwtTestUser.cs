using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Authentications.JwtTestUser
{
    public interface IJwtTestUser
    {
        int Id { get; }
        string UserName { get; }
    }
}