using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        //public string Description { get; set; }

        public const string Administrator = "Admin";
        public const string AdminDefaultPassword = "AdminPassword";

    }
}
