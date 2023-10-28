namespace FoodierAPI.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Deliverred { get; set; }
        public string ProductName { get; set; }
        public string Thumbnail {  get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
