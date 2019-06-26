using System;
using System.Collections.Generic;
using System.Text;
using ShopWebApp.Models;
using ShopWebApp.Repository;

using System.Linq;

namespace ShopWebAppTest.Repository
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public FakeProductRepository()
        {
            _products = new List<Product>()
            {
                new Product() {Id = 1, Name = "Fridge", Price = 1200}
            };
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products.AsEnumerable();
        }

        public void InsertProduct(Product product)
        {
            _products.Add(product);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
