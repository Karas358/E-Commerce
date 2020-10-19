using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebDevelopment_Assignment2.App_Data;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<ShoppingContext>(opt => opt.UseSqlServer(
                    Configuration.GetConnectionString("ConnectionStringMain")
                ));
            }
            else if(!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<ShoppingContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("ConnectionStringMainSqlite")));
            }
            services.AddHttpContextAccessor();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<Order>();
            services.AddTransient<Cart>();
            services.AddTransient<IShoppingRepo, SqlShoppingRepo>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp));
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddRazorOptions(options => {
                    options.ViewLocationFormats.Add("/{0}.cshtml");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddIdentity<IdentityUser, IdentityRole>(config => {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;

            })
              .AddEntityFrameworkStores<ShoppingContext>()
              .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(config => {
                config.Cookie.Name = "Shop.Cookie";
                config.LoginPath = "/Home/Login";
                config.AccessDeniedPath = "/Home/Error";
            });
            services.AddAuthorization(config => {

                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.Role)
                .Build();
                config.DefaultPolicy = defaultAuthPolicy;
            });
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            DBInitializer.beginSeed(app);
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapBlazorHub();
            });
            //Seeding Admin User On First Run
            SeedAdmin.SeedUser(app);
        }
    }
}
