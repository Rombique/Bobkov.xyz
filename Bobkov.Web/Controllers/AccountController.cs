﻿using Bobkov.BL.DTO;
using Bobkov.BL.Interfaces;
using Bobkov.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                UserDTO user = new UserDTO { Email = model.Email, UserName = model.Username, Password = model.Password };
                var result = await userService.Create(user);
                if (result.Succeedeed)
                {
                    await loginService.Login(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
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
                    await loginService.Login(new UserDTO() { UserName = model.Username, Password = model.Password, IsPersistent = model.IsPersistent });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("IncorrectLoginError", "Некорректные логин и(или) пароль");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await loginService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}