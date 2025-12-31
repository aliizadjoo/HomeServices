using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UpdateAdminByAdminViewModel
    {

        public int AppUserId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل (نام کاربری)")]
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "کد پرسنلی")]
        [Required(ErrorMessage = "کد پرسنلی الزامی است")]
        public string StaffCode { get; set; }

        [Display(Name = "کل درآمد")]
        public decimal TotalRevenue { get; set; }

        public string? ExistingImagePath { get; set; }
        public IFormFile? NewProfileImage { get; set; }
    }
}
