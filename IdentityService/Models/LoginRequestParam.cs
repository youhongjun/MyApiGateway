using System;
namespace IdentityService.Models
{
    public class LoginRequestParam
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
    }
}
