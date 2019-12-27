using Microsoft.EntityFrameworkCore;

namespace Bobkov.DAL.Contexts
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
