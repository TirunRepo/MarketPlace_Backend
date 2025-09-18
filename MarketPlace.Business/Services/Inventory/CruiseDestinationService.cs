using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
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
        public async Task<PagedData<DestinationResponse>> GetList()
        {
            return await _destinationRepository.GetList();
        }

        public async Task<DestinationResponse> GetById(int id)
        {
            return await _destinationRepository.GetById(id);
        }

        public async Task<DestinationRequest> Insert(DestinationRequest model)
        {
            return await _destinationRepository.Insert(model);
        }

        public async Task<DestinationRequest> Update(int Id, DestinationRequest model)
        {
            return await _destinationRepository.Update(Id, model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _destinationRepository.Delete(id);
        }
    }
}
