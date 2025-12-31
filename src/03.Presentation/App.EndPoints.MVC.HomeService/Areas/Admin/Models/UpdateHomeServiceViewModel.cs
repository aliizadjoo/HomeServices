using App.Domain.Core.Dtos.CategoryAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UpdateHomeServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام سرویس")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public string Description { get; set; }

        [Display(Name = "قیمت پایه (ریال)")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        public decimal BasePrice { get; set; }

        public string? ExistingImagePath { get; set; }

        [Display(Name = "تصویر جدید (اختیاری)")]
        public IFormFile? NewImageFile { get; set; }

        [Display(Name = "دسته‌بندی")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است.")]
        public int CategoryId { get; set; }

        public List<CategoryDto>? AvailableCategories { get; set; }
    }
}
