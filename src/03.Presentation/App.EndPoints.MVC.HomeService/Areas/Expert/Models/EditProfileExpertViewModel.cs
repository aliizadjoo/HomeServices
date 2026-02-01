using App.Domain.Core.Dtos.CityAgg;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Models
{
    public class EditProfileExpertViewModel
    {

        [Display(Name = "بیوگرافی")]
        [MaxLength(1000, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? Bio { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string LastName { get; set; }

        [Display(Name = "عکس پروفایل")]
        public string? ImagePath { get; set; }

        [Display(Name = "عکس پروفایل")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است")]
        public int CityId { get; set; }

        [Display(Name = "نام شهر")]
        public string? CityName { get; set; }

        public List<string> HomeServices { get; set; } = [];
        public List<int> HomeServicesId { get; set; } = [];

        public List<CityDto> AvailableCities { get; set; } = [];
        public List<HomeserviceDto> AvailableServices { get; set; } = [];

        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "فرمت {0} صحیح نیست (مثال: 09120000000)")]
        public string? PhoneNumber { get; set; }


    }
}
