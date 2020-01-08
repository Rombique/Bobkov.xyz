using System;

namespace Bobkov.BL.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int AuthorId { get; set; }
        public int Author { get; set; }
    }
}
