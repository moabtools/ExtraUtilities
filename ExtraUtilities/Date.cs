using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtilities
{
    public static class Date
    {
        /// <summary>
        /// Возвращает последнюю секунду дня исходной даты (23:59:59)
        /// </summary>
        /// <param name="dateTime">Исходная дата</param>
        /// <returns>Последняя секунда исходной даты</returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        /// <summary>
        /// Возвращает первую секунду дня исходной даты (0:00:00)
        /// </summary>
        /// <param name="dateTime">Исходная дата</param>
        /// <returns>Первая секунда дня исходной даты</returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Конвертирует дату в Unix Timestamp (количество секунд, прошедшее с 00:00:00 UTC on 1 January 1970)
        /// </summary>
        /// <param name="dateTime">Исходная дата</param>
        /// <returns>Unix Timestamp</returns>
        public static int ConvertToUnixTimestamp(this DateTime dateTime)
        {
            TimeSpan span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)span.TotalSeconds;
        }

        /// <summary>
        /// Конвертирует Unix Timestamp в дату
        /// </summary>
        /// <param name="timestamp">Unix Timestamp</param>
        /// <returns>Дата</returns>
        public static DateTime ConvertFromUnixTimestamp(this int timestamp)
        {
            DateTime startDate = new (1970, 1, 1, 0, 0, 0);
            return startDate.AddSeconds(timestamp);
        }
    }
}
