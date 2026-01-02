using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا رمز عبور فعلی را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا رمز عبور جدید را وارد کنید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار آن یکسان نیستند")]
        public string ConfirmPassword { get; set; }


        public int AppUserId { get; set; }
    }
}
