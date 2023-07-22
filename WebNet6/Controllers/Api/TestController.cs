using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebNet6.Filters.Action;
using WebNet6.Filters.Auth;

namespace WebNet6.Controllers.ApiDevelopment
{
    public class TestController : ApiControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly ILogger _logger;
        public TestController(IOptions<AppSetting> appSettings, ILogger<TestController> logger)
        {
            _appSetting = appSettings.Value;
            _logger = logger;
        }

        [HttpGet("MyValue")]
        public string GetMyValue()
        {
            _logger.LogInformation(_appSetting.MyValue);
            return _appSetting.MyValue;
        }

    }

}
