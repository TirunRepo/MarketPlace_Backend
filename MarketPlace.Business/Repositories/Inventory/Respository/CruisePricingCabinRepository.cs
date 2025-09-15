using AutoMapper;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.Types.Inventory;
using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Inventory.Respository
{
    public class CruisePricingCabinRepository : ICruisePricingCabinRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruisePricingCabinRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CruisePricingCabinDto>> GetAll(DateTime sailDate, int cruiseShipId, string groupId)
        {
            return await _context.Cabins
                .Where(x => x.CruisePricingInventory.CruiseInventory.SailDate == sailDate
                && x.CruisePricingInventory.CruiseInventory.CruiseShipId == cruiseShipId
                && x.CruisePricingInventory.CruiseInventory.GroupId == groupId)
                .Select(x => new CruisePricingCabinDto()
                {
                    CabinNo = x.CabinNo,
                    Status = x.Status,
                    CruisePricingInventoryId = x.CruisePricingInventoryId
                }).ToListAsync();
        }

        public async Task<List<CruiseCabinDto>> GetCruiseCabinAsyn()
        {
            return await _context.Cabins
                .Select(x => new CruiseCabinDto()
                {
                    Id = x.CruisePricingCabinId,
                    CruisePricingInventoryId = x.CruisePricingInventory.CruisePricingInventoryId,
                    CruiseInventoryId = x.CruisePricingInventory.CruiseInventoryId,
                    Date = x.CruisePricingInventory.CruiseInventory.SailDate,
                    CruiseShipId = x.CruisePricingInventory.CruiseInventory.CruiseShipId,
                    GroupId = x.CruisePricingInventory.CruiseInventory.GroupId,
                    CabinOccupancy = ((CabinOccupancyEnum)int.Parse(x.CruisePricingInventory.CabinOccupancy)).ToString(),
                    CabinNumber = x.CabinNo,
                    CategoryId = x.CruisePricingInventory.Category,
                    CabinStatus = x.Status,
                    Stateroom = ((CabinCategoryEnum)int.Parse(x.CruisePricingInventory.CabinCategory)).ToString()
                }).ToListAsync();
        }

        public async Task UpdateCabinAsync(int id, string cabinNumber, string categoryId)
        {
            var cabin = await _context.Cabins.FindAsync(id);
            cabin.CabinNo = cabinNumber;

            var cruisePricingInventory = await _context.CruisePricingInventories.FindAsync(cabin.CruisePricingInventoryId);
            cruisePricingInventory.Category = categoryId;

            await _context.SaveChangesAsync();
        }

        public async Task InsertCabinsAsync(List<CruisePricingCabinDto> cabinDtos)
        {
            var cabins = _mapper.Map<List<CruisePricingCabin>>(cabinDtos);
            _context.Cabins.AddRangeAsync(cabins);
            await _context.SaveChangesAsync();
        }
    }
}
