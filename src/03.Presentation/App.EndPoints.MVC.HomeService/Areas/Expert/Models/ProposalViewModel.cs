using App.EndPoints.MVC.HomeService.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Expert.Models
{
    public class ProposalViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "قیمت پیشنهادی (ریال)")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MinPriceBasedOn("BasePrice", ErrorMessage = "قیمت پیشنهادی نباید از قیمت پایه کمتر باشد.")]
        public decimal Price { get; set; }

        [Display(Name = "توضیحات و رزومه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(500, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string Description { get; set; }

        [Display(Name = "تاریخ پیشنهادی برای شروع")]
        [Required(ErrorMessage = "لطفاً {0} را مشخص کنید.")]
        [FuturePersianDate]
        public string SuggestedDateShamsi { get; set; }

        [Display(Name = "ساعت اجرا")]
        [Required(ErrorMessage = "لطفاً {0} را مشخص کنید.")]
        [DataType(DataType.Time)]
        public TimeSpan ExecutionTime { get; set; }
        public decimal BasePrice { get; set; }
        public string? OrderTitle { get; set; }
    }
}
