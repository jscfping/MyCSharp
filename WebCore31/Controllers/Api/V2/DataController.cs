using Microsoft.AspNetCore.Mvc;
using System;

namespace WebNet6.Controllers.Api.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {


        /// <summary>
        /// test version 2.0
        /// </summary>
        [HttpGet]
        [Obsolete]
        public string Get()
        {
            return "data from api v2";
        }

        /// <summary>
        /// test version 2.1
        /// </summary>
        [HttpGet]
        [ApiVersion("2.1")]
        public string Get2_1()
        {
            return "data from api v2.1";
        }

        /// <summary>
        /// test version 2.2
        /// </summary>
        [HttpGet]
        [ApiVersion("2.2")]
        public string Get2_2()
        {
            return "data from api v2.2";
        }
    }
}
