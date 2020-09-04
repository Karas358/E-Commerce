using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment_Assignment2.App_Data;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.Controllers
{
    [Route("api/shopping")]
    [ApiController]
    public class ShoppingAPIController : ControllerBase
    {
        private readonly IShoppingRepo _shoppingRepo;

        public ShoppingAPIController(IShoppingRepo shoppingRepo)
        {
            _shoppingRepo = shoppingRepo;
        }
        [HttpGet("/Product")]
        public ActionResult <IEnumerable<Product>> getAllProducts()
        {
            var products = _shoppingRepo.getAllProducts().ToList();
            if (products != null) return products;
            return NotFound();
        }
        [HttpGet("/Product/{id}")]
        public ActionResult<Product> getProductById(int id)
        {
            var product = _shoppingRepo.getProductById(id);
            if (product != null) return product;
            return NotFound();
        }
        [HttpPost("/AddProduct")]
        public ActionResult createProduct(Product product)
        {
            _shoppingRepo.createProduct(product);
            _shoppingRepo.saveChanges();
            return CreatedAtAction(nameof(getProductById), new { id = product.ProductID }, product);
        }

        [HttpPost("/AddToCart")]
        public ActionResult AddToCart(CartItem cartItem)
        {
            _shoppingRepo.AddToCart(cartItem);
            _shoppingRepo.saveChanges();
            return NoContent();
        }
    }
}
