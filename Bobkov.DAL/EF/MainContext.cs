using Bobkov.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bobkov.DAL.EF
{
    public class MainContext : IdentityDbContext<User, Role, int>
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
