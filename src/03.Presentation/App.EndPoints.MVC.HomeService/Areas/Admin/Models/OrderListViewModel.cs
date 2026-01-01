using App.Domain.Core.Dtos.OrderAgg;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class OrderListViewModel
    {
        public List<OrderDto> Orders { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
