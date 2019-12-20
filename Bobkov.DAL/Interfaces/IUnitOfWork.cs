namespace Bobkov.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Commit();
    }
}
