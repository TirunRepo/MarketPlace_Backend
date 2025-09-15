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
    public class SailDateRepository : ISailDateRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SailDateRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<SailDateDTO> Insert(SailDateDTO sailDateDTO)
        {
            try
            {
                var sail = _mapper.Map<SailDate>(sailDateDTO);
                sail.CreatedOn = DateTime.UtcNow; // Set CreatedAt if not provided
                _context.SailDates.Add(sail);
                await _context.SaveChangesAsync();
                return _mapper.Map<SailDateDTO>(sail);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<SailDateDTO> Update(SailDateDTO sailDateDTO)
        {
            var sail = await _context.SailDates.FindAsync(sailDateDTO.SaleDateId);
            if (sail == null) throw new KeyNotFoundException("Sail not found");

            _mapper.Map(sailDateDTO, sail); // Map DTO to entity, preserving ID
            await _context.SaveChangesAsync();
            return _mapper.Map<SailDateDTO>(sail);
        }

        public async Task<SailDateDTO> GetById(int sailDateID)
        {
            var sail = await _context.SailDates
                .Include(r => r.SaleDateId)
                .FirstOrDefaultAsync(r => r.SaleDateId == sailDateID);
            return sail == null ? null : _mapper.Map<SailDateDTO>(sail);
        }

        public async Task<IEnumerable<SailDateDTO>> GetAll()
        {
            var sail = await _context.SailDates
                .Include(r => r.CruiseShip)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SailDateDTO>>(sail);
        }

        public async Task<bool> Delete(int sailDateID)
        {
            var sail = await _context.SailDates.FindAsync(sailDateID);
            if (sail == null) return false;

            _context.SailDates.Remove(sail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
