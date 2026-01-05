using App.Domain.AppServices.CustomerAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Customer.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Controllers
{

    [Area(AreaConstants.Customer)]
    [Authorize(Roles = RoleConstants.Customer)]
    public class ProfileController(ICustomerAppService _customerAppService ,
        IAccountAppService _accountAppService ,
        ICityAppService _cityAppService, ILogger<ProfileController> _logger) 
        : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {

            var userId=_accountAppService.GetUserId(User);
            if (userId<=0)
            {
                _logger.LogWarning(" آیدی معتبر نیست.");
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var result=await _customerAppService.GetProfileCustomerByAppUserId(userId, cancellation);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(CancellationToken cancellation)
        {
            var userId = _accountAppService.GetUserId(User);
            if (userId <= 0)
            {
                _logger.LogWarning(" آیدی معتبر نیست.");
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var result = await _customerAppService.GetProfileCustomerByAppUserId(userId, cancellation);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var cities=await _cityAppService.GetAll(cancellation);
            ViewBag.Cities = cities;

            EditProfileCustomerViewModel  editProfileCustomerViewModel = new EditProfileCustomerViewModel() 
            {
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                ImagePath = result.Data.ImagePath,
                Address = result.Data.Address,
                CityName = result.Data.CityName,
            };

            return View(editProfileCustomerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileCustomerViewModel editProfileCustomerViewModel, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("تلاش برای ثبت فرم با داده‌های نامعتبر توسط کاربر.");
                ViewBag.Cities = await _cityAppService.GetAll(cancellation);
                return View(editProfileCustomerViewModel);
            }

            if (editProfileCustomerViewModel.ImageFile != null && editProfileCustomerViewModel.ImageFile.Length > 0)
            {
               
                var fileName = editProfileCustomerViewModel.ImageFile.UploadFile("profiles");
                if (fileName != null)
                {
                    editProfileCustomerViewModel.ImagePath = fileName; 
                }
            }

            ProfileCustomerDto profileCustomerDto = new ProfileCustomerDto()
            {
                FirstName = editProfileCustomerViewModel.FirstName,
                LastName = editProfileCustomerViewModel.LastName,
                ImagePath = editProfileCustomerViewModel.ImagePath,
                Address = editProfileCustomerViewModel.Address,
                CityId = editProfileCustomerViewModel.CityId,
                WalletBalance = editProfileCustomerViewModel.WalletBalance
                
            };

            var appuserId = _accountAppService.GetUserId(User);
            var result = await _customerAppService.ChangeProfileCustomer(appuserId, profileCustomerDto, false,cancellation);

          
            if (!result.IsSuccess)
            {
                
                _logger.LogWarning("ویرایش پروفایل برای مشتری {Id} انجام نشد. علت: {Message}", appuserId, result.Message);

               
                ModelState.AddModelError(string.Empty, result.Message);

               
                ViewBag.Cities = await _cityAppService.GetAll(cancellation);

               
                return View(editProfileCustomerViewModel);
            }

          
             _logger.LogInformation("پروفایل مشتری {Id} با موفقیت آپدیت شد.", appuserId);
             TempData["SuccessMessage"] = result.Message;
             return RedirectToAction("Index");
        }


        public  IActionResult ChangePassword() 
        {
             
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

         
            var dto = new ChangePasswordDto
            {
                OldPassword = model.OldPassword,
                NewPassword = model.NewPassword
            };

            var result = await _accountAppService.ChangePassword(User, dto);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index"); 
            }

           
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }

    }
}
