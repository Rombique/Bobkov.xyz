using Bobkov.DAL.Contexts;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Interfaces;

namespace Bobkov.DAL.Repositories
{
    public class ProfileManager : IProfileManager
    {
        public IdentityContext IdentityContext { get; set; }
        public ProfileManager(IdentityContext identityContext)
        {
            IdentityContext = identityContext;
        }

        public void Create(UserProfile profile)
        {
            IdentityContext.UserProfiles.Add(profile);
            IdentityContext.SaveChanges();
        }

        public void Dispose()
        {
            IdentityContext.Dispose();
        }
    }
}
