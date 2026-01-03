
using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Controllers
{
    public class HomeServiceController
        (IHomeserviceAppService _homeserviceAppService
        , ILogger<HomeServiceController> _logger
        )
        : Controller
    {
        public async Task<IActionResult> Index(int categoryId, int pageNumber = 1, int pageSize = 8, CancellationToken cancellationToken = default)
        {
           
            _logger.LogInformation("درخواست مشاهده سرویس‌ها برای دسته‌بندی با شناسه {CategoryId} و صفحه {PageNumber} دریافت شد.", categoryId, pageNumber);

            var result = await _homeserviceAppService.GetServicesByCategoryId(categoryId, pageNumber, pageSize, cancellationToken);

           
            _logger.LogInformation("تعداد {Count} سرویس برای دسته‌بندی {CategoryId} واکشی شد.", result.Data.HomeserviceDtos.Count, categoryId);

            ViewBag.CategoryId = categoryId;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(result.Data);
        }


    }
}