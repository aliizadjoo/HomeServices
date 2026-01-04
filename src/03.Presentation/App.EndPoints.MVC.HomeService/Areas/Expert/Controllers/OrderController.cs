using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Dtos.OrderAgg;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Expert.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Controllers
{

    [Area(AreaConstants.Expert)]
    [Authorize(Roles = RoleConstants.Expert)]
    public class OrderController
        (IOrderAppService _orderAppService,
        IAccountAppService _accountAppService
        
        ) 
        : Controller
    {
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            int expertId = _accountAppService.GetExpertId(User);
            var result = await _orderAppService.GetAvailableForExpert(expertId, pageNumber, pageSize, cancellationToken);

            if (!result.IsSuccess)
            {
              
                TempData["InfoMessage"] = result.Message;
               
                return View(new AvailableOrdersPagedDto());
            }

           
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(result.Data.TotalCount / (double)pageSize);

            return View(result.Data);
        }



      
    }
}
