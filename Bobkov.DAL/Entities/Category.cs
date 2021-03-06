﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public class Category : Base
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
