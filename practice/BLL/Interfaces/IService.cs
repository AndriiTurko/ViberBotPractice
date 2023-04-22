namespace practice.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<bool> CreateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> UpdateAsync(T item);
    }
}
