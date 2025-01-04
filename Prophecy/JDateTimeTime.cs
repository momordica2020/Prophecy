using System;

namespace Prophecy
{

    public partial class JDateTime
    {
        #region 时分秒部分


        private int _ghour;
        private int _gminute;
        private int _gsecond;
        private int _gmillisecond;
        /// <summary>
        /// 小时
        /// </summary>
        public int Hour { get { UpdateGerogeDate(); return _ghour; } }

        /// <summary>
        /// 分钟
        /// </summary>
        public int Minute { get { UpdateGerogeDate(); return _gminute; } }

        /// <summary>
        /// 秒
        /// </summary>
        public int Second { get { UpdateGerogeDate(); return _gsecond; } }

        /// <summary>
        /// 毫秒
        /// </summary>
        public int Millisecond => (int)((GetTimeFraction(JulianDate).totalSeconds % 1) * 1000);

        /// <summary>
        /// 今日已过的时间TimeSpan
        /// </summary>
        public TimeSpan TimeOfDay => new TimeSpan(Hour, Minute, Second);

        private (double totalHours, double totalMinutes, double totalSeconds) GetTimeFraction(double jd)
        {
            jd += 0.5;
            double fractionalDay = jd - Math.Floor(jd);
            double totalSeconds = fractionalDay * 86400; // Total seconds in a day
            double totalMinutes = totalSeconds / 60;
            double totalHours = totalMinutes / 60;
            return (totalHours, totalMinutes, totalSeconds);
        }



        #endregion

    }
}
