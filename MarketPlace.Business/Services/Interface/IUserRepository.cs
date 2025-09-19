using MarketPlace.DataAccess.Entities;
namespace MarketPlace.Business.Services.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task SaveChangesAsync();
        void Add(User user);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);

    }
}
