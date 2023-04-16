using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebNet6.Filters.Action;
using WebNet6.Filters.Auth;

namespace WebNet6.Controllers.ApiDevelopment
{
    public class RabbitMQController : ApiControllerBase
    {
        private readonly IRabbitMQPublishService _rabbitMQPublishService;

        public RabbitMQController(IRabbitMQPublishService rabbitMQPublishService)
        {
            _rabbitMQPublishService = rabbitMQPublishService;
        }

        [HttpPost("Message")]
        public void RabbitMQSend(string message)
        {
            _rabbitMQPublishService.SendMessage(message);
        }

    }

}
