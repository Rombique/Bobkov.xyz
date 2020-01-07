using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bobkov.DAL.Entities
{
    public class UserProfile : Base
    {
        [Key]
        [ForeignKey("User")]
        public new int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
