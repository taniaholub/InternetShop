using InternetShop.Repositories.Interfaces;
using InternetShop.Models;

namespace InternetShop.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        AbstractAppDbContext db;

        //DI
        public OrderItemRepository(AbstractAppDbContext db)
        {
            this.db = db;
        }

        // Adds a new OrderItem entity to the database
        public void Add(OrderItem entity)
        {
            db.OrderItems.Add(entity);
            db.SaveChanges();
        }

        // Deletes an OrderItem entity from the database based on its id
        public bool Delete(int id)
        {
            bool deleted = false;
            var orderItem = db.OrderItems.FirstOrDefault(o => o.Id == id);

            if (orderItem != null)
            {
                db.OrderItems.Remove(orderItem);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        // Retrieves all OrderItem entities from the database
        public List<OrderItem> GetAll()
        {
            return db.OrderItems.ToList();
        }

        // Retrieves a single OrderItem entity by its id
        public OrderItem? GetById(int id)
        {
            OrderItem? orderItem = db.OrderItems.FirstOrDefault(o => o.Id == id);
            return orderItem;
        }

        // Saves changes to the database
        public void Save()
        {
            db.SaveChanges();
        }

        // Saves or updates an OrderItem entity in the database
        public void Save(OrderItem orderItem)
        {
            if (GetById(orderItem.Id) == null)
                db.Add(orderItem);
            db.SaveChanges();
        }
    }
}
