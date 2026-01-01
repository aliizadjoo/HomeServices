using App.Domain.Core.Contract.ReviewAgg.AppService;
using App.Domain.Core.Contract.ReviewAgg.Service;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ReviewAgg;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class ReviewController(IReviewAppService _reviewAppService) : Controller
    {
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4, CancellationToken cancellationToken = default)
        {
            var result = await _reviewAppService.GetAll(pageNumber, pageSize, cancellationToken);
            var totalCount = await _reviewAppService.GetCount(cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                
            }

            var viewModel = new ReviewListViewModel 
            {
                Reviews = result.Data ,
                CurrentPage = pageNumber,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, ReviewStatus status, CancellationToken cancellationToken)
        {
            var result = await _reviewAppService.ChangeStatus(id, status, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
