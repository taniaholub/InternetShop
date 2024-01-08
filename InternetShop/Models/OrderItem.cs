
namespace InternetShop.Models
{
    // Represents an item within an order in the online shop.
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }

        // It's marked as nullable (Product?) for cases where the Product object is not available.
        public Product? Product { get; set; }
    }
}
