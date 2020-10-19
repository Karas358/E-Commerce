using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebDevelopment_Assignment2.App_Data
{
    public interface IUserRepo
    {
        ClaimsIdentity GetUserClaimsIdentity();
        bool isUserAdmin();
    }
}
