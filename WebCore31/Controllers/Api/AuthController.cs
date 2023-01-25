using System;
using Core31.Library.Authentications.JwtTestUser;
using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCore31.Filters.Action;
using WebCore31.Filters.Auth;

namespace WebCore31.Controllers.ApiDevelopment
{
    public class AuthController : ApiControllerBase
    {
        private readonly IJwtTestUserAuth _jwtTestUserAuth;
        public AuthController(IJwtTestUserAuth jwtTestUserAuth)
        {
            _jwtTestUserAuth = jwtTestUserAuth;
        }

        /// <summary>
        /// test User auth
        /// </summary>
        /// <remarks>curl -k -X GET "https://localhost:5001/api/Auth/User" -b "User=Tester" -i</remarks> 
        [HttpGet("User")]
        [AuthUser]
        public string TestAuthUser()
        {
            return "hello User!";
        }


        /// <summary>
        /// test VIP auth
        /// </summary>
        /// <remarks>curl -k -X GET "https://localhost:5001/api/Auth/Vip" -b "VIP=Super" -i</remarks> 
        [HttpGet("Vip")]
        [VIPNeed]
        public string TestVip()
        {
            return "hello VIP!";
        }


        [Obsolete]
        [HttpGet("GetJwtFromJwtUser")]
        public string CreateJwtUser(int id, string userName)
        {
            return _jwtTestUserAuth.GetJwt(id, userName);
        }

        [HttpGet("AuthJwtUser")]
        [JwtTestUserAuth]
        public IJwtTestUser GetJwtUser()
        {
            return _jwtTestUserAuth.GetJwtTestUser();
        }
    }

}
