using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class LearningDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

    }
}
