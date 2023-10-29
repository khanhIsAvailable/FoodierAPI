namespace FoodierAPI.Constants
{
    public static class StoredProcedure
    {
        public static readonly string SP_SEARCHPRODUCT = "sp_searchProduct";
        public static readonly string SP_GETPRODUCTIMAGE = "sp_getproductimage";

        public static readonly string SP_REMOVECARTITEM = "sp_RemoveCartItem";
        public static readonly string SP_ADDTOCART = "sp_addtocart";
        public static readonly string SP_UPDATECARTITEM = "sp_updatecartitem";

        public static readonly string SP_GETORDERDETAILS = "[sp_getcartitem]";
        public static readonly string SP_CHECKOUT = "sp_checkout";

        public static readonly string SP_GETGROCERY = "sp_getgrocery";

        public static readonly string SP_LOGIN = "[sp_Login]";
    }
}
