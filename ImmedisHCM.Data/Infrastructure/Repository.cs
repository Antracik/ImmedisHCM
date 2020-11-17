using NHibernate;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate.Linq;
using ImmedisHCM.Data.Entities;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace ImmedisHCM.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IBaseEntity
    {

        private readonly ISession _session;
        private IQueryable<TEntity> _query;

        protected ISession Session => _session;
        public IQueryable<TEntity> Entity => _query;

        public Repository(ISession session)
        {
            _session = session;
            _query = _session.Query<TEntity>();
        }

        public void AddRange(IEnumerable<TEntity> rangeList)
        {
            foreach (var item in rangeList)
            {
                _session.Save(item);
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> rangeList)
        {
            foreach (var item in rangeList)
            {
                await _session.SaveAsync(item);
            }
        }

        public string AddItem(TEntity item)
        {
            return _session.Save(item).ToString();
        }

        public async Task<string> AddItemAsync(TEntity item)
        {
            return (await _session.SaveAsync(item)).ToString();
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 Func<IQueryable<TEntity>, IQueryable<TEntity>> fetch = null)
        {
            if (filter != null)
                _query = _query.Where(filter);

            if (fetch != null)
                _query = fetch(_query);
            

            if (orderBy != null)
                return orderBy(_query).ToList();

            return _query.ToList();
        }

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            Func<IQueryable<TEntity>, IQueryable<TEntity>> fetch = null)
        {
            if(filter != null)
                _query = _query.Where(filter);

            if (fetch != null)
                _query = fetch(_query);

            if (orderBy != null)
                return orderBy(_query).ToListAsync();

            return _query.ToListAsync();
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, 
                                IQueryable<TEntity>> fetch = null)
        {
            if (fetch != null)
                _query = fetch(_query);

            return _query.FirstOrDefault(filter);
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, 
                            Func<IQueryable<TEntity>, IQueryable<TEntity>> fetch = null)
        {
            if (fetch != null)
                _query = fetch(_query);

            return _query.FirstOrDefaultAsync(filter);
        }

        public TEntity GetById(object id)
        {
            return _session.Get<TEntity>(id);
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            return _session.GetAsync<TEntity>(id);
        }

        public void Update(TEntity item)
        {
            _session.Merge(item);
        }

        public Task UpdateAsync(TEntity item)
        {
            return _session.MergeAsync(item);
        }

        public void Delete(TEntity item)
        {
            _session.Delete(item);
        }

        public Task DeleteAsync(TEntity item)
        {
            return _session.DeleteAsync(item);
        }

       
    }
}
