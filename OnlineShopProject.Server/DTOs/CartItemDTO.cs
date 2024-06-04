namespace OnlineShopProject.Server.DTOs
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public string? Image { get; set; }
    }
}
