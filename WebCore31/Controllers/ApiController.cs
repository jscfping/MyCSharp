using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebCore31.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly AppSetting _appSetting;

        public ApiController(IOptions<AppSetting> appSettings)
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
