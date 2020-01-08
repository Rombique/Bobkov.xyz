using System;

namespace Bobkov.Web.Models.Account
{
    public class ProfileVM
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
