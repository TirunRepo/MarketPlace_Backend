using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Business.Services.Inventory
{
    public class CruiseDestinationService : IDestinationService
    {
        private readonly ICruiseDestinationRepository _destinationRepository;

        public CruiseDestinationService(ICruiseDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository ?? throw new ArgumentNullException(nameof(destinationRepository));
        }

        public async Task<IEnumerable<DestinationDto>> GetAll()
        {
            return await _destinationRepository.GetAll();
        }

        public async Task<DestinationDto> GetByCode(string code)
        {
            return await _destinationRepository.GetByCode(code);
        }

        public async Task<DestinationDto> Insert(DestinationDto destinationDto)
        {
            return await _destinationRepository.Insert(destinationDto);
        }

        public async Task<DestinationDto> Update(DestinationDto destinationDto)
        {
            return await _destinationRepository.Update(destinationDto);
        }

        public async Task<bool> Delete(string code)
        {
            return await _destinationRepository.Delete(code);
        }
    }
}
