using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{

    [Area(AreaConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class CategoryController(ICategoryAppService _categoryAppService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _categoryAppService.GetAll(cancellationToken);

           
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;

                return RedirectToAction("index" , "panel");
            }

            return View(result.Data);
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
           
            var result = await _categoryAppService.GetById(id, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new UpdateCategoryViewModel
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                ExistingImagePath = result.Data.ImagePath
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View(model);

            
            string? imagePath = model.ExistingImagePath;
            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                
                imagePath = model.NewImage.UploadFile("categories");
            }

            var categoryDto = new CategoryDto
            {
                Id = model.Id,
                Title = model.Title,
                ImagePath = imagePath ?? "default-category.png"
            };

            var result = await _categoryAppService.Update(categoryDto, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "دسته‌بندی با موفقیت بروزرسانی شد.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }







     
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View(model);

            
            string? imagePath = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                imagePath = model.ImageFile.UploadFile("categories");
            }

            
            var result = await _categoryAppService.Create(model.Title, imagePath, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

           
            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryAppService.Delete(id, cancellationToken);

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
