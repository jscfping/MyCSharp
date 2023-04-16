using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6.Library
{
    public class Net6Setting
    {
        public string PostgresConnectionString { get; }

        public Net6Setting(string? postgresConnectionString)
        {
            PostgresConnectionString = postgresConnectionString ?? throw new ArgumentNullException();
        }
    }
}
