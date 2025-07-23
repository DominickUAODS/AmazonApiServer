using Microsoft.EntityFrameworkCore;
using AmazonApiServer.Models;

namespace AmazonApiServer.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(u => u.Wishlist).WithMany(p => p.WishlistedBy); // todo delete wishlist item on user or product delete
            modelBuilder.Entity<User>().HasMany(e => e.Orders).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(e => e.Role).WithMany(e => e.Users);
            modelBuilder.Entity<User>().HasMany(e => e.Reviews).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(e => e.ReviewReviews).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasMany(e => e.ReviewReviews).WithOne(e => e.Review).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasMany(e => e.ReviewTags).WithOne(e => e.Review).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasMany(e => e.ReviewImages).WithOne(e => e.Review).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasOne(e => e.Product).WithMany(e => e.Reviews).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(e => e.Displays).WithOne(e => e.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(e => e.Details).WithOne(e => e.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(e => e.Features).WithOne(e => e.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasOne(e => e.Category).WithMany(e => e.Products).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Product>().HasMany(e => e.OrderItems).WithOne(e => e.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItem>().HasOne(e => e.Order).WithMany(e => e.OrderItems).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasMany(e => e.PropertyKeys).WithOne(e => e.Category).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
