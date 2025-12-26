using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Db.SqlServer.Ef.DbContextAgg
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expert> Experts { get; set; }

       
        public DbSet<Category> Categories { get; set; }
        public DbSet<HomeService> HomeServices { get; set; }

       
        public DbSet<Order> Orders { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
