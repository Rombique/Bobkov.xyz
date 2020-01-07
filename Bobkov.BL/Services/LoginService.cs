using Bobkov.BL.DTO;
using Bobkov.BL.Interfaces;
using Bobkov.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bobkov.BL.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> signInManager;

        public LoginService(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public Task<SignInResult> Login(UserDTO user)
        {
            return signInManager.PasswordSignInAsync(user.UserName, user.Password, user.IsPersistent, false);
        }

        public Task Logout() => signInManager.SignOutAsync();
    }
}
