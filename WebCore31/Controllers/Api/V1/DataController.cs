using Microsoft.AspNetCore.Mvc;
using System;

namespace WebNet6.Controllers.Api.V1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {

        /// <summary>
        /// test version 1.0
        /// </summary>
        [HttpGet()]
        [Obsolete]
        public string Get()
        {
            return "data from api v1";
        }


        /// <summary>
        /// test version 1.1
        /// </summary>
        [HttpGet()]
        [ApiVersion("1.1")]
        public string Get1_1()
        {
            return "data from api v1.1";
        }


    }
}
