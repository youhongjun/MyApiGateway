using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using IdentityService.Models;
using IdentityService.Repositories;
using IdentityService.Services;

using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // IoC - Service & Repository
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<ILoginUserRepository, LoginUserRepository>();

            // IdentityServer4
            //string basePath = "Path to certifiates";

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
                //    Configuration["Certificates:CerPath"]),
                //    Configuration["Certificates:Password"]))
                //.AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseMvc();
            /*
            app.Use(async (context, next) =>
            {
                if (context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) && !context.User.HasClaim(c => c.Type == "sub"))
                {
                    var newIdentity = ((ClaimsIdentity)context.User.Identity);
                    newIdentity.AddClaim(new Claim("sub", context.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    context.User = new ClaimsPrincipal(newIdentity);
                }
                await next();
            });
            //*/
        }
    }
}
