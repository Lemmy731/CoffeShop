using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CoffeeShop.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntityBase, new()   
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }  
        public async Task<string> AddAsync(T entity)
        {
           var add = await _appDbContext.Set<T>().AddAsync(entity); 
           var save = await _appDbContext.SaveChangesAsync();
            return "successfully added";
        }

        public async Task DeleteAsync(string id)
        {
            var find = await _appDbContext.Set<T>().FindAsync(id);
            if (find != null)
            {
              _appDbContext.Set<T>().Remove(find);
              await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = _dbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(string id)
        {
            var item = await _appDbContext.Set<T>().FindAsync(id); 
            return item;
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var find = await _appDbContext.Set<T>().FindAsync(id);
            if (find != null)
            {
                _appDbContext.Entry<T>(find).CurrentValues.SetValues(entity);       
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
