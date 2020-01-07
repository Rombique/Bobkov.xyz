using Bobkov.DAL.Entities;
using System;

namespace Bobkov.DAL.Interfaces
{
    public interface IProfileManager : IDisposable
    {
        void Create(UserProfile profile);
    }
}
