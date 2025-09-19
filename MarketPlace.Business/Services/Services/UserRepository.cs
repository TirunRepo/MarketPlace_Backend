using MarketPlace.Business.Services.Interface;
using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Business.Services.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<User?> GetByIdAsync(int id) => await _ctx.Users.FindAsync(id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken) =>
            await _ctx.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        public async Task SaveChangesAsync() => await _ctx.SaveChangesAsync();
        public void Add(User user) =>  _ctx.Users.Add(user);

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber) =>
            await _ctx.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

    }
}
