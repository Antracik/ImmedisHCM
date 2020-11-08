using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
