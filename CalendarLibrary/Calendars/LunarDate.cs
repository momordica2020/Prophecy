namespace CalendarLibrary.Calendars
{
    /// <summary>
    /// 农历日期类
    /// 表示农历日期，包括年、月、日、生肖等信息。
    /// </summary>
    public record LunarDate
    {
        public int Year { get; init; } // 农历年
        public int Month { get; init; } // 农历月
        public int Day { get; init; } // 农历日
        public bool IsLeapMonth { get; init; } // 是否为闰月

        public string Animal { get; init; } // 生肖
        public string HeavenlyStem { get; init; } // 天干
        public string EarthlyBranch { get; init; } // 地支

        public override string ToString()
        {
            return $"{Year}年 {Month}月{(IsLeapMonth ? "（闰）" : "")} {Day}日 ({HeavenlyStem}{EarthlyBranch}年, {Animal})";
        }
    }
}
