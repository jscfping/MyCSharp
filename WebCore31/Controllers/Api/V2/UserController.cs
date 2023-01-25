using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Models.User;
using Core31.Library.Response;
using Core31.Library.Services.User;
using Microsoft.AspNetCore.Mvc;
using WebCore31.Controllers.Api.ApiParams.User;

namespace WebCore31.Controllers.Api.V2
{

    [ApiVersion("2.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("")]
        public Task<AppResponse<UserData>> SignUpAsync(AddUserAPIParam param)
        {
            return _userService.SignUpAsync(param.email, param.password);
        }

        

    }
}