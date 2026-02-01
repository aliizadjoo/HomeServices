using App.Domain.Core.Dtos.CityAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UpdateExpertByAdminViewModel
    {
        public int AppUserId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل (نام کاربری)")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت وارد شده برای {0} صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "بیوگرافی")]
        [MaxLength(1000, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? Bio { get; set; }

        [Display(Name = "موجودی کیف پول")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} نمی‌تواند عدد منفی باشد")]
        public decimal WalletBalance { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است")]
        public int CityId { get; set; }

        public string? ExistingImagePath { get; set; }
        public IFormFile? NewProfileImage { get; set; }

        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت {0} صحیح نیست (مثال: 09120000000)")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "تخصص‌ها")]
        public List<int> SelectedHomeServicesId { get; set; } = new();

        public List<CityDto> AvailableCities { get; set; } = new();
        public List<HomeserviceDto> AvailableServices { get; set; } = new();
    }
}
