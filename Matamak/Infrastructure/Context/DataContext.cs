using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infrastructure.Context
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
            { 
        }
       

        public DbSet<Item> Items { get; set; }
            public DbSet<Country> Countries { get; set; }
            public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DineinOrder> DineinOrders { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
    }
}
