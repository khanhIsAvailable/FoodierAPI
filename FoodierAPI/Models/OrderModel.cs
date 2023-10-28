namespace FoodierAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime? DeliverredDate { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public bool CheckedOut { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
