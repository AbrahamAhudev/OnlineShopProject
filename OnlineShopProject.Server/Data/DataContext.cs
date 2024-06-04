using Microsoft.EntityFrameworkCore;
using OnlineShopProject.Server.Models;
namespace OnlineShopProject.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasOne<Cart>()
                .WithOne()
                .HasForeignKey<CartItem>(ci => ci.CartId);

            modelBuilder.Entity<User>()
                .HasOne<Cart>()
                .WithOne()
                .HasForeignKey<User>(u => u.CartId);
        }
    }
}
