using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public class Tag : Base
    {
        [Required]
        public string TagName { get; set; }
        [Required]
        public string Slug { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
