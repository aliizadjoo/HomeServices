using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace App.EndPoints.MVC.HomeService.DataAnnotations
{
    public class FuturePersianDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            try
            {
                var input = ConvertToEnglishDigits(value.ToString()!);

                var parts = input.Split('/');
                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                PersianCalendar pc = new PersianCalendar();
                DateTime dt = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

                if (dt.Date < DateTime.Today)
                {
                    return new ValidationResult("تاریخ انتخاب شده نمی‌تواند در گذشته باشد.");
                }

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("فرمت تاریخ وارد شده معتبر نیست.");
            }
        }

        private static string ConvertToEnglishDigits(string input)
        {
            var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            var arabicDigits = new[] { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

            for (int i = 0; i < 10; i++)
            {
                input = input.Replace(persianDigits[i], i.ToString()[0]);
                input = input.Replace(arabicDigits[i], i.ToString()[0]);
            }

            return input;
        }
    }
}
