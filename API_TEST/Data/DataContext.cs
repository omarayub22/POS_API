using API_TEST.Models;
using Microsoft.EntityFrameworkCore;

namespace API_TEST.Data
{
    public class DataContext : DbContext

    {
    public DataContext( DbContextOptions options) : base(options)
        {      
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
    
}