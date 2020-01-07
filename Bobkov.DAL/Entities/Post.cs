using System;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public class Post : Base
    {
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        public string Preview { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        public int AuthorId { get; set; }
        [Required]
        public User Author { get; set; }
    }
}
