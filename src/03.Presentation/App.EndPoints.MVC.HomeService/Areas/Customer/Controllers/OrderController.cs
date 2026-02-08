using App.Domain.AppServices.HomeserviceAgg;
using App.Domain.Core._common;
using App.Domain.Core.Contract.AccountAgg.AppServices;
using App.Domain.Core.Contract.CityAgg.AppService;
using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Contract.ProposalAgg.AppService;
using App.Domain.Core.Contract.ReviewAgg.AppService;
using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Dtos.ReviewAgg;
using App.Domain.Core.Enums.ProposalAgg;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using App.EndPoints.MVC.HomeService.Areas.Customer.Models;
using App.EndPoints.MVC.HomeService.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Controllers
{


    [Area(AreaConstants.Customer)]
    [Authorize(Roles = RoleConstants.Customer)]
    public class OrderController 
        (IHomeserviceAppService _homeserviceAppService,
        ICityAppService _cityAppService
        ,IOrderAppService _orderAppService,
        IAccountAppService _accountAppService,
        IProposalAppService _proposalAppService,
        IReviewAppService _reviewAppService
        )
        : Controller
    {


        public async Task<IActionResult> MyOrders(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
           int appUserId= _accountAppService.GetUserId(User);
           if (appUserId <= 0) return RedirectToAction("Login", "Account", new { area = "Identity" });


            var result = await _orderAppService.GetOrdersByAppUserId(appUserId, pageNumber, pageSize, cancellationToken);

            
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(result.Data.TotalCount / (double)pageSize);

            return View(result.Data);
        }

        public async Task<IActionResult> Create(int serviceId, CancellationToken cancellationToken)
        {
         
            var serviceResult = await _homeserviceAppService.GetById(serviceId, cancellationToken);
            if (serviceResult == null) return RedirectToAction("index" , "Homesrvice" , new { area = "" });

          
            var cities = await _cityAppService.GetAll(cancellationToken);

           
            var model = new CreateOrderViewModel
            {
                HomeServiceId = serviceId,
                ServiceName = serviceResult.Data.Name,
                BasePrice = serviceResult.Data.BasePrice.ToString("N0"),
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CityName,
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model, CancellationToken cancellationToken)
        {
            
            if (!ModelState.IsValid)
            {
                
                var cities = await _cityAppService.GetAll(cancellationToken);
                model.Cities = cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CityName }).ToList();
                return View(model);
            }

            var customerId = _accountAppService.GetCustomerId(User);
            if (customerId <= 0)
            {
                TempData["ErrorMessage"] = "خطا در احراز هویت مشتری. لطفاً دوباره وارد شوید.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            DateTime executionDate = model.ExecutionDateShamsi.ToGregorianDate();

            List<string> imagePaths = model.ImageFiles.UploadFiles("orders");


            var orderDto = new OrderCreateDto
            {
                HomeServiceId = model.HomeServiceId,
                Description = model.Description,
                ExecutionDate = executionDate,
                ExecutionTime = model.ExecutionTime,
                CityId = model.CityId,
                CustomerId = customerId, 
                ImagePaths = imagePaths
            };


            var result = await _orderAppService.Create(orderDto, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("MyOrders", "Order");
            }


            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }

        public async Task<IActionResult> Proposals(int orderId, CancellationToken cancellationToken)
        {
            var result = await _proposalAppService.GetOrderProposals(orderId, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;            
                return RedirectToAction("MyOrders");
            }

        
            foreach (var item in result.Data)
            {
                item.PersianExecutionDate = item.ExecutionDate.ToPersianDate();
            }

            ViewBag.OrderId = orderId;
            return View(result.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int proposalId, ProposalStatus newStatus, int orderId, CancellationToken cancellationToken)
        {
          
            var result = await _proposalAppService.ChangeStatus(proposalId, orderId, newStatus, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction("Proposals", "Order", new { area = "Customer", orderId = orderId });
        }

   
        public async Task<IActionResult> GetByExpertId(int expertId, int pageNumber = 1, int pageSize = 10)
        {
           
            var result = await _reviewAppService.GetByExpertId(pageSize, pageNumber, expertId, CancellationToken.None);

            if (result.IsSuccess && result.Data != null)
            {
                
                var totalPages = (int)Math.Ceiling(result.Data.TotalCount / (double)pageSize);
                foreach (var reviewDto in result.Data.ReviewDtos)
                {
                    reviewDto.CreatedAtShamsi = reviewDto.CreatedAt.ToPersianDate();
                }
                ViewBag.CurrentPage = pageNumber;
                ViewBag.TotalPages = totalPages;
                ViewBag.ExpertId = expertId;

               
                return View( result.Data);
            }

           
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;
            ViewBag.ExpertId = expertId;

           

            return View(new ReviewPagedDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Pay(int orderId, CancellationToken cancellationToken) 
        {

            var customerId=  _accountAppService.GetCustomerId(User);
            var resultPay= await _orderAppService.Pay(orderId, customerId, cancellationToken);
            if (!resultPay.IsSuccess)
            {
                TempData["ErrorMessage"] = resultPay.Message;
            }
            else
            {
                TempData["SuccessMessage"] = resultPay.Message;
            }

            return RedirectToAction("MyOrders");
        }

    }
}
