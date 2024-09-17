using CORE.Entity;

namespace CORE.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entidade);
        Task UpdateAsync(T entidade);
        Task DeleteAsync(int id);
    }
}
