using MarketPlace.Common.DTOs.RequestModels.Inventory;
using MarketPlace.DataAccess.Entities.Inventory;


namespace MarketPlace.DataAccess.Repositories.Inventory.Interface
{
    public interface ISailDateRepository
    {
        Task<SailDateDTO> Insert(SailDateDTO sailDateDTO);
        Task<SailDateDTO> Update(SailDateDTO sailDateDTO);
        Task<SailDateDTO> GetById(int sailDateID);
        Task<IEnumerable<SailDateDTO>> GetAll();
        Task<bool> Delete(int sailDateID);
    }
}
