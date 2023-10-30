using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private IConfiguration _configuration;
        private CartItemDAL _cartItemDAL;
        public CartItemController(IConfiguration configuration)
        {
            _configuration = configuration;
            _cartItemDAL = new CartItemDAL(configuration);
        }

        [Authorize]
        [HttpPost("Add-to-cart")]
        public CResponse AddToCart(int userid, int productid, int quantity, string? note)
        {
            return _cartItemDAL.AddToCart(userid, productid, quantity, note);
        }

        [Authorize]
        [HttpPut("Update-cart-item")]
        public CResponse UpdateCartItem(int cartid, int?quantity, string?note)
        {
            return _cartItemDAL.UpdateCartItem(cartid, quantity, note);
        }

        [Authorize]
        [HttpDelete("Remove-cart-item")]
        public CResponse RemoveCartItem(int cartid)
        {
            return _cartItemDAL.RemoveCartItem(cartid);
        }
    }
}
