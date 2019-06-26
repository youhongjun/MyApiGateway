using System;
using IdentityService.Models;

namespace IdentityService.Services
{
    public interface ILoginUserService
    {
        bool Authenticate(string _userName, string _userPassword, out LoginUser loginUser);
    }
}
