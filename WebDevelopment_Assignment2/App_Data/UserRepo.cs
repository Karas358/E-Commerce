using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebDevelopment_Assignment2.App_Data
{
    public class UserRepo : IUserRepo
    {
        private readonly IHttpContextAccessor _accessor;

        public UserRepo(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
       

        public ClaimsIdentity GetUserClaimsIdentity()
        {
            return _accessor?.HttpContext.User.Identity as ClaimsIdentity;

        }

        public bool isUserAdmin()
        {
            return (bool)_accessor?.HttpContext.User.IsInRole("Admin");
        }

        
    }
}
