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
    }
}
