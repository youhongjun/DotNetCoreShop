using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ShopWebApp.Models;

namespace ShopWebApp.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProductById(int id);

        void InsertProduct(Product product);

        void DeleteProduct(int id);

        void UpdateProduct(Product product);

        void Save();
    }
}
