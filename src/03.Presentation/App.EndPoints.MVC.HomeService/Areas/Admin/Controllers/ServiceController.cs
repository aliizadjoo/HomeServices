using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Contract.CategoryAgg.Repository;
using App.Domain.Core.Dtos.HomeServiceAgg;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class ServiceController(
    IHomeserviceAppService _homeServiceAppService,
    ICategoryAppService _categoryAppService
 ) : Controller
    {
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var search = new SearchHomeServiceDto(); 

            var result = await _homeServiceAppService.GetAll(pageSize, pageNumber, search, cancellationToken);
            var totalCount = await _homeServiceAppService.GetCount(cancellationToken);

            var viewModel = new HomeServiceListViewModel
            {
                Services = result.Data,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(viewModel);
        }

      
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
         
            var categories = await _categoryAppService.GetAll(cancellationToken);

            var viewModel = new CreateHomeServiceViewModel
            {
                AvailableCategories = categories.Data
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeServiceViewModel model, CancellationToken cancellationToken)
        {
            
            if (!ModelState.IsValid)
            {
                
                var categories = await _categoryAppService.GetAll(cancellationToken);
                model.AvailableCategories = categories.Data;
                return View(model);
            }

           
            string? fileName = "default-service.png"; 
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
              
                fileName = model.ImageFile.UploadFile("services");
            }

            
            var dto = new CreateHomeServiceDto
            {
                Name = model.Name,
                Description = model.Description,
                BasePrice = model.BasePrice,
                CategoryId = model.CategoryId,
                ImagePath = fileName ?? "default-service.png"
            };

          
            var result = await _homeServiceAppService.Create(dto, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("index");
            }

            
            ModelState.AddModelError(string.Empty, result.Message);
            var categoriesReload = await _categoryAppService.GetAll(cancellationToken);
            model.AvailableCategories = categoriesReload.Data;

            return View(model);
        }


        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
          
            var serviceResult = await _homeServiceAppService.GetById(id, cancellationToken);
            if (!serviceResult.IsSuccess) return RedirectToAction("index");

          
            var categories = await _categoryAppService.GetAll(cancellationToken);

            var viewModel = new UpdateHomeServiceViewModel
            {
                Id = serviceResult.Data.Id,
                Name = serviceResult.Data.Name,
                Description = serviceResult.Data.Description,
                BasePrice = serviceResult.Data.BasePrice,
                ExistingImagePath = serviceResult.Data.ImagePath,
                CategoryId = serviceResult.Data.CategoryId,
                AvailableCategories = categories.Data
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateHomeServiceViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryAppService.GetAll(cancellationToken);
                model.AvailableCategories = categories.Data;
                return View(model);
            }

        
            string? finalImagePath = model.ExistingImagePath;
            if (model.NewImageFile != null && model.NewImageFile.Length > 0)
            {
                finalImagePath = model.NewImageFile.UploadFile("services");
            }

            var dto = new HomeserviceDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                BasePrice = model.BasePrice,
                CategoryId = model.CategoryId,
                ImagePath = finalImagePath
            };

            var result = await _homeServiceAppService.Update(dto, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("index");
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _homeServiceAppService.Delete(id, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

           
            return RedirectToAction("index");
        }


    }
}
