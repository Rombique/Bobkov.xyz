using Bobkov.BL.DTO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bobkov.BL.Interfaces
{
    public interface ILoginService
    {
        Task<SignInResult> Login(UserDTO user);
        Task Logout();
    }
}
