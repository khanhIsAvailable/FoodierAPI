using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private IConfiguration _configuration;
        private GroceryDAL _groceryDAL;
        public GroceryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _groceryDAL = new GroceryDAL(configuration);
        }

        [HttpGet("Get-grocery")]
        public List<GroceryModel> GetGrocery()
        {
            return _groceryDAL.GetGrocery();
        }
    }
}
