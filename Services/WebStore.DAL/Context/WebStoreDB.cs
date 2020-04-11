using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;

namespace WebStore.DAL.Context
{
    //public class WebStoreDB : DbContext
    //public class WebStoreDB : IdentityDbContext // Будет использоваться стандартный класс User (в Identity)
    public class WebStoreDB : IdentityDbContext<User, Role, string>
    {
        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base(Options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
