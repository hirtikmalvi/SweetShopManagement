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
        }

        public DbSet<User> Users { get; set; }
    }
}
