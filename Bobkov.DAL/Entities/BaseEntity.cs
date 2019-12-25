using Bobkov.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
