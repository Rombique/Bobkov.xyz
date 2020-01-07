using Bobkov.DAL.Entities;
using Bobkov.DAL.Identity;
using System.Threading.Tasks;

namespace Bobkov.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }
        IProfileManager ProfileManager { get; }

        IBaseRepository<TEntity> Repository<TEntity>()
            where TEntity : Base;

        void Dispose();
        void Commit();
        Task CommitAsync();

        void IdentityCommit();
        Task IdentityCommitAsync();
    }
}
