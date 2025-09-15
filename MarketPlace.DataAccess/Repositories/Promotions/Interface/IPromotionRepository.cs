using MarketPlace.DataAccess.Entities.Promotions;

namespace MarketPlace.DataAccess.Repositories.Promotions.Interface
{
    public interface IPromotionRepository
    {
        Task<PromotionModel> GetByIdAsync(int id);
        Task<List<PromotionModel>> GetAllAsync();
        Task<PromotionModel> AddAsync(PromotionModel entity);
        Task<PromotionModel> UpdateAsync(PromotionModel entity);
        Task DeleteAsync(int id);
        Task<List<PromotionModel>> GetActivePromotionsAsync(DateTime bookingDate);
    }
}
