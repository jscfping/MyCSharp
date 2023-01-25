using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Models.User;
using Core31.Library.Response;

namespace Core31.Library.Services.User
{
    public interface IUserService
    {
        Task<AppResponse<UserData>> SignUpAsync(string email, string password);
        AppResponse<UserData> Login(string email, string password);
    }
}