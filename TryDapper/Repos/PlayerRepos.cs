using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TryDapper.Models.PlayerModels;

namespace TryDapper.Repos
{
    class PlayerRepos
    {
        public PlayerRepos(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        private readonly DbConnection _dbConnection;

        public Task CreatePlayerAsync()
        {
            string sql = @"
CREATE TABLE Player(
    name TEXT,
    age INT
);";
            return _dbConnection.ExecuteAsync(sql);
        }

        public Task AddPlayerAsync(List<Player> listPlayer)
        {
            string sql = @"INSERT INTO Player(name, age) VALUES(@name, @age);";
            return _dbConnection.ExecuteAsync(sql, listPlayer);
        }
        public Task<IEnumerable<Player>> GetPlayerAsync(string name)
        {
            string sql = @"SELECT * FROM Player WHERE name=@name;";
            return _dbConnection.QueryAsync<Player>(sql, new { name });
        }
        public Task<IEnumerable<Player>> GetPlayersAsync()
        {
            string sql = @"SELECT * FROM Player;";
            return _dbConnection.QueryAsync<Player>(sql);
        }
    }
}
