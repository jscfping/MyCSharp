using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCore31.Filters.Action;
using WebCore31.Filters.Auth;

namespace WebCore31.Controllers.ApiDevelopment
{
    public class RedisController : ApiControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisController(IRedisService redisService)
        {
            _redisService = redisService;
        }



        [HttpPost("")]
        public IActionResult SetRedisValue([FromQuery] string key, [FromQuery] string value, [FromQuery] int databaseNumber = 0)
        {
            _redisService.Set(databaseNumber, key, value);
            return Ok();
        }


        [HttpGet("")]
        public string GetRedisValue([FromQuery] string key, [FromQuery] int databaseNumber = 0)
        {
            return _redisService.Get(databaseNumber, key);
        }
    }

}
