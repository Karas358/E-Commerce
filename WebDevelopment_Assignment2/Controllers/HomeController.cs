using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using WebDevelopment_Assignment2.App_Data;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoppingRepo _shoppingRepo;
        private readonly Cart _cart;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepo _userRepo;

        public HomeController(ILogger<HomeController> logger, IShoppingRepo shoppingRepo, 
           Cart cart, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserRepo userRepo)
        {
            _shoppingRepo = shoppingRepo;
            _logger = logger;
            _cart = cart;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }

        public ViewResult ShoppingCart()
        {
            IEnumerable<CartItem> cartItems = _cart.GetAllCartItems().ToList();
            return View(cartItems);
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        public async Task<ViewResult> Processed()
        {
            IdentityUser user = await GetCurrentUserAsync();
            
            Order order = new Order();
            order.TotalPrice = _cart.GetShoppingCartTotal();
            order.CartID = _cart.CartID;
            order.OrderTimeStamp = System.DateTime.Now.ToString();
            order.UserEmail = user.Email;
            _shoppingRepo.createOrder(order);
            _shoppingRepo.saveChanges();
            _cart.ClearShoppingCart();
            CreatedAtAction(nameof(_shoppingRepo.getOrderById), new { id = order.OrderID }, order);
            return View(order);
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [Authorize(Roles = "Customer")]
        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        //public IActionResult AdminPanel()
        //{
        //    return View();
        //}
        
        
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            
            var user = await _userManager.FindByNameAsync(username);
            if (user != null) { 
                var signIn = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (signIn.Succeeded)
                {
                    var claims =await _userManager.GetClaimsAsync(user);
                    var roleClaim = claims[0].Value;
                    if (roleClaim == "Admin") { 
                        return RedirectToAction("ProductManagement", "Admin"); 
                    }
                    if (roleClaim == "Customer") {
                        return RedirectToAction("Account","Customer"); 
                    }
                }
                
            }
            return RedirectToAction("Error");
            
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Customer")
            };

            var user = new IdentityUser
            {
                UserName = username,
                Email = username
            };
            

            var signUp = await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimsAsync(user, claims);
            if (signUp.Succeeded)
            {
                var signIn = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (signIn.Succeeded)
                {
                    if(await _userManager.IsInRoleAsync(user, "Admin")) return RedirectToAction("AdminPanel");
                    if(await _userManager.IsInRoleAsync(user, "Customer")) return RedirectToAction("Account","Customer"); 
                    
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
