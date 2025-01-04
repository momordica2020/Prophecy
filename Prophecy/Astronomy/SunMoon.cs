using System;
using System.Text;

namespace Prophecy.Astronomy
{
    /// <summary>
    /// 太阳月亮计算类
    /// </summary>
    public class SunMoon
    {


        #region 公共属性(注: 初始转换时为公共字段, 已改写)

        #region 基本参数

        /// <summary>
        /// 力学时
        /// </summary>
        public double T { get; set; }    // = 0;

        /// <summary>
        /// 站点经度
        /// </summary>
        public double L { get; set; }    // = 0;

        /// <summary>
        /// 站点纬度
        /// </summary>
        public double fa { get; set; }    // = 0;

        /// <summary>
        /// TD-UT
        /// </summary>
        public double dt { get; set; }    // = 0;

        /// <summary>
        /// UT
        /// </summary>
        public double jd { get; set; }    // = 0;

        /// <summary>
        /// 黄经章
        /// </summary>
        public double dL { get; set; }    // = 0;

        /// <summary>
        /// 交角章动
        /// </summary>
        public double dE { get; set; }    // = 0;

        /// <summary>
        /// 真黄赤交角
        /// </summary>
        public double E { get; set; }    // = 0;

        /// <summary>
        /// 真恒星时(不考虑非多项式部分)
        /// </summary>
        public double gst { get; set; }    // = 0;

        #endregion 基本参数

        /// <summary>
        /// 月球黄道坐标
        /// </summary>
        public Coordinate moonH;

        /// <summary>
        /// 月球赤道坐标
        /// </summary>
        public Coordinate moonC;

        /// <summary>
        /// 月球黄道坐标，修正视差
        /// </summary>
        public Coordinate moonCX;

        /// <summary>
        /// 月球地平坐标
        /// </summary>
        public Coordinate moonS;


        /// <summary>
        /// 月球此时的天体时角
        /// </summary>
        public double mShiJ { get; set; }    // = 0;



        /// <summary>
        /// 太阳黄道坐标
        /// </summary>
        public Coordinate sunH;

        /// <summary>
        /// 太阳赤道坐标
        /// </summary>
        public Coordinate sunC;


        /// <summary>
        /// 太阳赤道坐标，修正视差
        /// </summary>
        public Coordinate sunCX;

        /// <summary>
        /// 太阳地平坐标
        /// </summary>
        public Coordinate sunS;

        /// <summary>
        /// 太阳此时的天体时角
        /// </summary>
        public double sShiJ { get; set; }




        #region 月球参数





        ///// <summary>
        ///// 月球视黄经
        ///// </summary>
        //public double mHJ { get; set; }    // = 0;

        ///// <summary>
        ///// 月球视黄纬
        ///// </summary>
        //public double mHW { get; set; }    // = 0;

        ///// <summary>
        ///// 地月质心距
        ///// </summary>
        //public double mR { get; set; }    // = 0;

        ///// <summary>
        ///// 月球视赤经
        ///// </summary>
        //public double mCJ { get; set; }    // = 0;

        ///// <summary>
        ///// 月球赤纬
        ///// </summary>
        //public double mCW { get; set; }    // = 0;

        ///// <summary>
        ///// 此时的天体时角
        ///// </summary>
        //public double mShiJ { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 月球视赤经
        ///// </summary>
        //public double mCJ2 { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 月球赤纬
        ///// </summary>
        //public double mCW2 { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 地月质心距
        ///// </summary>
        //public double mR2 { get; set; }    // = 0;

        ///// <summary>
        ///// 方位角
        ///// </summary>
        //public double mDJ { get; set; }    // = 0;

        ///// <summary>
        ///// 高度角
        ///// </summary>
        //public double mDW { get; set; }    // = 0;

        ///// <summary>
        ///// 方位角(大气折射修正后)
        ///// </summary>
        //public double mPJ { get; set; }    // = 0;

        ///// <summary>
        ///// 高度角(大气折射修正后)
        ///// </summary>
        //public double mPW { get; set; }    // = 0;

        #endregion



        #region 太阳参数

        ///// <summary>
        ///// 太阳视黄经
        ///// </summary>
        //public double sHJ { get; set; }    // = 0;

        ///// <summary>
        ///// 太阳视黄纬
        ///// </summary>
        //public double sHW { get; set; }    // = 0;

        ///// <summary>
        ///// 日地质心距
        ///// </summary>
        //public double sR { get; set; }    // = 0;

        ///// <summary>
        ///// 太阳视赤经
        ///// </summary>
        //public double sCJ { get; set; }    // = 0;

        ///// <summary>
        ///// 太阳视赤纬
        ///// </summary>
        //public double sCW { get; set; }    // = 0;

        ///// <summary>
        ///// 太阳时角
        ///// </summary>
        //public double sShiJ { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 太阳视赤经
        ///// </summary>
        //public double sCJ2 { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 太阳视赤纬
        ///// </summary>
        //public double sCW2 { get; set; }    // = 0;

        ///// <summary>
        ///// 修正了视差的赤道坐标: 日地质心距
        ///// </summary>
        //public double sR2 { get; set; }    // = 0;

        ///// <summary>
        ///// 方位角
        ///// </summary>
        //public double sDJ { get; set; }    // = 0;

        ///// <summary>
        ///// 高度角
        ///// </summary>
        //public double sDW { get; set; }    // = 0;

        ///// <summary>
        ///// 方位角(大气折射修正后)
        ///// </summary>
        //public double sPJ { get; set; }    // = 0;

        ///// <summary>
        ///// 高度角(大气折射修正后)
        ///// </summary>
        //public double sPW { get; set; }    // = 0;

        #endregion



        #region 其他参数

        /// <summary>
        /// 时差(单位:日)
        /// </summary>
        public double sc { get; set; }    // = 0;

        /// <summary>
        /// 平太阳时
        /// </summary>
        public double pty { get; set; }    // = 0;

        /// <summary>
        /// 真太阳时
        /// </summary>
        public double zty { get; set; }    // = 0;

        /// <summary>
        /// 月亮视半径(角秒)
        /// </summary>
        public double mRad { get; set; }    // = 0;

        /// <summary>
        /// 太阳视半径(角秒)
        /// </summary>
        public double sRad { get; set; }    // = 0;

        /// <summary>
        /// 月亮地心视半径(角秒)
        /// </summary>
        public double e_mRad { get; set; }    // = 0;

        /// <summary>
        /// 地本影在月球向径处的半径(角秒)
        /// </summary>
        public double eShadow { get; set; }    // = 0;

        /// <summary>
        /// 地本影在月球向径处的半径(角秒)
        /// </summary>
        public double eShadow2 { get; set; }    // = 0;

        /// <summary>
        /// 月亮被照面比例
        /// </summary>
        public double mIll { get; set; }    // = 0;

        /// <summary>
        /// 中心食计算: 经
        /// </summary>
        public double zx_J { get; set; }    // = 0;

        /// <summary>
        /// 中心食计算: 纬
        /// </summary>
        public double zx_W { get; set; }    // = 0;

        #endregion

        #endregion 公共属性




        #region 公共方法
        /// <summary>
        /// 初始化计算。参数：T 力学时,L 站点经度,fa 纬度,high 海拔(千米)
        /// </summary>
        /// <param name="jd2000">力学时</param>
        /// <param name="L">站点经度</param>
        /// <param name="fa">站点纬度</param>
        /// <param name="high">海拔</param>
        public SunMoon(double jd2000, Coordinate site, double high)
        {
            //基本参数计算
            T = jd2000;
            L = site.J;
            fa = site.W;
            dt = AstronomyOld.deltatT2(jd2000); //TD-UT
            jd = jd2000 - dt;    //UT
            jd2000 /= 36525;


            (dL, dE) = AstronomyOld.Nutation(jd2000);
            E = AstronomyOld.ObliquityOfEcliptic(jd2000) + dE; //真黄赤交角
            gst = AstronomyOld.gst(jd, dt) + dL * Math.Cos(E); //真恒星时(不考虑非多项式部分)
            //double[] z = new double[4];

            //=======月亮========
            //月亮黄道坐标
            moonH = AstronomyOld.M_coord(jd2000, -1, -1, -1); //月球坐标
            // 光行差修正
            moonH.J = Util.rad2mrad(moonH.J + AstronomyOld.gxc_moonLon(jd2000) + dL);
            // 章动修正
            moonH.W += AstronomyOld.gxc_moonLat(jd2000);

            //this.mHJ = mcoord.J; 
            //this.mHW = mcoord.W; 
            //this.mR = mcoord.R; //月球视黄经,视黄纬,地月质心距

            //月球赤道坐标
            moonC = AstronomyOld.llrConv(moonH, E); //转为赤道坐标

            //this.mCJ = mcoord.J;
            //this.mCW = mcoord.W; //月球视赤经,月球赤纬

            //月亮时角计算
            mShiJ = Util.rad2mrad(gst - L - moonC.J); //得到此刻天体时角

            //修正了视差的赤道坐标
            moonCX = AstronomyOld.parallax(moonC, mShiJ, fa, high);


            //this.mCJ2 = mcoord.J; 
            //this.mCW2 = mcoord.W; 
            //this.mR2 = mcoord.R;

            //月亮时角坐标
            //转到相对于地平赤道分点的赤道坐标(时角坐标)
            moonS = new Coordinate(moonCX);
            moonS.J += Math.PI / 2d - gst + L;

            //月亮地平坐标
            //转到地平坐标(只改经纬度)
            moonS = AstronomyOld.llrConv(moonS, Math.PI / 2 - fa);

            moonS.J = Util.rad2mrad(Math.PI / 2 - moonS.J);
            //this.mDJ = mcoord.J; 
            //this.mDW = mcoord.W; //方位角,高度角
            if (moonS.W > 0) moonS.W += AstronomyOld.AR2(moonS.W); //大气折射修正

            //this.mPJ = mcoord.J; 
            //this.mPW = mcoord.W; //方位角,高度角

            //=======太阳========
            //太阳黄道坐标
            //地球坐标
            sunH = AstronomyOld.E_coord(jd2000, -1, -1, -1);

            sunH.J = Util.rad2mrad(sunH.J + Math.PI + AstronomyOld.gxc_sunLon(jd2000) + dL);  //补上太阳光行差及章动
            sunH.W = -sunH.W + AstronomyOld.gxc_sunLat(jd2000); //z数组为太阳地心黄道视坐标

            //太阳视黄经,视黄纬,日地质心距
            //this.sHJ = mcoord.J; 
            //this.sHW = mcoord.W; 
            //this.sR = mcoord.R; 

            //太阳赤道坐标
            sunC = AstronomyOld.llrConv(sunH, E); //转为赤道坐标

            //太阳视赤经,视赤纬
            //this.sCJ = mcoord.J; 
            //this.sCW = mcoord.W; 

            //太阳时角计算
            sShiJ = Util.rad2mrad(gst - L - sunC.J); //得到此刻天体时角

            //修正了视差的赤道坐标
            sunCX = AstronomyOld.parallax(sunC, sShiJ, fa, high);

            //this.sCJ2 = mcoord.J;
            //this.sCW2 = mcoord.W; 
            //this.sR2 = mcoord.R;

            //太阳时角坐标
            sunS = new Coordinate(sunCX);
            sunS.J += Math.PI / 2 - gst + L;  //转到相对于地平赤道分点的赤道坐标

            //太阳地平坐标
            sunS = AstronomyOld.llrConv(sunS, Math.PI / 2 - fa);
            sunS.J = Util.rad2mrad(Math.PI / 2 - sunS.J);
            //mcoord.W -= 8.794/rad/mcoord.R*Math.cos(mcoord.W); //直接在地平坐标中视差修正(这里把地球看为球形,精度比ZB.parallax()稍差一些)
            //this.sDJ = mcoord.J; 
            //this.sDW = mcoord.W; //方位角,高度角

            if (sunS.W > 0) sunS.W += AstronomyOld.AR2(sunS.W); //大气折射修正
            //this.sPJ = mcoord.J; 
            //this.sPW = mcoord.W; //方位角,高度角

            //=======其它========
            //时差计算
            double t = jd2000 / 10; double t2 = t * t, t3 = t2 * t, t4 = t3 * t, t5 = t4 * t;
            double Lon = (1753469512 + 6283319653318 * t + 529674 * t2 + 432 * t3 - 1124 * t4 - 9 * t5 + 630 * Math.Cos(6 + 3 * t)) / 1000000000 + Math.PI - 20.5 / Util.rad; //修正了光行差的太阳平黄经
            Lon = Util.rad2mrad(Lon - (sunC.J - dL * Math.Cos(E))); //(修正了光行差的平黄经)-(不含dL*cos(E)的视赤经)
            if (Lon > Math.PI) Lon -= Math.PI * 2; //得到时差,单位是弧度
            sc = Lon / (Math.PI * 2);   //时差(单位:日)

            //真太阳与平太阳
            pty = jd - L / (Math.PI * 2);  //平太阳时
            zty = jd - L / (Math.PI * 2) + sc; //真太阳时

            //视半径
            //this.mRad = XL.moonRad(this.mR,this.mDW);  //月亮视半径(角秒)
            mRad = 358473400d / moonCX.R; //月亮视半径(角秒)
            sRad = 959.63 / sunCX.R; //太阳视半径(角秒)
            e_mRad = 358473400d / moonH.R; //月亮地心视半径(角秒)
            eShadow = (Util.cs_rEarA / moonH.R * Util.rad - (959.63 - 8.794) / sunH.R) * 51 / 50; //地本影在月球向径处的半径(角秒),式中51/50是大气厚度补偿
            eShadow2 = (Util.cs_rEarA / moonH.R * Util.rad + (959.63 + 8.794) / sunH.R) * 51 / 50; //地半影在月球向径处的半径(角秒),式中51/50是大气厚度补偿
            mIll = AstronomyOld.moonIll(jd2000); //月亮被照面比例

            //中心食计算
            if (Math.Abs(Util.rad2rrad(moonC.J - sunC.J)) < 50 / 180 * Math.PI)
            {
                //?
                var sunC2 = new Coordinate(sunC);
                sunC2.R *= Util.cs_AU;

                var res = AstronomyOld.line_earth(moonC, sunC2);
                zx_J = Util.rad2rrad(gst - res.J);
                zx_W = res.W; //无解返回值是100
            }
            else zx_J = zx_W = 100;
        }


        /// <summary>
        /// 把太阳月亮信息形成 HTML 字符串
        /// </summary>
        /// <param name="showDetail">是否显示ΔT, 黄经章等信息</param>
        /// <returns></returns>
        public string ToHtml(bool showDetail)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellspacing=1 cellpadding=0 bgcolor='#FFC0C0'>");

            sb.Append("<tr><td bgcolor=white align=center>");
            sb.Append("平太阳 " + new JDateTime(pty, true).ToString() + " 真太阳 <font color=red>" + new JDateTime(zty, true).ToString() + "</font><br>");
            sb.Append("时差 " + Util.m2fm(sc * 86400, 2, 1) + " 月亮被照亮 " + (mIll * 100).ToString("F2") + "% ");
            sb.Append("</td></tr>");

            sb.Append("<tr><td bgcolor=white><center><pre style='margin-top: 0; margin-bottom: 0'><font color=blue><b>表一       月亮            太阳</b></font>\r\n");
            sb.Append("视黄经 " + Util.rad2str(moonH.J, 0) + "  " + Util.rad2str(sunH.J, 0) + "\r\n");
            sb.Append("视黄纬 " + Util.rad2str(moonH.W, 0) + "  " + Util.rad2str(sunH.W, 0) + "\r\n");
            sb.Append("视赤经 " + Util.rad2str(moonC.J, 1) + "  " + Util.rad2str(sunC.J, 1) + "\r\n");
            sb.Append("视赤纬 " + Util.rad2str(moonC.W, 0) + "  " + Util.rad2str(sunC.W, 0) + "\r\n");
            sb.Append("距离     " + moonH.R.ToString("F0") + "千米          " + sunH.R.ToString("F6") + "AU" + "\r\n");
            sb.Append("</pre></center></td></tr>");

            sb.Append("<tr><td bgcolor=white><center><pre style='margin-top: 0; margin-bottom: 0'><font color=blue><b>表二       月亮            太阳</b></font>\r\n");
            sb.Append("方位角 " + Util.rad2str(moonS.J, 0) + "  " + Util.rad2str(sunS.J, 0) + "\r\n");
            sb.Append("高度角 " + Util.rad2str(moonS.W, 0) + "  " + Util.rad2str(sunS.W, 0) + "\r\n");
            sb.Append("时角   " + Util.rad2str(mShiJ, 0) + "  " + Util.rad2str(sShiJ, 0) + "\r\n");
            sb.Append("视半径(观测点) " + Util.m2fm(mRad, 2, 0) + "     " + Util.m2fm(sRad, 2, 0) + "\r\n");
            sb.Append("</pre></center></td></tr>");

            if (showDetail)
            {
                sb.Append("<tr><td bgcolor=white align=center>");
                sb.Append("力学时 " + new JDateTime(T, true).ToString());
                sb.Append(" ΔT=" + (dt * 86400).ToString("F1") + "秒<br>");
                sb.Append("黄经章 " + (dL / (Math.PI * 2) * 360 * 3600).ToString("F2") + "\" ");
                sb.Append("交角章 " + (dE / (Math.PI * 2) * 360 * 3600).ToString("F2") + "\" ");
                sb.Append("ε=" + Util.rad2str(E, 0).Trim());
                sb.Append("</td></tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }



        /// <summary>
        /// 把太阳月亮信息形成纯文本字符串
        /// </summary>
        /// <param name="showDetail">是否显示ΔT, 黄经章等信息</param>
        /// <returns></returns>
        public string ToText(bool showDetail)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("平太阳 " + new JDateTime(pty, true) + " 真太阳 " + new JDateTime(zty, true) + "\r\n");
            sb.Append("时差 " + Util.m2fm(sc * 86400, 2, 1) + " 月亮被照亮 " + (mIll * 100).ToString("F2") + "% ");
            sb.Append("\r\n");

            sb.Append("\r\n表一       月亮            太阳\r\n");
            sb.Append("视黄经 " + Util.rad2str(moonH.J, 0) + "  " + Util.rad2str(sunH.J, 0) + "\r\n");
            sb.Append("视黄纬 " + Util.rad2str(moonH.W, 0) + "  " + Util.rad2str(sunH.W, 0) + "\r\n");
            sb.Append("视赤经 " + Util.rad2str(moonC.J, 1) + "  " + Util.rad2str(sunC.J, 1) + "\r\n");
            sb.Append("视赤纬 " + Util.rad2str(moonC.W, 0) + "  " + Util.rad2str(sunC.W, 0) + "\r\n");
            sb.Append("距离     " + moonH.R.ToString("F0") + "千米          " + sunH.R.ToString("F6") + "AU" + "\r\n");

            sb.Append("\r\n表二       月亮            太阳\r\n");
            sb.Append("方位角 " + Util.rad2str(moonS.J, 0) + "  " + Util.rad2str(sunS.J, 0) + "\r\n");
            sb.Append("高度角 " + Util.rad2str(moonS.W, 0) + "  " + Util.rad2str(sunS.W, 0) + "\r\n");
            sb.Append("时角   " + Util.rad2str(mShiJ, 0) + "  " + Util.rad2str(sShiJ, 0) + "\r\n");
            sb.Append("视半径(观测点) " + Util.m2fm(mRad, 2, 0) + "     " + Util.m2fm(sRad, 2, 0) + "\r\n");

            if (showDetail)
            {
                sb.Append("\r\n力学时 " + new JDateTime(T, true).ToString());
                sb.Append(" ΔT=" + (dt * 86400).ToString("F1") + "秒\r\n");
                sb.Append("黄经章 " + (dL / (Math.PI * 2) * 360 * 3600).ToString("F2") + "\" ");
                sb.Append("交角章 " + (dE / (Math.PI * 2) * 360 * 3600).ToString("F2") + "\" ");
                sb.Append("\r\nε=" + Util.rad2str(E, 0).Trim());
            }
            return sb.ToString();
        }

        #endregion
    }
}
