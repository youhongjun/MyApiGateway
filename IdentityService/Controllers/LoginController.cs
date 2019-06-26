using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using IdentityService.Models;
using System.Net.Http;
using IdentityModel.Client;

namespace IdentityService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration configuration;
        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "IdentityService-login-value1", "IdentityService-login-value2", configuration[$"IdentityClients:client.api.service:ClientSecret"] };
        }

        private async Task<TokenResponse> GetPasswordToken()
        {
            // request token
            var client = new HttpClient();
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:6000/connect/token",

                ClientId = "agent.api.service",
                ClientSecret = "agentsecret",
                Scope = "agentservice clientservice productservice",

                UserName = "edison@gmail.com",
                Password = "edisonpassword"
            });

            return response;
        }

        [HttpPost]
        public async Task<ActionResult> RequestToken([FromBody]LoginRequestParam model)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = model.ClientId;
            dict["client_secret"] = configuration[$"IdentityClients:{model.ClientId}:ClientSecret"];
            dict["grant_type"] = configuration[$"IdentityClients:{model.ClientId}:GrantType"];
            dict["username"] = model.UserName;
            dict["password"] = model.Password;

            using (HttpClient http = new HttpClient())
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await http.PostAsync(configuration["IdentityService:TokenUri"], content);
                if (!msg.IsSuccessStatusCode)
                {
                    return StatusCode(Convert.ToInt32(msg.StatusCode));
                }

                string result = await msg.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
        }
    }
}
