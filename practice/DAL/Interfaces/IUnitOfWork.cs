using practice.DAL.Models;

namespace practice.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TrackLocation> TrackLocations { get; }

        IRepository<Walk> Walks { get; }

        Task<int> SaveChangesAsync();

    }
}
