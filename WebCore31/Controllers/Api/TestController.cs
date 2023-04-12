using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebNet6.Filters.Action;
using WebNet6.Filters.Auth;

namespace WebNet6.Controllers.ApiDevelopment
{
    public class TestController : ApiControllerBase
    {
        private readonly AppSetting _appSetting;
        public TestController(IOptions<AppSetting> appSettings)
        {
            _appSetting = appSettings.Value;
        }

        [HttpGet("MyValue")]
        public string GetMyValue()
        {
            return _appSetting.MyValue;
        }

    }

}
