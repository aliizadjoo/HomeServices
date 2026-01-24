using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.AdminAgg.AppService;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.CustomerAgg.AppService;
using App.Domain.Core.Contract.ExpertAgg.AppService;
using App.Domain.Core.Dtos.AccountAgg;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CityAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{

    [Area(AreaConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class UserController
        (ICustomerAppService _customerAppService ,
        IExpertAppService _expertAppService,
        IAdminAppService _adminAppService
        ,ICityAppService _cityAppService,
        IAccountAppService _accountAppService,
        IHomeserviceAppService _homeserviceAppService,
        ILogger<UserController> _logger
        )
        : Controller
    {
        public async Task<IActionResult> Index(int cPageNumber = 1 , int ePageNumber = 1, int aPageNumber = 1 , string activeTab = "customers" , int pageSize = 4, CancellationToken cancellationToken = default)
        {
            
            var resultCustomer = await _customerAppService.GetAll(cPageNumber, pageSize, cancellationToken);
            var resultExpert = await _expertAppService.GetAll(ePageNumber, pageSize, cancellationToken);
            var resultAdmin = await _adminAppService.GetAll(aPageNumber, pageSize, cancellationToken);

           
            if (!resultCustomer.IsSuccess) ViewBag.CustomerError = resultCustomer.Message;
            if (!resultExpert.IsSuccess) ViewBag.ExpertError = resultExpert.Message;
            if (!resultAdmin.IsSuccess) ViewBag.AdminError = resultAdmin.Message;

            var viewModel = new UserManagementViewModel
            {
                CustomerPage = cPageNumber,
                ExpertPage = ePageNumber,
                AdminPage = aPageNumber,
                PageSize = pageSize,
                ActiveTab = activeTab,
                

                Customers =  resultCustomer.Data.Customers ,
                CustomerTotalCount =  resultCustomer.Data.TotalCount ,


                Experts =  resultExpert.Data.Experts ,
                ExpertTotalCount =  resultExpert.Data.TotalCount,

                Admins =  resultAdmin.Data.Admins ,
                AdminTotalCount =  resultAdmin.Data.TotalCount 
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            
            var cityDtos = await _cityAppService.GetAll(cancellationToken);
            var roleDtos = await _accountAppService.GetRoles(cancellationToken);

            ViewBag.CityDtos = cityDtos;
            ViewBag.RoleDtos = roleDtos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserBaseViewModel model, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                ViewBag.CityDtos = await _cityAppService.GetAll(cancellationToken);
                ViewBag.RoleDtos = await _accountAppService.GetRoles(cancellationToken);
                return View(model);
            }

          
            var roles = await _accountAppService.GetRoles(cancellationToken);
            var selectedRole = roles.FirstOrDefault(r => r.Id == model.RoleId);

            if (selectedRole == null)
            {
                ModelState.AddModelError("", "نقش انتخاب شده معتبر نیست.");
                ViewBag.CityDtos = await _cityAppService.GetAll(cancellationToken);
                ViewBag.RoleDtos = await _accountAppService.GetRoles(cancellationToken);
                return View(model);
            }

            
            string? fileName = null;
            if (model.ProfileImage != null)
            {
                fileName = model.ProfileImage.UploadFile("profiles");
            }

           
            var registerDto = new UserRegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Role = selectedRole.Name, 
                RoleId = model.RoleId,
                CityId = model.CityId,
                ImagePath = fileName
            };

           
            var result = await _accountAppService.Register(registerDto, cancellationToken);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "کاربر با موفقیت ایجاد شد.";
                _logger.LogInformation("کاربر جدید با نقش {Role} ایجاد شد.", selectedRole.Name);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            TempData["ErrorMessage"] = "عملیات ثبت کاربر با خطا مواجه شد.";
            ViewBag.CityDtos = await _cityAppService.GetAll(cancellationToken);
            ViewBag.RoleDtos = await _accountAppService.GetRoles(cancellationToken);
            return View(model);
        }

        public IActionResult ChangePassword(int appUserId)
        {
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel
            {
                AppUserId = appUserId,
            };

            return View(changePasswordViewModel);
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
                NewPassword = model.NewPassword,
                AppUserId = model.AppUserId,
            };

            var result = await _accountAppService.ChangePassword(dto);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }


            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }


        #region Customer
        public async Task<IActionResult> UpdateCustomer(int appUserId, CancellationToken cancellationToken)
        {

            var result = await _customerAppService.GetProfileCustomerByAppUserId(appUserId, cancellationToken);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("تلاش برای ویرایش مشتری ناموفق بود. شناسه: {appUserId}", appUserId);
                return RedirectToAction("Index");
            }

            var customer = result.Data;


            var viewModel = new UpdateCustomerByAdminViewModel
            {
                AppUserId = customer.AppUserId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email!,
                Address = customer.Address,
                WalletBalance = customer.WalletBalance,
                CityId = customer.CityId,
                ExistingImagePath = customer.ImagePath,
                PhoneNumber = customer.PhoneNumber,
                
            };


            viewModel.Cities = await _cityAppService.GetAll(cancellationToken);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerByAdminViewModel model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                model.Cities = await _cityAppService.GetAll(cancellationToken);
                return View(model);
            }


            string? imagePath = model.ExistingImagePath;
            if (model.NewProfileImage != null)
            {
                imagePath = model.NewProfileImage.UploadFile("profiles");
            }


            var updateDto = new ProfileCustomerDto
            {
                AppUserId = model.AppUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                WalletBalance = model.WalletBalance,
                CityId = model.CityId,
                ImagePath = imagePath,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _customerAppService.ChangeProfileCustomer(model.AppUserId, updateDto, true, cancellationToken);

            if (result.IsSuccess)
            {

                return RedirectToAction("Index");
            }


            ModelState.AddModelError(string.Empty, result.Message);
            model.Cities = await _cityAppService.GetAll(cancellationToken);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int appUserId, CancellationToken cancellationToken)
        {

            var result = await _customerAppService.DeleteUser(appUserId, cancellationToken);

            if (result.IsSuccess)
            {

                TempData["SuccessMessage"] = "مشتری مورد نظر با موفقیت حذف  شد.";
            }
            else
            {

                TempData["ErrorMessage"] = result.Message;
            }


            return RedirectToAction("index");
        }

        #endregion


        #region Expert

       
        public async Task<IActionResult> UpdateExpert(int appUserId, CancellationToken cancellationToken)
        {
          
            var result = await _expertAppService.GetProfile(appUserId, cancellationToken);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("کارشناس با شناسه {Id} یافت نشد.", appUserId);
                return RedirectToAction("Index");
            }

            var expert = result.Data;

           
            var cities = await _cityAppService.GetAll(cancellationToken);
            var servicesResult = await _homeserviceAppService.GetAll(cancellationToken);

           
            var viewModel = new UpdateExpertByAdminViewModel
            {
                AppUserId = expert.AppUserId,
                FirstName = expert.FirstName,
                LastName = expert.LastName,
                Email = expert.Email!,
                Bio = expert.Bio,
                WalletBalance = expert.WalletBalance ?? 0,
                CityId = expert.CityId,
                ExistingImagePath = expert.ImagePath,
                SelectedHomeServicesId = expert.HomeServicesId,
                AvailableCities = cities,
                AvailableServices = servicesResult.Data,
                PhoneNumber = expert.PhoneNumber,
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateExpert(UpdateExpertByAdminViewModel model, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                model.AvailableCities = await _cityAppService.GetAll(cancellationToken);
                var servicesResult = await _homeserviceAppService.GetAll(cancellationToken);
                model.AvailableServices = servicesResult.Data;

                return View(model);
            }

         
            string? imagePath = model.ExistingImagePath;
            if (model.NewProfileImage != null && model.NewProfileImage.Length > 0)
            {
               
                imagePath = model.NewProfileImage.UploadFile("Profiles");
            }

           
            var profileDto = new ProfileExpertDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Bio = model.Bio,
                WalletBalance = model.WalletBalance,
                CityId = model.CityId,
                ImagePath = imagePath,
                HomeServicesId = model.SelectedHomeServicesId,
                PhoneNumber = model.PhoneNumber

            };

         
            var result = await _expertAppService.ChangeProfile(model.AppUserId, profileDto, isAdmin: true, cancellationToken);

            if (result.IsSuccess)
            {
               
                TempData["SuccessMessage"] = "اطلاعات کارشناس با موفقیت به‌روزرسانی شد.";
                return RedirectToAction("Index");
            }

           
            ModelState.AddModelError(string.Empty, result.Message);
            model.AvailableCities = await _cityAppService.GetAll(cancellationToken);
            var servicesResultReload = await _homeserviceAppService.GetAll(cancellationToken);
            model.AvailableServices = servicesResultReload.Data;

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpert(int appUserId, CancellationToken cancellationToken)
        {
         
            var result = await _expertAppService.Delete(appUserId, cancellationToken);

            if (result.IsSuccess)
            {
             
                TempData["SuccessMessage"] = "کارشناس مورد نظر با موفقیت حذف  شد.";
            }
            else
            {
                
                TempData["ErrorMessage"] = result.Message;
            }

           
            return RedirectToAction("Index");
        }



        #endregion


        #region Admin


        public async Task<IActionResult> UpdateAdmin(int appUserId, CancellationToken cancellationToken)
        {
            
            var result = await _adminAppService.GetProfileByAppUserId(appUserId, cancellationToken);

            if (!result.IsSuccess)
            {
               
               _logger.LogWarning("تلاش برای ویرایش مدیر ناموفق بود. شناسه کاربر: {appUserId}", appUserId);
                return RedirectToAction("Index");
            }

            var admin = result.Data;

           
            var viewModel = new UpdateAdminByAdminViewModel
            {
                AppUserId = admin.AppUserId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email!,
                StaffCode = admin.StaffCode,
                TotalRevenue = admin.TotalRevenue,
                ExistingImagePath = admin.ImagePath,
                PhoneNumber = admin.PhoneNumber
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminByAdminViewModel model, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

           
            string? imagePath = model.ExistingImagePath;

            if (model.NewProfileImage != null && model.NewProfileImage.Length > 0)
            {
                
                imagePath = model.NewProfileImage.UploadFile("profiles");
            }

      
            var adminProfileDto = new AdminProfileDto
            {
                AppUserId = model.AppUserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StaffCode = model.StaffCode,
                ImagePath = imagePath,
                 Email = model.Email,
                 PhoneNumber= model.PhoneNumber
                
            };

           
            var result = await _adminAppService.UpdateProfile(adminProfileDto, cancellationToken);

            if (result.IsSuccess)
            {
               
                TempData["SuccessMessage"] = "پروفایل مدیر با موفقیت بروزرسانی شد.";

                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

        
            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdmin(int appUserId, CancellationToken cancellationToken)
        {
            
            var result = await _adminAppService.Delete(appUserId, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "حساب ادمین مورد نظر با موفقیت حذف شد.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction("Index", "User");
        }


        #endregion


    }
}
