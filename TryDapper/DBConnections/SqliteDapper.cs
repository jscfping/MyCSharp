using System;
using System.Data.SQLite;

namespace TryDapper.DBConnections
{
    public class SqliteDapper
    {
        public string DbPath { get; } = $@".\{DateTime.Now:yyyyMMddHHmmss}.sqlite";
        //private string DbPath => $@".\{DateTime.Now:yyyyMMddHHmmss}.sqlite"; //time would change
        private string CnStr => $"data source={DbPath}";

        public SQLiteConnection Facyory()
        {
            return new SQLiteConnection(CnStr);
        }
        

    }
}
