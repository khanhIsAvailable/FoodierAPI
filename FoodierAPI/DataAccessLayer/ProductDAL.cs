using FoodierAPI.Constants;
using FoodierAPI.Models;
using FoodierAPI.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodierAPI.DataAccessLayer
{
    public class ProductDAL
    {
        private IConfiguration _configuration;
        private FoodierConnection _connection;
        public ProductDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new FoodierConnection(configuration);
        }

        public List<ProductModel> GetProduct(int? productID, string? productName, int? shopID, string? shopName, int? groceryId, string? groceryName)
        {
            SqlParameter[] listParam = new[]
            {
                new SqlParameter(){
                    ParameterName = "@pID",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= true,
                    Value = productID,
                },
                new SqlParameter(){
                    ParameterName = "@pName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 30,
                    IsNullable= true,
                    Value = productName
                },
                new SqlParameter(){
                    ParameterName = "@pShopID",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= true,
                    Value = shopID
                },
                new SqlParameter(){
                    ParameterName = "@pShopName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 30,
                    IsNullable= true,
                    Value = shopName
                },
                new SqlParameter(){
                    ParameterName = "@pGroceryID",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= true,
                    Value = groceryId
                },
                new SqlParameter()
                {
                    ParameterName = "@pGroceryName",
                    SqlDbType =SqlDbType.NVarChar,
                    Size = 30,
                    IsNullable= true,
                    Value = groceryName
                },
            };

            DataSet ds = _connection.Get(StoredProcedure.SP_SEARCHPRODUCT, listParam);

            List<ProductModel> productLst = new List<ProductModel>();

            DataTable dt = ds.Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                ProductModel model = new ProductModel();
                model.ProductID = dr.Field<int>("ProductID");
                model.ProductName = dr.Field<string>("ProductName")??"";
                model.ProductDescription = dr.Field<string>("ProductDescription") ?? "";
                model.ProductQuantity = dr.Field<int>("ProductQuantity");
                double ProductDiscount = (double)dr["ProductDiscount"], ProductPrice = (double)dr["ProductPrice"];
                model.ProductOrigin = dr.Field<string>("ProductDescription") ?? "";
                model.ProductDiscount = (float)ProductDiscount;
                model.ProductPrice = (float)ProductPrice;

                model.ShopID = dr.Field<int>("ShopID");
                model.ShopName = dr.Field<string>("ShopName") ?? "";
                model.ShopDescription = dr.Field<string>("ShopDescription") ?? "";


                model.GroceryId = dr.Field<int>("CategoryID");
                model.GroceryName = dr.Field<string>("CategoryName") ?? "";
                model.CategoryDescription = dr.Field<string>("CategoryDescription") ?? "";
                model.Thumbnail = dr.Field<string>("Thumbnail") ?? "";

                productLst.Add(model);
            }

            return productLst;
        }
    }
}
