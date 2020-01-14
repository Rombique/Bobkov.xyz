using Bobkov.DAL.Entities;
using Bobkov.DAL.Identity;
using Bobkov.DAL.Repositories;
using System.Threading.Tasks;

namespace Bobkov.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }

        PostsRepository PostsRepository { get; }

        IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : Base;

        int Commit();
        Task<int> CommitAsync();
    }
}
