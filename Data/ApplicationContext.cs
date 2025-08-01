using Microsoft.EntityFrameworkCore;
using AmazonApiServer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonApiServer.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetailss { get; set; }
        public DbSet<ProductDisplay> ProductDisplays { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<PropertyKey> PropertyKeys { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }
        public DbSet<ReviewReview> ReviewReviews { get; set; }
        public DbSet<ReviewTag> ReviewTags { get; set; }
        public DbSet<Role> Roles { get; set; }
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
            modelBuilder.Entity<Category>().HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
