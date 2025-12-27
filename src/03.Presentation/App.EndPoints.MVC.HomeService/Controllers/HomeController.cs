using App.EndPoints.MVC.HomeService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.EndPoints.MVC.HomeService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult AccessDenied(string message)
        {
          
            ViewBag.ErrorMessage = message;
            return View();
        }
        public IActionResult Index()
        {


            _logger.LogInformation("صفحه اصلی توسط کاربر باز شد در ساعت {Time}", DateTime.Now.ToLongTimeString());
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
