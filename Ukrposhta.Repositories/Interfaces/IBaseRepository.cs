using Ukrposhta.Entities.Interfaces;

namespace Ukrposhta.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task<int> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
