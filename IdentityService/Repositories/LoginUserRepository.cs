using System;
using System.Collections.Generic;
using System.Linq;

using IdentityService.Models;

namespace IdentityService.Repositories
{
    public class LoginUserRepository : ILoginUserRepository
    {
        // some dummy data. Get account from database, LDAP, Okta or somewhere else.
        private readonly List<LoginUser> _users = new List<LoginUser>
        {
            new LoginUser{
                Id = 1,
                UserName = "damienbod",
                Password = "damienbod",
                RealName = "Damienbod Green",
                Email = "damienbod@gmail.com"
            },
            new LoginUser{
                Id = 2,
                UserName = "raphael",
                Password = "raphael",
                RealName = "Raphael Smith",
                Email = "raphael@gmail.com"
            },
        };

        public LoginUser Authenticate(string _userName, string _userPassword)
        {
            var user = FindByUsername(_userName);
            if ((user != null) && user.Password.Equals(_userPassword))
            {
                return user;
            }

            return null;
        }

        public LoginUser FindById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public LoginUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
