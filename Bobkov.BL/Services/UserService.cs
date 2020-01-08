using Bobkov.BL.DTO;
using Bobkov.BL.Infrastructure;
using Bobkov.BL.Interfaces;
using Bobkov.DAL.Entities;
using Bobkov.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bobkov.BL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork UnitOfWork { get; set; }

        public UserService(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await UnitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.UserName };
                IdentityResult result = await UnitOfWork.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().Description, "");

                await UnitOfWork.UserManager.AddToRoleAsync(user, userDto.Role ?? "User");

                UserProfile userProfile = new UserProfile { Id = user.Id, LastActivity = DateTime.Now, Name = user.UserName };
                UnitOfWork.Repository<UserProfile>().AddNew(userProfile);
                await UnitOfWork.CommitAsync();
                return new OperationDetails(true, "Регистрация успешно завершена!", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким e-mail уже существует", "UserService.Create");
            }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            User user = await UnitOfWork.UserManager.FindByIdAsync(id.ToString());
            if (user == null)
                return null;

            return new UserDTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id
            };
        }

        public UserProfileDTO GetProfileById(int id)
        {
            UserProfile userProfile = UnitOfWork.Repository<UserProfile>().GetById(id, true);
            if (userProfile == null)
                return null;

            return new UserProfileDTO()
            {
                Avatar = userProfile.Avatar,
                LastActivity = userProfile.LastActivity,
                Name = userProfile.Name,
                Id = userProfile.Id
            };
        }

        public async Task<OperationDetails> UpdateProfile(UserProfileDTO profileDTO)
        {
            UserProfile userProfile = UnitOfWork.Repository<UserProfile>().GetById(profileDTO.Id, true);
            userProfile.Name = profileDTO.Name;
            if (profileDTO.Avatar != null)
                userProfile.Avatar = profileDTO.Avatar;
            await UnitOfWork.CommitAsync();
            return new OperationDetails(true, "Обновление успешно завершена!", "");
        }
    }
}
