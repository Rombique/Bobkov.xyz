using Bobkov.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Bobkov.DAL.Entities
{
    public abstract class Base : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
