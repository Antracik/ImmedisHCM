using ImmedisHCM.Data.Entities;
using NHibernate;
using System;
using System.Threading.Tasks;

namespace ImmedisHCM.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : IBaseEntity;
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
