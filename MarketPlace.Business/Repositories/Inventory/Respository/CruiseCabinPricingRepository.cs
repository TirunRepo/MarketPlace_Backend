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
    public class CruiseCabinPricingRepository : ICruisePricingCabinRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseCabinPricingRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> InsertCabinsAsync(CruiseCabinPricingModel cabinDtos)
        {
           var cabins = _mapper.Map<CruisePricingInventory>(cabinDtos);
           await _context.CruiseCabinPricingInventories.AddAsync(cabins);

           if(await _context.SaveChangesAsync() != 0)
           {
               return true;
           }
           return false;
        }
    }
}
