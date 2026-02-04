using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.WebApI.HomeService.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "نام الزامی است")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [MinLength(3, ErrorMessage = "رمز عبور حداقل 3 کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "نقش الزامی است")]
        public string Role { get; set; }

        [Required(ErrorMessage = "شهر الزامی است")]
        public int CityId { get; set; }

      
    }
}
