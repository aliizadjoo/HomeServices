using App.Domain.Core.Contract.CategoryAgg.AppService;
using App.Domain.Core.Dtos.CategoryAgg;
using App.EndPoints.MVC.HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Controllers
{
    public class HomeController
        (
        ILogger<HomeController> _logger
         , ICategoryAppService _categoryAppService
        )
        : Controller
    {
       
      
        public async Task<IActionResult> Index(int pageSize = 4, int pageNumber = 1,  CancellationToken cancellationToken=default)
        {
           var result= await _categoryAppService.GetAll(pageSize, pageNumber, null,cancellationToken);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(result.Data);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccessDenied(string message)
        {

            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}
