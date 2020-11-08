using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void AddRange(IEnumerable<TEntity> rangeList);
        Task AddRangeAsync(IEnumerable<TEntity> rangeList);
        string AddItem(TEntity item);
        Task<string> AddItemAsync(TEntity item);

        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity GetById(string id);
        Task<TEntity> GetByIdAsync(string id);

        void Update(TEntity item);
        Task UpdateAsync(TEntity item);

        void Delete(TEntity item);
        Task DeleteAsync(TEntity item);
    }
}
