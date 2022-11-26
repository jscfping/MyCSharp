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
        public AuthController()
        {

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
    }

}
