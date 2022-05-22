using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string message = "forbidden.") : base(message)
        {
        }

        public override int HttpCode => 403;
    }
}