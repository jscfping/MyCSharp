using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library
{
    public class Core31LibraryParas
    {
        public Core31LibraryParas(string jwtTestUserKey)
        {
            JwtTestUserKey = jwtTestUserKey;
        }
        public string JwtTestUserKey { get; }

    }
}