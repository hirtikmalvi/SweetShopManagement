using Microsoft.EntityFrameworkCore;
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
        async Task<List<Sweet>> ISweetsRepository.GetAllSweets()
        {
            var sweets = await context.Sweets.ToListAsync();
            return sweets;
        }
        async Task<List<Sweet>> ISweetsRepository.SearchSweets(string? name, string? category, decimal? minPrice, decimal? maxPrice)
        {
            var query = context.Sweets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(s => s.Category == category);
            }
            if (minPrice.HasValue)
            {
                query = query.Where(s => s.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(s => s.Price <= maxPrice);
            }
            return await query.ToListAsync();
        }
        public async Task<bool> SweetExist(int sweetId)
        {
            return await context.Sweets.AnyAsync(s => s.SweetId == sweetId);
        }
        public async Task UpdateSweet()
        {
            await SaveChangesAsync();
        }
        public async Task<bool> DeleteSweet(int sweetId)
        {
            var sweet = await context.Sweets.FirstOrDefaultAsync(s => s.SweetId == sweetId);
            var updated = context.Sweets.Remove(sweet!);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Sweet?> GetSweetById(int sweetId)
        {
            var sweet = await context.Sweets.FirstOrDefaultAsync(s => s.SweetId == sweetId);
            return sweet;
        }
        public async Task<List<Sweet>> GetSweetsWithMinimumQty()
        {
            var sweets = await context.Sweets.Where(s => s.QuantityInStock == context.Sweets.Min(ns => ns.QuantityInStock)).ToListAsync();
            return sweets;
        }
        private async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
