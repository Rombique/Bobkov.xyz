using Bobkov.DAL.Contexts;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Identity;
using Bobkov.DAL.Interfaces;
using Bobkov.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bobkov.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext mainContext;
        private readonly IdentityContext identityContext;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }
        public IProfileManager ProfileManager { get; }

        public UnitOfWork(IdentityContext identity, MainContext context, AppUserManager userManager, AppRoleManager roleManager)
        {
            mainContext = context;
            identityContext = identity;
            this.RoleManager = roleManager;
            this.UserManager = userManager;
            ProfileManager = new ProfileManager(identity);
        }

        public IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : Base
        {
            var entityType = typeof(TEntity);
            if (repositories.ContainsKey(entityType))
                return (IBaseRepository<TEntity>)repositories[entityType];

            var newRepository = new BaseRepository<TEntity>(mainContext);
            repositories.Add(entityType, newRepository);
            return newRepository;
        }

        public void Commit()
        {
            mainContext.SaveChanges();
        }

        public void IdentityCommit()
        {
            identityContext.SaveChanges();
        }

        public Task CommitAsync()
        {
            return mainContext.SaveChangesAsync();
        }

        public Task IdentityCommitAsync()
        {
            return identityContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mainContext.Dispose();
                    identityContext.Dispose();
                    ProfileManager.Dispose();
                    UserManager.Dispose();
                    RoleManager.Dispose();
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
