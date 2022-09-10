using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Services.RabbitMQ
{
    public interface IRabbitMQPublishService
    {
        void SendMessage(string message);
    }
}