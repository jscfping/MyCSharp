using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.Library.Utils;

namespace Core31.Library.Models.User
{
    public class UserInDB
    {
        public UserInDB(int id, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException();
            Id = id;
            Email = email;
            Salt = CryptographyUtil.GenerateSalt();
            ShaVal = CryptographyUtil.ComputeSHA256Hash(password + Salt);
        }
        public int Id { get; }
        public string Email { get; }
        public string Salt { get; }
        public string ShaVal { get; }

        public bool IsPasswordMath(string password)
        {
            return CryptographyUtil.ComputeSHA256Hash(password + Salt) == ShaVal;
        }

        public UserData ToUserData(string jwt)
        {
            return new UserData(Id, Email, jwt);
        }
    }
}