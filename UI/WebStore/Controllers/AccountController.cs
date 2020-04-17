using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> logger)
        {
            userManager = UserManager;
            signInManager = SignInManager;
            this.logger = logger;
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

            using (logger.BeginScope("Создание нового пользователя {0}", Model.UserName))
            {
                var registerResult = await userManager.CreateAsync(user, Model.Password);

                if (registerResult.Succeeded)
                {
                    logger.LogInformation("Пользователь {0} успешно зарегистрирован", Model.UserName);

                    await userManager.AddToRoleAsync(user, Role.User);
                    logger.LogInformation("Пользователю {0} добавлена роль {1}", Model.UserName, Role.User);

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in registerResult.Errors)
                    ModelState.AddModelError("", error.Description);
                    
                logger.LogError("Ошибка при создании пользователя {0}:{1}", Model.UserName, string.Join(", ", registerResult.Errors.Select(x => x.Description)));
            }

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
                logger.LogInformation("Пользователь {0} успешно вошел в систему.", Model.UserName);
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Не верное имя пользователя или пароль.");
            logger.LogWarning("Ошибка ввода учетных данных пользователем {0}", Model.UserName);

            return View(Model);
        }

        public async Task<IActionResult> Logout() 
        {
            var username = User.Identity.Name;
            await signInManager.SignOutAsync();

            logger.LogInformation("Пользователь {0} вышел из системы.", username);

            return RedirectToAction("Index", "Home");
        }
    }
}