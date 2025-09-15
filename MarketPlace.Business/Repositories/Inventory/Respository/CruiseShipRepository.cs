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
    public class CruiseShipRepository : IcruiseShipRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseShipRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CruiseShipDto> Insert(CruiseShipDto cruiseShipDto)
        {
            var cruiseShip = _mapper.Map<CruiseShip>(cruiseShipDto);

            // Attach CruiseLine if needed (avoid tracking issues)
            if (cruiseShip.CruiseLine != null)
            {
                _context.Attach(cruiseShip.CruiseLine);
            }

            _context.CruiseShips.Add(cruiseShip);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruiseShipDto>(cruiseShip);
        }

        public async Task<CruiseShipDto> Update(CruiseShipDto cruiseShipDto)
        {
            try
            {
                var cruiseShip = await _context.CruiseShips
                        .Include(cs => cs.CruiseLine)
                        .FirstOrDefaultAsync(cs => cs.CruiseShipId == cruiseShipDto.CruiseShipId);

                if (cruiseShip == null)
                    throw new KeyNotFoundException("Cruise ship not found");

                _mapper.Map(cruiseShipDto, cruiseShip);
                await _context.SaveChangesAsync();
                return _mapper.Map<CruiseShipDto>(cruiseShip);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<CruiseShipDto> GetById(int id)
        {
            var cruiseShip = await _context.CruiseShips
                .Include(cs => cs.CruiseLine)
                .FirstOrDefaultAsync(cs => cs.CruiseShipId == id);

            return cruiseShip == null ? null : _mapper.Map<CruiseShipDto>(cruiseShip);
        }

        public async Task<IEnumerable<CruiseShipDto>> GetAll()
        {
            var cruiseShips = await _context.CruiseShips
                .Include(cs => cs.CruiseLine)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CruiseShipDto>>(cruiseShips);
        }

        public async Task<bool> Delete(int id)
        {
            var cruiseShip = await _context.CruiseShips.FindAsync(id);
            if (cruiseShip == null) return false;

            _context.CruiseShips.Remove(cruiseShip);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<CruiseShip>> GetByCruiseLineIdAsync(int cruiseLineId)
        {
            return await _context.CruiseShips
                .Where(s => s.CruiseLineId == cruiseLineId)
                .ToListAsync();
        }
    }
}
