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
        public DbSet<Cart> carts { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderMaster> OrderMaster { get; set; }


    }
}
