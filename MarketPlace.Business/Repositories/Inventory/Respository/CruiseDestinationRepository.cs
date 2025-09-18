using AutoMapper;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
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
    public class CruiseDestinationRepository : ICruiseDestinationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseDestinationRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DestinationRequest> Insert(DestinationRequest model)
        {
            var entity = _mapper.Map<Destination>(model);


            _context.Destinations.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DestinationRequest>(entity);
        }

        public async Task<DestinationRequest> Update(int Id, DestinationRequest model)
        {
            var existing = await _context.Destinations
                .Include(dp => dp.Id)
                .FirstOrDefaultAsync(dp => dp.Id == Id);

            if (existing == null)
                throw new KeyNotFoundException("Departure Port not found");

            _mapper.Map(model, existing);

            await _context.SaveChangesAsync();

            return _mapper.Map<DestinationRequest>(existing);
        }


        public async Task<bool> Delete(int id)
        {
            var departurePort = await _context.Destinations.FindAsync(id);
            if (departurePort == null) return false;

            _context.Destinations.Remove(departurePort);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedData<DestinationResponse>> GetList()
        {

            var departurePort = await _context.Destinations.ToListAsync();

            return _mapper.Map<PagedData<DestinationResponse>>(departurePort);
        }

        public async Task<DestinationResponse> GetById(int id)
        {
            var port = await _context.Destinations
                .Include(dp => dp.Id)
                .FirstOrDefaultAsync(dp => dp.Id == id);

            return port == null ? null : _mapper.Map<DestinationResponse>(port);
        }
    }
}
