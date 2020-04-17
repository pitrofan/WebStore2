using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }


        #region Регистрация пользователя в системе

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
                return View(Model);

            //var user = new User
            //{
            //    UserName = Model.UserName,
            //};
            var user = mapper.Map<User>(Model);

            var registerResult = await userManager.CreateAsync(user, Model.Password);

            if (registerResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Role.User);

                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registerResult.Errors)
                ModelState.AddModelError("", error.Description);

            return View(Model);
        }
        #endregion


        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl});

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model) 
        {
            if (!ModelState.IsValid) return View(Model);

            var loginResult = await signInManager.PasswordSignInAsync(Model.UserName, Model.Password, Model.RememberMe, false);

            if (loginResult.Succeeded)
            {
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Не верное имя пользователя или пароль.");

            return View(Model);
        }

        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}