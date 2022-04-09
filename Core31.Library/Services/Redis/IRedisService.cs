using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Services.Redis
{
    public interface IRedisService
    {
        public void Set(int databaseNumber, string key, string value);
        public string Get(int databaseNumber, string key);
    }
}