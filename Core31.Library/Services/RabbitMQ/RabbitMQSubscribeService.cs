using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core31.Library.Services.RabbitMQ
{
    public class RabbitMQSubscribeService
    {
        public RabbitMQSubscribeService(IServiceProvider serviceProvider, RabbitMQServiceParas paras, Action<IServiceProvider, object, BasicDeliverEventArgs> act)
        {
            var factory = new ConnectionFactory()
            {
                HostName = paras.HostName,
                UserName = paras.UserName,
                Password = paras.Password
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();



            channel.QueueDeclare(queue: paras.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) => act(serviceProvider, sender, args);
            // consumer.Received += (model, ea) =>
            // {
            //     var body = ea.Body.ToArray();
            //     var message = Encoding.UTF8.GetString(body);
            //     Console.WriteLine($" [x] Received {message}");
            // };

            channel.BasicConsume(queue: paras.QueueName, autoAck: true, consumer: consumer);



        }
    }
}