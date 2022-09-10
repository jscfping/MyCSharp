using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core31.Library.Services.RabbitMQ;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client.Events;

namespace WebCore31.Hubs
{
    public class RabbitMQHub : Hub
    {






        public static void Subscribe(IServiceProvider sp, object model, BasicDeliverEventArgs ea)
        {
            var hub = (IHubContext<RabbitMQHub>)sp.GetService(typeof(IHubContext<RabbitMQHub>));

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }


}