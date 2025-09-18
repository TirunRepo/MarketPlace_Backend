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
    public class CruiseInventoryRepository : ICruiseInventoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseInventoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<CruiseInventoryModel> Insert(CruiseInventoryModel model)
        {
            // Create a fresh CruiseInventory instance and map only scalar fields
            var cruiseInventory = _mapper.Map<CruiseInventory>(model);  
            //Save
            _context.CruiseInventories.Add(cruiseInventory);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruiseInventoryModel>(cruiseInventory);
        }
    }
}
