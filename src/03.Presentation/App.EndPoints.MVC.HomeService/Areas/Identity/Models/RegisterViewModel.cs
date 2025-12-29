using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Identity.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "نام الزامی است")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن برابری نمی‌کنند")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "انتخاب نقش الزامی است")]
        [Display(Name = "نوع کاربری")]
        public string Role { get; set; }

        [Required(ErrorMessage = "انتخاب شهر الزامی است")]
        [Display(Name = "شهر")]
        public int CityId { get; set; }


    }
}
