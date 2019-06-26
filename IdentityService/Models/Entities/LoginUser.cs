using System;
namespace IdentityService.Models
{
    public class LoginUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }

        public string Email { get; set; }
    }
}
