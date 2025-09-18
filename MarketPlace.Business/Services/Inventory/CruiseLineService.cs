using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.Common.DTOs.ResponseModels.Inventory;
using MarketPlace.Common.PagedData;
using MarketPlace.DataAccess.Entities.Inventory;
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



        public async Task<CruiseLineRequest> Insert(CruiseLineRequest model)
        {
            return await _cruiselineRepository.Insert(model);
        }


        public async Task<CruiseLineRequest> Update(int Id, CruiseLineRequest model)
        {
            return await _cruiselineRepository.Update(Id,model);
        }

        public async Task<CruiseLineModal> GetById(int Id)
        {
            return await _cruiselineRepository.GetById(Id);
        }

        public async Task<PagedData<CruiseLineResponse>> GetList()
        {
            return await _cruiselineRepository.GetList();
        }

        public async Task<bool> Delete(int id)
        {
            return await _cruiselineRepository.Delete(id);
        }






    }
}
