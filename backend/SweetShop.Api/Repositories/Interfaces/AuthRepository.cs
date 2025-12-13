using Microsoft.EntityFrameworkCore;
using SweetShop.Api.Data;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Implementations;

namespace SweetShop.Api.Repositories.Interfaces
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext context;

        public AuthRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<User?> Register(User request)
        {
            var userCreated = await context.Users.AddAsync(request);
            await SaveChangesAsync();
            return userCreated.Entity;
        }
        public async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        private async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
