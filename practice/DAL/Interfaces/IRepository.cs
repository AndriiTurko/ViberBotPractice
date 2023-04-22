using Microsoft.EntityFrameworkCore;

namespace practice.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T item);
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int? id);
        DbSet<T> GetAllRaw();
        Task<bool> UpdateAsync(T item);
    }
}