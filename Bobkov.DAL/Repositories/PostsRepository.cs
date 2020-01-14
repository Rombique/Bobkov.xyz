using Bobkov.DAL.EF;
using Bobkov.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bobkov.DAL.Repositories
{
    public class PostsRepository : BaseRepository<Post>
    {
        public PostsRepository(MainContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetPaged(int page, int pageSize = 10, bool asNoTracking = false)
        {
            var includes = dbSet
                    .Include(i => i.Author)
                    .Include(i => i.Category)
                    .OrderByDescending(o => o.Id);
            var entities = asNoTracking
                ? includes.AsNoTracking()
                : includes;

            return entities.Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
    }
}
