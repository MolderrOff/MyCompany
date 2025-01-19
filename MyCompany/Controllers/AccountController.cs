using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyCompany.Models;

namespace MyCompany.Controllers
{
    [Authorize] // для данной области на сайте действует правило авторизации
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
        {
            userManager = userMgr; //через внедрение зависимостей этого класса через конструктор передаём userManager 
            signInManager = signinMgr; // для того чтобы оперировать пользователями в БД через контекст
        }

        [AllowAnonymous] // атрибут, чтобы залогиниться на сайте нужно быть анонимным пользователем
        // действие логин исключается из правила Authorize
        public IActionResult Login(string returnUrl)  //действие login
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel()); //передаём в качестве модели в представление только что созданную LoginViewModel.cs в представление Login
        }

        [HttpPost] //пост версия этого действия Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false); //пытаемся зайти, model.Password - пароль с модели, с html формы
                    // также передаём флажокmodel.RememberMe запомнить меня, (и м. б.  последнее свойство заблокировать пользователя при неудачно попытке входа)
                    if (result.Succeeded) //если действие успешное то перенаправляем пользователя по returnUrl
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            //если пользователь не найден
            return View(model); //и отправляем модель в браузер пользователя с этой ошибкой
        }

        //----------------------добавил --->>>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //---------------------добавил ---<<<
    }
}
