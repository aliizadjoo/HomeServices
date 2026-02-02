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
       (IProposalAppService _proposalAppService ,
        IAccountAppService _accountAppService,
        IOrderAppService _orderAppService,
        IReviewAppService _reviewAppService
        ) 
        : Controller
    {

        public IActionResult Create(int orderId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel createReviewViewModel , CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return View(createReviewViewModel);
            }

            var resultStatus= await _orderAppService.IsFinished(createReviewViewModel.OrderId, cancellationToken);
            if (!resultStatus)
            {
                TempData["ErrorMessage"] = "تا زمانیکه سفارش به وضعیت تکمیل نرسیده است نمیتوانید نظری ثبت کنید.";
                return RedirectToAction("MyOrders", "Order");
            }

            var customerId = _accountAppService.GetCustomerId(User);

            var resultHasCustomerCommented = await _reviewAppService.HasCustomerCommentedOnOrder(createReviewViewModel.OrderId , customerId , cancellationToken);

            if (resultHasCustomerCommented.IsSuccess)
            {
                TempData["ErrorMessage"] = resultHasCustomerCommented.Message;
                return RedirectToAction("MyOrders", "Order");
            }

            var result =await _proposalAppService.GetExpertIdByOrderId(createReviewViewModel.OrderId, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("MyOrders", "Order");
            }

            CreateReviewDto createReviewDto = new CreateReviewDto() 
            {
              Comment = createReviewViewModel.Comment,
              Score = createReviewViewModel.Score,
              ExpertId = result.Data,
              OrderId = createReviewViewModel.OrderId,
              CustomerId = customerId
            };

            var resultCreate= await  _reviewAppService.Create(createReviewDto , cancellationToken);
            if (!resultCreate.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(createReviewViewModel);

            }

            TempData["SuccessMessage"] = resultCreate.Message;
            return RedirectToAction("MyOrders", "Order");
        }
    }
}
