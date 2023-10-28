using FoodierAPI.Constants;
using FoodierAPI.Models;
using FoodierAPI.Utils;
using System.Collections.Generic;
using System.Data;

namespace FoodierAPI.DataAccessLayer
{
    public class GroceryDAL
    {
        private IConfiguration _configuration;
        private FoodierConnection _foodierConnection;
        public GroceryDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _foodierConnection = new FoodierConnection(configuration);
        }

        public List<GroceryModel> GetGrocery()
        {
            DataSet ds = _foodierConnection.Get(StoredProcedure.SP_GETGROCERY, null);
            var lst = new List<GroceryModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new GroceryModel()
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Description = Convert.ToString(dr["Description"]),
                    BackgroundColor = Convert.ToString(dr["BackgroundColor"]),
                    BorderColor = Convert.ToString(dr["BorderColor"]),
                    Image = Convert.ToString(dr["Image"])
                });
            }
            return lst;

        }

    }
}
