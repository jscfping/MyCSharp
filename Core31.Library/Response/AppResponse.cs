using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Response
{
    public class AppResponse<T>
    {
        public string message{get;}
        public T data{get;}
        public AppResponse(string message, T data)
        {
            this.message = message;
            this.data = data;
        }
    }

    public class AppResponse : AppResponse<string>
    {
        public AppResponse(string message) : base(message, null)
        {
        }
    }
}