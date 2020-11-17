using ImmedisHCM.Data.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ImmedisHCM.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();

            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : IBaseEntity
        {
            if (_repositories.TryGetValue(typeof(TEntity), out object repo))
                return repo as IRepository<TEntity>;

            var repository = new Repository<TEntity>(_session);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Cannot have more than one transaction per session.");
            if(_session == null)
                _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            if (!_transaction.IsActive)
                throw new InvalidOperationException("Cannot commit to inactive transaction.");
            _transaction.Commit();
            Dispose();

        }

        public Task CommitAsync()
        {
            if (!_transaction.IsActive)
                throw new InvalidOperationException("Cannot commit to inactive transaction.");
            return _transaction.CommitAsync();
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
                _transaction.Rollback();
        }

        public Task RollbackAsync()
        {
            if (_transaction.IsActive)
                return _transaction.RollbackAsync();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            if(_repositories != null)
            {
                _repositories.Clear();
            }
        }

    }
}
