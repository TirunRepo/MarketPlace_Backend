using AutoMapper;
using MarketPlace.Common.DTOs;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Markup;
using MarketPlace.Common.DTOs.RequestModels.Promotions;
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
            CreateMap<CruiseLine, CruiseLineDto>();
            CreateMap<CruiseLineDto, CruiseLine>();

            CreateMap<CruiseShip, CruiseShipDto>();
            CreateMap<CruiseShipDto, CruiseShip>();
            //
            CreateMap<Destination, DestinationDto>();
            CreateMap<DestinationDto, Destination>();

            CreateMap<DeparturePort, DeparturePortDto>();
            CreateMap<DeparturePortDto, DeparturePort>();
            CreateMap<CruiseInventory, CruiseInventoryDto>()
                 .ForMember(dest => dest.CruiseInventoryId, opt => opt.MapFrom(src => src.CruiseInventoryId));
            CreateMap<string, DestinationDto>()
            .ForMember(dest => dest.DestinationCode, opt => opt.MapFrom(src => src));

            CreateMap<CruiseInventoryDto, CruiseInventory>()
                .ForMember(dest => dest.CruiseInventoryId, opt => opt.MapFrom(src => src.CruiseInventoryId));

            CreateMap<CruiseShipDto, CruiseShip>()
            .ForMember(dest => dest.CruiseLine, opt => opt.Ignore());
            // Entity to DTO (convert child entities to string list)
            CreateMap<CruisePricingInventory, CruisePricingInventoryDto>()
                .ForMember(dest => dest.CabinNoType, opt => opt.MapFrom(src => src.Cabins.Select(c => c.CabinNo).ToList()));

            //// DTO to Entity (convert string list to child entities)
            //CreateMap<CruisePricingInventoryDto, CruisePricingInventory>()
            //    .ForMember(dest => dest.Cabins, opt => opt.MapFrom(src =>
            //        src.CabinNoList != null
            //            ? src.CabinNoList
            //                .Where(c => !string.IsNullOrWhiteSpace(c))
            //                .Select(c => new CruisePricingCabin { CabinNo = c.Trim() })
            //                .ToList()
            //            : new List<CruisePricingCabin>()));

            CreateMap<CruisePricingCabinDto, CruisePricingCabin>();
            CreateMap<CruisePricingCabin, CruisePricingCabinDto>();
            CreateMap<CruisePricingInventoryDto, CruisePricingInventory>();
        }
    }
}
