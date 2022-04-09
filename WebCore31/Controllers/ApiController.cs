using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebCore31.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IRedisService _redisService;

        public ApiController(IOptions<AppSetting> appSettings, IRedisService redisService)
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
    }

}
