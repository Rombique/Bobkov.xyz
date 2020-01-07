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

                UserProfile userProfile = new UserProfile { Id = user.Id, LastActivity = DateTime.Now };
                UnitOfWork.ProfileManager.Create(userProfile);
                await UnitOfWork.IdentityCommitAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
