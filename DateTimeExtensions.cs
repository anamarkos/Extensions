using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        // Usage: var day = DateTime.Now.OrdinalSuffix(); // Returns 13th
        public static string OrdinalSuffix(this DateTime datetime)
        {
            int day = datetime.Day;

            if (day % 100 >= 11 && day % 100 <= 13)
                return string.Concat(day, "th");

            switch (day % 10)
            {
                case 1:
                    return string.Concat(day, "st");
                case 2:    
                    return string.Concat(day, "nd");
                case 3:    
                    return string.Concat(day, "rd");
                default:   
                    return string.Concat(day, "th");
            }
        }


        /// <summary>
        /// Gets the start of day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Returns a DateTime for the start of the day 00:00:00</returns>
        public static DateTime GetStartOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// Gets the end of day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Returns a DateTime for the end of the day 23:59:59</returns>
        public static DateTime GetEndOfDay(this DateTime date)
        {
            return date.Date.AddSeconds(86399);
        }


        /// <summary>
        /// Gets the next working day
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Returns a DateTime for the next working day at 9 a.m.</returns>
        public static DateTime GetNextWorkingDay(this DateTime dateTime, TimeSpan hours)
        {
            dateTime = dateTime.Date.AddDays(1);
            while (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                dateTime = dateTime.Date.AddDays(1);
            }
            return dateTime.Date + hours;

        }

        /// <summary>
        /// Checks if the given date time is between working hours
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsWorkingHours(this DateTime dateTime)
        {
            var isWorkingHours = true;
            var WorkingShiftStartUtc = new TimeSpan(7, 0, 0);
            var WorkingShiftEndUtc = new TimeSpan(15, 0, 0);

            if ((dateTime.TimeOfDay < WorkingShiftStartUtc || dateTime.TimeOfDay > WorkingShiftEndUtc) || (dateTime.DayOfWeek == DayOfWeek.Saturday) || (dateTime.DayOfWeek == DayOfWeek.Sunday))
                isWorkingHours = false;
            return isWorkingHours;
        }

        /// <summary>
        /// Returns a list of dates between a range of dates.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d));
        }

        public static List<DateTime> SortAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }

    }
}
