using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bobkov.Web.Models.Blog
{
    public class PostDetailsVM
    {
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
