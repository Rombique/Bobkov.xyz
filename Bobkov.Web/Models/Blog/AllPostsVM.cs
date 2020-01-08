using System.Collections.Generic;

namespace Bobkov.Web.Models.Blog
{
    public class AllPostsVM
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<PostDetailsVM> Posts { get; set; }
    }
}
