using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Models.User;
using Core31.Library.Response;
using Core31.Library.Services.User;
using Microsoft.AspNetCore.Mvc;
using WebNet6.Controllers.Api.ApiParams.User;

namespace WebNet6.Controllers.Api.V2
{

    [ApiVersion("2.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login/user")]
        public Task<AppResponse<UserData>> LoginUser(LoginUserAPIParam param)
        {
            var result = _userService.Login(param.email, param.password);
            return Task.FromResult(result);
        }
    }
}