using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.Controllers;
using WebDevelopment_Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebDevelopment_Assignment2.App_Data
{
    public class SqlShoppingRepo : IShoppingRepo
    {
        
        private readonly ShoppingContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public SqlShoppingRepo(ShoppingContext shoppingContext, UserManager<IdentityUser> userManager)
        {
            _context = shoppingContext;
            _userManager = userManager;

        }
        //Cart
        //------
        //Create
        //AddToCart
        //getAllCartItems
        public void createCart(Cart cart)
        {
            if (cart == null) throw new NotImplementedException();
            _context.Carts.Add(cart);
        }

        public void AddToCart(CartItem cartItem)
        {
            if (cartItem == null) throw new NotImplementedException();
            _context.CartItems.Add(cartItem);
        }
        public IEnumerable<CartItem> getAllCartItems(int id)
        {
            throw new NotImplementedException();
        }
        public Cart getCartById(int id)
        {
            throw new NotImplementedException();
        }
        //Order
        //---
        //Create
        //GetAll
        //GetByID
        public void createOrder(Order order)
        {
            if (order == null) throw new NotImplementedException();
            
            _context.Orders.Add(order);
        }
        public IEnumerable<Order> getAllOrders(string res)
        {
            return _context.Orders.Where(c => c.UserEmail == res).ToList();
        }
        public Order getOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(i => i.OrderID == id);
        }

        //|Product
        //---
        //Create
        //GetAll
        //GetByID
        //Remove
        //Edit
        public void createProduct(Product product)
        {
            if(product == null) throw new NotImplementedException();
            _context.Products.Add(product);
        }
        public IEnumerable<Product> getAllProducts()
        {
            return _context.Products.ToList();
        }
        public Product getProductById(int id)
        {
            var products  = _context.Products.First(i => i.ProductID == id);
            return products;
        }
        public void removeProduct(Product product)
        {
            
            if (product == null) throw new NotImplementedException();
            _context.Products.Remove(product);
        }
        public void editProduct(Product product)
        {
            if (product == null) throw new NotImplementedException();
            var prod = getProductById(product.ProductID);
            prod.Price = product.Price;
            _context.Entry(prod).State = EntityState.Modified;
            //_context.Products.Update(product);
        }

        //save changes
        public bool saveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
