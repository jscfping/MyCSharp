using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class UnAuthorizedException : AppException
    {
        public UnAuthorizedException(string message = "unauthorized.") : base(message)
        {
        }

        public override int HttpCode => 401;
    }
}