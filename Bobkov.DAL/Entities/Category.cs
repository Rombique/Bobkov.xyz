using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public class Category : Base
    {
        public Category()
        {
            Posts = new List<Post>();
        }

        [Required]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
