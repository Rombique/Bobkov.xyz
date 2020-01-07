using Microsoft.AspNetCore.Identity;
using System;

namespace Bobkov.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual UserProfile ClientProfile { get; set; }
    }
}
