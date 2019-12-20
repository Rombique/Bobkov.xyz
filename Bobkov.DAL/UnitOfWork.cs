using Bobkov.DAL.Contexts;
using Bobkov.DAL.Interfaces;
using System;

namespace Bobkov.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext context;

        public UnitOfWork(MainContext context)
        {
            this.context = context ?? throw new ArgumentNullException("Context is null");
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
