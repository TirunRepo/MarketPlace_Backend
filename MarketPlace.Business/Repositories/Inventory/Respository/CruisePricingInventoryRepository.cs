using AutoMapper;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
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
    public class CruisePricingInventoryRepository : ICruisePricingInventoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruisePricingInventoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CruisePricingInventoryDto>> GetAll()
        {
            var pricingList = await _context.CruisePricingInventories
                .Include(p => p.CruiseInventory)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CruisePricingInventoryDto>>(pricingList);
        }

        public async Task<CruisePricingInventoryDto> GetById(int id)
        {
            var pricing = await _context.CruisePricingInventories
                .Include(p => p.CruiseInventory)
                .FirstOrDefaultAsync(p => p.CruisePricingInventoryId == id);

            return pricing == null ? null : _mapper.Map<CruisePricingInventoryDto>(pricing);
        }

        public async Task<bool> Insert(CruisePricingInventoryDto dto)
        {

            // Create a fresh CruiseInventory instance and map only scalar fields
            var cruisePriceInventory = _mapper.Map<CruisePricingInventory>(dto);

            // Manually assign foreign keys only (not full objects)
            //cruisePriceInventory.CruiseInventoryId = dto.CruiseInventoryId;


            // Save
            _context.CruisePricingInventories.Add(cruisePriceInventory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CruisePricingInventoryDto> Update(CruisePricingInventoryDto dto)
        {
            var existing = await _context.CruisePricingInventories.FindAsync(dto.CrusiePricingInventoryId);
            if (existing == null) throw new KeyNotFoundException("Pricing record not found");

            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruisePricingInventoryDto>(existing);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.CruisePricingInventories.FindAsync(id);
            if (entity == null) return false;

            _context.CruisePricingInventories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<IEnumerable<CruisePricingInventoryDto>> GetByInventoryId(int inventoryId)
        {
            var records = await _context.CruisePricingInventories
                .Where(p => p.CruiseInventoryId == inventoryId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CruisePricingInventoryDto>>(records);
        }



    }
}
