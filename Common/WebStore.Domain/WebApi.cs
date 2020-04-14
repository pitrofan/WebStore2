using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain
{
    public static class WebApi
    {
        public const string Employees = "api/v1/employees"; 

        public const string Products = "api/v1/products"; 

        public const string Orders = "api/v1/orders"; 

        public static class Identity
        {
            public const string Users = "api/v1/users";
            public const string Roles = "api/v1/roles";
        }
    }
}
