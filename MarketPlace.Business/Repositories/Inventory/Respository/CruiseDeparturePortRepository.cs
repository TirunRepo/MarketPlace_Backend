using AutoMapper;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
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

        public async Task<DeparturePortDto> Insert(DeparturePortDto departurePortDto)
        {
            var entity = _mapper.Map<DeparturePort>(departurePortDto);

            // Audit fields (replace with actual user ID handling as needed)
            entity.CreatedOn = DateTime.UtcNow;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.CreatedBy = 1;
            entity.ModifiedBy = 1;

            _context.DeparturePorts.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DeparturePortDto>(entity);
        }

        public async Task<DeparturePortDto> Update(DeparturePortDto departurePortDto)
        {
            var existing = await _context.DeparturePorts
                .Include(dp => dp.Destination)
                .FirstOrDefaultAsync(dp => dp.DeparturePortId == departurePortDto.DeparturePortId);

            if (existing == null)
                throw new KeyNotFoundException("Departure Port not found");

            _mapper.Map(departurePortDto, existing);
            existing.LastModifiedOn = DateTime.UtcNow;
            existing.ModifiedBy = 1;

            await _context.SaveChangesAsync();

            return _mapper.Map<DeparturePortDto>(existing);
        }


        public async Task<bool> Delete(int id)
        {
            var departurePort = await _context.DeparturePorts.FindAsync(id);
            if (departurePort == null) return false;

            _context.DeparturePorts.Remove(departurePort);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DeparturePortDto>> GetAll()
        {
            var ports = await _context.DeparturePorts
                .Include(dp => dp.Destination).Select(x => new DeparturePortDto
                {
                    DeparturePortId = x.DeparturePortId,
                    DeparturePortCode = x.DeparturePortCode,
                    DestinationCode = x.DestinationCode,
                    DeparturePortName = x.DeparturePortName,
                    DestinationCodeObj = new DestinationDto
                    {
                        DestinationCode = x.DestinationCode,
                        DestinationName = x.Destination.DestinationName
                    },
                    CreatedAt = x.CreatedOn,
                    CreatedBy = x.CreatedBy
                })
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeparturePortDto>>(ports);
        }

        public async Task<DeparturePortDto> GetById(int id)
        {
            var port = await _context.DeparturePorts
                .Include(dp => dp.Destination)
                .FirstOrDefaultAsync(dp => dp.DeparturePortId == id);

            return port == null ? null : _mapper.Map<DeparturePortDto>(port);
        }

        public async Task<IEnumerable<DeparturePort>> GetByDestinationCodeAsync(string destinationCode)
        {
            return await _context.DeparturePorts
                                 .Where(p => p.DestinationCode == destinationCode)
                                 .ToListAsync();
        }
    }
}
