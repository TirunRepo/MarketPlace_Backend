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
    public class CruiseDeparturePortService : IDeparturePortService
    {
        private readonly ICruiseDeparturePortRepository _departurePortRepository;

        public CruiseDeparturePortService(ICruiseDeparturePortRepository departurePortRepository)
        {
            _departurePortRepository = departurePortRepository ?? throw new ArgumentNullException(nameof(departurePortRepository));
        }

        public async Task<IEnumerable<DeparturePortDto>> GetAll()
        {
            return await _departurePortRepository.GetAll();
        }

        public async Task<DeparturePortDto> GetById(int id)
        {
            return await _departurePortRepository.GetById(id);
        }

        public async Task<DeparturePortDto> Insert(DeparturePortDto portDto)
        {
            return await _departurePortRepository.Insert(portDto);
        }

        public async Task<DeparturePortDto> Update(DeparturePortDto portDto)
        {
            return await _departurePortRepository.Update(portDto);
        }

        public async Task<bool> Delete(int id)
        {
            return await _departurePortRepository.Delete(id);
        }



        public async Task<IEnumerable<DeparturePort>> GetByDestinationCodeAsync(string destinationCode)
        {

            return await _departurePortRepository.GetByDestinationCodeAsync(destinationCode);
        }

    }
}
