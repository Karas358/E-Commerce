using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.App_Data
{
    public class DBInitializer
    {
        public static void beginSeed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                ShoppingContext context = serviceScope.ServiceProvider.GetService<ShoppingContext>();
                context.Database.EnsureCreated();
                if (!context.Products.Any())
                {
                    var product = new Product[]
                    {
                        new Product
                        {
                            ProductName = "Water",
                            Price = 22.50M
                        },
                        new Product
                        {
                            ProductName = "Meat",
                            Price = 42.50M
                        },
                        new Product
                        {
                            ProductName = "Rice",
                            Price = 32.50M
                        },
                        new Product
                        {
                            ProductName = "Skittles",
                            Price = 12.50M
                        },
                        new Product
                        {
                            ProductName = "Waffles",
                            Price = 22.50M
                        },
                        new Product
                        {
                            ProductName = "Pan Cakes",
                            Price = 52.50M
                        },
                        new Product
                        {
                            ProductName = "Cake",
                            Price = 72.50M
                        },
                        new Product
                        {
                            ProductName = "Yo yo",
                            Price = 22.50M
                        }
                    };
                    context.AddRange(product);
                }
                context.SaveChanges();
            }   
        }
    }
}
