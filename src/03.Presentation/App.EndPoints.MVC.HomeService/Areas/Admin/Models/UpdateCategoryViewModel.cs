using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان دسته‌بندی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است.")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "تصویر فعلی")]
        public string? ExistingImagePath { get; set; }

        [Display(Name = "تصویر جدید (اختیاری)")]
        public IFormFile? NewImage { get; set; }
    }
}
