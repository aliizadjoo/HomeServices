using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Dtos.AccountAgg;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Identity.Controllers
{

    [Area(AreaConstants.Identity)]
    public class AccountController
        (IAccountAppService _accountAppService,
        ICityAppService _cityAppService,
        ILogger<AccountController> _logger
        ) : Controller
    {
        

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userLoginViewModel);
            }

            var userLoginDto = new UserLoginDto
            {
                UserName = userLoginViewModel.UserName,
                Password = userLoginViewModel.Password,
                RememberMe = userLoginViewModel.RememberMe
            };

            var result = await _accountAppService.Login(userLoginDto);

            if (result)
            {
                _logger.LogInformation("User {UserName} logged in successfully.", userLoginViewModel.UserName);
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            _logger.LogWarning("Failed login attempt for user {UserName}.", userLoginViewModel.UserName);
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View(userLoginViewModel);
        }

        public async Task<IActionResult> Register(CancellationToken cancellation)
        {
            ViewBag.Cities = await _cityAppService.GetAll(cancellation);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellation)
        {
        
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("تلاش برای ثبت‌نام با داده‌های نامعتبر توسط: {Email}", model.Email);

             
                ViewBag.Cities = await _cityAppService.GetAll(cancellation);
                return View(model);
            }

            
            var registerDto = new UserRegisterDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
                CityId = model.CityId
            };

         
            var result = await _accountAppService.Register(registerDto ,cancellation);

            if (result.Succeeded)
            {
               
                _logger.LogInformation("کاربر {UserName} با موفقیت ثبت‌نام کرد.", registerDto.Email);
                TempData["SuccessMessage"] = "ثبت‌نام شما با موفقیت انجام شد. اکنون می‌توانید وارد شوید.";
                return RedirectToAction("Login");
            }

         
            _logger.LogWarning("خطا در ثبت‌نام کاربر {Email}. جزییات: {Errors}",
             registerDto.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

        
            ViewBag.Cities = await _cityAppService.GetAll(cancellation);

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
