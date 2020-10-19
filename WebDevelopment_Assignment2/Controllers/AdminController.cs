using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment_Assignment2.App_Data;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IShoppingRepo _shoppingRepo;
        public AdminController(IShoppingRepo shoppingRepo)
        {
            _shoppingRepo = shoppingRepo;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ProductManagement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult removeProduct(Product product)
        {
            _shoppingRepo.removeProduct(product);
            _shoppingRepo.saveChanges();
            return RedirectToAction("ProductManagement");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(int id)
        {/*
            _shoppingRepo.createProduct(product);
            _shoppingRepo.saveChanges();*/
            Product product = _shoppingRepo.getProductById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult createProduct(Product product)
        {
            _shoppingRepo.createProduct(product);
            _shoppingRepo.saveChanges();
            return RedirectToAction("Index");
        }
    }
}
