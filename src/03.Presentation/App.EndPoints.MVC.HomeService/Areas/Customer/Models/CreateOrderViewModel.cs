using App.EndPoints.MVC.HomeService.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Customer.Models
{
    public class CreateOrderViewModel
    {

        public int HomeServiceId { get; set; }

        [Display(Name = "نام سرویس")]
        public string? ServiceName { get; set; } 

        [Display(Name = "قیمت پایه")]
        public string? BasePrice { get; set; } 

      

        [Display(Name = "توضیحات سفارش")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
        [MaxLength(1000, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string Description { get; set; }

        [Display(Name = "تاریخ اجرا")]
        [Required(ErrorMessage = "لطفاً {0} را مشخص کنید.")]
        [FuturePersianDate]
        public string ExecutionDateShamsi { get; set; }

        [Display(Name = "ساعت اجرا")]
        [Required(ErrorMessage = "لطفاً {0} را مشخص کنید.")]
        [DataType(DataType.Time)]
        public TimeSpan ExecutionTime { get; set; }

        [Display(Name = "شهر محل انجام")]
        [Required(ErrorMessage = "لطفاً {0} را انتخاب کنید.")]
        public int CityId { get; set; }

     
        public List<SelectListItem>? Cities { get; set; }

       

        [Display(Name = "تصاویر مربوط به سفارش")]
      
        public List<IFormFile>? ImageFiles { get; set; }
    }
}


