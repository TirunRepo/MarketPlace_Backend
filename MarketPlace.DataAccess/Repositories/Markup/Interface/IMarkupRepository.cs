using MarketPlace.DataAccess.Entities.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataAccess.Repositories.Markup.Interface
{
    public interface IMarkupRepository
    {
        Task<MarkupDetail> AddAsync(MarkupDetail entity);
        Task<MarkupDetail> UpdateAsync(MarkupDetail entity);
        Task DeleteAsync(int id);
        Task<List<MarkupDetail>> GetMarkupDetails();
        Task<MarkupDetail> GetByIdAsync(int id);
    }
}
