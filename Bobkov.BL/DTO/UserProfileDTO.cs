using System;

namespace Bobkov.BL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public byte[] Avatar { get; set; }
        public string Name { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
