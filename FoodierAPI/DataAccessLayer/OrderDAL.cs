using FoodierAPI.Constants;
using FoodierAPI.Models;
using FoodierAPI.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodierAPI.DataAccessLayer
{
    public class OrderDAL
    {
        private IConfiguration _configuration;
        private FoodierConnection _connection;
        public OrderDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new FoodierConnection(configuration);
        }

        public OrderModel GetOrderDetails(int userId, int?orderId, bool?deliverred)
        {
            SqlParameter[] listParam = new[]
            {
                new SqlParameter(){
                    ParameterName = "@userId",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= false,
                    Value = userId,
                },
                new SqlParameter(){
                    ParameterName = "@orderId",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= true,
                    Value = orderId
                },
                new SqlParameter(){
                    ParameterName = "@delivered",
                    SqlDbType = SqlDbType.Bit,
                    IsNullable= true,
                    Value = deliverred
                },
            };

            DataSet ds = _connection.Get(StoredProcedure.SP_GETORDERDETAILS, listParam);

            OrderModel od = new OrderModel();

            if (ds.Tables[0].Rows.Count == 0) {
                return od;
            }

            List<CartItemModel> lst = new List<CartItemModel>();

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new CartItemModel()
                {
                    Id = Convert.ToInt32(dr["cartid"]),
                    UserId = Convert.ToInt32(dr["UserID"]),
                    Deliverred = Convert.ToBoolean(dr["Deliverred"]),
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    ProductName = Convert.ToString(dr["Name"]),
                    Price = Convert.ToDouble(dr["Price"]),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    Thumbnail = Convert.ToString(dr["Thumbnail"] == DBNull.Value ? "" : dr["Thumbnail"] == DBNull.Value),
                    Note = Convert.ToString(dr["Note"] == DBNull.Value ? "" : dr["Note"]),
                });
            }

            
            od.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["orderid"]);
            od.CheckedOut = Convert.ToBoolean(ds.Tables[0].Rows[0]["CheckedOut"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CheckedOut"]);
            double total = 0;
            foreach (var item in lst)
            {
                total += item.Price * item.Quantity;
            }
            total = Math.Round(total, 2);
            od.Total = total; 
            od.CartItems = lst;
            return od;
        }


        public CResponse CheckOut(int orderid) {
            SqlParameter[] listParam = new[]
            {
                new SqlParameter(){
                    ParameterName = "@orderId",
                    SqlDbType = SqlDbType.Int,
                    IsNullable= false,
                    Value = orderid,
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

            return _connection.Post(StoredProcedure.SP_CHECKOUT, listParam);
        }

    }
}
