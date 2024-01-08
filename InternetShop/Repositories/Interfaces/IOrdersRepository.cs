using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        List<OrderItem> GetAllItems(Order order);
        List<Order> GetAllOrders(User user);
        Order? GetOpenOrder(User user);
    }
}