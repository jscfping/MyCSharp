using Core31.Library.Services.RabbitMQ;
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
