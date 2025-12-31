using App.Domain.Core.Dtos.CategoryAgg;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class CategoryListViewModel
    {

        public List<CategoryDto> Categories { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
