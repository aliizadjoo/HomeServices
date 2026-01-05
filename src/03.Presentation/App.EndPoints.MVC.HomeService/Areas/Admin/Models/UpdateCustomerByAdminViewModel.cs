using App.Domain.Core.Dtos.CityAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UpdateCustomerByAdminViewModel
    {
      
        public int AppUserId { get; set; }

      
        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل (نام کاربری)")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت وارد شده برای {0} صحیح نیست")]
        public string Email { get; set; }


        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "موجودی کیف پول")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} نمی‌تواند عدد منفی باشد")]
        public decimal? WalletBalance { get; set; }

        [Display(Name = "شهر")]
        public int CityId { get; set; }

     
        public string? ExistingImagePath { get; set; } 
        public IFormFile? NewProfileImage { get; set; } 

        
        public List<CityDto>? Cities { get; set; }
    }
}
