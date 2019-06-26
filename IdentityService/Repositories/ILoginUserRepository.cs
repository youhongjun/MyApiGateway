using System;
using IdentityService.Models;

namespace IdentityService.Repositories
{
    public interface ILoginUserRepository
    {
        LoginUser Authenticate(string _userName, string _userPassword);

        LoginUser FindById(int id);

        LoginUser FindByUsername(string username);
    }
}
