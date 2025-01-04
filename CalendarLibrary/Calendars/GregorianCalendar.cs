namespace CalendarLibrary.Calendars
{
    /// <summary>
    /// 公历日历类
    /// 包括公历的日期操作和生成功能。
    /// </summary>
    public class GregorianCalendar : Calendar
    {
        /// <summary>
        /// 转换公历日期为农历日期
        /// </summary>
        /// <param name="gregorianDate">公历日期</param>
        /// <returns>农历日期</returns>
        public override LunarDate ConvertToLunar(DateTime gregorianDate)
        {
            // 示例代码 - 实现应根据农历转换算法
            // 此处使用伪代码，请用真实农历转换逻辑替换
            int lunarYear = gregorianDate.Year;
            int lunarMonth = gregorianDate.Month;
            int lunarDay = gregorianDate.Day;

            return new LunarDate
            {
                Year = lunarYear - 1, // 简单示例
                Month = lunarMonth,
                Day = lunarDay,
                IsLeapMonth = false,
                Animal = "鼠",
                HeavenlyStem = "甲",
                EarthlyBranch = "子"
            };
        }

        /// <summary>
        /// 转换农历日期为公历日期
        /// </summary>
        /// <param name="lunarDate">农历日期</param>
        /// <returns>公历日期</returns>
        public override DateTime ConvertToGregorian(LunarDate lunarDate)
        {
            // 示例代码 - 实现应根据农历转换算法
            // 此处使用伪代码，请用真实农历转换逻辑替换
            return new DateTime(lunarDate.Year + 1, lunarDate.Month, lunarDate.Day);
        }

        /// <summary>
        /// 获取星座
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>星座名称</returns>
        public string GetStarSign(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;

            return month switch
            {
                1 => day <= 19 ? "摩羯座" : "水瓶座",
                2 => day <= 18 ? "水瓶座" : "双鱼座",
                3 => day <= 20 ? "双鱼座" : "白羊座",
                4 => day <= 19 ? "白羊座" : "金牛座",
                5 => day <= 20 ? "金牛座" : "双子座",
                6 => day <= 21 ? "双子座" : "巨蟹座",
                7 => day <= 22 ? "巨蟹座" : "狮子座",
                8 => day <= 22 ? "狮子座" : "处女座",
                9 => day <= 22 ? "处女座" : "天秤座",
                10 => day <= 23 ? "天秤座" : "天蝎座",
                11 => day <= 22 ? "天蝎座" : "射手座",
                12 => day <= 21 ? "射手座" : "摩羯座",
                _ => "未知星座"
            };
        }


        public List<string> GenerateWithDate(int year, int month, int day)
        {
            // 简单示例逻辑
            DateTime targetDate = new DateTime(year, month, day);
            List<string> calendar = new List<string>();

            // 生成一周的日历
            DateTime startOfWeek = targetDate.AddDays(-(int)targetDate.DayOfWeek);
            for (int i = 0; i < 7; i++)
            {
                calendar.Add(startOfWeek.AddDays(i).ToShortDateString());
            }

            return calendar;
        }


        public List<(string Name, DateTime Date)> GetSolarTerms(int year)
        {
            // 示例逻辑，实际需要节气算法
            List<(string Name, DateTime Date)> solarTerms = new List<(string Name, DateTime Date)>
    {
        ("立春", new DateTime(year, 2, 3)),
        ("雨水", new DateTime(year, 2, 18)),
        // 添加其他节气
    };

            return solarTerms;
        }
    }
}
