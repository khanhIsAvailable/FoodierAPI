namespace FoodierAPI.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductOrigin { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }
        public float ProductDiscount { get; set; }
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public int GroceryId { get; set; }
        public string GroceryName { get; set; }
        public string CategoryDescription { get; set; }
        public string Thumbnail { get; set; }
        public string SpecialName { get; set; }
        public int SpecialId { get; set; }
        public string Unit { get; set; }


    }
}
