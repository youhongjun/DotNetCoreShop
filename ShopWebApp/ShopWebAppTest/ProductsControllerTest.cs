using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Xunit;

using ShopWebApp.Models;
using ShopWebApp.Repository;
using ShopWebApp.Controllers;
using ShopWebAppTest.Repository;

namespace ShopWebAppTest
{
    public class ProductsControllerTest
    {
        ProductsController _controller;
        public ProductsControllerTest()
        {
            IProductRepository productRepository = new FakeProductRepository();
            _controller = new ProductsController(productRepository);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.Get();

            var items = Assert.IsType<List<Product>>(okResult);
            Assert.Single(items);
        }
    }
}
