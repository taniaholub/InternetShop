using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class OrderService : IOrderService
    {
        IOrderItemRepository orderItemRepository;
        IOrdersRepository orderRepository;
        UserAuthentication authenticationUser;

        public OrderService(IOrdersRepository orderRepository, IOrderItemRepository orderItemRepository, UserAuthentication authenticationUser)
        {
            this.authenticationUser = authenticationUser;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void Add(Order order)
        {
            orderRepository.Add(order);
        }

        public void Cancel(Order order)
        {
            order.Status = OrderStatus.Cancelled;
            orderRepository.Save();
        }

        public void Complete(Order order)
        {
            order.Status = OrderStatus.Completed;
            orderRepository.Save();
        }

        public bool Delete(int id)
        {
            return orderRepository.Delete(id);
        }

        public List<Order> GetAll()
        {
            return orderRepository.GetAll();
        }

        public List<OrderItem> GetAllItems(Order order)
        {
            return orderRepository.GetAllItems(order);
        }

        public bool Order(Product product, int quantity)
        {
            // ціна останнього доданого товару
            double totalItemsPrice = product.Price * quantity;

            // створення нового замовлення / отримання вже наявного відкритого замовлення поточного користувача
            // додати загальну ціну продуктів, які передаються як аргумент
            Order order = GetOpenOrder(authenticationUser.User);
            double totalSum = order.TotalSum + totalItemsPrice;

            if (totalSum > authenticationUser.User.Balance)
            {
                return false;
            }

            order.TotalSum = totalSum;
            orderRepository.Save(order);

            // створення позиції замовлення для замовленого вище
            OrderItem orderItem = new OrderItem();
            orderItem.Quantity = quantity;
            orderItem.OrderId = order.Id;
            orderItem.ProductId = product.Id;
            orderItemRepository.Save(orderItem);

            return true;
        }

        public List<Order> GetAllOrders(User user)
        {
            return orderRepository.GetAllOrders(user);
        }

        public Order GetOpenOrder(User user)
        {
            Order? order = orderRepository.GetOpenOrder(user);
            if (order is null)
            {
                order = new Order();
                order.Status = OrderStatus.Open;
                order.UserId = user.Id;
            }
            return order;
        }

        public Order? GetById(int id)
        {
            return orderRepository.GetById(id);
        }

        public void Save()
        {
            orderRepository.Save();
        }
    }
}
