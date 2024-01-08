using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IOrderService
    {
        void Add(Order order); // Adds a new order to the system
        public bool Order(Product product, int quantity); // Places an order for a specified product and quantity.
        bool Delete(int id);
        List<Order> GetAll(); 
        List<OrderItem> GetAllItems(Order order); // Retrieves all items of a specific order.
        Order? GetById(int id); // Retrieves a single order by its ID.
        void Save();
        void Complete(Order order);
        void Cancel(Order order);
        Order GetOpenOrder(User user);// Retrieves the current open order for a specified user.
        List<Order> GetAllOrders(User user);// Retrieves all orders made by a user.
    }
}
