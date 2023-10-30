using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private AccountDAL _accountDAL;
        public AccountController(IConfiguration configuration) 
        {
            _configuration = configuration;
            _accountDAL = new AccountDAL(configuration);
        }

        [HttpPost("Login")]
        public CResponse Login(string username, string password)
        {
            CResponse res =  _accountDAL.Login(username, password);

            if(res.ReturnCode == 0)
            {
                var token = GenarationToken(username, password);

                var result = new CResponse
                {
                    ReturnCode = res.ReturnCode,
                    ReturnMessage = res.ReturnMessage,
                    ReturnData = JsonConvert.SerializeObject(new { Token = token, UserId = Convert.ToInt32(res.ReturnData) })
                };

                return result;
            } else
            {
                return res;
            }
        }

        [HttpPost("Register")]
        public CResponse Register(string username, string password, string fullname, string location, string number, int gender)
        {
            return _accountDAL.CreateAccount(username, password, fullname, location, number, gender);

        }

        private string GenarationToken(string username, string password)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("Token:SecretKey").Value);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("username", username),
                    new Claim("password", password),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                //Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

    }
}
