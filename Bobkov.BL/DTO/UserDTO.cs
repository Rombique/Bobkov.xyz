namespace Bobkov.BL.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public UserDTO(string userName, string password, bool isPersistent)
        {
            UserName = userName;
            Password = password;
            IsPersistent = isPersistent;
        }

        public UserDTO(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsPersistent { get; set; }
    }
}
