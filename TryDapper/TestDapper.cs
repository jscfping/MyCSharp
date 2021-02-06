using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TryDapper
{

    public class TestDapper
    {
        private readonly DapperConnect _dapperConnect = new DapperConnect();

        public void Execute()
        {
            ExecuteAsync().Wait();
        }

        private async Task ExecuteAsync()
        {
            await CreatePlayer();
            await AddPlayer();
            await GetPlayersAsync();
        }

        public async Task CreatePlayer()
        {
            await _dapperConnect.CreatePlayerAsync();
        }
        public async Task AddPlayer()
        {
            var listPlayer = new List<Player>()
            {
                new Player() { Name = "playerA", Age = 5 } ,
                new Player() { Name = "playerB", Age = 15 } ,
                new Player() { Name = "playerC", Age = 25 } ,
            };
            await _dapperConnect.AddPlayerAsync(listPlayer);
        }
        public async Task GetPlayersAsync()
        {
            var a = (await _dapperConnect.GetPlayerAsync("playerA"))
                .FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{a.Name}, {a.Age}");

            var b = (await _dapperConnect.GetPlayerAsync("playerB"))
                .FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{b.Name}, {b.Age}");

            var x = (await _dapperConnect.GetPlayersAsync()).ToList();
            foreach(var c in x)
            {
                Console.WriteLine($"{c.Name}, {c.Age}");
            }

            Console.ReadLine();
        }
    }

    public class DapperConnect
    {
        private string DbPath { get; } = $@".\{DateTime.Now:yyyyMMddHHmmss}.sqlite";
        //private string DbPath => $@".\{DateTime.Now:yyyyMMddHHmmss}.sqlite"; //time would change
        private string CnStr => $"data source={DbPath}";
        public Task CreatePlayerAsync()
        {
            if (File.Exists(DbPath)) return Task.CompletedTask;

            using var cn = new SQLiteConnection(CnStr);
            string sql = @"
CREATE TABLE Player(
    name TEXT,
    age INT
);";
            return cn.ExecuteAsync(sql);
        }

        public Task AddPlayerAsync(List<Player> listPlayer)
        {
            using var cn = new SQLiteConnection(CnStr);
            string sql = @"INSERT INTO Player(name, age) VALUES(@name, @age);";
            return cn.ExecuteAsync(sql, listPlayer);
        }
        public Task<IEnumerable<Player>> GetPlayerAsync(string name)
        {
            using var cn = new SQLiteConnection(CnStr);
            string sql = @"SELECT * FROM Player WHERE name=@name;";
            return cn.QueryAsync<Player>(sql, new { name });
        }
        public Task<IEnumerable<Player>> GetPlayersAsync()
        {
            using var cn = new SQLiteConnection(CnStr);
            string sql = @"SELECT * FROM Player;";
            return cn.QueryAsync<Player>(sql);
        }

    }




    public class Player
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
