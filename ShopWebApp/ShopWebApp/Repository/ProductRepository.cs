using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWebApp.Models;

using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Hosting;

namespace ShopWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            string productPath = _hostingEnvironment.ContentRootPath + $"/Assets/Products/{id}.json";
            if(File.Exists(productPath))
            {
                using (var stream = File.Open(productPath, FileMode.Open))
                {
                    DataContractJsonSerializer jsonSerialize = new DataContractJsonSerializer(typeof(Product));
                    product = (Product)jsonSerialize.ReadObject(stream);
                }
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            string productDir = _hostingEnvironment.ContentRootPath + $"/Assets/Products";
            var files = from file in Directory.EnumerateFiles(productDir, "*.json", SearchOption.TopDirectoryOnly)
                        select file;
            foreach (var file in files)
            {
                using (var stream = File.Open(file, FileMode.Open))
                {
                    DataContractJsonSerializer jsonSerialize = new DataContractJsonSerializer(typeof(Product));
                    list.Add((Product)jsonSerialize.ReadObject(stream));
                }
            }

            return list.AsEnumerable();
        }

        public void InsertProduct(Product product)
        {
            if ((product.Name.Length > 0) && (product.Id > 0))
            {
                string productPath = _hostingEnvironment.ContentRootPath + $"/Assets/Products/{product.Id}.json";
                if (File.Exists(productPath))
                    throw new Exception("Product is already existed!");

                var js = new DataContractJsonSerializer(typeof(Product));
                using (var stream = File.Create(productPath))
                {
                    js.WriteObject(stream, product);
                }
            }
            else
            {
                throw new Exception("Invalid product!");
            }
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
