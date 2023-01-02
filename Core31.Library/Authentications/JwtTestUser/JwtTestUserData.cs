using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core31.Library.Authentications.JwtTestUser
{
    public class JwtTestUserData : IJwtTestUser
    {
        public JwtTestUserData(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public int Id { get; }

        public string UserName { get; }

        public Claim[] ToClaims()
        {
            return new Claim[]
            {
                new Claim(nameof(Id), Id.ToString()),
                new Claim(nameof(UserName), UserName),
            };
        }
    }
}