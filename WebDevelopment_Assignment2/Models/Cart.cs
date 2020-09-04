using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.App_Data;

namespace WebDevelopment_Assignment2.Models
{
    public class Cart
    {
        private readonly ShoppingContext _context;
        public Cart(ShoppingContext context)
        {
            _context = context;
            CartItems = new HashSet<CartItem>();
        }
        [Key]
        public string CartID { get; set; }
        [ForeignKey("CustomerID")]
        public int? CustomerID { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ShoppingContext>();
            String CartID = session.GetString("CartID") ?? Guid.NewGuid().ToString();
            session.SetString("CartID", CartID);
            return new Cart(context) { CartID = CartID };
        }
        public void AddToCart(CartItem cartItem)
        {
            cartItem.CartID = CartID;
            CartItems.Add(cartItem);
            var cart = _context.Carts.SingleOrDefault(i => i.CartID == CartID);
            if (cart == null) _context.Add(this);

            var cartProd = _context.CartItems.SingleOrDefault(i => i.ProductID == cartItem.ProductID && i.CartID == CartID);

            if (cartProd == null) _context.CartItems.Add(cartItem);
            else {
                cartProd.Qty += cartItem.Qty;
                JsonPatchDocument<CartItem> patchDocument = new JsonPatchDocument<CartItem>();
                patchDocument.ApplyTo(cartProd);
            } 
            _context.SaveChanges();
        }
        public IEnumerable<CartItem> GetAllCartItems()
        {
           return _context.CartItems.Where(c => c.CartID == CartID).Include(p => p.Product);
        }
        public int CountAllCartItems()
        {
            return _context.CartItems.Where(c => c.CartID == CartID).Include(p => p.Product).Count();
        }
        public decimal GetShoppingCartTotal()
        {
            return _context.CartItems.Where(c => c.CartID == CartID)
                .Select(c => c.Product.Price * c.Qty).Sum();
        }
        public decimal GetShoppingCartVat()
        {
           

            decimal Vat = (decimal)00.1400;
            decimal Value = _context.CartItems.Where(c => c.CartID == CartID)
                .Select(c => c.Product.Price * c.Qty).Sum();

            Value = Value * Vat;
            return Value;
        }

        public decimal GetShoppingCartWithoutVat()
        {
            decimal Vat = (decimal)00.1400;
            decimal Value = _context.CartItems.Where(c => c.CartID == CartID)
                .Select(c => c.Product.Price * c.Qty).Sum();

            Value = Value - (Value * Vat);
            return Value;
        }
        public void ClearShoppingCart()
        {
            var cartItemsRemove = _context.CartItems.Where(i => i.CartID == CartID).ToList();
            _context.CartItems.RemoveRange(cartItemsRemove);
            _context.SaveChanges();
        }
    }
}
