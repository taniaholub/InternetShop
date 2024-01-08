using InternetShop.Models;

namespace InternetShop.Services.Interfaces
{
    public interface IProductService
    {
        public void Add(Product product);
        public bool Delete(int id);
        public List<Product> GetAll();
        public Product? GetById(int id);
        public void Save();
    }
}
