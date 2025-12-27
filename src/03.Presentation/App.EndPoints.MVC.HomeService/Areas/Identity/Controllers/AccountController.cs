using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Dtos.AccountAgg;
using App.EndPoints.MVC.HomeService.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Identity.Controllers
{

    [Area(AreaConstants.Identity)]
    public class AccountController
        (IAccountAppService _accountAppService,
        ILogger<AccountController> _logger
        ) : Controller
    {
        

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }

            var result = await _accountAppService.Login(userLoginDto);

            if (result)
            {
                _logger.LogInformation("User {UserName} logged in successfully.", userLoginDto.UserName);
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            _logger.LogWarning("Failed login attempt for user {UserName}.", userLoginDto.UserName);
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View(userLoginDto);
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }


            var registerDto = new UserRegisterDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role
            };

            var result = await _accountAppService.Register(registerDto);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {UserName} Registered in successfully.", registerDto.Email);
                return RedirectToAction("Login");
            }

            _logger.LogWarning("User registration failed for {Email}. Errors: {Errors}",
              registerDto.Email, string.Join(", ", result.Errors.Select(e => e.Description)));



            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _accountAppService.Logout();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
