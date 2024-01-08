namespace InternetShop.Models
{
    // The possible states of an order
    public enum OrderStatus
    {
        Open,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public double TotalSum { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}