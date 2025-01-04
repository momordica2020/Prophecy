using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Prophecy.Astronomy;

namespace Prophecy.Data
{




    /// <summary>
    /// 日月的升中天降,不考虑气温和气压的影响
    /// </summary>
    public class RaiseFallInfo
    {

        /// <summary>
        /// 站点地理经度,向东测量为负
        /// </summary>
        private double LocalJ = 0;             //站点地理经度,向东测量为负

        /// <summary>
        /// 站点地理纬度
        /// </summary>
        private double LocalW = 0;            //站点地理纬度

        /// <summary>
        /// TD-UT
        /// </summary>
        private double DeltaT = 0;            //TD-UT

        /// <summary>
        /// 黄赤交角
        /// </summary>
        private double Ecliptic = 0.409092614;   //黄赤交角

        //public Coordinate coordinate;


        ///// <summary>
        ///// 日出时的天体时角
        ///// </summary>
        //public double H0;
        ///// <summary>
        ///// 日落时的天体时角
        ///// </summary>
        //public double H1;
        ///// <summary>
        ///// 天体时角 Hour Angle
        ///// 定义为天子午圈与天体的赤经圈在北极所成的球面角，或在天赤道上所夹的弧度。
        ///// 时角的单位通常采用相应的时间单位，以0~24时取代0°~360°
        ///// </summary>
        //public double H;


        /// <summary>
        /// 日升jd
        /// </summary>
        public double SunRaise;

        /// <summary>
        /// 日中jd
        /// </summary>
        public double SunMiddle;

        /// <summary>
        /// 日降jd
        /// </summary>
        public double SunFall;
        /// <summary>
        /// 月升jd
        /// </summary>
        public double MoonRaise;

        /// <summary>
        /// 月中jd
        /// </summary>
        public double MoonMiddle;

        /// <summary>
        /// 月降jd
        /// </summary>
        public double MoonFall;
        /// <summary>
        /// 昼jd
        /// </summary>
        public double morning;

        /// <summary>
        /// 夜jd
        /// </summary>
        public double nightfall;

        /// <summary>
        /// 光照时长
        /// </summary>
        public double lightLength { get { return nightfall - morning; } }     // -0.5?

        /// <summary>
        /// 昼长
        /// </summary>
        public double dayLength { get { return SunFall - SunRaise; } } // -0.5?

        // 多天的升中降



        //public SunMoonRaiseFall(double localJ, double localW)
        //{
        //    this.LocalJ = localJ;
        //    this.LocalW = localW;
        //}

        /// <summary>
        /// 多天升中降计算,jd是当地起始儒略日(中午时刻),sq是时区
        /// </summary>
        /// <param name="jd2000">起始儒略日</param>
        /// <param name="n">天数</param>
        /// <param name="coord">经纬度</param>
        /// <param name="sq">时区偏移</param>
        public static List<RaiseFallInfo> GetSunMoonRaiseFall(double jd2000, double n, Coordinate coord, double sq)
        {
            var rts = new List<RaiseFallInfo>();
            sq /= 24; //设置站点参数




            // LocalJ = Jdl;
            //LocalW = Wdl; 


            for (int i = -1; i <= n; i++)
            {
                if (i >= 0 && i < n)
                {
                    //太阳   -sq????
                    var rfInfo = new RaiseFallInfo(coord, jd2000 + i + sq);
                    rfInfo.SunRaise -= sq;
                    rfInfo.SunMiddle -= sq;
                    rfInfo.SunFall -= sq;

                    rfInfo.MoonRaise -= sq;
                    rfInfo.MoonMiddle -= sq;
                    rfInfo.MoonFall -= sq;

                    rfInfo.morning -= sq;
                    rfInfo.nightfall -= sq;

                    rts.Add(rfInfo);
                }
                //var moonRF = new RaiseFallInfo(Jdl, Wdl, jd2000 + i + sq, false);
                //var dayIndex = Math.Floor(moonRF.raise - sq + 0.5) - jd2000; if (dayIndex >= 0 && dayIndex < n) (rts[(int)dayIndex]).Ms = new JDateTime(r.raise - sq);
                //dayIndex = Math.Floor(moonRF.middleday - sq + 0.5) - jd2000; if (dayIndex >= 0 && dayIndex < n) (rts[(int)dayIndex]).Mz = new JDateTime(r.middleday - sq);
                //dayIndex = Math.Floor(r.fall - sq + 0.5) - jd2000; if (dayIndex >= 0 && dayIndex < n) (rts[(int)dayIndex]).Mj = new JDateTime(r.fall - sq);

            }
            //rts.dn = n;
            return rts;
        }




        /// <summary>
        /// h地平纬度,w赤纬,返回时角
        /// </summary>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public double getH(double h, double w)
        {
            double c = (Math.Sin(h) - Math.Sin(LocalW) * Math.Sin(w)) / Math.Cos(LocalW) / Math.Cos(w);
            if (Math.Abs(c) > 1) return Math.PI;
            return Math.Acos(c);
        }





        /// <summary>
        /// 日章动 同时影响恒星时和天体坐标,所以不计算章动。返回时角及赤经纬
        /// </summary>
        /// <param name="jd2000"></param>
        /// <param name="h0"></param>
        /// <param name="h1"></param>
        public (double H, double H0, double H1) SunCoord(double jd2000)
        {
            Coordinate c = new Coordinate
            {
                J = AstronomyOld.E_Lon((jd2000 + DeltaT) / 36525, 5) + Math.PI - 20.5 / Util.rad, //太阳坐标(修正了光行差)
                W = 0,
                R = 1
            };
            c = AstronomyOld.llrConv(c, Ecliptic);       // 转为赤道坐标

            var H = Util.rad2rrad(AstronomyOld.gst(jd2000, DeltaT) - LocalJ - c.J); //得到此刻天体时角
            var H0 = getH(-50 * 60 / Util.rad, c.W);                  //地平以下50分
            var H1 = getH(-Math.PI / 30, c.W);                        // 地平以下6度
            //coordinate = c;

            return (H, H0, H1);

        }


        /// <summary>
        /// 月章动 同时影响恒星时和天体坐标,所以不计算章动。返回时角及赤经纬
        /// </summary>
        /// <param name="jd2000">J2000起算的儒略日</param>
        /// <returns>H 赤经纬，H0 升起对应的时角</returns>
        public (double H, double H0) MoonCoord(double jd2000)
        {

            var c = AstronomyOld.M_coord((jd2000 + DeltaT) / 36525, 30, 20, 8); //低精度月亮赤经纬
            c = AstronomyOld.llrConv(c, Ecliptic); //转为赤道坐标

            //coordinate = c;
            var H = Util.rad2mrad(AstronomyOld.gst(jd2000, DeltaT) - LocalJ - c.J);
            var H0 = getH(0.7275 * Util.cs_rEar / c.R - 34 * 60 / Util.rad, c.W); //升起对应的时角
            return (H, H0);
        }



        /// <summary>
        /// 月亮到中升降时刻计算,传入jd含义与St()函数相同
        /// </summary>
        /// <param name="coord">经纬度</param>
        /// <param name="jd2000">儒略日时刻</param>
        public RaiseFallInfo(Coordinate coord, double jd2000)
        {
            LocalJ = coord.J;
            LocalW = coord.W;
            DeltaT = AstronomyOld.deltatT2(jd2000);
            Ecliptic = AstronomyOld.ObliquityOfEcliptic(jd2000 / 36525);

            // 日
            //查找最靠近当日中午的日上中天,mod2的第1参数为本地时角近似值
            var jd2000Sun = jd2000 - Util.mod2(jd2000 - LocalJ / (Math.PI * 2), 1);
            SunRaise = SunMiddle = SunFall = morning = nightfall = jd2000Sun;
            double H, H0, H1;
            (H, H0, H1) = SunCoord(jd2000Sun);       //太阳坐标
            SunRaise += (-H0 - H) / (Math.PI * 2);   //升起
            SunFall += (H0 - H) / (Math.PI * 2);    //降落
            morning += (-H1 - H) / (Math.PI * 2);   //民用晨
            nightfall += (H1 - H) / (Math.PI * 2);    //民用昏
            SunMiddle += (0 - H) / (Math.PI * 2);       //中天
            (H, H0, H1) = SunCoord(SunRaise); SunRaise += (-H0 - H) / (Math.PI * 2);
            (H, H0, H1) = SunCoord(SunFall); SunFall += (+H0 - H) / (Math.PI * 2);
            (H, H0, H1) = SunCoord(morning); morning += (-H1 - H) / (Math.PI * 2);
            (H, H0, H1) = SunCoord(nightfall); nightfall += (+H1 - H) / (Math.PI * 2);
            (H, H0, H1) = SunCoord(SunMiddle); SunMiddle += (0 - H) / (Math.PI * 2);


            // 月
            //查找最靠近当日中午的月上中天,mod2的第1参数为本地时角近似值
            var jd2000Moon = jd2000 - Util.mod2(0.1726222 + 0.966136808032357 * jd2000 - 0.0366 * DeltaT - LocalJ / (Math.PI * 2), 1);
            MoonMiddle = MoonRaise = MoonFall = jd2000Moon;

            double sv = Math.PI * 2 * 0.966;
            (H, H0) = MoonCoord(jd2000Moon); //月亮坐标
            MoonRaise += (-H0 - H) / sv;
            MoonFall += (H0 - H) / sv;
            MoonMiddle += (0 - H) / sv;

            (H, H0) = MoonCoord(MoonRaise); MoonRaise += (-H0 - H) / sv;
            (H, H0) = MoonCoord(MoonFall); MoonFall += (+H0 - H) / sv;
            (H, H0) = MoonCoord(MoonMiddle); MoonMiddle += (0 - H) / sv;

        }


    }
}
