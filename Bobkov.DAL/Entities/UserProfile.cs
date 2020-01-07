using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bobkov.DAL.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
