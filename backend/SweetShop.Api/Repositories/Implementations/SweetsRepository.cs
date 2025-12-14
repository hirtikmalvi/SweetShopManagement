using SweetShop.Api.Data;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Interfaces;

namespace SweetShop.Api.Repositories.Implementations
{
    public class SweetsRepository : ISweetsRepository
    {
        private readonly AppDbContext context;
        public SweetsRepository(AppDbContext _context) 
        {
            context = _context;
        }
        public async Task<Sweet?> CreateSweet(Sweet request)
        {
            var sweet = await context.Sweets.AddAsync(request);
            await SaveChangesAsync();
            return sweet.Entity;
        }
        private async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
