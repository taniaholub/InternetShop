using Microsoft.EntityFrameworkCore;

namespace InternetShop.Models
{
    // DbContext is a part of Entity Framework, which is an ORM (Object-Relational Mapper) framework.
    // AbstractAppDbContext is used to configure and interact with the database.
    public abstract class AbstractAppDbContext : DbContext
    {
        // За допомогою цієї властивості можна запитувати та зберігати сутності користувача.
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
