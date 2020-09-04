using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.Controllers;
using WebDevelopment_Assignment2.Models;

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

        public void createOrder(Order order)
        {
            if (order == null) throw new NotImplementedException();
            
            _context.Orders.Add(order);
        }

        public void createProduct(Product product)
        {
            if(product == null) throw new NotImplementedException();
            _context.Products.Add(product);
        }

        public IEnumerable<CartItem> getAllCartItems(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> getAllOrders(string res)
        {
            return _context.Orders.Where(c => c.UserEmail == res).ToList();
        }
        public IEnumerable<Product> getAllProducts()
        {
            return _context.Products.ToList();
        }

        public Cart getCartById(int id)
        {
            throw new NotImplementedException();
        }

        public Order getOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(i => i.OrderID == id);
        }

        public Product getProductById(int id)
        {
            var products  = _context.Products.First(i => i.ProductID == id);
            return products;
        }

        public bool saveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
