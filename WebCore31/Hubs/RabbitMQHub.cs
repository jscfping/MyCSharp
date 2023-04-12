using System;
using Microsoft.AspNetCore.SignalR;

namespace WebNet6.Hubs
{
    public class RabbitMQHub : Hub
    {






        public static void Subscribe(IServiceProvider sp, string message)
        {
            var hub = (IHubContext<RabbitMQHub>)sp.GetService(typeof(IHubContext<RabbitMQHub>));
            hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }


}