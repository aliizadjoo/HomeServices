using App.Domain.Core.Dtos.CategoryAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class CreateHomeServiceViewModel
    {
        [Display(Name = "نام سرویس")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public string Description { get; set; }

        [Display(Name = "قیمت پایه (ریال)")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [Range(1000, 1000000000, ErrorMessage = "قیمت باید بین ۱,۰۰۰ تا ۱,۰۰۰,۰۰۰,۰۰۰ ریال باشد.")]
        public decimal BasePrice { get; set; }

        [Display(Name = "تصویر سرویس")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "دسته‌بندی")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public int CategoryId { get; set; }

        
        public List<CategoryDto>? AvailableCategories { get; set; }
    }
}
