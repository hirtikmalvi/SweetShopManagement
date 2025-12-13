using Microsoft.EntityFrameworkCore;

namespace SweetShop.Api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
