using System;
using System.Collections.Generic;
using System.Text;

namespace Prophecy.Astronomy
{
    /// <summary>
    /// 节气信息
    /// </summary>
    public class SolarTerm
    {

        public SolarTerm()
        {
            Name = string.Empty;
            YueJian = string.Empty;
        }

        /// <summary>
        /// 节气名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 交节时间(儒历日)
        /// </summary>
        public double JDTime { get; set; }

        /// <summary>
        /// 交节时间串
        /// </summary>
        public JDateTime Time { get; set; }

        /// <summary>
        /// 实历交节时间(儒历日, 精度仅至年月日)
        /// </summary>
        public double HistoricalJDTime { get; set; }

        /// <summary>
        /// 实历交节时间串(仅年月日可用, 时间部分均为 12:00:00)
        /// </summary>
        public JDateTime HistoricalTime { get; set; }

        /// <summary>
        /// 交节时间与实历交节时间是否存在差异的标记
        /// </summary>
        public bool DifferentTime { get; set; }

        /// <summary>
        /// 节气标志:  True - 节, False - 气
        /// </summary>
        public bool IsJie { get; set; }

        /// <summary>
        /// 月建(地支), 仅对节论月建(气不论月建)
        /// </summary>
        public string YueJian { get; set; }

        /// <summary>
        /// 实历月建(地支), 仅对节论月建(气不论月建)
        /// </summary>
        public int DayDifference { get; set; }


    }
}
