using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShopWebApp.Repository;
using ShopWebApp.Models;
using System.Transactions;

namespace ShopWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetProducts();
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public Product GetProduct(int id)
        {
            return _productRepository.GetProductById(id);
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.UpdateProduct(product);
                scope.Complete();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.DeleteProduct(id);
                scope.Complete();
            }
        }
    }
}
