using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Inventory
{

    public class CruiseShipServices : ICruiseShipService
    {

        private readonly IcruiseShipRepository _cruiseShipRepository;

        public CruiseShipServices(IcruiseShipRepository cruiseShipRepository)
        {
            _cruiseShipRepository = cruiseShipRepository ?? throw new ArgumentNullException(nameof(cruiseShipRepository));
        }

        public async Task<CruiseShipDto> Insert(CruiseShipDto cruiseShipDto)
        {
            return await _cruiseShipRepository.Insert(cruiseShipDto);
        }

        public async Task<CruiseShipDto> Update(CruiseShipDto cruiseShipDto)
        {
            return await _cruiseShipRepository.Update(cruiseShipDto);
        }

        public async Task<CruiseShipDto> GetById(int cruiseShipId)
        {
            return await _cruiseShipRepository.GetById(cruiseShipId);
        }

        public async Task<IEnumerable<CruiseShipDto>> GetAll()
        {
            return await _cruiseShipRepository.GetAll();
        }

        public async Task<bool> Delete(int cruiseShipId)
        {
            return await _cruiseShipRepository.Delete(cruiseShipId);
        }
        public async Task<IEnumerable<CruiseShip>> GetByCruiseLineIdAsync(int cruiseLineId)
        {
            return await _cruiseShipRepository.GetByCruiseLineIdAsync(cruiseLineId);

        }



    }
}
