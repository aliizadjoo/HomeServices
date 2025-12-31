using App.Domain.Core.Dtos.CategoryAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class HomeServiceListViewModel
    {
        public List<HomeserviceDto> Services { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

       
        public List<CategoryDto> Categories { get; set; }
    }
}
