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

        public async Task<IEnumerable<CruiseInventoryDto>> GetAll()
        {
            var inventories = await _context.CruiseInventories
                .Include(c => c.CruiseShip)
                    .ThenInclude(c => c.CruiseLine)
                .Include(c => c.Destination)
                .Include(c => c.DeparturePort)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CruiseInventoryDto>>(inventories);
        }

        public async Task<CruiseInventoryDto> GetById(int id)
        {
            var inventory = await _context.CruiseInventories
                .Include(c => c.CruiseShip)
                     .ThenInclude(c => c.CruiseLine)
                .Include(c => c.Destination)
                .Include(c => c.DeparturePort)
                .FirstOrDefaultAsync(ci => ci.CruiseInventoryId == id);

            return inventory == null ? null : _mapper.Map<CruiseInventoryDto>(inventory);
        }

        //public async Task<CruiseInventoryDto> Insert(CruiseInventoryDto cruiseInventoryDto)
        //{
        //    //var CruiseShip = _mapper.Map<CruiseShip>(cruiseInventoryDto.CruiseShip);
        //    //var DeparturePort = _mapper.Map<DeparturePort>(cruiseInventoryDto.DeparturePort);
        //    //var Destination = _mapper.Map<Destination>(cruiseInventoryDto.Destination);
        //    //var cruiseInventory = _mapper.Map<CruiseInventory>(cruiseInventoryDto);

        //    //// --- CruiseLine ---
        //    //var cruiseLine = _mapper.Map<CruiseLine>(cruiseInventoryDto.CruiseShip.CruiseLine);
        //    //if (cruiseInventoryDto.CruiseShip.CruiseLine.CruiseLineId != null)
        //    //{
        //    //    if (!_context.ChangeTracker.Entries<CruiseLine>().Any(e => e.Entity.CruiseLineId == cruiseLine.CruiseLineId))
        //    //    {
        //    //        _context.Entry(cruiseLine).State = EntityState.Unchanged;
        //    //    }
        //    //}
        //    var cruiseShip = _mapper.Map<CruiseShip>(cruiseInventoryDto.CruiseShip);
        //    // --- CruiseShip ---
        //    if (cruiseInventoryDto.CruiseShip.CruiseShipId != null)
        //    {
        //        if (!_context.ChangeTracker.Entries<CruiseShip>().Any(e => e.Entity.CruiseShipId == cruiseShip.CruiseShipId))
        //        {
        //            _context.Entry(cruiseShip).State = EntityState.Unchanged;
        //        }
        //    }
        //    var destination = _mapper.Map<Destination>(cruiseInventoryDto.Destination);
        //    // --- Destination ---
        //    if (cruiseInventoryDto.Destination != null)
        //    {
        //        if (!_context.ChangeTracker.Entries<Destination>().Any(e => e.Entity.DestinationCode == destination.DestinationCode))
        //        {
        //            _context.Entry(destination).State = EntityState.Unchanged;
        //        }
        //    }
        //    var departurePort = _mapper.Map<DeparturePort>(cruiseInventoryDto.DeparturePort);
        //    // --- DeparturePort ---
        //    if (cruiseInventoryDto.DeparturePort.DeparturePortId != null)
        //    {
        //        if (!_context.ChangeTracker.Entries<DeparturePort>().Any(e => e.Entity.DeparturePortId == departurePort.DeparturePortId))
        //        {
        //            _context.Entry(departurePort).State = EntityState.Unchanged;
        //        }
        //    }
        //    var cruiseInventory = _mapper.Map<CruiseInventory>(cruiseInventoryDto);
        //    cruiseInventory.CruiseShipId = cruiseShip.CruiseShipId;
        //    cruiseInventory.DestinationCode = destination.DestinationCode;
        //    cruiseInventory.DeparturePortId = departurePort.DeparturePortId;

        //    // --- Audit Fields ---
        //    cruiseInventory.CreatedOn = DateTime.UtcNow;
        //    cruiseInventory.LastModifiedOn = DateTime.UtcNow;
        //    cruiseInventory.CreatedBy = cruiseInventoryDto.CreatedBy;
        //    cruiseInventory.ModifiedBy = cruiseInventoryDto.LastModifiedBy;
        //    var local = _context.Set<CruiseShip>()
        //                .Local
        //                .FirstOrDefault(entry => entry.CruiseShipId == cruiseShip.CruiseShipId);

        //    if (local != null)
        //    {
        //        _context.Entry(local).State = EntityState.Detached;
        //    }

        //    _context.Attach(cruiseShip);
        //    // --- Save ---
        //    _context.CruiseInventory.Add(cruiseInventory);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<CruiseInventoryDto>(cruiseInventory);
        //}

        public async Task<CruiseInventoryDto> Insert(CruiseInventoryDto cruiseInventoryDto)
        {
            // Create a fresh CruiseInventory instance and map only scalar fields
            var cruiseInventory = _mapper.Map<CruiseInventory>(cruiseInventoryDto);

            // Manually assign foreign keys only (not full objects)
            cruiseInventory.CruiseShipId = cruiseInventoryDto.CruiseShip.CruiseShipId ?? 1;
            cruiseInventory.DeparturePortId = cruiseInventoryDto.DeparturePort?.DeparturePortId ?? 1;
            cruiseInventory.DestinationCode = cruiseInventoryDto.Destination?.DestinationCode;

            // Prevent entity tracking issues by not attaching full objects at all
            cruiseInventory.CruiseShip = null;
            cruiseInventory.DeparturePort = null;
            cruiseInventory.Destination = null;

            // Audit fields
            cruiseInventory.CreatedOn = DateTime.UtcNow;
            cruiseInventory.LastModifiedOn = DateTime.UtcNow;
            cruiseInventory.CreatedBy = cruiseInventoryDto.CreatedBy;
            cruiseInventory.ModifiedBy = cruiseInventoryDto.ModifiedBy;

            // Save
            _context.CruiseInventories.Add(cruiseInventory);
            await _context.SaveChangesAsync();

            return _mapper.Map<CruiseInventoryDto>(cruiseInventory);
        }


        public async Task<CruiseInventoryDto> Update(CruiseInventoryDto cruiseInventoryDto)
        {
            var existing = await _context.CruiseInventories.FindAsync(cruiseInventoryDto.CruiseInventoryId);
            if (existing == null) throw new KeyNotFoundException("Cruise Inventory not found");

            _mapper.Map(cruiseInventoryDto, existing);
            await _context.SaveChangesAsync();
            return _mapper.Map<CruiseInventoryDto>(existing);
        }

        public async Task<bool> Delete(int id)
        {
            var inventory = await _context.CruiseInventories.FindAsync(id);
            if (inventory == null) return false;

            _context.CruiseInventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
