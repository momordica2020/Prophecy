using System;
using System.Globalization;

namespace Prophecy
{

    public partial class JDateTime
    {

        #region IslamicCalendar




        /// <summary>
        /// 从回历年月日构建 JDateTime 实例。
        /// </summary>
        /// <param name="year">回历年</param>
        /// <param name="month">回历月（1-12）</param>
        /// <param name="day">回历日（1-30）</param>
        /// <returns>对应的 JDateTime 实例</returns>
        public static JDateTime FromIslamicDate(int year, int month, int day)
        {
            // 转换为儒略日
            double julianDate = IslamicToJulianDate(year, month, day);

            // 构造 JDateTime
            return new JDateTime(julianDate);
        }

        /// <summary>
        /// 判断回历年份是否是闰年。
        /// </summary>
        private static bool IsIslamicLeapYear(int year)
        {
            int mod = (year * 11 + 14) % 30;
            return mod < 11; // 闰年规则
        }

        /// <summary>
        /// 将回历年月日转换为儒略日数（精确）。
        /// </summary>
        public static double IslamicToJulianDate(int year, int month, int day)
        {
            // 计算到前一年的总天数
            int y = year - 1;
            int daysFromYears = y * 354 + (int)Math.Floor((3 + 11 * y) / 30.0); // 含闰年

            // 计算到前一月的天数
            int daysFromMonths = 0;
            for (int m = 1; m < month; m++)
            {
                daysFromMonths += (m % 2 == 1) ? 30 : 29; // 奇数月30天，偶数月29天
            }

            // 如果是闰年且是第12月，则第12月为30天
            if (month == 12 && IsIslamicLeapYear(year))
            {
                daysFromMonths++;
            }

            // 总天数
            int totalDays = daysFromYears + daysFromMonths + (day - 1);

            // 转为儒略日
            return JD_ISLAMIC_EPOCH + totalDays;
        }
        /// <summary>
        /// 回历年份
        /// </summary>
        public int IslamicYear
        {
            get
            {
                UpdateIslamicDate();
                return _islamicYear;
            }
        }

        /// <summary>
        /// 回历月份
        /// </summary>
        public int IslamicMonth
        {
            get
            {
                UpdateIslamicDate();
                return _islamicMonth;
            }
        }

        /// <summary>
        /// 回历日期
        /// </summary>
        public int IslamicDay
        {
            get
            {
                UpdateIslamicDate();
                return _islamicDay;
            }
        }

        #region Islamic Storage

        private int _islamicYear;
        private int _islamicMonth;
        private int _islamicDay;


        #endregion



        /// <summary>
        /// 计算回历的年月日
        /// </summary>
        void UpdateIslamicDate()
        {
            if (!_needUpdateIslamic) return;
            lock (updateMutex)
            {
                if (!_needUpdateIslamic) return;


                double z, y, m, d;
                d = JulianDateFrom2000 + 503105; z = Math.Floor((d + 0.1) / 10631);   // 10631为一周期(30年)
                d -= z * 10631; y = Math.Floor((d + 0.5) / 354.366);  // 加0.5的作用是保证闰年正确(一周中的闰年是第2,5,7,10,13,16,18,21,24,26,29年)
                d -= Math.Floor(y * 354.366 + 0.5); m = Math.Floor((d + 0.11) / 29.51);   // 分子加0.11,分每加0.01的作用是第354或355天的的月分保持为12月(m=11)
                d -= Math.Floor(m * 29.51 + 0.5);
                _islamicYear = (int)(z * 30 + y + 1);
                _islamicMonth = (int)(m + 1);
                if (_islamicMonth <= 0) _islamicMonth = 12;
                _islamicDay = (int)(d + 1);
                if (_islamicDay <= 0) _islamicDay = 30;

                //double jd = JulianDate;
                //double daysSinceEpoch = jd - JD_ISLAMIC_EPOCH;

                //// 回历年份计算
                //_islamicYear = (int)Math.Floor((30.0 * daysSinceEpoch + 10646.0) / 10631.0);
                //double startOfYear = JD_ISLAMIC_EPOCH + Math.Floor((10631.0 * _islamicYear - 10646.0) / 30.0);
                //double daysInYear = jd - startOfYear;

                //// 回历月份和日期计算
                //int[] islamicMonthLengths = { 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 30, 29 };
                //double dayCounter = daysInYear;
                //for (int i = 0; i < 12; i++)
                //{
                //    if (dayCounter < islamicMonthLengths[i])
                //    {
                //        _islamicMonth = i + 1;
                //        _islamicDay = (int)dayCounter + 1;
                //        break;
                //    }
                //    dayCounter -= islamicMonthLengths[i];
                //}

                _needUpdateIslamic = false;
            }
            //return (year, month, day);
        }


        ///// <summary>
        ///// 回历计算2
        ///// </summary>
        //public (int Year, int Month, int Day) GetIslamicDate2()
        //{
        //    // 以下算法使用Excel测试得到,测试时主要关心年临界与月临界
        //    double d = JulianDateFrom2000 + 503105; 
        //    double z = Math.Floor((d + 0.1) / 10631);   // 10631为一周期(30年)
        //    d -= z * 10631; 
        //    double y = Math.Floor((d + 0.5) / 354.366);  // 加0.5的作用是保证闰年正确(一周期中的闰年是第2,5,7,10,13,16,18,21,24,26,29年)
        //    d -= Math.Floor(y * 354.366 + 0.5); 
        //    double m = Math.Floor((d + 0.11) / 29.51);   // 分子加0.11,分每加0.01的作用是第354或355天的的月分保持为12月(m=11)
        //    d -= Math.Floor(m * 29.51 + 0.5);
        //    int IslamicYear = (int)(z * 30 + y + 1);
        //    int IslamicMonth = (int) m + 1;
        //    int IslamicDay = (int)d + 1;
        //    return (IslamicYear, IslamicMonth, IslamicDay);

        //}


        /// <summary>
        /// 将当前 JDateTime 转换为回历格式的字符串
        /// </summary>
        /// <param name="format">回历格式化字符串，例如 "yyyy-MM-dd HH:mm:ss"</param>
        /// <param name="formatProvider">格式化提供程序</param>
        /// <returns>格式化的回历日期字符串</returns>
        public string ToStringIslamic(string format, IFormatProvider formatProvider = null)
        {
            // 如果未提供格式化器，使用默认的文化信息
            var culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;


            // 构造虚拟的 DateTime 对象来格式化时间部分
            DateTime fakeDateTime = new DateTime(
                Math.Abs(IslamicYear),
                IslamicMonth,
                IslamicDay,
                Hour,
                Minute,
                Second,
                Millisecond
            );

            // 使用格式化器格式化时间部分
            string result = fakeDateTime.ToString(format, culture);

            // 如果是公元前的年份，手动替换年份部分
            if (IslamicYear <= 0)
            {
                string yearString = $"-{Math.Abs(IslamicYear):D4}";
                result = result.Replace(fakeDateTime.Year.ToString("D4"), yearString);
            }

            return result;
        }







        #endregion IslamicCalendar

    }
}
