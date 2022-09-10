using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Services.RabbitMQ
{
    public class RabbitMQServiceParas
    {
        public RabbitMQServiceParas(string hostName, string userName, string password, string queueName)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
            QueueName = queueName;
        }

        public string HostName { get; }
        public string UserName { get; }
        public string Password { get; }

        public string QueueName { get; }
    }
}