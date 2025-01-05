using System;
using System.Collections.Generic;
using System.Text;
using Prophecy.Astronomy;
using Prophecy.Data;

namespace Prophecy
{

    /// <summary>
    /// 日历计算类
    /// </summary>
    public partial class LunarYear
    {

#region 公共属性

 
        /// <summary>
        /// 年初（大年初一？）的jd
        /// </summary>
        public double jd0;

        /// <summary>
        /// 农历月信息
        /// </summary>
        public List<LunarMonth> Month = new List<LunarMonth>();

        /// <summary>
        /// 年有多少个农历月
        /// </summary>
        public int MonthNum { get { return Month.Count; } }

        ///// <summary>
        ///// TODO give up
        ///// 农历年份序号，即在正月初一之前，等于上一个公历年序号
        ///// </summary>
        //public int Year
        //{
        //    get
        //    {
        //        var jdt = new JDateTime(jd0);
        //        if(jd0 < )
        //    }
        //}

        /// <summary>
        /// 年干支
        /// TODO give up
        /// </summary>
        public GanZhi ganZhi
        {
            get
            {

                return new GanZhi();
            }
        }

        /// <summary>
        /// 年生肖
        /// </summary>
        public Shengxiao shengXiao { get { return (Shengxiao)ganZhi.Zhi; } }

        /// <summary>
        /// 年号
        /// </summary>
        public string nianHao;


        /// <summary>
        /// 全年合朔信息,15个。是完整儒略日 不是jd2000
        /// </summary>
        public List<double> HS  = new List<double>();

        /// <summary>
        /// 全年节气信息,26 = [上一年冬至, 小寒, ... 冬至, 上一年大雪, 上一年小雪] 
        /// 是完整儒略日 不是jd2000
        /// </summary>
        public List<double> ZQ=new List<double>();



        ///// <summary>
        ///// 保存生成的网页内容: 朝代名, 年号, 纪年干支, 属相
        ///// </summary>
        public string html0 { get {

                // 年份处理
                var c = $"{nianHao} 农历{ganZhi.ToString()}{shengXiao}年";       // 干支纪年
                if (c.Length > 33) c = $"<span style='font-size:12px'>{c}</span>";
                else c = $"<span style='font-size:16px;font-weight:bold'>{c}</span>";
                return c;
            } 
        }    // = "";       // C#: 属性 pg*, 用于生成网页内容

        ///// <summary>
        ///// 保存生成的网页内容: 月历
        ///// </summary>
        //public string html1 { get; set; }    // = "";

        ///// <summary>
        ///// 保存生成的网页内容: 月相, 交节信息
        ///// </summary>
        //public string html2 { get; set; }    // = "";


        /// <summary>
        /// 文本年号信息
        /// </summary>
        public string pg0_text
        {
            get
            {
                return $"{nianHao} 农历{ganZhi}年【{shengXiao}年】";
            }
        }
        ///// <summary>
        ///// 文本月历, 第1列: 日期, 第2列: 放假标识, 第3列: 星期几, 第4列: 农历名称(含节气名,纪念日名等)
        /////           第5列以后: 月相符号或节气标识
        ///// </summary>
        //public string pg1_text { get; set; }    // = "";

            ///// <summary>
            ///// 本月的月相与节气文本信息
            ///// </summary>
            //public string pg2_text { get; set; }    // = "";


            #endregion


        public LunarYear(int year) : this(new JDateTime(year).JulianDate)
        {
            //
        }

        /// <summary>
        /// 计算公历某一个月的"公农回"三合历, 并把相关信息保存到月对象   以及日对象  中
        /// </summary>
        /// <param name="jd">儒略日，据此推算所属年份的起止位置</param>
        public LunarYear(double jd)
        {
            
            var year0date = new JDateTime(jd);
            year0date = new JDateTime(year0date.GerogeYear, 1, 1, 0, 0, 0);
            jd0 = year0date.JulianDate;
            // 所属公历年对应的农历干支纪年
            //this.ganZhi = new GanZhi(year0date.LunarYearJiazi);
            this.nianHao = ChaodaiInfo.getChaodaiDesc(year0date.LunarYear, year0date.LunarMonth);



            // 农历排月序计算,可定出农历,有效范围：两个冬至之间(冬至一 ≤ d ＜ 冬至二)
            ZQ = AstronomyOld.calcJieQiDongzhi(jd);


            // 今年"首朔"的日月黄经差
            var winter = AstronomyOld.calShuo(ZQ[0]);
            // 求较靠近冬至的朔日
            if (winter > ZQ[0]) winter -= 29.53;
            // 该年所有朔,包含14个月的始末
            for (int i = 0; i < 15; i++)
            {
                HS.Add(AstronomyOld.calShuo(winter + 29.5306 * i));
            }


            initLunarMonths();

        }

        /// <summary>
        /// 初始化本年度的所有月信息
        /// </summary>
        void initLunarMonths()
        {
            const int MAXMonthNUM = 14;
            var LunarMonthSize = new int[MAXMonthNUM];
            var LunarMonthIndex = new int[MAXMonthNUM];
            var LunarMonthName = new string[MAXMonthNUM];
            var LeapMonth = -1;
            var realMonthNUM = 12;
            for (int i = 0; i < MAXMonthNUM; i++)
            {
                LunarMonthSize[i] = (int)(HS[i + 1] - HS[i]); // 月大小
                LunarMonthIndex[i] = i;
                //LunarMonthName[i] = i.ToString();  // 月序初始化
            }


            // 无中气置闰法确定闰月,(气朔结合法,数据源需有冬至开始的的气和朔)
            if (HS[13] <= ZQ[24])
            {
                // 第13月的月末没有超过冬至(不含冬至),说明今年含有13个月，即需要闰月
                int i = 1;
                //在13个月中找第1个没有中气的月份
                for (; HS[i + 1] > ZQ[2 * i] && i < 13; i++) ;
                LeapMonth = i;
                // 后续月名编号减一，即编号重复的那个月标记为闰月
                for (; i < MAXMonthNUM; i++)
                {
                    LunarMonthIndex[i] = i - 1;
                    //LunarMonthName[i] = (LunarMonth[i] - 1).ToString();

                }
            }

            // 名称转换(月建别名)
            for (int i = 0; i < MAXMonthNUM; i++)
            {
                double jdm0 = HS[i];// 初一的儒略日
                var v = LunarMonthIndex[i];  //月建序号
                string name = GetLunarMonthName(v);    // 月建对应的默认月名称：建子十一,建丑十二,建寅为正……
                if (jdm0 >= 1724360 && jdm0 <= 1729794) name = GetLunarMonthName(v + 1);        //  8.01.15至 23.12.02 建子为十二,其它顺推
                else if (jdm0 >= 1807724 && jdm0 <= 1808699) name = GetLunarMonthName(v + 1);   // 237.04.12至239.12.13 建子为十二,其它顺推
                else if (jdm0 >= 1999349 && jdm0 <= 1999467) name = name = GetLunarMonthName(v + 2);   // 761.12.02至762.03.30 建子为正月,其它顺推
                else if (jdm0 >= 1973067 && jdm0 <= 1977112) { if (v % 12 == 0) name = "正"; if (v == 2) name = "一"; }    // 689.12.18至701.01.14 建子为正月,建寅为一月,其它不变

                if (jdm0 == 1729794 || jdm0 == 1808699) name = "拾贰";    // 239.12.13及23.12.02均为十二月,为避免两个连续十二月，此处改名

                LunarMonthName[i] = name;
            }

            // -721年至-104年的后九月及月建的闰月名称有变化,与朔有关，与气无关
            double YY = Math.Floor((ZQ[0] + 10 + 180) / 365.2422); // 确定年份
            if (YY >= -721 && YY <= -104)
            {
                double ly = 0, b0 = 0, k0 = 0, x0 = 0;    // ly为历元(本历首月的儒略数),x0月建,lName闰月名称,b0,k0为置闰拟合参数
                string lName = "";
                if (YY >= -721) { ly = 1457698; k0 = 12.368422; b0 = 0.342; lName = "十三"; x0 = 2; }    // 春秋历,ly为-722.12.17
                if (YY >= -479) { ly = 1546083; k0 = 12.368422; b0 = 0.500; lName = "十三"; x0 = 2; }    // 战国历,ly为-480.12.11
                if (YY >= -220) { ly = 1640641; k0 = 12.369000; b0 = 0.866; lName = "后九"; x0 = 11; }   // 秦汉历,ly为-221.10.31;
                double nY = Math.Floor((HS[0] - ly + 100) / 365.25);    // 年积数
                double f1 = Math.Floor(b0 + nY * k0);       // f1有本年首的月积数
                double f2 = Math.Floor(b0 + nY * k0 + k0);  //f2为下一年首的月积数
                for (int i = 0; i < MAXMonthNUM; i++)
                {
                    double f3 = Math.Floor((HS[i] - ly + 15) / 29.5306);    // 该月积数
                    if (f3 < f2) f3 -= f1; else f3 -= f2;
                    if (f3 < 12) LunarMonthName[i] = GetLunarMonthName(f3 + x0); else LunarMonthName[i] = lName;
                }

            }
            realMonthNUM = LeapMonth > 0 ? 13 : 12;
            for (int i = 0; i < MAXMonthNUM; i++)
            {
                Month.Add(new LunarMonth(
                    this, 
                    HS[i], 
                    LunarMonthIndex[i], 
                    LunarMonthName[i], 
                    LunarMonthSize[i], 
                    i > 0 && LunarMonthIndex[i] == LunarMonthIndex[i-1],
                    i + 1 == realMonthNUM
                ));
            }
        }

        /// <summary>
        /// 月建对应的默认月名称：建子十一,建丑十二,建寅为正……
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetLunarMonthName(double index)
        {
            /// <summary>
            /// 农历各月的名称, 从 "十一" 月开始, 即从月建 "子" 开始, 与十二地支的顺序对应
            /// </summary>
            string[] ymc = new string[] { "十一", "十二", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十" }; //月名称,建寅

            return ymc[((int)(index)) % ymc.Length];
        }

        public string ToHTML(bool useGanzhi = false)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Month.Count; i++)
            {
                if (HS[i + 1] > ZQ[24]) break;    // 已包含下一年的冬至
                if (Month[i].isLeap) sb.Append("闰");  else sb.Append("·");
                sb.Append($"{Month[i].Name}月{(Month[i].isBig ? "大" : "小")} {(useGanzhi?$"{(Tiangan)((int)((HS[i] + 9) % 10))}{(Dizhi)((int)((HS[i] + 1) % 12))}":"")} {new JDateTime(Month[i].month0JDate).ToStringGeroge("MM月dd日")}");

                var vjd = AstronomyOld.so_accurate2(HS[i]);
                var s2 = $"({new JDateTime(vjd, true).ToStringGeroge("MM月dd日")})";    // C#: 取每月朔的时间(即初一)
                // 精朔和粗朔有区别，加红标记
                if (Math.Floor(vjd + 0.5) != HS[i]) s2 = $"<font color=red>{s2}</font>";
                //v=(v+0.5+LunarHelper.J2000)%1; if(v>0.5) v=1-v; if(v<8/1440) s2 = "<u>"+s2+"</u>"; //对靠近0点的加注
                sb.Append(s2);

                for (var j = -2; j < 24; j++)
                {
                    double qi = 0d;
                    if (j >= 0) qi = ZQ[j];
                    if (j == -1) qi = ZQ[25];
                    if (j == -2) qi = ZQ[26];

                    if (qi < HS[i] || qi >= HS[i + 1]) continue;

                    var v = AstronomyOld.qi_accurate2(qi);

                    if (useGanzhi)
                    {
                        var v2 = qi + JDateTime.JD_2000;
                        sb.Append($" {((LunarDayName)(int)(v2 - v)).ToString()} {(Tiangan)((int)((v2 + 9) % 10))}{(Dizhi)((int)((v2 + 1) % 12))}");
                        sb.Append($"{((JieQi)((j + 24) % 24)).ToString()} {new JDateTime(qi + JDateTime.JD_2000).ToStringGeroge("M月d日")}");
                    }
                    else
                    {
                        // 取节气名称和实历交节日期
                        sb.Append($" {((JieQi)((j + 24) % 24)).ToString()}{new JDateTime(qi, true).ToStringGeroge("M月d日")}");
                        s2 = $"({new JDateTime(v + JDateTime.JD_2000).ToStringGeroge("d日")})";    // 取节气时间(上年大雪-本年大雪)
                        if (Math.Floor(v + 0.5) != qi - JDateTime.JD_2000) s2 = $"<font color=red>{s2}</font>";
                        //v=(v+0.5+LunarHelper.J2000)%1; if(v>0.5) v=1-v; if(v<8/1440) s2 = "<u>"+s2+"</u>"; //对靠近0点的加注
                        sb.Append(s2);
                    }
                }
                sb.Append("<br>");
            }
            return sb.ToString();
        }




        ///// <summary>
        ///// 按交节时刻生成 html 年历
        ///// </summary>
        ///// <param name="y"></param>
        ///// <returns></returns>
        //public string nianLiHTML(double y)
        //{
        //    int i, j;
        //    StringBuilder s = new StringBuilder();    // C#: 为提高字符串处理效率, 使用 StringBuilder
        //    StringBuilder s1 = new StringBuilder();    // C#: 为提高字符串处理效率, 使用 StringBuilder
        //    string s2;
        //    double v, qi = 0;
        //    TrueNMST trueNMST = new TrueNMST(Math.Floor((y - 2000) * 365.2422 + 180));
        //    //TrueNMST.calcY();

        //    for (i = 0; i < 14; i++)
        //    {
        //        if (trueNMST.HS[i + 1] > trueNMST.ZQ[24]) break;    // 已包含下一年的冬至
        //        if (trueNMST.LeapMonth != 0 && i == trueNMST.LeapMonth) s1.Append("闰");
        //        else s1.Append("·");
        //        s1.Append(trueNMST.LunarMonthName[i]);
        //        if (s1.ToString().Length < 3) s1.Append("月");
        //        s1.Append(trueNMST.LunarMonthSize[i] > 29 ? "大" : "小");
        //        s1.Append($" {new JDateTime(trueNMST.HS[i] + JDateTime.JD_2000).ToString("MM月dd日")}");    // C#: 取实历初一的时间

        //        v = LunarData.so_accurate2(trueNMST.HS[i]);
        //        s2 = "(" + new JDateTime(v + JDateTime.JD_2000).ToString("MM月") + ")";    // C#: 取每月朔的时间(即初一)
        //        if (Math.Floor(v + 0.5) != trueNMST.HS[i]) s2 = $"<font color=red>{s2}</font>";
        //        //v=(v+0.5+LunarHelper.J2000)%1; if(v>0.5) v=1-v; if(v<8/1440) s2 = "<u>"+s2+"</u>"; //对靠近0点的加注
        //        s1.Append(s2);

        //        for (j = -2; j < 24; j++)
        //        {
        //            if (j >= 0) qi = trueNMST.ZQ[j];
        //            if (j == -1) qi = trueNMST.ZQ[25];
        //            if (j == -2) qi = trueNMST.ZQ[26];

        //            if (qi < trueNMST.HS[i] || qi >= trueNMST.HS[i + 1]) continue;

        //            // C#: 取节气名称和实历交节日期
        //            s1.Append($" {((JieQi)((j+24)%24)).ToString()}{new JDateTime(qi, true).ToString("M月d日")}");    

        //            v = LunarData.qi_accurate2(qi);
        //            s2 = $"({new JDateTime(v + JDateTime.JD_2000).ToString("d日")})";    // C#: 取节气时间(上年大雪-本年大雪)
        //            if (Math.Floor(v + 0.5) != qi) s2 = $"<font color=red>{s2}</font>";
        //            //v=(v+0.5+LunarHelper.J2000)%1; if(v>0.5) v=1-v; if(v<8/1440) s2 = "<u>"+s2+"</u>"; //对靠近0点的加注
        //            s1.Append(s2);
        //        }
        //        s.Append(s1.ToString() + "<br>");
        //        s1.Remove(0, s1.Length);     // C#: 在转换时将原来的字符串 s1 改写为 StringBuiler, 因此添加本句
        //    }
        //    return s.ToString();
        //}

        ///// <summary>
        ///// 按交节干支生成 html 年历
        ///// </summary>
        ///// <param name="y"></param>
        ///// <returns></returns>
        //public string nianLi2HTML(double y)
        //{
        //    //int i, j;
        //    StringBuilder s = new StringBuilder(); 
        //    StringBuilder s1 = new StringBuilder(); 
        //    //double v, v2, qi = 0;
        //    TrueNMST trueNMST=new TrueNMST(Math.Floor((y - 2000) * 365.2422 + 180));
        //    for (int i = 0; i < 14; i++)
        //    {
        //        if (trueNMST.HS[i + 1] > trueNMST.ZQ[24]) break; //已包含下一年的冬至
        //        if (trueNMST.LeapMonth != 0 && i == trueNMST.LeapMonth) s1.Append("闰");
        //        else s1.Append("·");
        //        s1.Append(trueNMST.LunarMonthName[i]);
        //        if (s1.ToString().Length < 3) s1.Append("月");
        //        s1.Append(trueNMST.LunarMonthSize[i] > 29 ? "大" : "小");
        //        var v = trueNMST.HS[i] + JDateTime.JD_2000;
        //        s1.Append($" {(Tiangan)((int)((v + 9) % 10))}{(Dizhi)((int)((v + 1) % 12))} {new JDateTime(v).ToString("M月d日")}");

        //        for (int j = -2; j < 24; j++)
        //        {
        //            var qi = 0d;
        //            if (j >= 0) qi = trueNMST.ZQ[j];
        //            if (j == -1) qi = trueNMST.ZQ[25];
        //            if (j == -2) qi = trueNMST.ZQ[26];

        //            if (qi < trueNMST.HS[i] || qi >= trueNMST.HS[i + 1]) continue;
        //            var v2 = qi + JDateTime.JD_2000;
        //            s1.Append($" {((LunarDayName)(int)(v2-v)).ToString()} {(Tiangan)((int)((v2 + 9) % 10))}{(Dizhi)((int)((v2 + 1) % 12))}");
        //            s1.Append($"{((JieQi)((j + 24) % 24)).ToString()} {new JDateTime(qi + JDateTime.JD_2000).ToString("M月d日")}");
        //        }
        //        s.Append(s1.ToString() + "<br>");
        //        s1.Remove(0, s1.Length);     // C#: 在转换时将原来的字符串 s1 改写为 StringBuiler, 因此添加本句
        //    }
        //    return s.ToString();
        //}





        /// <summary>
        /// 指定某日, 计算出它的所属节(气), 上一节(气), 下一节(气)信息
        /// </summary>
        /// <param name="jd2000"></param>
        /// <returns>长度3的节气信息列表 </returns>
        public static List<SolarTerm> CalcJieQiInfo(double jd2000, bool calJie = false, bool calQi = false)
        {
            JDateTime dt = new JDateTime(jd2000, true);
            double y = dt.GerogeYear;
            int Jindex = -1, jieQiPos = 0;
            List<SolarTerm> jieqiList = new List<SolarTerm>();

            double v, qi = 0;
            //TrueNMST trueNMST = new TrueNMST(dt.JulianDateFrom2000);
            // 计算节气, 以"霜降"开始
            var JQList = AstronomyOld.calcJieQiShuangjiang(Math.Floor(y * 365.2422 + 180));

            for (int j = 0; j < JQList.Count; j++)    // 建表
            {
                if (!calQi && (j % 24) % 2 == 0)   // 只计算节  跳过气    
                    continue;
                else if (!calJie && (j % 24) % 2 == 1)    // 只计算气 跳过节
                    continue;

                qi = JQList[j];

                v = AstronomyOld.qi_accurate2(qi);

                jieQiPos = j % 24 + 20;      // 与 obb.jqmc 对应, "霜降"距首位"冬至"的距离为 20
                if (jieQiPos >= 24) jieQiPos -= 24;

                jieqiList.Add(new SolarTerm
                {
                    Time = new JDateTime(v, true),
                    IsJie = true,
                    Name = ((JieQi)(jieQiPos)).ToString(),
                    //YueJian = LunarData.JieQiYueJian[jieQiPos],
                    JDTime = v + JDateTime.JD_2000,
                    HistoricalTime = new JDateTime(qi, true),
                    HistoricalJDTime = qi + JDateTime.JD_2000,
                    DifferentTime = (Math.Floor(v + 0.5) != qi ? true : false),
                    DayDifference = (int)(Math.Floor(v + 0.5) - qi),

                });



                //counter++;
            }

            if (dt.GerogeYear >= 0)
            {
                int ymd = dt.GerogeYear * 10000 + dt.GerogeMonth * 100 + dt.GerogeDay;     // 转换成为数值表示的日期: 如 20090422
                for (int j = jieqiList.Count - 1; j >= 0; j--)    // 逆序查表(表中的交节日期数据是由小到大排列的)
                {
                    JDateTime jieqiTime = jieqiList[j].Time;
                    int jieqiYear = jieqiTime.GerogeYear;// int.Parse(jieqiTime.Substring(0, 5));
                    int jieqiMonth = jieqiTime.GerogeMonth;// int.Parse(jieqiTime.Substring(6, 2));
                    int jieqiDay = jieqiTime.GerogeDay;// int.Parse(jieqiTime.Substring(9, 2));
                    if (jieqiYear * 10000 + jieqiMonth * 100 + jieqiDay <= ymd)    // 找到所属的节气
                    {
                        Jindex = j;
                        break;
                    }
                }
            }
            else
            {
                int ymd = dt.GerogeYear * 10000 - (99 - dt.GerogeMonth) * 100 - (99 - dt.GerogeDay);
                for (int j = 0; j < jieqiList.Count; j++)    // 顺序查表(表中的交节日期数据是由小到大排列的, 计算为数值时也要严格递增)
                {
                    JDateTime jieqiTime = jieqiList[j].Time;
                    int jieqiYear = jieqiTime.GerogeYear;// int.Parse(jieqiTime.Substring(0, 5));
                    int jieqiMonth = jieqiTime.GerogeMonth;// int.Parse(jieqiTime.Substring(6, 2));
                    int jieqiDay = jieqiTime.GerogeDay;// int.Parse(jieqiTime.Substring(9, 2));
                    int jieqiYmd = jieqiYear * 10000 - (99 - jieqiMonth) * 100 - (99 - jieqiDay);
                    if (jieqiYmd >= ymd)
                    {
                        if (jieqiYmd > ymd)
                            Jindex = j - 1;
                        else
                            Jindex = j;
                        break;
                    }
                }
            }
            List<SolarTerm> res = new List<SolarTerm>();
            
            if (Jindex > 0 && Jindex < jieqiList.Count)
            {
                res.Add(jieqiList[Jindex - 1]);
                res.Add(jieqiList[Jindex]);
                res.Add(jieqiList[Jindex + 1]);
            }

            return res;
        }







    }
}