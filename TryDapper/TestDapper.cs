using System;
using System.Threading.Tasks;
using TryDapper.DBConnections;
using TryDapper.Services;

namespace TryDapper
{

    public class TestDapper
    {
        public void Execute()
        {
            ExecuteAsync().Wait();
            Console.ReadLine();
        }

        private async Task ExecuteAsync()
        {
            var sqliteDapper = new SqliteDapper();
            using var dbConnection = sqliteDapper.Facyory();

            var aPlayerServices = new PlayerServices(dbConnection);
            await aPlayerServices.CreatePlayer(sqliteDapper.DbPath);
            await aPlayerServices.AddPlayer();
            await aPlayerServices.GetPlayersAsync();
        }
    }
}
