using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Models
{
    public class EditProfileCustomerViewModel
    {

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string LastName { get; set; }
        public string? ImagePath { get; set; }

        [Display(Name = "تصویر جدید")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public string? Address { get; set; }

       
        public string? CityName { get; set; }


        [Display(Name = "نام شهر")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است")]
        public int CityId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "موجودی کیف پول نمی‌تواند عدد منفی باشد.")]
        public decimal? WalletBalance { get; set; }

        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت {0} صحیح نیست (مثال: 09120000000)")]
        public string? PhoneNumber { get; set; }
    }
}
