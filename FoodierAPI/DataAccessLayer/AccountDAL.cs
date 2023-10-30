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
                },
                new SqlParameter() {
                    ParameterName = "@userId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                },
            };

            return _foodierConnection.Login(StoredProcedure.SP_LOGIN, param);
        }


        public CResponse CreateAccount(string username, string password, string fullname, string location, string number, int gender)
        {
            SqlParameter[] param = new[]
            {
                new SqlParameter(){
                    ParameterName = "@pUsername",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 30,
                    Value = username
                },
                new SqlParameter(){
                    ParameterName = "@pPassword",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Value = password
                },
                new SqlParameter(){
                    ParameterName = "@pFullname",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = -1,
                    Value = password
                },
                new SqlParameter(){
                    ParameterName = "@pAddress",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 300,
                    Value = location
                },
                new SqlParameter(){
                    ParameterName = "@pNumber",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20,
                    Value = number
                },
                new SqlParameter(){
                    ParameterName = "@pGender",
                    SqlDbType = SqlDbType.Int,
                    Value = gender
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

            return _foodierConnection.GetCResponse(StoredProcedure.SP_CREATEACCOUNT, param);
        }

    }
}
