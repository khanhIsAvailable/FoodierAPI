using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IConfiguration _configuration;
        private OrderDAL _orderDAL;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _orderDAL = new OrderDAL(configuration);
        }

        [Authorize]
        [HttpGet("Get-order-details")]
        public OrderModel GetOrderDetails(int userId, int?orderId, bool? deliverred)
        {
            return _orderDAL.GetOrderDetails(userId, orderId, deliverred);
        }

        [Authorize]
        [HttpPost("Check-out")]
        public CResponse CheckOut(int orderid)
        {
            return _orderDAL.CheckOut(orderid);
        }
    }
}
