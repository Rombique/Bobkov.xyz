using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using System.Threading.Tasks;

namespace Bobkov.BL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<UserDTO> GetUserById(int id);
        UserProfileDTO GetProfileById(int id);
        Task<OperationDetails> UpdateProfile(UserProfileDTO profileDTO);
    }
}
