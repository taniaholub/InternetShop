using InternetShop.Models;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services.Interfaces;

namespace InternetShop.Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add(Product product)
        {
            productRepository.Add(product);
        }

        public bool Delete(int id)
        {
            return productRepository.Delete(id);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product? GetById(int id)
        {
            return productRepository.GetById(id);
        }

        public void Save()
        {
            productRepository.Save();
        }
    }
}
