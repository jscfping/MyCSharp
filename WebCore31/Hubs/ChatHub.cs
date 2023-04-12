using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace WebNet6.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMemoryCache _cache;

        public ChatHub(IMemoryCache cache)
        {
            _cache = cache;
        }


        public async Task SendMessageAsync(string message)
        {
            var aUser = GetUser();
            if (aUser == null) throw new ServerException("no user");
            await Clients.All.SendAsync("SomeoneSays", $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}", aUser, message);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["user"];

            var aUser = new User(userId, username);
            _cache.Set(Context.ConnectionId, aUser);
            
            await Clients.All.SendAsync("SystemBroadcast", $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}", $"{aUser.GetMessageName()} has joined the chat room.");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var aUser = GetUser();
            if (aUser == null) throw new ServerException("no user");
            _cache.Remove(Context.ConnectionId);

            await Clients.All.SendAsync("SystemBroadcast", $"[{DateTime.Now:yyyyMMdd HH:mm:ss}]", $"{aUser.GetMessageName()} has left the chat room.");

            await base.OnDisconnectedAsync(exception);
        }


        private User GetUser()
        {
            _cache.TryGetValue(Context.ConnectionId, out User aUser);
            return aUser;
        }



        public class User
        {
            public User(string connectionId, string name)
            {
                ConnectionId = connectionId;
                Name = name;
            }
            public string ConnectionId { get; }
            public string Name { get; }

            public string GetMessageName() => $"{Name}[{ConnectionId}]";
        }
    }
}