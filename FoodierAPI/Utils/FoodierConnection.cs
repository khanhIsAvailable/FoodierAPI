using FoodierAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace FoodierAPI.Utils
{
    public class FoodierConnection
    {

        private IConfiguration _configuration;
        private SqlConnection _sqlConnection;

         public FoodierConnection(IConfiguration configuration) 
        {
            _configuration= configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
        }

        public DataSet Get(string spname, SqlParameter[] sqlParameters) {
            _sqlConnection.Open();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(spname, _sqlConnection);
            if(sqlParameters != null)
                cmd.Parameters.AddRange(sqlParameters);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
             
            _sqlConnection.Close();

            return ds;
        }

        public CResponse Post(string spname, SqlParameter[] sqlParameters)
        {
            _sqlConnection.Open();


            SqlCommand cmd = new SqlCommand(spname, _sqlConnection);

            cmd.Parameters.AddRange(sqlParameters);


            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            _sqlConnection.Close();

            return new CResponse(Convert.ToInt32(cmd.Parameters["@returnCode"].Value), cmd.Parameters["@returnMessage"].Value.ToString());

        }

        public CResponse GetCResponse(string spname, SqlParameter[] sqlParameters)
        {
            _sqlConnection.Open();


            SqlCommand cmd = new SqlCommand(spname, _sqlConnection);

            cmd.Parameters.AddRange(sqlParameters);


            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            _sqlConnection.Close();

            return new CResponse(Convert.ToInt32(cmd.Parameters["@returnCode"].Value), cmd.Parameters["@returnMessage"].Value.ToString());

        }

        public CResponse Login(string spname, SqlParameter[] sqlParameters)
        {
            _sqlConnection.Open();


            SqlCommand cmd = new SqlCommand(spname, _sqlConnection);

            cmd.Parameters.AddRange(sqlParameters);


            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            _sqlConnection.Close();

            return new CResponse(Convert.ToInt32(cmd.Parameters["@returnCode"].Value), cmd.Parameters["@returnMessage"].Value.ToString(), Convert.ToString(cmd.Parameters["@userId"].Value));

        }

    }
}
