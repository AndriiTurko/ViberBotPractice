using Microsoft.EntityFrameworkCore;
using practice.DAL.Infrastructure;
using practice.DAL.Interfaces;
using practice.DAL.Models;

namespace practice.DAL.Repositories
{
    public class TrackLocationRepository : IRepository<TrackLocation>
    {
        private readonly TrackLocationContext _context;

        public TrackLocationRepository(TrackLocationContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TrackLocation trackLocation)
        {
            var createdItem = await _context.TrackLocations.AddAsync(trackLocation);

            return createdItem != null;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            var existsItem = await _context.TrackLocations.FirstOrDefaultAsync(trackLocation => trackLocation.Id == id);

            if (existsItem != null)
                _context.TrackLocations.Remove(existsItem);

            return existsItem != null;
        }

        public async Task<IEnumerable<TrackLocation>> GetAllAsync()
        {
            return await _context.TrackLocations.ToListAsync();
        }

        public async Task<TrackLocation?> GetByIdAsync(int? id)
        {
            return await _context.TrackLocations.FirstOrDefaultAsync(item => item.Id == id);
        }

        public DbSet<TrackLocation> GetAllRaw()
        {
            return _context.TrackLocations;
        }

        public async Task<bool> UpdateAsync(TrackLocation item)
        {
            var existsItem = await _context.TrackLocations.FirstOrDefaultAsync(i => i.Id == item.Id);

            var update = existsItem != null;

            if (update)
                _context.TrackLocations.Update(item);

            return update;
        }
    }
}

