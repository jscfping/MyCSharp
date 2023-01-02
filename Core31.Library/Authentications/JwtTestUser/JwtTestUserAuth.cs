using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Core31.Library.Services.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Core31.Library.Authentications.JwtTestUser
{
    public class JwtTestUserAuth : IJwtTestUserAuth
    {
        public static int ExpirationSecs => 60 * 60;
        private readonly string _key;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISystemService _systemService;
        public JwtTestUserAuth(
            Core31LibraryParas paras,
            IHttpContextAccessor httpContextAccessor,
            ISystemService systemService
            )
        {
            _key = paras.JwtTestUserKey;
            _httpContextAccessor = httpContextAccessor;
            _systemService = systemService;
        }
        public string GetJwt(int id, string userName)
        {
            var aJwtTestUser = new JwtTestUserData(id, userName);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(aJwtTestUser.ToClaims()),
                Expires = _systemService.GetNowTime().AddSeconds(ExpirationSecs),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IJwtTestUser GetJwtTestUser()
        {
            return _httpContextAccessor.HttpContext.Items[nameof(JwtTestUser)] as IJwtTestUser;
        }

        public JwtTestUserData GetJwtTestUserData(AuthorizationFilterContext context, string authValue)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var claims = handler.ValidateToken(
                    GetToken(authValue.ToString()),
                    GetTokenValidationParameters(),
                    out var validatedToken).Claims;

                var id = int.Parse(claims.First(x => x.Type == nameof(JwtTestUserData.Id)).Value);
                var name = claims.First(x => x.Type == nameof(JwtTestUserData.UserName)).Value;

                return new JwtTestUserData(id, name);
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch
            {
                throw new UnAuthorizedException();
            }

        }
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        private string GetToken(string authValueString)
        {
            if (!authValueString.StartsWith(nameof(JwtTestUser))) throw new BadRequestException($"no {nameof(JwtTestUser)}");
            try
            {
                return authValueString.Split(" ")[1];
            }
            catch
            {
                throw new BadRequestException($"bad {nameof(JwtTestUser)}");
            }
        }
    }
}