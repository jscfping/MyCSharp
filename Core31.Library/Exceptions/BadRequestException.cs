using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public override int HttpCode => 400;
    }
}