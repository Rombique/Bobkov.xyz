using System;

namespace Bobkov.Web.Models.Post
{
    public class AllPostsVM
    {
        public string Title { get; set; }   
        public string Preview { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
