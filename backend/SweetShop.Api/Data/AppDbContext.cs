using Microsoft.EntityFrameworkCore;
using SweetShop.Api.Entities;

namespace SweetShop.Api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<User>().Property(u => u.IsAdmin).HasDefaultValue(false);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Hitesh",
                    Email = "Hitesh@gmail.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEPB00o6RYiRCONfnBM9vh5Vx5U2pwbwKX2fKNd7+R7ewPvx0uklyzeRCoIcVSbsyCA==",
                    IsAdmin = true
                }
            );

            modelBuilder.Entity<Sweet>().HasKey(s => s.SweetId);
            modelBuilder.Entity<Sweet>().Property(s => s.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Sweet>().HasCheckConstraint("CK_Sweet_QuantityInStock", "[QuantityInStock] >= 1");
            modelBuilder.Entity<Sweet>().HasCheckConstraint("CK_Sweet_Price", "[Price] >= 0.0");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sweet> Sweets { get; set; }
    }
}
