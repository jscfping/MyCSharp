using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public abstract class AppException : Exception
    {
        public AppException(string message) : base(message)
        {

        }
        public abstract int HttpCode { get; }
    }
}