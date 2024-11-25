using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy
{
    public class JDateTime
    {
        private double julianDate; // 使用儒略日数表示时间

        /// <summary>
        /// 构造函数 - 使用年、月、日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public JDateTime(int year, int month, int day)
        {
            this.julianDate = ToJulianDate(year, month, day, 0, 0, 0);
        }

        /// <summary>
        /// 构造函数 - 使用年、月、日、时、分、秒
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public JDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.julianDate = ToJulianDate(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 构造函数 - 使用现有 DateTime 对象
        /// </summary>
        /// <param name="dateTime"></param>
        public JDateTime(DateTime dateTime)
        {
            this.julianDate = ToJulianDate(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        /// <summary>
        /// 构造函数 - 使用儒略日直接构造
        /// </summary>
        /// <param name="julianDate"></param>
        /// <param name="from2000">是否从2000年起算？</param>
        public JDateTime(double julianDate, bool from2000 = false)
        {
            if (from2000)
            {
                this.julianDate = julianDate + 2451545.0;
            }
            else
            {
                this.julianDate = julianDate;
            }

        }
        //public JDateTime(double j2000Days)
        //{
        //    // J2000 起算的儒略日转为标准儒略日
        //    double jd = j2000Days + 2451545.0;


        //}
        /// <summary>
        /// 转换当前时间为儒略日数
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        private static double ToJulianDate(int year, int month, int day)
        {
            if (month <= 2)
            {
                month += 12;
                year--;
            }

            double A = Math.Floor(year / 100.0);
            double B = 2 - A + Math.Floor(A / 4.0);

            return Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
        }


        /// <summary>
        /// 私有方法 - 将日期转换为儒略日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static double ToJulianDate(int year, int month, int day, int hour, int minute, int second)
        {
            if (month <= 2)
            {
                year--;
                month += 12;
            }

            int A = year / 100;
            int B = 2 - A + A / 4;

            // 公元前日期不应用格里高利历改正项
            if (year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day < 15))))
            {
                B = 0;
            }

            double julianDay = Math.Floor(365.25 * (year + 4716))
                             + Math.Floor(30.6001 * (month + 1))
                             + day + B - 1524.5;

            double dayFraction = (hour + (minute / 60.0) + (second / 3600.0)) / 24.0;

            return julianDay + dayFraction;
        }

        /// <summary>
        /// 将当前 JDateTime 对象转换为儒略日
        /// </summary>
        /// <returns>当前日期的儒略日数</returns>
        public double ToJulianDate()
        {
            return julianDate;
        }
        private static DateTime JDToGregorian(double jd)
        {
            double Z = Math.Floor(jd + 0.5);
            double F = (jd + 0.5) - Z;

            double A;
            if (Z < 2299161)
            {
                A = Z;
            }
            else
            {
                double alpha = Math.Floor((Z - 1867216.25) / 36524.25);
                A = Z + 1 + alpha - Math.Floor(alpha / 4);
            }

            double B = A + 1524;
            double C = Math.Floor((B - 122.1) / 365.25);
            double D = Math.Floor(365.25 * C);
            double E = Math.Floor((B - D) / 30.6001);

            double day = B - D - Math.Floor(30.6001 * E) + F;
            int month = (E < 14) ? (int)(E - 1) : (int)(E - 13);
            int year = (month > 2) ? (int)(C - 4716) : (int)(C - 4715);

            double dayFraction = day - Math.Floor(day);
            double hours = dayFraction * 24;
            int hour = (int)Math.Floor(hours);
            double minutes = (hours - hour) * 60;
            int minute = (int)Math.Floor(minutes);
            double seconds = (minutes - minute) * 60;
            int second = (int)Math.Floor(seconds);
            int millisecond = (int)((seconds - second) * 1000);

            return new DateTime(year, month, (int)Math.Floor(day), hour, minute, second, millisecond);
        }
        /// <summary>
        /// 将儒略日转换为年月日、时分秒
        /// </summary>
        /// <param name="julianDate"></param>
        /// <returns></returns>
        private (int year, int month, int day, int hour, int minute, int second) FromJulianDate(double julianDate)
        {
            double jd = julianDate + 0.5;
            int Z = (int)Math.Floor(jd);
            double F = jd - Z;

            int A;
            if (Z < 2299161)
            {
                A = Z;
            }
            else
            {
                int alpha = (int)((Z - 1867216.25) / 36524.25);
                A = Z + 1 + alpha - alpha / 4;
            }

            int B = A + 1524;
            int C = (int)((B - 122.1) / 365.25);
            int D = (int)(365.25 * C);
            int E = (int)((B - D) / 30.6001);

            int day = B - D - (int)(30.6001 * E);
            int month = (E < 14) ? E - 1 : E - 13;
            int year = (month > 2) ? C - 4716 : C - 4715;

            // 处理时间部分
            double dayFraction = F * 24;
            int hour = (int)Math.Floor(dayFraction);
            double minuteFraction = (dayFraction - hour) * 60;
            int minute = (int)Math.Floor(minuteFraction);
            int second = (int)Math.Round((minuteFraction - minute) * 60);

            if (second >= 60)
            {
                second -= 60;
                minute++;
            }
            if (minute >= 60)
            {
                minute -= 60;
                hour++;
            }

            return (year, month, day, hour, minute, second);
        }

        public int Year => FromJulianDate(julianDate).year;
        public int Month => FromJulianDate(julianDate).month;
        public int Day => FromJulianDate(julianDate).day;
        public int Hour => FromJulianDate(julianDate).hour;
        public int Minute => FromJulianDate(julianDate).minute;
        public int Second => FromJulianDate(julianDate).second;
        public int Millisecond => (int)((GetTimeFraction(julianDate).totalSeconds % 1) * 1000);

        public TimeSpan TimeOfDay => new TimeSpan(Hour, Minute, Second);
        public DateTime Date => new DateTime(Year, Month, Day);

        private (double totalHours, double totalMinutes, double totalSeconds) GetTimeFraction(double jd)
        {
            jd += 0.5;
            double fractionalDay = jd - Math.Floor(jd);
            double totalSeconds = fractionalDay * 86400; // Total seconds in a day
            double totalMinutes = totalSeconds / 60;
            double totalHours = totalMinutes / 60;
            return (totalHours, totalMinutes, totalSeconds);
        }

        public JDateTime Add(TimeSpan value)
        {
            this.julianDate += value.TotalDays;
            return this;// new JDateTime(this.julianDate + value.TotalDays);
        }
        public JDateTime AddDays(double value)
        {
            this.julianDate += value;
            return this;
        }
        public JDateTime AddHours(double value)
        {
             this.julianDate += value / 24.0;
            return this;
        }
        public JDateTime AddMinutes(double value)
        {
            this.julianDate += value / 1440.0;
            return this;
        }
        public JDateTime AddSeconds(double value)
        {
            this.julianDate += value / 86400.0;
            return this;
        }
        public JDateTime AddMilliseconds(double value)
        {
             this.julianDate += value / 86400000.0;
            return this;
        }
        public JDateTime AddTicks(long value)
        {
            this.julianDate += value / (double)TimeSpan.TicksPerDay;
            return this;
        }
        public JDateTime AddMonths(int months)
        {
            int totalMonths = this.Month + months;
            int year = this.Year + (totalMonths - 1) / 12;
            int month = (totalMonths - 1) % 12 + 1;
            this.julianDate = new JDateTime(year, month, this.Day, Hour, Minute, Second).ToJulianDate();
            return this;
        }
        public JDateTime AddYears(int value)
        {
            return AddMonths(value * 12);
        }





       

        public TimeSpan Subtract(JDateTime value)
        {
            return TimeSpan.FromDays(this.julianDate - value.julianDate);
        }

        public JDateTime Subtract(TimeSpan value)
        {
            return new JDateTime(this.julianDate - value.TotalDays);
        }



        public static int Compare(JDateTime t1, JDateTime t2)
        {
            if (t1.ToJulianDate() > t2.ToJulianDate()) return 1;
            if (t1.ToJulianDate() < t2.ToJulianDate()) return -1;
            return 0;
        }

        public int CompareTo(JDateTime value)
        {
            return Compare(this, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is JDateTime other)
            {
                return ToJulianDate().Equals(other.ToJulianDate());
            }
            return false;
        }

        
        public static bool Equals(JDateTime t1, JDateTime t2)
        {
            return t1.ToJulianDate() == t2.ToJulianDate();
        }

        public override int GetHashCode()
        {
            // 基于日期时间组成部分计算哈希值
            return HashCode.Combine(Year, Month, Day, Hour, Minute, Second, Millisecond);
        }

        /// <summary>
        /// 转换为标准的 DateTime 对象（公元前日期将抛出异常）
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            if (this.Year <= 0)
            {
                throw new InvalidOperationException("Cannot convert BC dates to DateTime.");
            }
            return new DateTime(this.Year, this.Month, this.Day);
        }
        // 格式化 ToString
        public override string ToString()
        {
            return ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.InvariantCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;
            DateTime dateTime = new DateTime(Math.Abs(Year), Month, Day, Hour, Minute, Second, Millisecond);

            string result = dateTime.ToString(format, culture);
            if (Year <= 0)
            {
                string yearString = $"-{Math.Abs(Year):D4}";
                result = result.Replace(dateTime.Year.ToString("D4"), yearString);
            }
            return result;
        }
        // Parse method for creating JDateTime
        public static JDateTime Parse(string s)
        {
            DateTime parsed = DateTime.Parse(s, CultureInfo.InvariantCulture);
            return new JDateTime(parsed.Year, parsed.Month, parsed.Day, parsed.Hour, parsed.Minute, parsed.Second);
        }
        public static JDateTime ParseExact(string s, string format, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException("Input string or format is null or empty.");

            DateTime parsedDateTime = DateTime.ParseExact(s, format, provider);
            return new JDateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, parsedDateTime.Hour, parsedDateTime.Minute, parsedDateTime.Second);
        }
        public static bool TryParse(string s, out JDateTime result)
        {
            result = null;
            if (DateTime.TryParse(s, out DateTime parsed))
            {
                result = new JDateTime(parsed.Year, parsed.Month, parsed.Day, parsed.Hour, parsed.Minute, parsed.Second);
                return true;
            }
            return false;
        }

        public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles styles, out JDateTime result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(s) || formats == null || formats.Length == 0)
                return false;

            if (DateTime.TryParseExact(s, formats, provider, styles, out DateTime parsedDateTime))
            {
                result = new JDateTime(parsedDateTime.Year, parsedDateTime.Month, parsedDateTime.Day, parsedDateTime.Hour, parsedDateTime.Minute, parsedDateTime.Second);
                return true;
            }

            return false;
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                // 计算 Julian Day
                double julianDay = ToJulianDate();
                // 星期几：0 (Sunday) 到 6 (Saturday)
                int dayOfWeekIndex = (int)((julianDay + 1.5) % 7);
                return (DayOfWeek)dayOfWeekIndex;
            }
        }
        public int DayOfYear
        {
            get
            {
                // 每个月的天数（平年）
                int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                // 闰年调整
                if (IsLeapYear())
                {
                    daysInMonths[1] = 29;
                }

                // 累加当前日期之前的天数
                int dayOfYear = 0;
                for (int i = 0; i < Month - 1; i++)
                {
                    dayOfYear += daysInMonths[i];
                }
                dayOfYear += Day; // 加上当月的天数
                return dayOfYear;
            }
        }

        // 判断是否为闰年
        private bool IsLeapYear()
        {
            if (Year < 0) // 公元前年份同样适用规则
                return ((-Year) % 4 == 0 && (-Year) % 100 != 0) || ((-Year) % 400 == 0);
            return (Year % 4 == 0 && Year % 100 != 0) || (Year % 400 == 0);
        }
        public static JDateTime Now
        {
            get
            {
                // 使用当前系统时间（UTC 时间）
                DateTime now = DateTime.UtcNow;
                return new JDateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }
        }
    }
}
