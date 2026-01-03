using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Identity.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل الزامی است")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
