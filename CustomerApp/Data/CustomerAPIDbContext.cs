using CustomerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Data
{
    public class CustomerAPIDbContext : DbContext
    {
        public CustomerAPIDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
