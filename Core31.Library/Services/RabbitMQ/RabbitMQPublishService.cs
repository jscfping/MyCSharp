using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Core31.Library.Services.RabbitMQ
{
    public class RabbitMQPublishService : IRabbitMQPublishService
    {
        private readonly RabbitMQServiceParas _paras;

        public RabbitMQPublishService(RabbitMQServiceParas paras)
        {
            _paras = paras;
        }


        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _paras.HostName,
                UserName = _paras.UserName,
                Password = _paras.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _paras.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _paras.QueueName, basicProperties: null, body: body);
            }
        }
    }
}