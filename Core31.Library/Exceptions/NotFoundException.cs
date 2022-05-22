using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message = "not found.") : base(message)
        {
        }

        public override int HttpCode => 404;
    }
}