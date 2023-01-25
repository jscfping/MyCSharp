using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Library.Models.User
{
    public class UserData
    {
        public UserData(int id, string email, string jwt)
        {
            this.id = id;
            this.email = email;
            this.jwt = jwt;
        }

        public int id{get;}
        public string email{get;}
        public string jwt{get;}
    }
}