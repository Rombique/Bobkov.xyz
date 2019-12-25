using Bobkov.DAL.Contexts;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Interfaces;
using Bobkov.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Bobkov.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(MainContext context)
        {
            this.context = context ?? throw new ArgumentNullException("Context is null");
        }

        public IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity
        {
            var entityType = typeof(TEntity);
            if (repositories.ContainsKey(entityType))
                return (IBaseRepository<TEntity>)repositories[entityType];

            var newRepository = new BaseRepository<TEntity>(context);
            repositories.Add(entityType, newRepository);
            return newRepository;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
