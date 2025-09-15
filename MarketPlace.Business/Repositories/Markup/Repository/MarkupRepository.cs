using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Entities.Markup;
using MarketPlace.DataAccess.Repositories.Markup.Interface;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataAccess.Repositories.Markup.Repository
{
    public class MarkupRepository : IMarkupRepository
    {
        private readonly AppDbContext _context;
        public MarkupRepository(AppDbContext context) => _context = context;
        public async Task<MarkupDetail> AddAsync(MarkupDetail entity)
        {
            _context.MarkupDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MarkupDetails.FindAsync(id);
            if (entity != null)
            {
                _context.MarkupDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MarkupDetail>> GetMarkupDetails()
        {
            return await _context.MarkupDetails.ToListAsync();
        }

        public async Task<MarkupDetail> UpdateAsync(MarkupDetail entity)
        {
            _context.MarkupDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MarkupDetail> GetByIdAsync(int id) => await _context.MarkupDetails.FindAsync(id);
    }
}
