using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class ServerException : AppException
    {
        public ServerException(string message) : base(message)
        {
        }

        public override int HttpCode => 500;
    }
}