using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCore31.Filters.Action;
using WebCore31.Filters.Auth;

namespace WebCore31.Controllers.ApiDevelopment
{
    public class TestController : ApiControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IRedisService _redisService;

        public TestController(IOptions<AppSetting> appSettings, IRedisService redisService)
        {
            _appSetting = appSettings.Value;
            _redisService = redisService;
        }

        [HttpGet("MyValue")]
        public string GetMyValue()
        {
            return _appSetting.MyValue;
        }

        [HttpPost("Resis")]
        public IActionResult SetRedisValue([FromQuery] string key, [FromQuery] string value, [FromQuery] int databaseNumber = 0)
        {
            _redisService.Set(databaseNumber, key, value);
            return Ok();
        }


        [HttpGet("Resis")]
        public string GetRedisValue([FromQuery] string key, [FromQuery] int databaseNumber = 0)
        {
            return _redisService.Get(databaseNumber, key);
        }

        /// <summary>
        /// curl -X GET "http://localhost:5000/api/Test/User" -b "User=Tester" -i
        /// </summary>
        [HttpGet("User")]
        [AuthUser]
        public string TestAuthUser()
        {
            return "hello User!";
        }


        /// <summary>
        /// curl -X GET "http://localhost:5000/api/Test/Vip" -b "VIP=Super" -i
        /// </summary>
        [HttpGet("Vip")]
        [VIPNeed]
        public string TestVip()
        {
            return "hello VIP!";
        }

    }

}
