using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarLibrary.Utils;

namespace CalendarLibrary.Julian
{
    /// <summary>
    /// 提供儒略日（Julian Day）与公历日期的转换工具
    /// </summary>
    public static class JulianConverter
    {

        /// <summary>
        /// J2000.0 的儒略日（2000年1月1日中午12点）
        /// </summary>
        public static readonly double JulianDayJ2000 = 2451545.0; 


        /// <summary>
        /// 一年的天数（儒略历平均值）
        /// </summary>
        public static readonly double DaysPerYear = 365.25; 

        /// <summary>
        /// 将公历日期转换为儒略日
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <param name="hour">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        /// <returns>儒略日</returns>
        public static double ToJulianDay(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            // 如果月份为1或2，需要将年份减1，月份加12
            if (month <= 2)
            {
                year--;
                month += 12;
            }

            // 儒略历和格里历的分界点
            int A = year / 100;
            int B = 2 - A + (A / 4);

            double dayFraction = hour / 24.0 + minute / 1440.0 + second / 86400.0;

            // 计算儒略日
            double julianDay = Math.Floor(365.25 * (year + 4716))
                               + Math.Floor(30.6001 * (month + 1))
                               + day + B - 1524.5 + dayFraction;

            return CUtil.Round(julianDay, 10); // 保留10位小数
        }

        /// <summary>
        /// 将儒略日转换为公历日期
        /// </summary>
        /// <param name="julianDay">儒略日</param>
        /// <returns>公历日期</returns>
        public static DateTime FromJulianDay(double julianDay)
        {
            julianDay += 0.5;
            int Z = (int)Math.Floor(julianDay);
            double F = julianDay - Z;

            int A = Z;
            if (Z >= 2299161)
            {
                int alpha = (int)((Z - 1867216.25) / 36524.25);
                A += 1 + alpha - alpha / 4;
            }

            int B = A + 1524;
            int C = (int)((B - 122.1) / DaysPerYear);
            int D = (int)(DaysPerYear * C);
            int E = (int)((B - D) / 30.6001);

            int day = B - D - (int)(30.6001 * E);
            int month = (E < 14) ? E - 1 : E - 13;
            int year = (month > 2) ? C - 4716 : C - 4715;

            double dayFraction = F * 24.0;
            int hour = (int)Math.Floor(dayFraction);
            dayFraction = (dayFraction - hour) * 60.0;
            int minute = (int)Math.Floor(dayFraction);
            dayFraction = (dayFraction - minute) * 60.0;
            int second = (int)Math.Round(dayFraction);

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 将日期时间对象转换为儒略日
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns>儒略日</returns>
        public static double ToJulianDay(DateTime dateTime)
        {
            return ToJulianDay(dateTime.Year, dateTime.Month, dateTime.Day,
                               dateTime.Hour, dateTime.Minute, dateTime.Second);
        }


        /// <summary>
        /// 计算指定儒略日距 J2000.0 的儒略日
        /// </summary>
        /// <param name="julianDay">儒略日</param>
        /// <returns>距 J2000.0 的儒略日</returns>
        public static double Julian2000(double julianDay)
        {
            return julianDay - JulianDayJ2000;
        }


        /// <summary>
        /// 计算指定儒略日距 J2000.0 的世纪数
        /// </summary>
        /// <param name="julianDay">儒略日</param>
        /// <returns>儒略世纪数</returns>
        public static double JulianCentury(double julianDay)
        {
            return Julian2000(julianDay) / DaysPerYear / 100;
        }

        /// <summary>
        /// 计算指定儒略日距 J2000.0 的千年数
        /// </summary>
        /// <param name="julianDay">儒略日</param>
        /// <returns>儒略千年数</returns>
        public static double JulianThousandYear(double julianDay)
        {
            return Julian2000(julianDay) / DaysPerYear / 1000;
        }

    }
}
