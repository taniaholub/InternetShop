using Microsoft.EntityFrameworkCore;

namespace InternetShop.Models
{
    // AppDbContext is a concrete implementation of the AbstractAppDbContext.
    // It is used to interact with the database using Entity Framework Core.
    public class AppDbContext : AbstractAppDbContext
    {
        public AppDbContext()
        {
            // EnsureCreated will create the database if it does not already exist.
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;port=3306;Database=kursovaDB;User=root;password=Qwertyuiop123456789;",
                new MySqlServerVersion(new Version(8, 0, 35)));

        }
    }
}
