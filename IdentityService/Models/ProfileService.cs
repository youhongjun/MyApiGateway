using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Extensions;

using IdentityService.Repositories;
using System.Security.Claims;

namespace IdentityService.Models
{
    public class ProfileService : IProfileService
    {
        protected readonly ILoginUserRepository _userRepository;

        public ProfileService(ILoginUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = context.Subject.Claims.ToList();
            context.IssuedClaims = claims.ToList();

            /*
            int userId = Convert.ToInt32(context.Subject.GetSubjectId());
            var user = _userRepository.FindById(userId);

            var claims = new List<Claim>
            {
                new Claim("role", "dataEventRecords.admin"),
                new Claim("role", "dataEventRecords.user"),
                new Claim("username", user.UserName),
                new Claim("email", user.Email)
            };

            context.IssuedClaims = claims;
            */
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //int userId = Convert.ToInt32(context.Subject.GetSubjectId());
            //var user = _userRepository.FindById(userId);
            //context.IsActive = user != null;
            var identity = context.Subject.Identity;
            context.IsActive = true;
        }
    }
}
