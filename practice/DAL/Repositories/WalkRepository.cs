using Microsoft.EntityFrameworkCore;
using practice.DAL.Infrastructure;
using practice.DAL.Interfaces;
using practice.DAL.Models;

namespace practice.DAL.Repositories
{
    public class WalkRepository : IRepository<Walk>
    {
        private readonly TrackLocationContext _context;

        public WalkRepository(TrackLocationContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Walk item)
        {
            var createdItem = await _context.Walks.AddAsync(item);

            return createdItem != null;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            var existsItem = await _context.Walks.FirstOrDefaultAsync(item => item.Id == id);

            if (existsItem != null)
                _context.Walks.Remove(existsItem);

            return existsItem != null;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _context.Walks.ToListAsync();
        }

        public DbSet<Walk> GetAllRaw()
        {
            return _context.Walks;
        }

        public async Task<Walk?> GetByIdAsync(int? id)
        {
            return await _context.Walks.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<bool> UpdateAsync(Walk item)
        {
            var existsItem = await _context.Walks.FirstOrDefaultAsync(i => i.Id == item.Id);

            var update = existsItem != null;

            if (update)
                _context.Walks.Update(item);

            return update;
        }
    }
}
