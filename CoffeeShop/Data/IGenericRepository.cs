using System.Linq.Expressions;

namespace CoffeeShop.Data
{
    public interface IGenericRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(string id);
        Task<string> AddAsync(T entity);
        Task UpdateAsync(string id, T entity);  
        Task DeleteAsync(string id);   

    }
}
