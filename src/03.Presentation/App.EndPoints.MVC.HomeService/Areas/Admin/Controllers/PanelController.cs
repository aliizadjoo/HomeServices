using App.EndPoints.MVC.HomeService.Areas.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{

    [Area(AreaConstants.Admin)]
    [Authorize(Roles =RoleConstants.Admin)]
    public class PanelController
        : Controller
    {
        public IActionResult Index()
        {

            return View();
        }





    }
}
