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
                _logger.LogInformation("کاربر جدید با نقش {Role} ایجاد شد.", selectedRole.Name);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewBag.CityDtos = await _cityAppService.GetAll(cancellationToken);
            ViewBag.RoleDtos = await _accountAppService.GetRoles(cancellationToken);
            return View(model);
        }



    }
}
