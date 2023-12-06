using Microsoft.EntityFrameworkCore;
using SoftserveFinalProject.Models;

namespace SoftserveFinalProject.Data
{
    public class CustomerAPIDbContext : DbContext
    {
        public CustomerAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
