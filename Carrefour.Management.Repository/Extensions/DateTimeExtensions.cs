using System.Runtime.InteropServices;

namespace Carrefour.Management.Repository.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime BrazilTimeZone(this DateTime dateTime)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var timezone = TimeZoneInfo.FindSystemTimeZoneById(isWindows ? "E. South America Standard Time" : "America/Sao_Paulo");
            return TimeZoneInfo.ConvertTime(dateTime, timezone);
        }

        public static string ToDateDiffSttring(this TimeSpan timeSpan)
        {
            var expireDateTime = "";

            var absTimeSpan = timeSpan.Ticks < 0 ? new TimeSpan(Math.Abs(timeSpan.Ticks)) : timeSpan;

            int years = (DateTime.MinValue + absTimeSpan).Year - 1;
            int months = (DateTime.MinValue + absTimeSpan).Month - 1;
            int days = Math.Max(1, (DateTime.MinValue + absTimeSpan).Day - 1);

            if (years > 0)
                expireDateTime += string.Format("{0} ano(s) ", years);

            if (months > 0)
                expireDateTime += string.Format("{0} mês(es) ", months);

            if (days > 0)
                expireDateTime += string.Format("{0} dia(s) ", days);

            return expireDateTime;
        }

        public static DayOfWeek GetPreviousDayOfWeek(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday : dayOfWeek - 1;
        }
    }
}
