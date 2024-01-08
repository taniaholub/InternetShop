using InternetShop.Repositories.Interfaces;
using InternetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        AbstractAppDbContext db;

        // DI
        public OrdersRepository(AbstractAppDbContext db)
        {
            this.db = db;
        }

        // Adds a new Order entity to the database
        public void Add(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
        }

        // Deletes an Order entity from the database based on its id
        public bool Delete(int id)
        {
            bool deleted = false;
            var order = db.Orders.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        // Retrieves all Order entities from the database
        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        // Retrieves all OrderItem entities for a specific Order
        public List<OrderItem> GetAllItems(Order order)
        {
            // get all items of the order which is passed as an argument to the method
            return db.OrderItems.FromSqlRaw(String.Format("SELECT * FROM orderitems WHERE orderitems.OrderId = {0}", order.Id))
                                .ToList();
        }


        public List<Order> GetAllOrders(User user)
        {
            // get all orders of the user who is passed as an argument to the method
            return db.Orders.FromSqlRaw(String.Format("SELECT * FROM orders WHERE orders.UserId = {0}", user.Id))
                            .ToList();
        }

        public Order? GetOpenOrder(User user)
        {
            return db.Orders.FromSqlRaw(
                String.Format("SELECT * FROM orders WHERE orders.UserId = {0} AND orders.Status = 0", user.Id)).FirstOrDefault();

        }

        public Order? GetById(int id)
        {
            Order? order = db.Orders.FirstOrDefault(o => o.Id == id);
            return order;
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public void Save(Order order)
        {
            if (GetById(order.Id) == null)
                db.Add(order);
            db.SaveChanges();
        }
    }
}
