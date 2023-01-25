using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Core31.Library.Models.User;
using Microsoft.Extensions.Caching.Memory;
using Core31.Library.Response;
using Core31.Library.Authentications.JwtTestUser;

namespace Core31.Library.Services.User
{
    public class UserInMemoryService : IUserService
    {
        private readonly string _nowUserIdKey = "nowUserId";
        private readonly IMemoryCache _memoryCache;
        private readonly IJwtTestUserAuth _jwtTestUserAuth;
        public UserInMemoryService(IMemoryCache memoryCache, IJwtTestUserAuth jwtTestUserAuth)
        {
            _memoryCache = memoryCache;
            _jwtTestUserAuth = jwtTestUserAuth;
        }

        public async Task<AppResponse<UserData>> SignUpAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) throw new BadRequestException("must have email, password.");
            if (_memoryCache.TryGetValue(email, out _)) throw new BadRequestException("email already exists.");

            int newUserId = await _memoryCache.GetOrCreateAsync(_nowUserIdKey, entry =>
                        {
                            entry.AbsoluteExpirationRelativeToNow = null;

                            return Task.FromResult(1);
                        });

            _memoryCache.Set(_nowUserIdKey, newUserId + 1);

            var aNewUser = new UserInDB(newUserId, email, password);
            _memoryCache.Set(email, aNewUser, new MemoryCacheEntryOptions()
            {
                Priority = CacheItemPriority.NeverRemove,
            });
            return new AppResponse<UserData>("login success.", aNewUser.ToUserData(_jwtTestUserAuth.GetJwt(aNewUser.Id, aNewUser.Email)));
        }

        public AppResponse<UserData> Login(string email, string password)
        {
            if (!_memoryCache.TryGetValue(email, out UserInDB user)) throw new BadRequestException("email doesn't exists.");
            if (!user.IsPasswordMath(password)) throw new UnAuthorizedException();

            return new AppResponse<UserData>("login success.", user.ToUserData(_jwtTestUserAuth.GetJwt(user.Id, user.Email)));
        }


    }
}