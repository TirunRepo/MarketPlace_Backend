using AutoMapper;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataAccess.Repositories.Inventory.Respository
{
    public class CruiseDeparturePortRepository : ICruiseDeparturePortRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseDeparturePortRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DeparturePortRequest> Insert(DeparturePortRequest model)
        {
            var entity = _mapper.Map<DeparturePort>(model);


            _context.DeparturePorts.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DeparturePortRequest>(entity);
        }

        public async Task<DeparturePortRequest> Update(int Id, DeparturePortRequest model)
        {
            var existing = await _context.DeparturePorts
                .Include(dp => dp.Id)
                .FirstOrDefaultAsync(dp => dp.Id == Id);

            if (existing == null)
                throw new KeyNotFoundException("Departure Port not found");

            _mapper.Map(model, existing);

            await _context.SaveChangesAsync();

            return _mapper.Map<DeparturePortRequest>(existing);
        }


        public async Task<bool> Delete(int id)
        {
            var departurePort = await _context.DeparturePorts.FindAsync(id);
            if (departurePort == null) return false;

            _context.DeparturePorts.Remove(departurePort);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedData<CruiseDeparturePortResponse>> GetList()
        {

            var departurePort = await _context.DeparturePorts.ToListAsync();

            return _mapper.Map<PagedData<CruiseDeparturePortResponse>>(departurePort);
        }

        public async Task<CruiseDeparturePortResponse> GetById(int id)
        {
            var port = await _context.DeparturePorts
                .Include(dp => dp.DestinationId)
                .FirstOrDefaultAsync(dp => dp.Id == id);

            return port == null ? null : _mapper.Map<CruiseDeparturePortResponse>(port);
        }

    }
}
