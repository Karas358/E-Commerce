using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace WebDevelopment_Assignment2.App_Data
{
    public class SeedAdmin
    {
        public static async void SeedUser(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                  .CreateScope())
            {
                ShoppingContext context = serviceScope.ServiceProvider.GetService<ShoppingContext>();
                UserManager<IdentityUser> _userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Admin")
            };
                var user = new IdentityUser
                {
                    UserName = "Admin",
                    Email = "Admin"
                };
                var signUp = await _userManager.CreateAsync(user, "Password5");
                await _userManager.AddClaimsAsync(user, claims);
            }
        }
    }
}
