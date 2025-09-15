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
    public class CruiseLineService : ICruiseLineService
    {
        private readonly ICruiseLineRepository _cruiselineRepository;

        public CruiseLineService(ICruiseLineRepository cruiselineRepository)
        {
            _cruiselineRepository = cruiselineRepository ?? throw new ArgumentNullException(nameof(cruiselineRepository));
        }



        public async Task<CruiseLineDto> Insert(CruiseLineDto cruiselineDto)
        {
            return await _cruiselineRepository.Insert(cruiselineDto);
        }


        public async Task<CruiseLineDto> Update(CruiseLineDto cruiselineDto)
        {
            return await _cruiselineRepository.Update(cruiselineDto);
        }

        public async Task<CruiseLineDto> GetById(int CruiseLineId)
        {
            return await _cruiselineRepository.GetById(CruiseLineId);
        }

        public async Task<IEnumerable<CruiseLineDto>> GetAll()
        {
            return await _cruiselineRepository.GetAll();
        }

        public async Task<bool> Delete(int CruiseLineId)
        {
            return await _cruiselineRepository.Delete(CruiseLineId);
        }






    }
}
