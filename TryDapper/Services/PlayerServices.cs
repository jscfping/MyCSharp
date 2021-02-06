using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TryDapper.Models.PlayerModels;
using TryDapper.Repos;

namespace TryDapper.Services
{
    class PlayerServices
    {
        public PlayerServices(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        private readonly DbConnection _dbConnection;

        public async Task CreatePlayer(string dbName)
        {
            var playerRepos = new PlayerRepos(_dbConnection);

            if (File.Exists(dbName)) return;
            await playerRepos.CreatePlayerAsync();
        }

        public async Task AddPlayer()
        {
            var playerRepos = new PlayerRepos(_dbConnection);

            var listPlayer = new List<Player>()
            {
                new Player() { Name = "playerA", Age = 5 } ,
                new Player() { Name = "playerB", Age = 15 } ,
                new Player() { Name = "playerC", Age = 25 } ,
            };
            await playerRepos.AddPlayerAsync(listPlayer);
        }

        public async Task GetPlayersAsync()
        {
            var playerRepos = new PlayerRepos(_dbConnection);

            var playerA = (await playerRepos.GetPlayerAsync("playerA"))
                .FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{playerA.Name}, {playerA.Age}");

            var playerB = (await playerRepos.GetPlayerAsync("playerB"))
                .FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{playerB.Name}, {playerB.Age}");

            var listPlayer = (await playerRepos.GetPlayersAsync()).ToList();
            foreach (var aPlayer in listPlayer)
            {
                Console.WriteLine($"{aPlayer.Name}, {aPlayer.Age}");
            }
        }
    }
}
