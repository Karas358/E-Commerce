using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment_Assignment2.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController()
        {

        }
        [Authorize(Roles = "Customer")]
        public IActionResult Account()
        {
            return View();
        }
    }
}
