using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities.Promotions;
using MarketPlace.DataAccess.Repositories.Promotions.Interface;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataAccess.Repositories.Promotions.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;
        public PromotionRepository(AppDbContext context) => _context = context;

        public async Task<PromotionModel> GetByIdAsync(int id) => await _context.Promotions.FindAsync(id);
        public async Task<List<PromotionModel>> GetAllAsync()
        {
            return await _context.Set<PromotionModel>()
                .Select(x => new PromotionModel()
                {
                    AffiliateName = x.AffiliateName,
                    CabinCountRequired = x.CabinCountRequired,
                    DiscountAmount = x.DiscountAmount,
                    DiscountPer = x.DiscountPer,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    FreeNthPassenger = x.FreeNthPassenger,
                    Id = x.Id,
                    IncludesAirfare = x.IncludesAirfare,
                    IncludesHotel = x.IncludesHotel,
                    IncludesShoreExcursion = x.IncludesShoreExcursion,
                    IncludesWiFi = x.IncludesWiFi,
                    IsActive = x.IsActive,
                    IsAdultTicketDiscount = x.IsAdultTicketDiscount,
                    IsChildTicketDiscount = x.IsChildTicketDiscount,
                    IsFirstTimeCustomer = x.IsFirstTimeCustomer,
                    IsStackable = x.IsStackable,
                    LoyaltyLevel = x.LoyaltyLevel,
                    MaxPassengerAge = x.MaxPassengerAge,
                    MinNoOfAdultRequired = x.MinNoOfAdultRequired,
                    MinNoOfChildRequired = x.MinNoOfChildRequired,
                    MinPassengerAge = x.MinPassengerAge,
                    OnboardCreditAmount = x.OnboardCreditAmount,
                    PassengerType = x.PassengerType,
                    PromoCode = x.PromoCode,
                    PromotionDescription = x.PromotionDescription,
                    PromotionName = x.PromotionName,
                    PromotionType = x.PromotionType,
                    PromotionTypeId = x.PromotionTypeId,
                    SailingId = x.SailingId,
                    SupplierId = x.SupplierId
                })
                .ToListAsync();
        }
        public async Task<PromotionModel> AddAsync(PromotionModel entity)
        {
            _context.Promotions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<PromotionModel> UpdateAsync(PromotionModel entity)
        {
            _context.Promotions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Promotions.FindAsync(id);
            if (entity != null)
            {
                _context.Promotions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<PromotionModel>> GetActivePromotionsAsync(DateTime bookingDate)
        {
            return await _context.Set<PromotionModel>()
                .Where(p => p.IsActive &&
                            p.StartDate.Date <= bookingDate.Date &&
                            p.EndDate.Date >= bookingDate.Date)
                .Select(x => new PromotionModel()
                {
                    AffiliateName = x.AffiliateName,
                    CabinCountRequired = x.CabinCountRequired,
                    DiscountAmount = x.DiscountAmount,
                    DiscountPer = x.DiscountPer,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    FreeNthPassenger = x.FreeNthPassenger,
                    Id = x.Id,
                    IncludesAirfare = x.IncludesAirfare,
                    IncludesHotel = x.IncludesHotel,
                    IncludesShoreExcursion = x.IncludesShoreExcursion,
                    IncludesWiFi = x.IncludesWiFi,
                    IsActive = x.IsActive,
                    IsAdultTicketDiscount = x.IsAdultTicketDiscount,
                    IsChildTicketDiscount = x.IsChildTicketDiscount,
                    IsFirstTimeCustomer = x.IsFirstTimeCustomer,
                    IsStackable = x.IsStackable,
                    LoyaltyLevel = x.LoyaltyLevel,
                    MaxPassengerAge = x.MaxPassengerAge,
                    MinNoOfAdultRequired = x.MinNoOfAdultRequired,
                    MinNoOfChildRequired = x.MinNoOfChildRequired,
                    MinPassengerAge = x.MinPassengerAge,
                    OnboardCreditAmount = x.OnboardCreditAmount,
                    PassengerType = x.PassengerType,
                    PromoCode = x.PromoCode,
                    PromotionDescription = x.PromotionDescription,
                    PromotionName = x.PromotionName,
                    PromotionType = x.PromotionType,
                    PromotionTypeId = x.PromotionTypeId,
                    SailingId = x.SailingId,
                    SupplierId = x.SupplierId
                })
                .ToListAsync();
        }
    }
}