using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Prophecy
{



    /// <summary>
    /// 月对象, 保存若干个日对象(OB)
    /// </summary>
    public class LunarMonth
    {
        /// <summary>
        /// 本月所属农历年
        /// </summary>
        public readonly LunarYear Year;

        /// <summary>
        /// 月初那天对应的公历日期（1日）的中午12时的时刻，这是为了避免取整或时辰-小时换算时出问题
        /// </summary>
        public readonly JDateTime month0JDate;

        /// <summary>
        /// 月干支
        /// </summary>
        public GanZhi ganZhi
        {
            get
            {
                return new GanZhi((int)Year.ganZhi.Gan % 5 + 2, Index);
            }
        }

        /// <summary>
        /// 月序号
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// 月农历名。因为存在特殊例外，所以要由年计算时填入
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 月初那一天的jd2000
        /// 对应的公历日期（1日）的中午12时的时刻，这是因为儒略日从12时起算
        /// </summary>
        public double jd0 { get { return month0JDate.JulianDate; } }



        ///// <summary>
        ///// 月末那一天的jd2000
        ///// </summary>
        //public double jdNext { get
        //    {
        //        return new JDateTime(month0JDate).AddMonths(1).JulianDateFrom2000;// C#: 如果月份大于 12, 则年数 + 1, 月数取 1
        //        //var dNum = (int)(Math.Floor(monthNextJDate.JulianDateFrom2000) - jd0); // 本月天数(公历)

        //    }
        //}

        ///// <summary>
        ///// 公历月的天数
        ///// </summary>
        //public int DayNum { get { return (int)(jdNext - jd0); } }


        /// <summary>
        /// 是否闰月
        /// </summary>
        public readonly bool isLeap;

        /// <summary>
        /// 是否是本年度最后一个月，用于计算除夕等
        /// </summary>
        public readonly bool isLastMonth;


        /// <summary>
        /// 月内的日期
        /// </summary>
        public readonly List<LunarDay> LDay = new List<LunarDay>();

        /// <summary>
        /// 本月第一天的星期
        /// </summary>
        public int WeekOfMonth0 { get { return (int)((jd0 + 1) % 7); } }

        /// <summary>
        /// 本月的总周数
        /// </summary>
        public int WeekNumOfThisMonth
        {
            get
            {
                return (int)(Math.Floor((this.WeekOfMonth0 + DayNum - 1) / 7d) + 1);
            }
        }

        /// <summary>
        /// 本月天数（农历）
        /// </summary>
        public int DayNum { get { return LDay.Count; } }

        /// <summary>
        /// 是不是大月？
        /// </summary>
        public bool isBig { get { return DayNum > 29; } }

        public LunarMonth(LunarYear year, double jd, int index, string name, int dayNum, bool isleap, bool islast)
        {
            Year = year;
            Index = index;
            Name = name;
            isLeap = isleap;
            isLastMonth = islast;

            month0JDate = new JDateTime(jd);
            // 日历物件初始化
            //month0JDate = new JDateTime(month0JDate.GerogeYear, 1, 1, 0, 0, 0);
            //DayJ.h = 12; DayJ.m = 0; DayJ.s = 0.1;
            //DayJ.Y = By; DayJ.M = Bm; DayJ.D = 1; 


            //jd0 = Math.Floor(month0JDate.JulianDateFrom2000);  // 公历某年的月首,中午
            //var monthNextJDate = new JDateTime(month0JDate).AddMonths(1);// C#: 如果月份大于 12, 则年数 + 1, 月数取 1
            //var dNum = (int)(Math.Floor(monthNextJDate.JulianDateFrom2000) - jd0); // 本月天数(公历)

            //// 纪月处理,1998年12月7(大雪)开始连续进行节气计数,0为甲子
            //var mk = (int)Math.Floor((jd0 - year.ZQ[0]) / 30.43685);
            ////相对大雪的月数计算,mk的取值范围0-12
            //if (mk < 12 && jd0 >= year.ZQ[2 * mk + 1]) mk++;
            ////相对于1998年12月7(大雪)的月数
            //var D = mk + Math.Floor((year.ZQ[12] + 390) / 365.2422) * 12;
            //ganZhi = new GanZhi(D);


            // 循环提取各日信息
            for (int i = 0; i < dayNum; i++)
            {
                var day = new LunarDay(this, i);





                LDay.Add(day);




                //ob.JDayFrom2000 = Bd0 + i;   // 儒略日,北京时12:00



                //TrueNMST trueNMST = new TrueNMST(jd);

                //int mk = (int)Math.Floor((jd - trueNMST.HS[0]) / 30); 
                //if (mk < 13 && trueNMST.HS[mk + 1] <= jd) mk++; // 农历所在月的序数

                //ob.Ldi = (int)(jd - trueNMST.HS[mk]);   // 距农历月首的编移量,0对应初一
                //ob.Ldc = Data.LunarData.rmc[(int)(ob.Ldi)];      // 农历日名称
                //ob.cur_dz = jd - trueNMST.ZQ[0];     // 距冬至的天数
                //ob.cur_xz = jd - trueNMST.ZQ[12];    // 距夏至的天数
                //ob.cur_lq = jd - trueNMST.ZQ[15];    // 距立秋的天数
                //ob.cur_mz = jd - trueNMST.ZQ[11];    // 距芒种的天数
                //ob.cur_xs = jd  - trueNMST.ZQ[13];    // 距小暑的天数
                //if (jd == trueNMST.HS[mk] || jd == jdMonth0)
                //{
                //    // 月的信息
                //    ob.Lmc = trueNMST.LunarMonthName[mk];  // 月名称
                //    ob.Ldn = trueNMST.LunarMonthSize[mk];  // 月大小
                //    ob.Lleap = (trueNMST.LeapMonth != 0 && trueNMST.LeapMonth == mk) ? "闰" : "";   // 闰状况
                //    ob.Lmc2 = mk < 13 ? trueNMST.LunarMonthName[mk + 1] : "未知";   // 下个月名称,判断除夕时要用到
                //}
                //else
                //{
                //    var ob2 = (this.dayInfo[i - 1]);
                //    ob.Lmc = ob2.Lmc;
                //    ob.Ldn = ob2.Ldn;
                //    ob.Lleap = ob2.Lleap; 
                //    ob.Lmc2 = ob2.Lmc2;
                //}

                // 上一节气名称
                //int qk = (int)Math.Floor((jd - trueNMST.ZQ[0] - 7) / 15.2184); 
                //if (qk < 23 && jd >= trueNMST.ZQ[qk + 1]) qk++; //节气的取值范围是0-23
                //if (jd == trueNMST.ZQ[qk]) ob.Ljq = Data.LunarData.jqmc[qk];
                //else ob.Ljq = "";

                //ob.yxmc = ob.yxjd = ob.yxsj = "";    // 月相名称,月相时刻(儒略日),月相时间串
                //ob.jqmc = ob.jqjd = ob.jqsj = "";    // 定气名称,节气时刻(儒略日),节气时间串

                //// 干支纪年处理
                //// 以立春为界定年首
                //var D = trueNMST.ZQ[3] + (jd < trueNMST.ZQ[3] ? -365 : 0) + 365.25 * 16 - 35; //以立春为界定纪年
                //ob.Lyear = Math.Floor(D / 365.2422 + 0.5); //农历纪年(10进制,1984年起算)

                //// 以下几行以正月初一定年首
                //D = trueNMST.HS[2];     // 一般第3个月为春节
                //for (int j = 0; j < 14; j++)
                //{
                //    // 找春节
                //    if (trueNMST.LunarMonthName[j] != "正") continue;
                //    D = trueNMST.HS[j];
                //    if (jd < D) { D -= 365; break; }   // 无需再找下一个正月
                //}
                //D = D + 5810;    // 计算该年春节与1984年平均春节(立春附近)相差天数估计
                //ob.Lyear0 = Math.Floor(D / 365.2422 + 0.5);   // 农历纪年(10进制,1984年起算)

                //ob.Lyear2 = new GanZhi(ob.Lyear + 9000);    // 干支纪年(立春)
                //ob.Lyear3 = new GanZhi(ob.Lyear0 + 9000);   // 干支纪年(正月)
                //ob.Lyear4 = ob.Lyear0 + 1984 + 2698;    // 黄帝纪年




                //// 星座
                //mk = (int)Math.Floor((jd - trueNMST.ZQ[0] - 15) / 30.43685); 
                //if (mk < 11 && jd >= trueNMST.ZQ[2 * mk + 2]) mk++; //星座所在月的序数,(如果j=13,ob.d0不会超过第14号中气)
                //ob.XiZ =  (Xingzuo)((int)((mk + 12) % 12));

                //// 回历
                ////LunarData.getHuiLi(jd, ob);

                //// 节日
                //ob.A = ob.B = ob.C = ""; ob.Fjia = 0;
                //LunarData.getDayNameJ(ob);   //公历
                //LunarData.getDayNameL(ob);   //农历
            }

            //// 以下是月相与节气的处理
            ////double d;
            //var jd2 = jdMonth0 + DayInfo.deltatT2(jdMonth0) - 8d / 24d;
            ////int xn;

            //// 月相查找
            //var w = Ephemeris.MS_aLon(jd2 / 36525, 10, 3);
            //var D = 0d;
            //w = Math.Floor((w - 0.78) / Math.PI * 2) * Math.PI / 2;
            //do
            //{
            //    var d = Data.LunarData.so_accurate(w);
            //    D = Math.Floor(d + 0.5);
            //    var xn = (int)Math.Floor(w / (Math.PI * 2) * 4 + 4000000.01) % 4;
            //    w += (Math.PI * 2) / 4;
            //    if (D >= jdMonth0 + DayOfMonth) break;
            //    if (D < jdMonth0) continue;
            //    //var ob = (this.dayInfo[(int)(D - jdMonth0)]);
            //    //ob.yxmc = Data.LunarData.yxmc[xn];     // 取得月相名称
            //    //ob.yxjd = d.ToString();
            //    //ob.yxsj = new JDateTime(d, true).ToString();
            //} while (D + 5 < jdMonth0 + DayOfMonth);

            //// 节气查找
            //w = Ephemeris.S_aLon(jd2 / 36525, 3);
            //w = Math.Floor((w - 0.13) / (Math.PI * 2) * 24) * (Math.PI * 2) / 24;
            //do
            //{
            //    var d = Data.LunarData.qi_accurate(w);
            //    D = Math.Floor(d + 0.5);
            //    var xn = (int)Math.Floor(w / (Math.PI * 2) * 24 + 24000006.01) % 24;
            //    w += (Math.PI * 2) / 24;
            //    if (D >= jdMonth0 + DayOfMonth) break;
            //    if (D < jdMonth0) continue;
            //    //var ob = (this.dayInfo[(int)(D - jdMonth0)]);
            //    //ob.jqmc = Data.LunarData.jqmc[xn];     // 取得节气名称
            //    //ob.jqjd = d.ToString();
            //    //ob.jqsj = new JDateTime(d, true).ToString();
            //} while (D + 12 < jdMonth0 + DayOfMonth);

            //CalcRiJianThisMonth();    // 计算本月所有日期的日十二建信息
        }



        public string text1;
        public string text2;
        /// <summary>
        /// 文本月历生成
        /// </summary>
        /// <param name="By"></param>
        /// <param name="Bm"></param>
        /// <param name="curJD"></param>
        public void ToText()
        {
            int i, j;
            string c;
            string isM;
            //DayInfo ob; //日历物件

            

            //月历处理
            StringBuilder sb = new StringBuilder();
            //遍历本月各日(公历), 生成第 i 日的日历页面
            for (i = 0; i < this.DayNum; i++)
            {
                var ob = (this.LDay[i]);
                if (i == 0) { for (j = 0; j < this.WeekOfMonth0; j++) sb.AppendLine(); } //首行前面的空单元格

                sb.Append(ob.Index.ToString() + "\t");
                sb.Append(ob.isHolidy?"休":"正" + "\t");    // C#: 添加放假的标识
                sb.Append(ob.WeekNum.ToString() + "\t");    // C#: 添加星期的标识

                c = ""; isM = "";    // 文字格式控制项
                if (ob.Feasts(1).Count > 0) c = string.Join(",", ob.Feasts(1).Select(f => f.Name));
                if (ob.Feasts(2).Count > 0) c = string.Join(",", ob.Feasts(2).Select(f => f.Name));
                //if (ob.Feasts(3).Count > 0) c = string.Join(",", ob.Feasts(3).Select(f => f.Name));
                if (c.Length <= 0)
                {
                    if (ob.Index == 0) c = $"{(isLeap ? "闰" : "")}{Name}月{(isBig ? "大" : "小")}";
                    else c = $"{ob.Name}";
                }
                isM = ob.MoonState.ToString();
                isM += (ob.isTodayJieqi ? "◆" : "");
                //if (ob.yxmc == "朔") isM = "●";    // 取月相
                //if (ob.yxmc == "望") isM = "○";    // 取月相
                //if (ob.yxmc == "上弦") isM = "∪";
                //if (ob.yxmc == "下弦") isM = "∩";

                //if (ob.jqmc.Length > 0) isM += "◆";    // 定气标记

                sb.AppendLine($"{c}\t{isM}");    // 公历的日名称
            }
            text1 = sb.ToString();

            text2 = html2.Replace("&nbsp;", " ").Replace("<br>", "\r\n");
        }



        //public string ToHtml()
        //{
        //    return ToText().Replace("&nbsp;", " ").Replace("<br>", "\r\n");
        //}



        ///// <summary>
        ///// 计算本月所有日期的日十二建信息
        ///// </summary>
        //private void CalcRiJianThisMonth()
        //{
        //    string yuejian = String.Empty;

        //    //OB ob = new OB();

        //    for (int i = 0; i < this.DayNum; i++)    // 遍历月
        //    {
        //        var lunOb = this.Month[i];

        //        //if (i == 0 || lunOb.jqmc.Trim().Length > 0 || lunOb.Ljq.Trim().Length > 0)    // 每月第 1 日, 或遇到了交节气日, 则计算该日的所属节气等
        //        //{
        //        //    ob.y = lunOb.y;
        //        //    ob.m = lunOb.m;
        //        //    ob.d = lunOb.d;
        //        //    this.CalcJieQiInfo(ob, CalcJieQiType.CalcJie);
        //        //}
        //        //yuejian = ob.ThisJieQi.YueJian;
        //        //if (ob.ThisJieQi.DifferentTime)
        //        //    yuejian = LunarHelper.SUBSTRING(lunOb.Lmonth2, 1, 1);     // 调整为实历

        //        // 可直接使用该属性的月建而无需再次计算节气, 但上述被注释的代码也可用(主要为了测试 CalcJieQiInfo 方法, 暂保留)
        //        //yuejian = Util.SUBSTR(lunOb.Lmonth2, 1, 1);

        //        //lunOb.Ri12Jian = this.GetRi12Jian(yuejian, Util.SUBSTR(lunOb.Lday2, 1, 1));    // 计算日十二建
        //    }
        //}







        //public string html0;
        public string html1;
        public string html2;
        /// <summary>
        /// html月历生成,结果返回在lun中,curJD为当前日期(用于设置今日标识)
        /// </summary>
        /// <param name="By">要生成月历的年</param>
        /// <param name="Bm">要生成月历的月</param>
        /// <param name="curJD">当前日期(用于设置今日标识)</param>
        public void ToHtml(double curJD)
        {
            string sty_head = " style='font-family: 宋体; font-size: 14px; text-align: center; background-color: #E0E0FF; color: #000000; font-weight: bold' ";
            string sty_body = " style='font-family: 宋体; font-size: 12px; text-align: center ' ";
            string sty_date = " style='font-family: Arial Black; text-align: center;font-size: 20px' ";
            string sty_date2 = " style='font-family: Arial Black; text-align: center;font-size: 20px; color: #FF0000' ";
            string sty_cur = " style='background-color:#90D050' ";


            StringBuilder cr = new StringBuilder();
            var c = "";


            //月历处理
            StringBuilder ta0 = new StringBuilder(
                "<tr>"
              + $"<td{sty_head} width='%14'>日</td>"
              + $"<td{sty_head} width='%14'>一</td>"
              + $"<td{sty_head} width='%14'>二</td>"
              + $"<td{sty_head} width='%14'>三</td>"
              + $"<td{sty_head} width='%14'>四</td>"
              + $"<td{sty_head} width='%14'>五</td>"
              + $"<td{sty_head} width='%14'>六</td><tr>");    // C#: 为提高字符串处理效率, 使用 StringBuilder
            for (int i = 0; i < this.DayNum; i++)
            {
                // 遍历本月各日(公历), 生成第 i 日的日历页面
                var ob = (this.LDay[i]);
                if (i == 0)
                {
                    for (int j = 0; j < this.WeekOfMonth0; j++)     // 首行前面的空单元格(依据: 本月首日的星期)
                        cr.Append("<td" + sty_body + "></td>");
                }

                c = ""; 
                var isM = "";  // 文字格式控制项
                if (ob.Feasts(1).Count > 0) c = $"<font color=red>{string.Join(",", ob.Feasts(1).Select(f => f.Name))}</font>";
                if (ob.Feasts(2).Count > 0) c = $"<font color=blue>{string.Join(",", ob.Feasts(2).Select(f => f.Name))}</font>";
                //if (ob.Feasts(3).Count > 0) c = string.Join(",", ob.Feasts(3).Select(f => f.Name));
                if (c.Length <= 0)
                {
                    if (ob.Index == 0) c = $"{(isLeap ? "闰" : "")}{Name}月{(isBig ? "大" : "小")}";
                    else c = $"{ob.Name}";
                }
                isM = $"font color=#505000>{ob.MoonState.ToString()}</font>";
                isM += (ob.isTodayJieqi ? "<font color=#00C000>◆</font>" : "");
                var c2 = ob.isHolidy ? sty_date2 : sty_date;                 //节日置红色

                c2 = $"<span{c2}>{ob.jDate.ToStringGeroge("dd")}</span>";         // 公历的日名称
                if (ob.jd0 == curJD) c2 = $"<span{sty_cur}>{c2}</span>";        // 今日标识


                //if (ob.yxmc == "朔") isM = "<font color=#505000>●</font>";           // 取月相
                //if (ob.yxmc == "望") isM = "<font color=#F0B000>●</font>";           // 取月相
                //if (ob.yxmc == "上弦") isM = "<font color=#F0B000><b>∪</b></font>";
                //if (ob.yxmc == "下弦") isM = "<font color=#F0B000><b>∩</b></font>";



                //cr += "<td" + sty_body + "width='14%'>" + c2 + "<br>" + isM + c + "</td>";      // C#: 注释, 改写如下:
                cr.Append($"<td{sty_body} width='14%' onmouseover='changeBackcolor(this,1)' onmouseout='changeBackcolor(this,0)'>" 
                    + $"{c2}<br>{isM}{c}</td>");               // 鼠标事件处理

                if (i == this.DayNum - 1)
                {
                    for (int j = 0; j < 6 - ob.WeekIndex; j++)      // 末行后面的空单元格(依据: 本月末日的星期)
                        cr.Append($"<td{sty_body}></td>");
                }
                if (i == this.DayNum - 1 || ob.WeekIndex == 6)
                {
                    ta0.Append($"<tr>{cr.ToString()}</tr>");
                    cr.Remove(0, cr.Length);
                }
            }
            html1 = $"<table border=0 cellpadding=3 cellspacing=1 width='100%'>{ta0.ToString()}</table>";

            string b2 = "", b3 = "", b4 = "";
            for (int i = 0; i < this.DayNum; i++)
            {
                var ob = (this.LDay[i]);
                int c__ = i + 1;
                if (c__ < 10)  c = "&nbsp;" + c__;
                else   c = c__.ToString();

                if(ob.MoonState.shuowang == ShuoWang.朔 || ob.MoonState.shuowang == ShuoWang.望) b2 += $"{c}日 {ob.MoonState.ToStringDesc()}月 &nbsp;";
                else if (ob.MoonState.shuowang == ShuoWang.上弦 || ob.MoonState.shuowang == ShuoWang.下弦) b2 += $"{c}日 {ob.MoonState.ToStringDesc()} &nbsp;";
                //if(ob.Ldc =="初一") b1 += c +"日 "+ob.Lleap+ob.Lmc+"月" + (ob.Ldn==30?"大":"小")+" &nbsp;";
                //if (ob.yxmc == "朔" || ob.yxmc == "望") b2 += c + "日 " + ob.yxsj + ob.yxmc + "月 &nbsp;";
                //if (ob.yxmc == "上弦" || ob.yxmc == "下弦") b3 += c + "日 " + ob.yxsj + ob.yxmc + " &nbsp;";


                if (ob.isTodayJieqi) b4 += $"{c}日 {ob.Jieqi.ToString()} &nbsp;";
            }
            html2 = $"{b2}<br>{b3}<br>{b4}";
            //return html2;
            //this.ToText(By, Bm, curJD);   // C#: 转换时新增功能而增加的语句
        }

        //public void yueLiHTML(int By, int Bm, double curJD, int day)
        //{
        //    string sty_head = " style='font-family: 宋体; font-size: 14px; text-align: center; background-color: #E0E0FF; color: #000000; font-weight: bold' ";
        //    string sty_body = " style='font-family: 宋体; font-size: 12px; text-align: center ' ";
        //    string sty_date = " style='font-family: Arial Black; text-align: center;font-size: 20px' ";
        //    string sty_date2 = " style='font-family: Arial Black; text-align: center;font-size: 20px; color: #FF0000' ";
        //    string sty_cur = " style='background-color:#90D050' ";

        //    int i, j;
        //    string c, c2;
        //    StringBuilder cr = new StringBuilder();    // C#: 为提高字符串处理效率, 使用 StringBuilder
        //    string isM;
        //    DayInfo ob;     // 日历物件

        //    this.yueLiCalc(By, Bm);    // 农历计算

        //    // 年份处理
        //    c = this.nianhao + " 农历" + this.Ly + "年【" + this.ShX + "年】";       // 干支纪年
        //    if (c.Length > 33) c = "<span style='font-size:12px'>" + c + "</span>";
        //    else c = "<span style='font-size:16px;font-weight:bold'>" + c + "</span>";
        //    this.pg0 = c;

        //    //月历处理
        //    StringBuilder ta0 = new StringBuilder("<tr>"
        //      + "<td" + sty_head + "width='%14'>日</td>"
        //      + "<td" + sty_head + "width='%14'>一</td>"
        //      + "<td" + sty_head + "width='%14'>二</td>"
        //      + "<td" + sty_head + "width='%14'>三</td>"
        //      + "<td" + sty_head + "width='%14'>四</td>"
        //      + "<td" + sty_head + "width='%14'>五</td>"
        //      + "<td" + sty_head + "width='%14'>六</td><tr>");    // C#: 为提高字符串处理效率, 使用 StringBuilder

        //    day--;
        //    //for (day = 0; day < this.dn; day++)
        //    //{
        //    // 遍历本月各日(公历), 生成第 i 日的日历页面
        //    ob = (this.lun[day]);
        //    if (day == 0)
        //    {
        //        for (j = 0; j < this.w0; j++)     // 首行前面的空单元格(依据: 本月首日的星期)
        //            cr.Append("<td" + sty_body + "></td>");
        //    }

        //    c = ""; isM = "";  // 文字格式控制项
        //    if (ob.A.Length > 0)
        //        c += "<font color=red>" + this.substr2(ob.A, 4, "..") + "</font>";
        //    if (c.Length <= 0 && ob.B.Length > 0)
        //        c = "<font color=blue>" + this.substr2(ob.B, 4, "..") + "</font>";
        //    if (c.Length <= 0 && ob.Ldc == "初一")
        //        c = ob.Lleap + ob.Lmc + "月" + (ob.Ldn == 30 ? "大" : "小");   // 农历历月(闰月及大小等)
        //    if (c.Length <= 0)
        //        c = ob.Ldc;   // 取农历日名称

        //    if (ob.yxmc == "朔") isM = "<font color=#505000>●</font>";           // 取月相
        //    if (ob.yxmc == "望") isM = "<font color=#F0B000>●</font>";           // 取月相
        //    if (ob.yxmc == "上弦") isM = "<font color=#F0B000><b>∪</b></font>";
        //    if (ob.yxmc == "下弦") isM = "<font color=#F0B000><b>∩</b></font>";

        //    if (ob.jqmc.Length > 0)
        //        isM += "<font color=#00C000>◆</font>";  // 定气标记

        //    if (ob.Fjia != 0)
        //        c2 = sty_date2; //节日置红色
        //    else c2 = sty_date;

        //    //c2 += " onmouseover='showMessD(" + i + ")'";     // C#: 注掉鼠标事件, 下同
        //    //c2 += " onmouseout ='showMessD(-1)'";

        //    c2 = "<span" + c2 + ">" + ob.Day + "</span>"; //公历的日名称

        //    if (ob.JDayFrom2000 == curJD)
        //        c2 = "<span" + sty_cur + ">" + c2 + "</span>";   // 今日标识

        //    //cr += "<td" + sty_body + "width='14%'>" + c2 + "<br>" + isM + c + "</td>";      // C#: 注释, 改写如下:
        //    cr.Append("<td" + sty_body + "width='14%' onmouseover='changeBackcolor(this,1)' " +
        //                  "onmouseout='changeBackcolor(this,0)'>" +
        //                  c2 + "<br>" + isM + c + "</td>");               // C#: 新改写的语句, 增加鼠标事件处理

        //    if (day == this.dn - 1)
        //    {
        //        for (j = 0; j < 6 - ob.Week; j++)      // 末行后面的空单元格(依据: 本月末日的星期)
        //            cr.Append("<td" + sty_body + "></td>");
        //    }
        //    if (day == this.dn - 1 || ob.Week == 6)
        //    {
        //        ta0.Append("<tr>" + cr.ToString() + "</tr>");
        //        cr.Remove(0, cr.Length);
        //    }
        //    // }
        //    this.pg1 = "<table border=0 cellpadding=3 cellspacing=1 width='100%'>" + ta0.ToString() + "</table>";

        //    string b2 = "", b3 = "", b4 = "";
        //    int c__;
        //    for (i = 0; i < this.dn; i++)
        //    {
        //        ob = (this.lun[i]);
        //        c__ = i + 1;
        //        if (c__ < 10)
        //            c = "&nbsp;" + c__;
        //        else
        //            c = c__.ToString();
        //        //if(ob.Ldc =="初一") b1 += c +"日 "+ob.Lleap+ob.Lmc+"月" + (ob.Ldn==30?"大":"小")+" &nbsp;";
        //        if (ob.yxmc == "朔" || ob.yxmc == "望") b2 += c + "日 " + ob.yxsj + ob.yxmc + "月 &nbsp;";
        //        if (ob.yxmc == "上弦" || ob.yxmc == "下弦") b3 += c + "日 " + ob.yxsj + ob.yxmc + " &nbsp;";
        //        if (ob.jqmc.Length > 0) b4 += c + "日 " + ob.jqsj + ob.jqmc + " &nbsp;";
        //    }
        //    this.pg2 = b2 + "<br>" + b3 + "<br>" + b4;

        //    this.yueLiText(By, Bm, curJD);   // C#: 转换时新增功能而增加的语句
        //}
    }



}
