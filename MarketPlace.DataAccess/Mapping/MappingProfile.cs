using AutoMapper;
using MarketPlace.Common.DTOs;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Markup;
using MarketPlace.Common.DTOs.RequestModels.Promotions;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Markup;
using MarketPlace.Common.DTOs.ResponseModels.Promotions;
using MarketPlace.DataAccess.Entities;
using MarketPlace.DataAccess.Entities.Inventory;
using MarketPlace.DataAccess.Entities.Markup;
using MarketPlace.DataAccess.Entities.Promotions;

namespace MarketPlace.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginResponse>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));

            CreateMap<User, AuthDto>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));
            CreateMap<RegisterUser, User>().ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<PromotionModel, PromotionRequest>().ReverseMap();
            CreateMap<PromotionModel, PromotionResponse>().ReverseMap();

            CreateMap<PromotionCalculationRequest, PromotionModel>().ReverseMap();
            CreateMap<PromotionModel, PromotionCalculationResponse>();
            CreateMap<MarkupDetail, MarkupRequest>().ReverseMap();
            CreateMap<MarkupDetail, MarkupResponse>().ReverseMap();
            // ========================
            // Cruiseline ↔ CruiselineDto
            // ========================
            CreateMap<CruiseLine, CruiseLineResponse>().ReverseMap();
            CreateMap<CruiseLine, CruiseLineRequest>().ReverseMap();

            CreateMap<CruiseShip, CruiseLineRequest>().ReverseMap();
            CreateMap<CruiseShip, CruiseLineResponse>().ReverseMap();
            //
            CreateMap<Destination, DestinationRequest>().ReverseMap();
            CreateMap<Destination, DestinationResponse>().ReverseMap();

            CreateMap<DeparturePort, DeparturePortRequest>().ReverseMap();
            CreateMap<DeparturePort, CruiseDeparturePortResponse>().ReverseMap();
            CreateMap<CruiseInventory, CruiseInventoryModel>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CruiseInventoryModel, CruiseInventory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CruiseCabinPricingModel, CruiseCabinPricingInventory>();
            CreateMap<CruiseCabinPricingInventory, CruiseCabinPricingModel>();
        }
    }
}
