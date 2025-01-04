using CalendarLibrary.Julian;
using CalendarLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarLibrary.Astronomy
{
    /// <summary>
    /// 天文学计算模块，包括地球时间差（DeltaT）、摄动、春分点等计算
    /// </summary>
    public static class Astronomy
    {
        /// <summary>
        /// 均值朔望月长(mean length of Synodic Month)
        /// </summary>
        private const double MeanSynodicMonth = 29.530588853; 

        /// <summary>
        /// 以2000年的第一个均值新月点为基准点，此基准点为2000年1月6日14时20分37秒(TT)，
        /// 其对应真实新月点为2000年1月6日18时13分42秒(TT)
        /// </summary>
        private const double BaseNewMoon = 2451550.0976504628; // 基准点儒略日



        /// <summary>
        /// 地球自转速度调整值 DeltaT : 地球时和UTC的时差（单位：天）
        /// </summary>
        public static double DeltaTDays(double year, double month)
        {
            return CUtil.Round(DeltaTSeconds(year, month) / 86400.0, 16);
        }

        /// <summary>
        /// 地球自转速度调整值 DeltaT : 地球时和UTC的时差 （单位：分钟）
        /// </summary>
        public static double DeltaTMinutes(double year, double month)
        {
            return CUtil.Round(DeltaTSeconds(year, month) / 60.0, 16);
        }


        /// <summary>
        /// 计算地球自转速度调整值 DeltaT : 地球时和UTC的时差 （秒）
        /// 计算方法参考: https://eclipse.gsfc.nasa.gov/SEhelp/deltatpoly2004.html
	    /// 此算法在-1999年到3000年之间有效
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>DeltaT 秒数</returns>
        public static double DeltaTSeconds(double year, double month)
        {
            if (year < -1999 || year > 3000)
                throw new ArgumentOutOfRangeException("DeltaT 计算只支持 -1999 至 3000 年");

            double y = year + (month - 0.5) / 12;
            double dt;

            if (year <= -500)
            {
                double u = (year - 1820) / 100;
                dt = -20 + 32 * Math.Pow(u, 2);
            }
            else if (year < 500)
            {
                double u = y / 100;
                dt = 10583.6 - 1014.41 * u + 33.78311 * Math.Pow(u, 2) - 5.952053 * Math.Pow(u, 3) - 0.1798452 * Math.Pow(u, 4) + 0.022174192 * Math.Pow(u, 5) + 0.0090316521 * Math.Pow(u, 6);
            }
            else if (year < 1600)
            {
                double u = (y - 1000) / 100;
                dt = 1574.2 - 556.01 * u + 71.23472 * Math.Pow(u, 2) + 0.319781 * Math.Pow(u, 3) - 0.8503463 * Math.Pow(u, 4) - 0.005050998 * Math.Pow(u, 5) + 0.0083572073 * Math.Pow(u, 6);
            }
            else if (year < 1700)
            {
                double t = y - 1600;
                dt = 120 - 0.9808 * t - 0.01532 * Math.Pow(t, 2) + Math.Pow(t, 3) / 7129;
            }
            else if (year < 1800)
            {
                double t = y - 1700;
                dt = 8.83 + 0.1603 * t - 0.0059285 * Math.Pow(t, 2) + 0.00013336 * Math.Pow(t, 3) - Math.Pow(t, 4) / 1174000;
            }
            else if (year < 1860)
            {
                double t = y - 1800;
                dt = 13.72 - 0.332447 * t + 0.0068612 * Math.Pow(t, 2) + 0.0041116 * Math.Pow(t, 3) - 0.00037436 * Math.Pow(t, 4) + 0.0000121272 * Math.Pow(t, 5) - 0.0000001699 * Math.Pow(t, 6) + 0.000000000875 * Math.Pow(t, 7);
            }
            else if (year < 1900)
            {
                double t = y - 1860;
                dt = 7.62 + 0.5737 * t - 0.251754 * Math.Pow(t, 2) + 0.0017275 * Math.Pow(t, 3) + 0.000651814 * Math.Pow(t, 4) + 0.00002373599 * Math.Pow(t, 5) + 0.01680668 * Math.Pow(t, 3) - 0.0004473624 * Math.Pow(t, 4) + Math.Pow(t, 5) / 233174;
            }
            else if (year < 1920)
            {
                double t = y - 1900;
                dt = -2.79 + 1.494119 * t - 0.0598939 * Math.Pow(t, 2) + 0.0061966 * Math.Pow(t, 3) - 0.000197 * Math.Pow(t, 4);
            }
            else if (year < 1941)
            {
                double t = y - 1920;
                dt = 21.2 + 0.84493 * t - 0.0761 * Math.Pow(t, 2) + 0.0020936 * Math.Pow(t, 3);
            }
            else if (year < 1961)
            {
                double t = y - 1950;
                dt = 29.07 + 0.407 * t - Math.Pow(t, 2) / 233 + Math.Pow(t, 3) / 2547;
            }
            else if (year < 1986)
            {
                double t = y - 1975;
                dt = 45.45 + 1.067 * t - Math.Pow(t, 2) / 260 - Math.Pow(t, 3) / 718;
            }
            else if (year < 2005)
            {
                double t = y - 2000;
                dt = 63.86 + 0.3345 * t - 0.060374 * Math.Pow(t, 2) + 0.0017275 * Math.Pow(t, 3) + 0.000651814 * Math.Pow(t, 4) + 0.00002373599 * Math.Pow(t, 5);
            }
            else if (year < 2050)
            {
                double t = y - 2000;
                dt = 62.92 + 0.32217 * t + 0.005589 * Math.Pow(t, 2);
            }
            else if (year < 2150)
            {
                double u = (y - 1820) / 100;
                dt = -20 + 32 * Math.Pow(u, 2) - 0.5628 * (2150 - y);
            }
            else
            {
                double u = (y - 1820) / 100;
                dt = -20 + 32 * Math.Pow(u, 2);
            }

            // 以上的∆T值均假定月球的长期加速度为-26弧秒/cy^2
            // 而Canon中使用的ELP-2000/82月历使用的值略有不同，为-25.858弧秒/cy^2
            // 因此，必须在∆T多项式表达式得出的值上加上一个小的修正“c”，然后才能将其用于标准中
            // 由于1955年至2005年期间的ΔT值是独立于任何月历而得出的，因此该期间无需校正。
            if (year < 1955 || year >= 2005)
            {
                var c = -0.000012932 * (y - 1955) * (y - 1955);
                dt += c;
            }

            return dt; // 保留6位小数
        }


        /// <summary>
        /// 计算地球在绕日运行时会因受到其他星球之影响而产生摄动(perturbation)（单位：天）
        /// 算法公式摘自Jean Meeus在1991年出版的《Astronomical Algorithms》第27章 Equinoxes and solsticesq (第177页)
        /// http://www.agopax.it/Libri_astronomia/pdf/Astronomical%20Algorithms.pdf
        /// 公式: 0.00001S/∆λ
        /// S = Σ[A cos(B + CT)]
        /// B和C的单位是度
        /// T = JDE0 - J2000 / 36525
        /// J2000 = 2451545.0
        /// 36525是儒略历一个世纪的天数
        /// ∆λ = 1 + 0.0334cosW+0.0007cos2W
        /// W = (35999.373T - 2.47)π/180
        /// 注释: Liu Min<liujiawm@163.com> https://github.com/liujiawm
        /// </summary>
        /// <returns>某时刻(儒略日历)的摄动偏移量</returns>
        public static double Perturbation(double jd)
        {
            double[] ptsA = { 485, 203, 199, 182, 156, 136, 77, 74, 70, 58, 52, 50, 45, 44, 29, 18, 17, 16, 14, 12, 12, 12, 9, 8 };
            double[] ptsB = { 324.96, 337.23, 342.08, 27.85, 73.14, 171.52, 222.54, 296.72, 243.58, 119.81, 297.17, 21.02, 247.54, 325.15, 60.93, 155.12, 288.79, 198.04, 199.76, 95.39, 287.11, 320.81, 227.73, 15.45 };
            double[] ptsC = { 1934.136, 32964.467, 20.186, 445267.112, 45036.886, 22518.443, 65928.934, 3034.906, 9037.513, 33718.147, 150.678, 2281.226, 29929.562, 31555.956, 4443.417, 67555.328, 4562.452, 62894.029, 31436.921, 14577.848, 31931.756, 34777.259, 1222.114, 16859.074 };

            double T = JulianConverter.JulianCentury(jd);
            double S = 0;

            for (int k = 0; k < 24; k++)
            {
                S += ptsA[k] * Math.Cos(ptsB[k] * 2 * Math.PI / 360 + ptsC[k] * 2 * Math.PI / 360 * T);
            }

            double W = (35999.373 * T - 2.47) * 2 * Math.PI / 360;
            double L = 1 + 0.0334 * Math.Cos(W) + 0.0007 * Math.Cos(2 * W);

            return CUtil.Round(0.00001 * S / L, 16);
        }

        /// <summary>
        /// 计算指定年的春分点（儒略日）
        /// 算法公式摘自Jean Meeus在1991年出版的《Astronomical Algorithms》第27章 Equinoxes and solsticesq (第177页)
        /// http://www.agopax.it/Libri_astronomia/pdf/Astronomical%20Algorithms.pdf
        /// 此公式在-1000年至3000年之间比较准确
        /// 在公元前1000年之前或公元3000年之后也可以延申使用，但因外差法求值，年代越远，算出的结果误差就越大。
        /// </summary>
        /// <returns>春分点儒略日</returns>
        public static double VernalEquinox(double year)
        {
            if (year >= 1000 && year <= 3000)
            {
                double m = (year - 2000) / 1000.0;
                return CUtil.Round(2451623.80984 + 365242.37404 * m + 0.05169 * Math.Pow(m, 2) - 0.00411 * Math.Pow(m, 3) - 0.00057 * Math.Pow(m, 4), 10);
            }
            else
            {
                double m = year / 1000.0;
                return CUtil.Round(1721139.29189 + 365242.1374 * m + 0.06134 * Math.Pow(m, 2) + 0.00111 * Math.Pow(m, 3) - 0.00071 * Math.Pow(m, 4), 10);
            }
        }



        /// <summary>
        /// 计算实际新月点
        /// 以2000年初的第一个均值新月点为0点求出的均值新月点和其朔望月之序數 k 代入此副程式來求算实际新月点
        /// 对于指定的日期时刻JD值jd,算出其为相对于基准点(之后或之前)的第k个朔望月之内。
        /// k=INT(jd-bnm)/msm
        /// 新月点估值(new moon estimated)为：nme=bnm+msm×k
        /// 估计的世纪变数值为：t = (nme - J2000) / 36525
        /// 此t是以2000年1月1日12时(TT)为0点，以100年为单位的时间变数，
        /// 由于朔望月长每个月都不同，msm所代表的只是其均值，所以算出新月点后，还需要加上一个调整值。
        /// adj = 0.0001337×t×t - 0.00000015×t×t×t + 0.00000000073×t×t×t×t
        /// 指定日期时刻所属的均值新月点JD值(mean new moon)：mnm=nme+adj
        /// </summary>
        public static double TrueNewMoon(double k)
        {
            double nme = NewMoonEstimated(k);
            double t = JulianConverter.JulianCentury(nme);
            double t2 = Math.Pow(t, 2);
            double t3 = Math.Pow(t, 3);
            double t4 = Math.Pow(t, 4);

            // 均值新月点
            double mnm = nme + 0.0001337 * t2 - 0.00000015 * t3 + 0.00000000073 * t4;

            // 太阳的平均近点角(从太阳观察)
            double m = 2.5534 + 29.10535669 * k - 0.0000218 * t2 - 0.00000011 * t3;

            // 月球的平均近点角(从地球观察)
            double ms = 201.5643 + 385.81693528 * k + 0.0107438 * t2 + 0.00001239 * t3 - 0.000000058 * t4;

            // 月球的纬度参数
            double f = 160.7108 + 390.67050274 * k - 0.0016341 * t2 - 0.00000227 * t3 + 0.000000011 * t4;

            // 月球绕日运行轨道升交点之经度
            double omega = 124.7746 - 1.5637558 * k + 0.0020691 * t2 + 0.00000215 * t3;

            // 乘式因子
            double e = 1 - 0.002516 * t - 0.0000074 * t2;

            // 摄动校正
            var pi180 = Math.PI / 180;
            double apt1 = - 0.4072 * Math.Sin(pi180*ms)
                          + 0.17241 * e * Math.Sin( pi180*m)
                          + 0.01608 * Math.Sin( pi180*2 * ms)
                          + 0.01039 * Math.Sin( pi180*2 * f)
                          + 0.00739 * e * Math.Sin( pi180*(ms - m))
                          - 0.00514 * e * Math.Sin( pi180*(ms + m))
                          + 0.00208 * e * e * Math.Sin( pi180*(2 * m))
                          - 0.00111 * Math.Sin( pi180*(ms - 2 * f))
                          - 0.00057 * Math.Sin( pi180*(ms + 2 * f))
                          + 0.00056 * e * Math.Sin( pi180*(2 * ms + m))
                          - 0.00042 * Math.Sin( pi180*3 * ms)
                          + 0.00042 * e * Math.Sin( pi180*(m + 2 * f))
                          + 0.00038 * e * Math.Sin( pi180*(m - 2 * f))
                          - 0.00024 * e * Math.Sin( pi180*(2 * ms - m))
                          - 0.00017 * Math.Sin( pi180*omega)
                          - 0.00007 * Math.Sin( pi180*(ms + 2 * m))
                          + 0.00004 * Math.Sin( pi180*(2 * ms - 2 * f))
                          + 0.00004 * Math.Sin( pi180*(3 * m))
                          + 0.00003 * Math.Sin( pi180*(ms + m - 2 * f))
                          + 0.00003 * Math.Sin( pi180*(2 * ms + 2 * f))
                          - 0.00003 * Math.Sin( pi180*(ms + m + 2 * f))
                          + 0.00003 * Math.Sin( pi180*(ms - m + 2 * f))
                          - 0.00002 * Math.Sin( pi180*(ms - m - 2 * f))
                          - 0.00002 * Math.Sin( pi180*(3 * ms + m))
                          + 0.00002 * Math.Sin( pi180*(4 * ms));


            double apt2 = 0.000325 * Math.Sin( pi180*(299.77 + 0.107408 * k - 0.009173 * t2))
                          + 0.000165 * Math.Sin( pi180*(251.88 + 0.016321 * k))
                          + 0.000164 * Math.Sin( pi180*(251.83 + 26.651886 * k))
                          + 0.000126 * Math.Sin( pi180*(349.42 + 36.412478 * k))
                          + 0.00011 * Math.Sin( pi180*(84.66 + 18.206239 * k))
                          + 0.000062 * Math.Sin( pi180*(141.74 + 53.303771 * k))
                          + 0.00006 * Math.Sin( pi180*(207.14 + 2.453732 * k))
                          + 0.000056 * Math.Sin( pi180*(154.84 + 7.30686 * k))
                          + 0.000047 * Math.Sin( pi180*(34.52 + 27.261239 * k))
                          + 0.000042 * Math.Sin( pi180*(207.19 + 0.121824 * k))
                          + 0.00004 * Math.Sin( pi180*(291.34 + 1.844379 * k))
                          + 0.000037 * Math.Sin( pi180*(161.72 + 24.198154 * k))
                          + 0.000035 * Math.Sin( pi180*(239.56 + 25.513099 * k))
                          + 0.000023 * Math.Sin( pi180*(331.55 + 3.592518 * k));



            return CUtil.Round(mnm + apt1 + apt2, 10);
        }

        /// <summary>
        /// 对于指定的日期时刻 JD 值，计算其为基准点(之后或之前)的第几个朔望月
        /// 为从2000年1月6日14时20分36秒起至指定年月日之农历月数,以synodic month为单位
        /// </summary>
        public static double ReferenceLunarMonthNum(double jd)
        {
            return Math.Floor((jd - BaseNewMoon) / MeanSynodicMonth);
        }

        /// <summary>
        /// 计算新月点估值
        /// </summary>
        public static double NewMoonEstimated(double k)
        {
            return BaseNewMoon + MeanSynodicMonth * k;
        }

        /// <summary>
        /// 计算指定日期所属的朔望月,求出其均值新月点的月序数
        /// </summary>
        public static (double K, double JD) MeanNewMoon(double jd)
        {
            double k = ReferenceLunarMonthNum(jd);
            double nme = NewMoonEstimated(k);
            double t = JulianConverter.JulianCentury(nme);
            double jdValue = nme + 0.0001337 * Math.Pow(t, 2) - 0.00000015 * Math.Pow(t, 3) + 0.00000000073 * Math.Pow(t, 4);

            return (k, CUtil.Round(jdValue, 10));
        }

        /// <summary>
        /// 获取指定年从春分开始的 24+2=26 节气时间(为了确保覆盖完一个公历年，该方法多取2个节气)
        /// 注意：该方法取出的节气时间是未经微调的
        /// </summary>
        public static double[] MeanSolarTermsJd(double year)
        {
            double ve = VernalEquinox(year);    // 该年的春分点jd
            double ty = VernalEquinox(year + 1) - ve; // 回归年长度 两个春分点之间为一个回归年长
            double ath = 2 * Math.PI / 24;

            double T = JulianConverter.JulianThousandYear(ve);
            double e = 0.0167086342 - 0.0004203654 * T - 0.0000126734 * Math.Pow(T, 2) + 0.0000001444 * Math.Pow(T, 3) - 0.0000000002 * Math.Pow(T, 4) + 0.0000000003 * Math.Pow(T, 5);
            
            double TT = year / 1000;
            double d = 111.25586939 - 17.0119934518333 * TT - 0.044091890166673 * Math.Pow(TT, 2) - 4.37356166661345E-04 * Math.Pow(TT, 3) + 8.16716666602386E-06 * Math.Pow(TT, 4);
            
            var rvp = d * 2 * Math.PI / 360;

            double[] peri = new double[26];
            for(int i=0;i<peri.Length;i++)
            {
                var flag = 0;
                var th = ath * (double)i + rvp;
                if(th>Math.PI && th<=3 * Math.PI)
                {
                    th = 2 * Math.PI - th;
                    flag = 1;
                }
                if(th > 3 * Math.PI)
                {
                    th = 4 * Math.PI - th;
                    flag = 2;
                }
                var f1 = 2 * Math.Atan(Math.Sqrt((1-e)/(1+e)) * Math.Tan(th /2));
                var f2 = (e * Math.Sqrt(1 - e * e) * Math.Sin(th)) / (1 + e * Math.Cos(th));
                var f = (f1 - f2) * ty / 2 / Math.PI;
                if (flag == 1) f = ty - f;
                else if (flag == 2) f = 2 * ty - f;
                peri[i] = f;
            }


            double[] mst = new double[26];

            for (int i = 0; i < peri.Length; i++)
            {
                mst[i] = CUtil.Round(ve + peri[i] - peri[0], 10);
            }

            return mst;
        }

        /// <summary>
        /// 获取指定年以春分开始的,经过摄动值和deltaT调整后的指定个数节气
        /// </summary>
        /// <param name="year"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double[] AdjustedSolarTermsJd(double year, int start, int end)
        {
            var mst = MeanSolarTermsJd(year);
            double[] jqs = new double[26];
            for (int i = 0; i < mst.Length; i++)
            {
                if (i < start || i > end) continue;
                // 取得受perturbation影响所需微调
                var pert = Perturbation(mst[i]);

                // 修正dynamical time to Universal time
                var month = Math.Floor((((double)i) + 1) / 2) + 3;
                var dtd = DeltaTDays(year, month);

                // 加上摄动调整值ptb,减去对应的Delta T值(分钟转换为日)
                jqs[i] = CUtil.Round(mst[i] + pert - dtd, 10);
            }

            return jqs;
        }

        /// <summary>
        /// 计算上一年从冬至开始的6个节气
        /// </summary>
        public static double[] LastYearSolarTerms(double year)
        {
            return AdjustedSolarTermsJd(year - 1, 18, 23);
        }
    }
}
