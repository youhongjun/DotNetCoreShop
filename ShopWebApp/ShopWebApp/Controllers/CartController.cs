using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ShopWebApp.Repository;
using ShopWebApp.Models;
using ShopWebApp.Helpers;

namespace ShopWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Cart
        [HttpGet]
        public IEnumerable<CartItem> Get()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
                cart = new List<CartItem>();
            return cart.AsEnumerable();
        }

        [HttpPost("add")]
        public void Add([FromBody] CartItem cartItem)
        {
            if (cartItem == null)
                throw new Exception("There is no produt to be added.");
            else if (cartItem.Quantity <= 0)
                throw new Exception("The quantity should be greater than 0.");

            Product product = _productRepository.GetProductById(cartItem.Product.Id);
            cartItem.Product = product ?? throw new Exception("Produt is not existed.");

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(cartItem);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = GetIndexOfProductInCart(cartItem.Product.Id);
                if (index != -1)
                {
                    cart[index].Quantity += cartItem.Quantity;
                }
                else
                {
                    cart.Add(cartItem);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        [HttpPost("remove")]
        public void Remove([FromBody] CartItem cartItem)
        {
            if (cartItem == null)
                throw new Exception("There is no produt to be removed.");
            else if (cartItem.Quantity <= 0)
                throw new Exception("The quantity to be removed should be greater than 0.");

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = GetIndexOfProductInCart(cartItem.Product.Id);
                if (index != -1)
                {
                    if (cart[index].Quantity > cartItem.Quantity)
                    {
                        cart[index].Quantity -= cartItem.Quantity;
                    }
                    else
                    {
                        cart.RemoveAt(index);
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        [HttpPost("update")]
        public void Update([FromBody] CartItem cartItem)
        {
            if (cartItem == null)
                throw new Exception("There is no produt to be added.");
            else if (cartItem.Quantity < 0)
                throw new Exception("The quantity should be greater than or equal to 0.");

            Product product = _productRepository.GetProductById(cartItem.Product.Id);
            cartItem.Product = product ?? throw new Exception("Produt is not existed.");

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                if (cartItem.Quantity > 0)
                {
                    List<CartItem> cart = new List<CartItem>();
                    cart.Add(cartItem);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = GetIndexOfProductInCart(cartItem.Product.Id);
                if (index != -1)
                {
                    if (cartItem.Quantity == 0)
                    {
                        cart.RemoveAt(index);
                    }
                    else
                    {
                        cart[index].Quantity = cartItem.Quantity;
                    }
                }
                else
                {
                    if (cartItem.Quantity > 0)
                    {
                        cart.Add(cartItem);
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        [HttpPost("clear")]
        public void Clear()
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                for (int i = cart.Count - 1; i >= 0; i--)
                {
                    cart.RemoveAt(i);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        private int GetIndexOfProductInCart(int productId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
