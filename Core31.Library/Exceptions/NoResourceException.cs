using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class NoResourceException : AppException
    {
        public NoResourceException(string message) : base(message)
        {
        }

        public override int HttpCode => 404;
    }
}