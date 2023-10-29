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

        public List<ProductModel> GetProduct(int? productID, string? productName, int? shopID, string? shopName, int? groceryId, string? groceryName , int?specialId)
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
                new SqlParameter()
                {
                    ParameterName = "@pSpecialId",
                    SqlDbType =SqlDbType.Int,
                    IsNullable= true,
                    Value = specialId
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
                model.ProductOrigin = dr.Field<string>("ProductOrigin") ?? "";
                model.ProductDiscount = (float)ProductDiscount;
                model.ProductPrice = (float)ProductPrice;

                model.ShopID = dr.Field<int>("ShopID");
                model.ShopName = dr.Field<string>("ShopName") ?? "";
                model.ShopDescription = dr.Field<string>("ShopDescription") ?? "";


                model.GroceryId = dr.Field<int>("CategoryID");
                model.GroceryName = dr.Field<string>("CategoryName") ?? "";
                model.CategoryDescription = dr.Field<string>("CategoryDescription") ?? "";
                model.Thumbnail = dr.Field<string>("Thumbnail") ?? "";
                model.Unit = dr["Unit"] == DBNull.Value ? "" : Convert.ToString(dr["Unit"]);

                model.SpecialId = dr["SpecialId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SpecialId"]);
                model.SpecialName = dr.Field<string>("SpecialName") ?? "";

                productLst.Add(model);
            }

            return productLst;
        }



        public List<ProductImageModel> GetProductImage(int productID)
        {
            SqlParameter[] listParam = new[]
            {
                new SqlParameter(){
                    ParameterName = "@productid",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= true,
                    Value = productID,
                },
            };

            DataSet ds = _connection.Get(StoredProcedure.SP_GETPRODUCTIMAGE, listParam);

            List<ProductImageModel> productLst = new List<ProductImageModel>();

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ProductImageModel model = new ProductImageModel();
                model.Id = dr.Field<int>("id");
                model.Url = dr.Field<string>("url");
                model.ProductId = dr.Field<int>("Productid");

                productLst.Add(model);
            }

            return productLst;
        }
    }
}
