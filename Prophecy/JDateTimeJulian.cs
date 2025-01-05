using System;

namespace Prophecy
{
    public partial class JDateTime
    {


        #region 儒略日相关
       

        /// <summary>
        /// 一年的天数（儒略历平均值）
        /// </summary>
        public const double DaysPerYear = 365.25;

        /// <summary>
        /// 公元2000年1月1日0时的儒略日数值
        /// </summary>
        public const double JD_2000 = 2451545.0;

        /// <summary>
        /// 回历起点的儒略日数（622年7月16日，午夜）
        /// </summary>
        public const double JD_ISLAMIC_EPOCH = 1948439.66667;






        /// <summary>
        /// 使用儒略日来统一化记录当前时刻。
        /// </summary>
        private double _julianDate;


        /// <summary>
        /// 距 J2000.0 的世纪数
        /// </summary>
        public double JulianCentury
        {
            get
            {
                return JulianDateFrom2000 / DaysPerYear / 100;
            }
            
        }

        /// <summary>
        //  距 J2000.0 的千年数
        /// </summary>
        public double JulianThousandYear
        {
            get
            {
                return JulianDateFrom2000 / DaysPerYear / 1000;
            }
        }


        /// <summary>
        /// 今天0时的儒略日值
        /// </summary>
        public double JulianDate0
        {
            get
            {
                return new JDateTime(GerogeYear, GerogeMonth, GerogeDay, 0, 0, 0).JulianDate;
            }
        }

        /// <summary>
        /// 今天子时的儒略日值
        /// </summary>
        public double JulianDateLunar0
        {
            get
            {
                return JulianDate0 - 1d / 24;
            }
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
        public static double ToJulianDate(int year, int month, int day, int hour, int minute, int second)
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
        /// 当前日期的儒略日数值
        /// </summary>
        /// <returns></returns>
        public double JulianDate
        {
            get
            {
                return _julianDate;
            }
            set
            {
                lock (updateMutex)
                {
                    _julianDate = value;
                    UpdateAll();
                }
            }
        }

        /// <summary>
        /// 当前日期从2000年起算的儒略日数值
        /// </summary>
        public double JulianDateFrom2000
        {
            get
            {
                return JulianDate - JD_2000;
            }
        }

        /// <summary>
        /// 当前日期所在公历年的起始（1月1日00:00）儒略日
        /// </summary>
        public double JulianDateYearBegin
        {
            get
            {
                return new JDateTime(this.GerogeYear, 1, 1, 0, 0, 0).JulianDate;
            }
        }

        ///// <summary>
        ///// 将儒略日转换为年月日、时分秒
        ///// </summary>
        ///// <param name="jd"></param>
        ///// <returns></returns>
        //public static DateTime JDToGregorian(double jd)
        //{
        //    var jdt = FromJulianDate(jd);
        //    return new DateTime(jdt.year, jdt.month, jdt.day, jdt.hour, jdt.minute, jdt.second, jdt.millisecond);
        //}

        /// <summary>
        /// 将儒略日转换为年月日、时分秒
        /// </summary>
        /// <returns></returns>
        private void UpdateGerogeDate()
        {
            if (!NeedUpdateGerogeDate) return;
            lock (updateMutex)
            {
                if (!NeedUpdateGerogeDate) return;

                double jd = JulianDate + 0.5;
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

                _gday = B - D - (int)(30.6001 * E);
                _gmonth = (E < 14) ? E - 1 : E - 13;
                _gyear = (_gmonth > 2) ? C - 4716 : C - 4715;

                // 处理时间部分
                double dayFraction = F * 24;
                _ghour = (int)Math.Floor(dayFraction);
                double minuteFraction = (dayFraction - _ghour) * 60;
                _gminute = (int)Math.Floor(minuteFraction);
                double secondFraction = (minuteFraction - _gminute) * 60;
                _gsecond = (int)Math.Floor(secondFraction);
                _gmillisecond = (int)((secondFraction - _gsecond) * 1000);


                //int second = (int)Math.Round((minuteFraction - minute) * 60);

                if (_gsecond >= 60)
                {
                    _gsecond -= 60;
                    _gminute++;
                }
                if (_gminute >= 60)
                {
                    _gminute -= 60;
                    _ghour++;
                }

                _needUpdateGeroge = false;
            }


            //return (year, month, day, hour, minute, second, millisecond);
        }



        /// <summary>
        /// 转换当前时间为儒略日数
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <returns></returns>
        public static double ToJulianDate(int year, int month, int day)
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



        #endregion

    }
}
