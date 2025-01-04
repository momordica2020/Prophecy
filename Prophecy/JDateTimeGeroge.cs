
using System;
using System.Globalization;

namespace Prophecy
{
    public partial class JDateTime
    {
        // 公历相关
        #region GerogeDateStorage

        private int _gyear;
        private int _gmonth;
        private int _gweek;
        private int _gday;

        #endregion

        /// <summary>
        /// 公历年
        /// </summary>
        public int GerogeYear { get { UpdateGerogeDate(); return _gyear; } }

        /// <summary>
        /// 公历月
        /// </summary>
        public int GerogeMonth { get { UpdateGerogeDate(); return _gmonth; } }


        /// <summary>
        /// 公历日
        /// </summary>
        public int GerogeDay { get { UpdateGerogeDate(); return _gday; } }



        /// <summary>
        /// 本公历月的总天数
        /// </summary>
        public int DayCountOfMonth
        {
            get
            {
                if (GerogeMonth == 2) // 判断2月
                {
                    return IsGerogeLeapYear() ? 29 : 28;
                }
                return GerogeMonth switch
                {
                    4 or 6 or 9 or 11 => 30, // 小月
                    _ => 31 // 大月
                };
            }
        }

        /// <summary>
        /// 本月的月首的星期
        /// </summary>
        public int WeekIndexOfMonthStart
        {
            get
            {
                // 获取本月第一天的日期
                var firstDayOfMonth = new JDateTime(GerogeYear, GerogeMonth, 1, 0, 0, 0);
                return (int)firstDayOfMonth.GerogeDayInWeek;
            }
        }

        /// <summary>
        /// 星期
        /// </summary>
        public int GerogeWeek => (int)GerogeDayInWeek;

        /// <summary>
        /// 在本月中的周序号
        /// </summary>
        public int GerogeWeekOfMonth
        {
            get
            {
                // 当前日期距离本月1号的天数
                int daysSinceMonthStart = GerogeDay - 1;

                // 本月第一天的星期索引
                int weekStart = WeekIndexOfMonthStart;

                // (本月第几周 = 从1月1日的天数 + 当前日期天数) ÷ 7
                return (daysSinceMonthStart + weekStart) / 7 + 1;
            }
        }

        /// <summary>
        /// 本月的总周数
        /// </summary>
        public int GerogeWeekCountOfMonth
        {
            get
            {
                // 获取本月总天数和首日星期
                int daysInMonth = DayCountOfMonth;
                int weekStart = WeekIndexOfMonthStart;

                // 总周数 = (本月天数 + 月首星期偏移) / 7（向上取整）
                return (int)Math.Ceiling((daysInMonth + weekStart) / 7.0);
            }
        }


        public DayOfWeek GerogeDayInWeek
        {
            get
            {
                // 星期几：0 (Sunday) 到 6 (Saturday)
                int dayOfWeekIndex = (int)((JulianDate + 1.5) % 7);
                return (DayOfWeek)dayOfWeekIndex;
            }
        }
        public int GerogeDayOfYear
        {
            get
            {
                // 每个月的天数（平年）
                int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                // 闰年调整
                if (IsGerogeLeapYear())
                {
                    daysInMonths[1] = 29;
                }

                // 累加当前日期之前的天数
                int dayOfYear = 0;
                for (int i = 0; i < GerogeMonth - 1; i++)
                {
                    dayOfYear += daysInMonths[i];
                }
                dayOfYear += GerogeDay; // 加上当月的天数
                return dayOfYear;
            }
        }

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        /// <returns></returns>
        private bool IsGerogeLeapYear()
        {
            if (GerogeYear < 0) // 公元前年份同样适用规则
                return ((-GerogeYear) % 4 == 0 && (-GerogeYear) % 100 != 0) || ((-GerogeYear) % 400 == 0);
            return (GerogeYear % 4 == 0 && GerogeYear % 100 != 0) || (GerogeYear % 400 == 0);
        }


        /// <summary>
        /// 格式化 ToString 输出公历信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToStringGeroge("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// 输出公历信息
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToStringGeroge(string format)
        {
            return ToStringGeroge(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 输出公历信息
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToStringGeroge(string format, IFormatProvider formatProvider)
        {
            var culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;
            DateTime dateTime = new DateTime(
                GerogeYear==0?1:Math.Abs(GerogeYear),
                GerogeMonth,
                GerogeDay,
                Hour,
                Minute,
                Second,
                Millisecond
            );

            string result = dateTime.ToString(format, culture);
            if (GerogeYear <= 0)
            {
                string yearString = $"-{Math.Abs(GerogeYear):D4}";
                result = result.Replace(dateTime.Year.ToString("D4"), yearString);
            }
            return result;
        }
    }
}
