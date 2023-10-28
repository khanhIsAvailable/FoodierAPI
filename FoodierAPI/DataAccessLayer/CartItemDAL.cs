using FoodierAPI.Constants;
using FoodierAPI.Models;
using FoodierAPI.Utils;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace FoodierAPI.DataAccessLayer
{
    public class CartItemDAL
    {
        private IConfiguration _configuration;
        private FoodierConnection _connection;
        public CartItemDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new FoodierConnection(configuration);
        }

        public CResponse AddToCart(int userID, int productID, int quantity, string? note)
        {
            SqlParameter[] param = new[]
            {
                new SqlParameter(){
                    ParameterName = "@userID",
                    SqlDbType = SqlDbType.Int,
                    Value = userID
                },
                new SqlParameter(){
                    ParameterName = "@productID",
                    SqlDbType = SqlDbType.Int,
                    Value = productID
                },
                new SqlParameter() {
                    ParameterName = "@quantity",
                    SqlDbType = SqlDbType.Int,
                    Value = quantity
                },
                new SqlParameter(){
                    ParameterName = "@note",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = note
                },
                new SqlParameter() {
                    ParameterName = "@returnCode",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter() {
                    ParameterName = "@returnMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size= 300,
                    Direction = ParameterDirection.Output
                }
        };

            return _connection.Post(StoredProcedure.SP_ADDTOCART,param);
        }
        
        public CResponse UpdateCartItem(int id, int?quantity, string?note)
        {
            SqlParameter[] param = new[]
            {
                new SqlParameter(){
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Value = id,
                    IsNullable = false,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter(){
                    ParameterName = "@quantity",
                    SqlDbType = SqlDbType.Int,
                    Value = quantity,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter() {
                    ParameterName = "@note",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = -1,
                    Value = note,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter() {
                    ParameterName = "@returnCode",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter() {
                    ParameterName = "@returnMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size= 300,
                    Direction = ParameterDirection.Output
                }
            };

            return _connection.Post(StoredProcedure.SP_UPDATECARTITEM, param);
        }


        public CResponse RemoveCartItem(int id)
        {
            SqlParameter[] param = new[]
            {
                new SqlParameter(){
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Value = id,
                    IsNullable = false,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter() {
                    ParameterName = "@returnCode",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                },
                new SqlParameter() {
                    ParameterName = "@returnMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size= 300,
                    Direction = ParameterDirection.Output
                }
            };

            return _connection.Post(StoredProcedure.SP_REMOVECARTITEM, param);
        }
    }
}
