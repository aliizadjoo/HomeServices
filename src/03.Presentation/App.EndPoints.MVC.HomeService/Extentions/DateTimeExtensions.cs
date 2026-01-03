using System.Globalization;

namespace App.EndPoints.MVC.HomeService.Extentions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime):00}/{pc.GetDayOfMonth(dateTime):00}";
        }

        public static string ToPersianDate(this DateTime? dateTime)
        {
           
            return dateTime.HasValue ? dateTime.Value.ToPersianDate() : "---";
        }

        public static DateTime ToGregorianDate(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentException("Persian date is null or empty");

          
            persianDate = persianDate
                .Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
                .Replace("۳", "3").Replace("۴", "4").Replace("۵", "5")
                .Replace("۶", "6").Replace("۷", "7").Replace("۸", "8")
                .Replace("۹", "9");

            var parts = persianDate.Split('/');

            if (parts.Length != 3)
                throw new FormatException("Invalid Persian date format. Expected yyyy/MM/dd");

            if (!int.TryParse(parts[0], out int year) ||
                !int.TryParse(parts[1], out int month) ||
                !int.TryParse(parts[2], out int day))
                throw new FormatException("Invalid Persian date numbers");

            PersianCalendar pc = new PersianCalendar();

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            if (day < 1 || day > 31)
                throw new ArgumentOutOfRangeException(nameof(day));

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }


    }
}
