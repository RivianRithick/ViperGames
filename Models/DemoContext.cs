using Microsoft.EntityFrameworkCore;

namespace EGames.Models
{
    public class DemoContext : DbContext
    {
        public DemoContext() { }
        public DemoContext(DbContextOptions options) : base(options) { }
        public DbSet<Admin> admins { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }

    }
}
