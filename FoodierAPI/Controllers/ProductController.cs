using FoodierAPI.DataAccessLayer;
using FoodierAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FoodierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IConfiguration _configuration;
        private ProductDAL _productDAL;
        public ProductController( IConfiguration configuration)
        {
            _configuration = configuration;
            _productDAL = new ProductDAL(configuration);
        }

        [HttpGet("search-product")]
        public List<ProductModel> SearchProduct(int? productID, string? productName, int? shopID, string? shopName, int? groceryId, string? groceryName, int?specialId)
        {
            
            return _productDAL.GetProduct(productID, productName, shopID, shopName, groceryId, groceryName, specialId);

        }

        [HttpGet("Get-product-image")]
        public List<ProductImageModel> GetProductImage (int productid)
        {
            return _productDAL.GetProductImage(productid);
        }


    }
}
