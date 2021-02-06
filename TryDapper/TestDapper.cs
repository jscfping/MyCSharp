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
            CreatePlayer();
            AddPlayer();
            GetPlayers();
        }
        public void CreatePlayer()
        {
            _dapperConnect.CreatePlayer();
        }
        public void AddPlayer()
        {
            var listPlayer = new List<Player>()
            {
                new Player() { Name = "playerA", Age = 5 } ,
                new Player() { Name = "playerB", Age = 15 } ,
                new Player() { Name = "playerC", Age = 25 } ,
            };
            _dapperConnect.AddPlayer(listPlayer);
        }
        public void GetPlayers()
        {
            var a = _dapperConnect.GetPlayer("playerA").FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{a.Name}, {a.Age}");
            var b = _dapperConnect.GetPlayer("playerB").FirstOrDefault() ?? new Player() { Name = "null player", Age = 0 };
            Console.WriteLine($"{b.Name}, {b.Age}");
            Console.ReadLine();
        }
    }

    public class DapperConnect
    {
        private string DbPath => $@".\{DateTime.Now:yyyyMMddHHmmss}.sqlite";
        private string CnStr => $"data source={DbPath}";
        public void CreatePlayer()
        {
            if (File.Exists(DbPath)) return;

            using var cn = new SQLiteConnection(CnStr);
            string sql = @"
CREATE TABLE Player(
    name TEXT,
    age INT
);";
            cn.Execute(sql);
        }

        public void AddPlayer(List<Player> listPlayer)
        {
            using var cn = new SQLiteConnection(CnStr);
            string sql = @"INSERT INTO Player(name, age) VALUES(@name, @age);";
            cn.Execute(sql, listPlayer);
        }
        public List<Player> GetPlayer(string name)
        {
            using var cn = new SQLiteConnection(CnStr);
            string sql = @"SELECT * FROM Player WHERE name=@name;";
            var result = cn.Query<Player>(sql, new { name }).ToList();
            return result;
        }

    }




    public class Player
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
