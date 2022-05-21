using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core31.Library.Response
{

    public class ErrorResponse : AppResponse<string>
    {
        public ErrorResponse(string message) : base(message, null)
        {
        }
    }
    public class DevErrorResponse : ErrorResponse
    {
        public int HttpCode { get; }

        public string StackTrace { get; }
        public DevErrorResponse(string message, int httpCode, string stackTrace = null) : base(message)
        {
            HttpCode = httpCode;
            StackTrace = stackTrace;
        }
    }
}