using App.Domain.Core.Contract.OrderAgg.AppService;
using App.Domain.Core.Dtos.OrderAgg;
using App.EndPoints.MVC.HomeService.Areas.Admin.Models;
using App.EndPoints.MVC.HomeService.Areas.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize(Roles = RoleConstants.Admin)]
    public class OrderController(IOrderAppService _orderAppService) : Controller
    {
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var result = await _orderAppService.GetAll(pageNumber, pageSize, cancellationToken);
            var totalCount = await _orderAppService.GetCount(cancellationToken);

            var viewModel = new OrderListViewModel
            {
                Orders = result.Data ,
                CurrentPage = pageNumber,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(viewModel);
        }
    }
}
