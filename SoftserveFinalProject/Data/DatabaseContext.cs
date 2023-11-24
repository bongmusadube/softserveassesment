using Microsoft.EntityFrameworkCore;
using SoftserveFinalProject.Models;

namespace SoftserveFinalProject.Data
{
    public class DatabaseContext : DbContext
    {
      

        public DbSet <Customer> Customers { get; set; }

    }
}
