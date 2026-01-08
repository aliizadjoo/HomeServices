using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Dtos.CityAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Customer.Models;
using App.EndPoints.MVC.HomeService.Areas.Expert.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Controllers
{

    [Area(AreaConstants.Expert)]
    [Authorize(Roles = RoleConstants.Expert)]
    public class ProfileController
        (IExpertAppService _expertAppService
        , IAccountAppService _accountAppService,
        IHomeserviceAppService _homeserviceAppService
        , ICityAppService _cityAppService
        , ILogger<ProfileController> _logger
        ) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var appuserId = _accountAppService.GetUserId(User);

            if (appuserId <= 0)
            {
                _logger.LogWarning(" آیدی معتبر نیست.");
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }


            var result = await _expertAppService.GetProfile(appuserId, cancellation);
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

            var result = await _expertAppService.GetProfile(userId, cancellation);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var citiesResult = await _cityAppService.GetAll(cancellation);
            var servicesResult = await _homeserviceAppService.GetAll(cancellation);
            var editProfileExpertViewModel = new EditProfileExpertViewModel()
            {
                Bio = result.Data.Bio,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                ImagePath = result.Data.ImagePath,
                CityId = result.Data.CityId,
                CityName = result.Data.CityName,
                HomeServices = result.Data.HomeServices,
                HomeServicesId = result.Data.HomeServicesId,
                AvailableCities = citiesResult  ,
                AvailableServices = servicesResult.Data ,
                PhoneNumber = result.Data.PhoneNumber,

            };

            return View(editProfileExpertViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileExpertViewModel editProfileExpertViewModel, CancellationToken cancellation) 
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("تلاش برای ثبت فرم با داده‌های نامعتبر توسط کاربر.");
                var cities = await _cityAppService.GetAll(cancellation);
                var servicesResult = await _homeserviceAppService.GetAll(cancellation);

                editProfileExpertViewModel.AvailableCities = cities;
                editProfileExpertViewModel.AvailableServices = servicesResult.Data;

                return View(editProfileExpertViewModel);
            }

            if (editProfileExpertViewModel.ImageFile != null && editProfileExpertViewModel.ImageFile.Length > 0)
            {

                var fileName = editProfileExpertViewModel.ImageFile.UploadFile("profiles");
                if (fileName != null)
                {
                    editProfileExpertViewModel.ImagePath = fileName;
                }
            }


            ProfileExpertDto profileExpertDto = new ProfileExpertDto()
            {
                Bio = editProfileExpertViewModel.Bio,
                FirstName = editProfileExpertViewModel.FirstName,
                LastName = editProfileExpertViewModel.LastName,
                ImagePath = editProfileExpertViewModel.ImagePath,
                CityId = editProfileExpertViewModel.CityId,
                HomeServicesId = editProfileExpertViewModel.HomeServicesId,
                PhoneNumber = editProfileExpertViewModel.PhoneNumber,

            };
            var appuserId = _accountAppService.GetUserId(User);
            var result = await _expertAppService.ChangeProfile(appuserId, profileExpertDto,false, cancellation);

            if (!result.IsSuccess)
            {

                _logger.LogWarning("ویرایش پروفایل برای کارشناس {Id} انجام نشد. علت: {Message}", appuserId, result.Message);


                ModelState.AddModelError(string.Empty, result.Message);


                return View(editProfileExpertViewModel);
            }


            _logger.LogInformation("پروفایل کارشناس {Id} با موفقیت آپدیت شد.", appuserId);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");

         
        }



    }
}
