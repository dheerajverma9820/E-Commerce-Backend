using ECommerce_app.Entities;
using ECommerce_app.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_app.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<CartItem> CartItems { get; set; } 
        public DbSet<UserAddressNew> userAddressNew {  get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");
            modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductCategory>()
        //        .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        //    modelBuilder.Entity<ProductCategory>()
        //        .HasOne(pc => pc.Product)
        //        .WithMany(p => p.ProductCategories)
        //        .HasForeignKey(pc => pc.ProductId);

        //    modelBuilder.Entity<ProductCategory>()
        //        .HasOne(pc => pc.Category)
        //        .WithMany(c => c.ProductCategories)
        //        .HasForeignKey(pc => pc.CategoryId);
        //}
    }
}
