using Bobkov.BL.DTO;
using Bobkov.BL.Interfaces;
using Bobkov.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bobkov.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly ILoginService loginService;

        public AccountController(ILogger<AccountController> logger, IUserService userService, ILoginService loginService) : base(logger)
        {
            this.loginService = loginService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO(model.Username, model.Email, model.Password);
                var result = await userService.Create(user);
                if (result.Succeedeed)
                {
                    await loginService.Login(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    logger.LogError(result.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await loginService.Login(new UserDTO(model.Username, model.Password, model.IsPersistent));
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else if (result.IsLockedOut)
                    ModelState.AddModelError("IncorrectLoginError", "Вы заблокированы!");
                else
                    ModelState.AddModelError("IncorrectLoginError", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await loginService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            UserProfileDTO userProfile = userService.GetProfileById(id);
            ProfileVM profile = new ProfileVM
            {
                Avatar = userProfile.Avatar.Length > 0 ? $"data:image/jpeg;base64,{(Convert.ToBase64String(userProfile.Avatar))}" : "none",
                Name = userProfile.Name,
                LastActivity = userProfile.LastActivity
            };
            return View(profile);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            int currentUserId = GetCurrentUserId();
            UserProfileDTO userProfile = userService.GetProfileById(currentUserId);
            EditProfileVM editProfile = new EditProfileVM
            {
                Id = userProfile.Id,
                OldAvatar = userProfile.Avatar != null ? $"data:image/jpeg;base64,{(Convert.ToBase64String(userProfile.Avatar))}" : "none",
                Name = userProfile.Name,
            };
            return View(editProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {
            if (ModelState.IsValid)
            {
                UserProfileDTO profile = new UserProfileDTO
                {
                    Name = model.Name,
                    Id = model.Id
                };
                if (model.Avatar != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    profile.Avatar = imageData;
                }
                var result = await userService.UpdateProfile(profile);
                if (result.Succeedeed)
                {
                    return RedirectToAction("Profile", new { id = profile.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
            return View(model);
        }
    }
}