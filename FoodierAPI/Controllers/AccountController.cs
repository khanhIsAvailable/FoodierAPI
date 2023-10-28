using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("login")]
        public CResponse Login(string username, string password)
        {
            return _accountDAL.Login(username, password);
        }

    }
}
