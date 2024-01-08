﻿using InternetShop.Repositories.Interfaces;
using InternetShop.Models;

namespace InternetShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        AbstractAppDbContext db;

        // DI
        public ProductRepository(AbstractAppDbContext db)
        {
            this.db = db;
        }

        public void Add(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product? GetById(int id)
        {
            Product? product = db.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Save(Product product)
        {
            if (GetById(product.Id) == null)
                db.Add(product);
            db.SaveChanges();
        }
    }
}
