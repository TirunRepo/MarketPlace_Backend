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
    public class CruiseDestinationRepository : ICruiseDestinationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseDestinationRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DestinationDto> Insert(DestinationDto destinationDto)
        {
            var destination = _mapper.Map<Destination>(destinationDto);


            destination.CreatedOn = DateTime.UtcNow;
            destination.CreatedBy = 1; // 🔹 Replace with actual user id
            destination.ModifiedBy = 1;
            destination.LastModifiedOn = DateTime.UtcNow;

            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return _mapper.Map<DestinationDto>(destination);
        }

        public async Task<DestinationDto> Update(DestinationDto destinationDto)
        {
            var destination = await _context.Destinations.FindAsync(destinationDto.DestinationCode);
            if (destination == null)
                throw new KeyNotFoundException("Destination not found");

            _mapper.Map(destinationDto, destination);

            destination.ModifiedBy = 1; // 🔹 Replace with actual user id
            destination.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<DestinationDto>(destination);
        }

        public async Task<bool> Delete(string destinationCode)
        {
            try
            {
                var destination = await _context.Destinations.FindAsync(destinationCode);
                if (destination == null) return false;

                _context.Destinations.Remove(destination);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<DestinationDto>> GetAll()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return _mapper.Map<IEnumerable<DestinationDto>>(destinations);
        }

        public async Task<DestinationDto> GetByCode(string destinationCode)
        {
            var destination = await _context.Destinations
                .FirstOrDefaultAsync(d => d.DestinationCode == destinationCode);

            return destination == null ? null : _mapper.Map<DestinationDto>(destination);
        }
    }
}
