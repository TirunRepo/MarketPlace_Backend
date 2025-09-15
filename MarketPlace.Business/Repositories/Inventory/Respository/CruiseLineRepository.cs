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
    public class CruiseLineRepository : ICruiseLineRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CruiseLineRepository(AppDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CruiseLineDto> Insert(CruiseLineDto cruiseLineDto)
        {
            var entity = _mapper.Map<CruiseLine>(cruiseLineDto);
            _context.CruiseLines.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruiseLineDto>(entity);
        }

        public async Task<CruiseLineDto> Update(CruiseLineDto cruiseLineDto)
        {
            var entity = await _context.CruiseLines.FindAsync(cruiseLineDto.CruiseLineId);
            if (entity == null) throw new KeyNotFoundException("Cruise line not found");

            _mapper.Map(cruiseLineDto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruiseLineDto>(entity);
        }

        public async Task<CruiseLineDto> GetById(int cruiseLineId)
        {
            var entity = await _context.CruiseLines.FindAsync(cruiseLineId);
            return entity == null ? null : _mapper.Map<CruiseLineDto>(entity);
        }

        public async Task<IEnumerable<CruiseLineDto>> GetAll()
        {
            var cruiseLines = await _context.CruiseLines.ToListAsync();
            return _mapper.Map<IEnumerable<CruiseLineDto>>(cruiseLines);
        }

        public async Task<bool> Delete(int cruiseLineId)
        {
            try
            {
                var entity = await _context.CruiseLines.FindAsync(cruiseLineId);
                if (entity == null) return false;

                _context.CruiseLines.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



    }
}
