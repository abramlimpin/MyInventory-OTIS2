using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyInventory.Models;

namespace MyInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
            
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MyInventory.Models.Contact> Contact { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<ChatModel> Chat { get; set; }
    }
}
