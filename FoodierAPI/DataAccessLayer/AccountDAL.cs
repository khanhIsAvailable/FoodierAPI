using FoodierAPI.Constants;
using FoodierAPI.Models;
using FoodierAPI.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodierAPI.DataAccessLayer
{
    public class AccountDAL
    {
        private IConfiguration _configuration;
        private FoodierConnection _foodierConnection;
        public AccountDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _foodierConnection = new FoodierConnection(configuration);
        }

        public CResponse Login(string username, string password)
        {
            SqlParameter[] param = new[]
            {
                new SqlParameter(){
                    ParameterName = "@pUsername",
                    SqlDbType = SqlDbType.VarChar,
                    Size = username.Length,
                    Value = username
                },
                new SqlParameter(){
                    ParameterName = "@pPassword",
                    SqlDbType = SqlDbType.VarChar,
                    Size = password.Length,
                    Value = password
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

            return _foodierConnection.GetCResponse(StoredProcedure.SP_LOGIN, param);
        }


    }
}
