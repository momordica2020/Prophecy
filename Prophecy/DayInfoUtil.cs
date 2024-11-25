using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Prophecy
{

    public static class DateTimeExtensions
    {
        /// <summary>
        /// 从儒略日数（JD）初始化一个 DateTime 对象
        /// </summary>
        /// <param name="jd">儒略日数</param>
        /// <returns>返回对应的 DateTime 对象</returns>
        public static DateTime FromJulianDate(this DateTime dateTime, double jd)
        {
            double Y = 2000;        // 年
            double M = 1;           // 月
            double D = 1;           // 日
            double h = 12;          // 时
            double m = 0;           // 分
            double s = 0;           // 秒
            jd += 0.5; // 对JD加0.5，确保我们从午夜12点开始计算
            double A = Math.Floor(jd), F = jd - A;  // 取得日数的整数部分A及小数部分F

            // 如果JD >= 2299161，说明是格里高利历日期
            if (A >= 2299161)
            {
                double D1 = Math.Floor((A - 1867216.25) / 36524.25); // 计算闰年修正
                A += 1 + D1 - Math.Floor(D1 / 4); // 修正JD，得到实际日期
            }
            A += 1524; // 将JD向前移动4年2个月
            Y = Math.Floor((A - 122.1) / 365.25); // 计算年份
            D = A - Math.Floor(365.25 * Y); // 获取剩余的日数
            M = Math.Floor(D / 30.6001); // 计算月份
            D = D - Math.Floor(M * 30.6001); // 计算日期

            Y -= 4716; M--; // 调整年份和月份
            if (M > 12) M -= 12; // 如果月份大于12，说明是新的一年
            if (M <= 2) Y++; // 调整年份

            // 小数部分转为时分秒
            F *= 24; h = Math.Floor(F); F -= h;  // 计算小时
            F *= 60; m = Math.Floor(F); F -= m;  // 计算分钟
            F *= 60; s = Math.Floor(F + 0.5);    // 计算秒，四舍五入

            // 处理小时、分钟、秒的溢出
            if (s >= 60) { s -= 60; m++; }
            if (m >= 60) { m -= 60; h++; }

            // 返回 DateTime 对象
            JulianCalendar julianCalendar = new JulianCalendar();
            return new DateTime((int)Y, (int)M, (int)D, (int)h, (int)m, (int)s);
        }

        /// <summary>
        /// 将 DateTime 转换为儒略日数（JD）
        /// </summary>
        /// <param name="dateTime">需要转换的 DateTime 对象</param>
        /// <returns>返回对应的儒略日数（JD）</returns>
        public static double ToJulianDate(this DateTime dateTime)
        {
            int y = dateTime.Year;
            int m = dateTime.Month;
            int d = dateTime.Day;
            int h = dateTime.Hour;
            int min = dateTime.Minute;
            int sec = dateTime.Second;

            // 格里高利历的算法
            if (m <= 2)
            {
                m += 12;
                y--;
            }

            int A = y / 100;
            int B = 2 - A + A / 4;

            double JD = Math.Floor(365.25 * (y + 4716)) + Math.Floor(30.6001 * (m + 1)) + d + B - 1524.5;

            // 处理时间部分（小时、分钟、秒）
            double dayFraction = (h / 24.0) + (min / 1440.0) + (sec / 86400.0);
            JD += dayFraction;

            return JD;
        }













    }



    /// <summary>
    /// 儒略日 日期元件
    /// </summary>
    public partial class DayInfo
    {

        //public static double Y = 2000;        // 年
        //public static double M = 1;           // 月
        //public static double D = 1;           // 日
        //public static double h = 12;          // 时
        //public static double m = 0;           // 分
        //public static double s = 0;           // 秒
        



        /// <summary>
        /// TD - UT1 计算表
        /// </summary>
        private static double[] dts = new double[] { 
            -4000,108371.7,-13036.80,392.000, 0.0000, -500, 17201.0,  -627.82, 16.170,-0.3413,
            -150, 12200.6,  -346.41,  5.403,-0.1593,  150,  9113.8,  -328.13, -1.647, 0.0377,
             500,  5707.5,  -391.41,  0.915, 0.3145,  900,  2203.4,  -283.45, 13.034,-0.1778,
            1300,   490.1,   -57.35,  2.085,-0.0072, 1600,   120.0,    -9.81, -1.532, 0.1403,
            1700,    10.2,    -0.91,  0.510,-0.0370, 1800,    13.4,    -0.72,  0.202,-0.0193,
            1830,     7.8,    -1.81,  0.416,-0.0247, 1860,     8.3,    -0.13, -0.406, 0.0292,
            1880,    -5.4,     0.32, -0.183, 0.0173, 1900,    -2.3,     2.06,  0.169,-0.0135,
            1920,    21.2,     1.69, -0.304, 0.0167, 1940,    24.2,     1.22, -0.064, 0.0031,
            1960,    33.2,     0.51,  0.231,-0.0109, 1980,    51.0,     1.29, -0.026, 0.0032,
            2000,    63.87,    0.1,   0,     0,      2005
        };

       


        /// <summary>
        /// 二次曲线外推
        /// </summary>
        /// <param name="y"></param>
        /// <param name="jsd"></param>
        /// <returns></returns>
        public static double deltatExt(double y, double jsd)
        {
            double dy = (y - 1820) / 100;
            return -20 + jsd * dy * dy;
        }



        /// <summary>
        /// 计算世界时与原子时之差,传入年
        /// 该函数的目的是计算世界时（UT）与原子时（TAI）之间的差值，通常用于天文计算。
        /// 具体来说，它计算了基于年份的 UTC 和 TAI 之间的时间差（单位：秒）。
        /// 此函数根据不同的年份范围采取不同的计算方式：
        ///对于 2005 年及以后的年份，使用外推公式。函数会根据 2005 年之后的速度和加速度来估计。
        ///对于 2005 年之前的年份，使用已有的时间差数据（例如来自 dts 数组）进行插值计算。
        /// </summary>
        /// <param name="y">年</param>
        /// <returns></returns>
        static double deltatT(double y)
        { 
            if (y >= 2005)
            {
                //sd 是2005年之后几年（一值到y1年）的速度估计。
                //jsd 是y1年之后的加速度估计。瑞士星历表jsd=31,NASA网站jsd=32,skmap的jsd=29
                double y1 = 2014, sd = 0.4, jsd = 31;
                if (y <= y1) 
                    return 64.7 + (y - 2005) * sd;    //直线外推
                double v = deltatExt(y, jsd);        //二次曲线外推
                double dv = deltatExt(y1, jsd) - (64.7 + (y1 - 2005) * sd); //y1年的二次外推与直线外推的差
                if (y < y1 + 100) 
                    v -= dv * (y1 + 100 - y) / 100;
                return v;
            }
            int i;
            double[] d = dts;
            for (i = 0; i < d.Length; i += 5) 
                if (y < d[i + 5]) 
                    break;
            double t1 = (y - d[i]) / (d[i + 5] - d[i]) * 10, t2 = t1 * t1, t3 = t2 * t1;
            return d[i + 1] + d[i + 2] * t1 + d[i + 3] * t2 + d[i + 4] * t3;
        }


        
        /// <summary>
        /// 传入儒略日(J2000起算),计算TD-UT(单位:日)
        /// </summary>
        /// <param name="jd2000">J2000起算的儒略日</param>
        /// <returns></returns>
        public static double deltatT2(double jd2000)
        {
            return deltatT(jd2000 / 365.2425 + 2000) / 86400.0;
        }


        /// <summary>
        /// 星期计算
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static double GetWeekFromJD(double jd)
        {
            return Math.Floor(jd + 1.5) % 7;
        }


        /// <summary>
        /// 求y年m月的第n个星期w的儒略日数
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="n"></param>
        /// <param name="w">星期w</param>
        /// <returns></returns>
        public static double GetJDFromYMNW(double y, double m, double n, double w)
        {
            // 获取该月1号的 DateTime 对象
            DateTime firstDayOfMonth = new DateTime((int)y, (int)m, 1);
            double jd = firstDayOfMonth.ToJulianDate(); // 月首儒略日数
            double w0 = (jd + 1) % 7;  // 月首的星期几
            double r = jd - w0 + 7 * n + w;  // 计算第n个星期w的儒略日数

            if (w >= w0) r -= 7;  // 第1个星期w可能落在上个月，造成多算1周，减去1周
            if (n == 5)
            {
                m++;
                if (m > 12) { m = 1; y++; }  // 下个月
                DateTime nextMonthFirstDay = new DateTime((int)y, (int)m, 1);
                if (r >= nextMonthFirstDay.ToJulianDate()) r -= 7; // 如果r超出本月范围，减去1周
            }

            return r;
        }

        ///// <summary>
        ///// 公历转儒略日
        ///// </summary>
        ///// <param name="y">年</param>
        ///// <param name="m">月</param>
        ///// <param name="d">日</param>
        ///// <returns></returns>
        //public static double GD2JD(double y, double m, double d)
        //{
        //    double n = 0, G = 0;
        //    if (y * 372 + m * 31 + Math.Floor(d) >= 588829) 
        //        G = 1;   //判断是否为格里高利历日1582*372+10*31+15
        //    if (m <= 2) { m += 12; y--; }
        //    if (G != 0)
        //    {
        //        n = Math.Floor(y / 100); 
        //        n = 2 - n + Math.Floor(n / 4);
        //    }    //加百年闰
        //    return Math.Floor(365.25 * (y + 4716) + 0.01) + Math.Floor(30.6001 * (m + 1)) + d + n - 1524.5;
        //}



        ///// <summary>
        ///// 公历转儒略日
        ///// </summary>
        ///// <returns></returns>
        //public static double ToJD()
        //{
        //    return GD2JD(Y, M, D + ((s / 60 + m) / 60 + h) / 24);
        //}



        /////// <summary>
        /////// 儒略日数转公历
        /////// </summary>
        /////// <param name="jd">儒略日</param>
        ////public static void InitFromJD(double jd)
        ////{




        ////}



        /////// <summary>
        /////// 日期转为串
        /////// </summary>
        /////// <returns></returns>
        ////public static string toStr()
        ////{ 
        ////    string Y = "     " + Y, M = "0" + M, D = "0" + D;
        ////    double h = h, m = m, s = Math.Floor(s + .5);
        ////    if (s >= 60) { s -= 60; m++; }
        ////    if (m >= 60) { m -= 60; h++; }
        ////    string hStr = "0" + h, mStr = "0" + m, sStr = "0" + s;
        ////    Y = Y.Substring(Y.Length - 5, 5); 
        ////    M = M.Substring(M.Length - 2, 2); 
        ////    D = D.Substring(D.Length - 2, 2);
        ////    hStr = hStr.Substring(hStr.Length - 2, 2); 
        ////    mStr = mStr.Substring(mStr.Length - 2, 2); 
        ////    sStr = sStr.Substring(sStr.Length - 2, 2);
        ////    return Y + "-" + M + "-" + D + " " + hStr + ":" + mStr + ":" + sStr;
        ////}


        ///// <summary>
        ///// 儒略日数转公历, 并且返回对应的 DateTime 对象
        ///// </summary>
        ///// <param name="jd">儒略日数</param>
        ///// <returns>返回对应的 DateTime 对象</returns>
        //public static DateTime JD2Date(double jd)
        //{
        //    double Y = 2000;        // 年
        //    double M = 1;           // 月
        //    double D = 1;           // 日
        //    double h = 12;          // 时
        //    double m = 0;           // 分
        //    double s = 0;           // 秒
        //    jd += 0.5; // 对JD加0.5，确保我们从午夜12点开始计算
        //    double A = Math.Floor(jd), F = jd - A;  // 取得日数的整数部分A及小数部分F

        //    // 如果JD >= 2299161，说明是格里高利历日期
        //    if (A >= 2299161)
        //    {
        //        double D = Math.Floor((A - 1867216.25) / 36524.25); // 计算闰年修正
        //        A += 1 + D - Math.Floor(D / 4); // 修正JD，得到实际日期
        //    }
        //    A += 1524; // 将JD向前移动4年2个月
        //    Y = Math.Floor((A - 122.1) / 365.25); // 计算年份
        //    D = A - Math.Floor(365.25 * Y); // 获取剩余的日数
        //    M = Math.Floor(D / 30.6001); // 计算月份
        //    D = D - Math.Floor(M * 30.6001); // 计算日期

        //    Y -= 4716; M--; // 调整年份和月份
        //    if (M > 12) M -= 12; // 如果月份大于12，说明是新的一年
        //    if (M <= 2) Y++; // 调整年份

        //    // 小数部分转为时分秒
        //    F *= 24; h = Math.Floor(F); F -= h;  // 计算小时
        //    F *= 60; m = Math.Floor(F); F -= m;  // 计算分钟
        //    F *= 60; s = Math.Floor(F + 0.5);    // 计算秒，四舍五入

        //    // 处理小时、分钟、秒的溢出
        //    if (s >= 60) { s -= 60; m++; }
        //    if (m >= 60) { m -= 60; h++; }

        //    // 返回 DateTime 对象
        //    return new DateTime((int)Y, (int)M, (int)D, (int)h, (int)m, (int)s);
        //}


        ///// <summary>
        ///// 提取jd中的时间(去除日期)
        ///// </summary>
        ///// <param name="jd"></param>
        ///// <returns></returns>
        //public static string GetHHMMSSFromJD(double jd)
        //{ 
        //    double h, m, s;
        //    jd += 0.5; jd = (jd - Math.Floor(jd));
        //    jd *= 24; h = Math.Floor(jd); jd -= h;
        //    jd *= 60; m = Math.Floor(jd); jd -= m;
        //    jd *= 60; s = Math.Floor(jd + 0.5);
        //    if (s >= 60) { s -= 60; m++; }
        //    if (m >= 60) { m -= 60; h++; }
        //    string hStr = "0" + h, mStr = "0" + m, sStr = "0" + s;
        //    return hStr.Substring(hStr.Length - 2, 2) + ':' + mStr.Substring(mStr.Length - 2, 2) + ':' + sStr.Substring(sStr.Length - 2, 2);
        //}









    }
}
