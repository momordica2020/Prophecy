using System;
using System.Globalization;

namespace Prophecy
{
    public partial class JDateTime
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private object updateMutex = new object();


        /// <summary>
        /// 表示日期是否被修改，需要更新其他组件。默认是懒加载，即当某一字段被调用，与之相关的预加载内容才会处理
        /// </summary>
        private bool _needUpdateGeroge = true;
        private bool _needUpdateLunar = true;
        private bool _needUpdateIslamic = true;

        /// <summary>
        /// 在日期发生变动时，通知各个模块可能需要重算缓存的参数
        /// </summary>
        private void UpdateAll()
        {
            _needUpdateGeroge = true;
            _needUpdateLunar = true;
            _needUpdateIslamic = true;
            
        }

        private bool NeedUpdateGerogeDate { get { return _needUpdateGeroge; } }

        private bool NeedUpdateLunar { get { return _needUpdateLunar; } }

        private bool NeedUpdateIslamic { get { return _needUpdateIslamic; } }

        #region 构造函数 


        /// <summary>
        /// 构造函数 - 使用年、月、日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public JDateTime(int year, int month, int day)
        {
            JulianDate = ToJulianDate(year, month, day, 0, 0, 0);
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
            JulianDate = ToJulianDate(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 构造函数 - 使用现有 DateTime 对象
        /// </summary>
        /// <param name="dateTime"></param>
        public JDateTime(DateTime dateTime)
        {
            JulianDate = ToJulianDate(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        /// <summary>
        /// 构造函数 - 使用现有 JDateTime 对象
        /// </summary>
        /// <param name="dateTime"></param>
        public JDateTime(JDateTime dateTime)
        {
            JulianDate = dateTime.JulianDate;
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
                JulianDate = julianDate + JD_2000;
            }
            else
            {
                JulianDate = julianDate;
            }
        }


        /// <summary>
        /// 构造函数——从字符串识别
        /// </summary>
        /// <param name="text"></param>
        public JDateTime(string text)
        {
            if (DateTime.TryParse(text, CultureInfo.InvariantCulture, out var parsed))
            {
                JulianDate = ToJulianDate(parsed.Year, parsed.Month, parsed.Day, parsed.Hour, parsed.Minute, parsed.Second);
            }
            else
            {
                // Throw exception??
                JulianDate = 0;
            }
        }

        #endregion




        #region Add Methods
        public JDateTime Add(TimeSpan value)
        {
           
                JulianDate += value.TotalDays;
            

            return this;// new JDateTime(this.julianDate + value.TotalDays);
        }
        public JDateTime AddDays(double value)
        {
            
                JulianDate += value;
            
            return this;
        }
        public JDateTime AddHours(double value)
        {
            
                JulianDate += value / 24.0;
            
            return this;
        }
        public JDateTime AddMinutes(double value)
        {
            
                JulianDate += value / 1440.0;
            
            return this;
        }
        public JDateTime AddSeconds(double value)
        {
            
                JulianDate += value / 86400.0;
            
            return this;
        }
        public JDateTime AddMilliseconds(double value)
        {
           
                JulianDate += value / 86400000.0;
            
            return this;
        }
        public JDateTime AddTicks(long value)
        {
            
                JulianDate += value / (double)TimeSpan.TicksPerDay;
            
            return this;
        }
        public JDateTime AddMonths(int months)
        {
            int totalMonths = this.GerogeMonth + months;
            int year = this.GerogeYear + (totalMonths - 1) / 12;
            int month = (totalMonths - 1) % 12 + 1;
            lock (updateMutex)
            {
                JulianDate = new JDateTime(year, month, GerogeDay, Hour, Minute, Second).JulianDate;
            }
            return this;
        }
        public JDateTime AddYears(int value)
        {
            return AddMonths(value * 12);
        }


        #endregion


        #region Sub Methods

        public TimeSpan Subtract(JDateTime value)
        {
            return TimeSpan.FromDays(this.JulianDate - value.JulianDate);
        }

        public JDateTime Subtract(TimeSpan value)
        {
            return new JDateTime(this.JulianDate - value.TotalDays);
        }

        #endregion

        #region Compare and Equal Methods

        public static int Compare(JDateTime t1, JDateTime t2)
        {
            if (t1.JulianDate > t2.JulianDate) return 1;
            if (t1.JulianDate < t2.JulianDate) return -1;
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
                return JulianDate.Equals(other.JulianDate);
            }
            return false;
        }

        
        public static bool Equals(JDateTime t1, JDateTime t2)
        {
            return t1.JulianDate == t2.JulianDate;
        }

        

        /// <summary>
        /// 基于日期时间组成部分 Year, Month, Day, Hour, Minute, Second, Millisecond 计算哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(GerogeYear, GerogeMonth, GerogeDay, Hour, Minute, Second, Millisecond);
        }

        #endregion


        /// <summary>
        /// 转化为DateTime对象
        /// </summary>
        public DateTime Date
        {
            get
            {
                if (this.GerogeYear <= 0)
                {
                    throw new InvalidOperationException("Cannot convert BC dates to DateTime.");
                }
                return new DateTime(this.GerogeYear, this.GerogeMonth, this.GerogeDay);
            }
        }

        public static JDateTime UtcNow
        {
            get
            {
                // 使用当前系统时间（UTC 时间）
                DateTime now = DateTime.UtcNow;
                return new JDateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }
        }

        public static JDateTime Now
        {
            get { return UtcNow; }
        }


        #region String <=> JDateTime


        



        /// <summary>
        /// 以DateTime的相同方式解析字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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


        #endregion













    }
}
