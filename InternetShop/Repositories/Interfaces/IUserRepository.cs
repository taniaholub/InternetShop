using InternetShop.Models;

namespace InternetShop.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User? GetByName(string name);
    }
}
