using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Services.System
{
    public class SystemService : ISystemService
    {
        public DateTime GetNowTime()
        {
            return DateTime.UtcNow.AddHours(8); //taiwan time
        }
    }
}