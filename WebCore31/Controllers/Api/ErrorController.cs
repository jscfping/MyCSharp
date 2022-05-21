using System;
using Core31.Library.Exceptions;
using Core31.Library.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebCore31.Controllers.Api
{
    public class ErrorController : ApiControllerBase
    {

        public ErrorController()
        {
        }

        [HttpGet("BadRequestException")]
        public string ThrowBadRequestException([FromQuery]string message)
        {
            throw new BadRequestException(message);
        }
        [HttpGet("NoResourceException")]
        public string ThrowNoResourceException([FromQuery]string message)
        {
            throw new NoResourceException(message);
        }
        [HttpGet("Exception")]
        public string ThrowException([FromQuery]string message)
        {
            throw new Exception(message);
        }
        
    }

}
