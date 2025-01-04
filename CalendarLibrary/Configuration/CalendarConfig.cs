using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarLibrary.Configuration
{
    /// <summary>
    /// 日历配置类
    /// 用于管理日历功能的配置选项
    /// </summary>
    public class CalendarConfig
    {
        /// <summary>
        /// 日历网格显示模式（按天、按周、按月）
        /// </summary>
        public GridMode Grid { get; set; } = GridMode.Month;

        /// <summary>
        /// 第一列显示的星期（0 表示周日，1 表示周一，以此类推）
        /// </summary>
        public int FirstWeek { get; set; } = 0;

        /// <summary>
        /// 时区名称
        /// </summary>
        public string TimeZoneName { get; set; } = TimeZoneInfo.Local.Id;

        /// <summary>
        /// 是否启用节气功能
        /// </summary>
        public bool EnableSolarTerms { get; set; } = true;

        /// <summary>
        /// 是否启用农历功能
        /// </summary>
        public bool EnableLunar { get; set; } = true;

        /// <summary>
        /// 是否启用干支（天干地支）功能
        /// </summary>
        public bool EnableHeavenlyEarthly { get; set; } = true;

        /// <summary>
        /// 是否区分早晚子时
        /// </summary>
        public bool DistinguishZiHour { get; set; } = true;

        /// <summary>
        /// 是否启用星座功能
        /// </summary>
        public bool EnableStarSign { get; set; } = true;

        /// <summary>
        /// 克隆当前配置对象
        /// </summary>
        /// <returns>配置对象的副本</returns>
        public CalendarConfig Clone()
        {
            return new CalendarConfig
            {
                Grid = this.Grid,
                FirstWeek = this.FirstWeek,
                TimeZoneName = this.TimeZoneName,
                EnableSolarTerms = this.EnableSolarTerms,
                EnableLunar = this.EnableLunar,
                EnableHeavenlyEarthly = this.EnableHeavenlyEarthly,
                DistinguishZiHour = this.DistinguishZiHour,
                EnableStarSign = this.EnableStarSign
            };
        }
    }

    /// <summary>
    /// 日历网格显示模式
    /// </summary>
    public enum GridMode
    {
        Day = 0,
        Week = 1,
        Month = 2
    }
}
