using App.Domain.Core.Dtos.CityAgg;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class CreateUserBaseViewModel
    {
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

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن برابر نیستند")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "انتخاب شهر الزامی است")]
        public int CityId { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public IFormFile? ProfileImage { get; set; }

        [Display(Name = "نقش کاربری")]
        [Required(ErrorMessage = "انتخاب نقش الزامی است")]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }
    }
}
