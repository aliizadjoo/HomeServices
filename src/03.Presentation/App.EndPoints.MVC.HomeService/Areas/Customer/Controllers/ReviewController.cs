using App.Domain.AppServices.AccountAgg;
using App.Domain.AppServices.OrderAgg;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Contract.ReviewAgg.AppService;
using App.Domain.Core.Dtos.ReviewAgg;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Customer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Controllers
{

    [Area(AreaConstants.Customer)]
    [Authorize(Roles = RoleConstants.Customer)]
    public class ReviewController
       (
        IAccountAppService _accountAppService,
        IReviewAppService _reviewAppService
        ) 
        : Controller
    {

        public IActionResult Create(int orderId)
        {
            return View();
        }

 
        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model, CancellationToken cancellationToken)
      
        {
            if (!ModelState.IsValid)
                return View(model);

            var customerId = _accountAppService.GetCustomerId(User);

            var dto = new CreateReviewDto
            {
                OrderId = model.OrderId,
                Comment = model.Comment,
                Score = model.Score,
                CustomerId = customerId
            };

            var result = await _reviewAppService.Create(dto, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("MyOrders", "Order");
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("MyOrders", "Order");
        }

    }
}
