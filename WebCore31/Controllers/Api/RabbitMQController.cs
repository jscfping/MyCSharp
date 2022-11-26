using Core31.Library.Services.RabbitMQ;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCore31.Filters.Action;
using WebCore31.Filters.Auth;

namespace WebCore31.Controllers.ApiDevelopment
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
