using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarLibrary.Calendars
{
    /// <summary>
    /// 日历基类
    /// 包含公历和农历之间的转换以及节气、星座等功能。
    /// </summary>
    public abstract class Calendar
    {
        /// <summary>
        /// 转换公历日期为农历日期
        /// </summary>
        /// <param name="gregorianDate">公历日期</param>
        /// <returns>农历日期</returns>
        public abstract LunarDate ConvertToLunar(DateTime gregorianDate);

        /// <summary>
        /// 转换农历日期为公历日期
        /// </summary>
        /// <param name="lunarDate">农历日期</param>
        /// <returns>公历日期</returns>
        public abstract DateTime ConvertToGregorian(LunarDate lunarDate);
    }
}
