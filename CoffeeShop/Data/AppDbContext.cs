using CoffeeShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => new
            {
                x.Id
            }
            );
            modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Food>().HasKey(x => new
            {
                x.Id
            }
           );
            modelBuilder.Entity<Food>().HasOne(x => x.Customer).WithMany(x => x.FoodPlates).HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<HotDrink>().HasKey(x => new
            {
                x.Id
            });
            modelBuilder.Entity<HotDrink>().HasOne(x => x.Customer).WithMany(x => x.HotDrinks).HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<ColdDrink>().HasKey(x => new
            {
                x.Id
            });
             modelBuilder.Entity<ColdDrink>().HasOne(x => x.Customer).WithMany(x => x.ColdDrinks).HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<OrderItem>().HasKey(x => new
            {
                x.Id
            }
            );
            modelBuilder.Entity<OrderItem>().HasOne(x => x.Order).WithMany(x => x.OrderItems).HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<UserRefreshToken>().HasKey(x => new
            {
                x.Id
            });

            modelBuilder.Entity<Animal>().HasKey(x => new
            {
                x.id 
            }
             );

            base.OnModelCreating(modelBuilder); 
        }

        public DbSet<ColdDrink> ColdDrinks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<HotDrink> HotDrinks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        
    }   
}
