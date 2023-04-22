using practice.DAL.Infrastructure;
using practice.DAL.Interfaces;
using practice.DAL.Models;
using practice.DAL.Repositories;

namespace practice.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrackLocationContext _context;

        bool disposed;

        private TrackLocationRepository? _trackLocationRepository;
        private WalkRepository? _walkRepository;

        public UnitOfWork(TrackLocationContext context)
        {
            _context = context;
            disposed = false;
        }

        public IRepository<TrackLocation> TrackLocations
        {
            get
            {
                _trackLocationRepository ??= new TrackLocationRepository(_context);

                return _trackLocationRepository;
            }
        }

        public IRepository<Walk> Walks
        {
            get
            {
                _walkRepository ??= new WalkRepository(_context);

                return _walkRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
