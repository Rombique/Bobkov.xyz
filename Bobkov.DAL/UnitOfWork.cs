using Bobkov.DAL.EF;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Identity;
using Bobkov.DAL.Interfaces;
using Bobkov.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bobkov.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext mainContext;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }

        public PostsRepository PostsRepository { get; }

        public UnitOfWork(MainContext context, AppUserManager userManager, AppRoleManager roleManager)
        {
            mainContext = context;
            RoleManager = roleManager;
            UserManager = userManager;
            PostsRepository = new PostsRepository(context);
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

        public int Commit()
        {
            return mainContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return mainContext.SaveChangesAsync();
        }
    }
}
