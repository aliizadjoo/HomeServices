using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Models
{
    public class CreateReviewViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "امتیاز الزامی است")]
        [Range(1, 5, ErrorMessage = "امتیاز باید بین 1 تا 5 باشد")]
        public int Score { get; set; }

        [Required(ErrorMessage = "نظر خود را وارد کنید")]
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}
