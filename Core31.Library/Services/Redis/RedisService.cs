using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Core31.Library.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string connectString)
        {
            if(string.IsNullOrWhiteSpace(connectString)) throw new Exception("no connectString");
            _redis = ConnectionMultiplexer.Connect(connectString);
        }


        public void Set(int databaseNumber, string key, string value)
        {
            IDatabase db = _redis.GetDatabase(databaseNumber);
            db.StringSet(key, value);
        }
        public string Get(int databaseNumber, string key)
        {
            IDatabase db = _redis.GetDatabase(databaseNumber);
            return db.StringGet(key);
        }

    }
}