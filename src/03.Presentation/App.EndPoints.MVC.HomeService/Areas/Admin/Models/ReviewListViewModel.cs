using App.Domain.Core.Dtos.ReviewAgg;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class ReviewListViewModel
    {
        public List<ReviewDto> Reviews { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
