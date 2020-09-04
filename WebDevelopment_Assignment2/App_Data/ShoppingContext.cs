using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.App_Data
{
    public class ShoppingContext: IdentityDbContext
    {
        public ShoppingContext() { }
        public ShoppingContext(DbContextOptions<ShoppingContext> opt) : base(opt)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
