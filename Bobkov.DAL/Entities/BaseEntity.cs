using Bobkov.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bobkov.DAL.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
