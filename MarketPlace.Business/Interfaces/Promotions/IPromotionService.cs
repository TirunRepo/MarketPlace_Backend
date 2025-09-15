using MarketPlace.Common.DTOs.RequestModels.Promotions;
using MarketPlace.Common.DTOs.ResponseModels.Promotions;

namespace MarketPlace.Business.Interfaces.Promotions
{
    public interface IPromotionService
    {
        Task<PromotionResponse> CreateAsync(PromotionRequest promotion);
        Task<PromotionResponse> UpdateAsync(PromotionRequest promotion);
        Task DeleteAsync(int id);
        Task<PromotionResponse?> GetByIdAsync(int id);
        Task<List<PromotionResponse>> GetAllAsync();
        Task<List<PromotionCalculationResponse>> CalculateDiscountAsync(PromotionCalculationRequest request);
    }
}
