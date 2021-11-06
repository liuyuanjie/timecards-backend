using System;

namespace Timecards.Application.Extensions
{
    public static class DateTimeExtensions
    {
        private const byte DaysInWeek = 7;
        
        /// <summary>
        /// Monday is set to the first day in week.
        /// </summary>
        /// <param name="day">The day.</param>
        public static DateTime GetFirstDayOfWeek(this DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? DaysInWeek
                : (byte) day.DayOfWeek) + 1);
        }
    }
}