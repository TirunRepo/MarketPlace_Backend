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
    public class SailDateService : ISailDateService
    {
        private readonly ISailDateRepository _sailDateRepository;
        public SailDateService(ISailDateRepository sailDateRepository)
        {
            _sailDateRepository = sailDateRepository ?? throw new ArgumentNullException(nameof(sailDateRepository));
        }

        public async Task<SailDateDTO> Insert(SailDateDTO sailDateDTO)
        {
            return await _sailDateRepository.Insert(sailDateDTO);
        }

        public async Task<SailDateDTO> Update(SailDateDTO sailDateDTO)
        {
            return await _sailDateRepository.Update(sailDateDTO);
        }

        public async Task<SailDateDTO> GetById(int sailDateID)
        {
            return await _sailDateRepository.GetById(sailDateID);
        }

        public async Task<IEnumerable<SailDateDTO>> GetAll()
        {
            return await _sailDateRepository.GetAll();
        }

        public async Task<bool> Delete(int sailDateID)
        {
            return await _sailDateRepository.Delete(sailDateID);
        }
    }
}
