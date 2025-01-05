using Prophecy.Astronomy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Prophecy
{
    public partial class JDateTime
    {


        #region Lunar Storage

        private int _lunarYear;         // 以大年初一定的农历年份
        private int _lunarYear0;        // 以立春界定的农历年份

        private List<double> HS;        // 月亮合朔的儒略日值，从上一年农历十一月初一开始，视作农历的月头，大年初一中午12时是HS[2]
        private List<double> ZQ;        // 节气中午12时的儒略日值，从冬至开始

        private int _lunarMonth;                            // 月序号 即0表示十一月（子），2表示正月（寅），闰月的序号和相应平月相同。
        private int _lunarMonthRealIndex;                   // 真实序号是为了查月信息，因为闰月和平月可能用同一个_lunarMonth值
        private double _lunarMonth0jd;                      // 农历月的起始子时儒略日期
        private double _lunarMonth1jd;                      // 农历下个月月头的子时儒略日期
        private int _jieqi;                                 // 节气序号，从0=冬至开始
        private bool _lunarIsLeapMonth;                     // 是否是闰月
        private int _lunarMonthNumOfYear;                   // 今年事实上有多少个月？12或者13
        private const int MAXMonthNUM = 14;                 // 最大月存储数=14
        int[] _LunarMonthSize = new int[MAXMonthNUM];
        int[] _LunarMonthIndex = new int[MAXMonthNUM];
        string[] _LunarMonthName = new string[MAXMonthNUM];





        private int _lunarDay;              // 农历日数，从1开始计数
                                            //private int _lunarHour;



        #endregion


        #region 农历年



        /// <summary>
        /// 本日所属以立春为界定的农历年序数
        /// </summary>
        public int LunarYear0 { get { UpdateLunar(); return _lunarYear0; } }
        //{
        //    get
        //    {
        //            // 以立春为界定年首
        //            var D = ZQ[3] + (JulianDateFrom2000 < ZQ[3] ? -365 : 0) + 365.25 * 16 - 35; //以立春为界定纪年
        //        var Lyear = Math.Floor(D / 365.2422 + 0.5); //农历纪年(10进制,1984年起算)
        //        return (int)Lyear;

        /// <summary>
        /// 本日所属以正月初一为界定的农历年序数
        /// </summary>
        public int LunarYear { get { UpdateLunar(); return _lunarYear; } }


        //        //ob.Lyear2 = new GanZhi(ob.Lyear + 9000);    // 干支纪年(立春)
        //        //ob.Lyear3 = new GanZhi(ob.Lyear0 + 9000);   // 干支纪年(正月)
        //        //ob.Lyear4 = ob.Lyear0 + 1984 + 2698;    // 黄帝纪年
        //    }
        //}

        ///// <summary>
        ///// 本日所属以正月初一为界定的农历年
        ///// </summary>
        //public int LunarYear0Zheng
        //{
        //    get
        //    {
        //        // 以下几行以正月初一定年首
        //        var D = Year.HS[2];     // 一般第3个月为春节
        //        for (int j = 0; j < 14; j++)
        //        {
        //            // 找春节
        //            if (Year.Month[j].Name != "正") continue;
        //            D = Year.HS[j];
        //            if (jd0 < D) { D -= 365; break; }   // 无需再找下一个正月
        //        }
        //        D = D + 5810;    // 计算该年春节与1984年平均春节(立春附近)相差天数估计
        //        return (int)(Math.Floor(D / 365.2422 + 0.5));   // 农历纪年(10进制,1984年起算)
        //    }
        //}



        /// <summary>
        /// 生肖
        /// </summary>
        public Shengxiao LunarShengxiao
        {
            get
            {
                var ls = LunarYear + 8;
                if (ls < 0) ls = (Math.Abs(ls / 12) + 1) * 12 + ls;
                ls = ls % 12;
                return (Shengxiao)ls;
            }
        }



        #endregion

        #region 农历月



        /// <summary>
        /// 农历月序号，1~12，闰月和上一个月相同
        /// </summary>
        public int LunarMonth { get { UpdateLunar(); return ((_lunarMonth + 10) % 12) + 1; } }

        /// <summary>
        /// 农历月真实序号，0~11或0~12
        /// </summary>
        public int LunarMonthReal { get { UpdateLunar();return _lunarMonthRealIndex; } }

        /// <summary>
        /// 今年农历一共多少个月
        /// </summary>
        public int LunarMonthNumOfYear { get { UpdateLunar();return _lunarMonthNumOfYear; } }

        /// <summary>
        /// 今年农历有没有闰月
        /// </summary>
        public bool isLunarYearHasLeapMonth { get { UpdateLunar(); return _lunarMonthNumOfYear > 12; } }

        /// <summary>
        /// 农历月的月名。由于存在历法变化，所以这名字不一定是完全合规的，需要预先生成。
        /// </summary>
        public string LunarMonthName
        {
            get
            {
                UpdateLunar();
                return _LunarMonthName[_lunarMonthRealIndex];
            }
        }

        /// <summary>
        /// 是否有农历的本月闰月存在？意思是下一个月是闰本月的吗？
        /// </summary>
        public bool IsLunarNextMonthLeapMonth { get { UpdateLunar(); return (_lunarMonthRealIndex < _lunarMonthNumOfYear && _LunarMonthIndex[_lunarMonthRealIndex] == _LunarMonthIndex[_lunarMonthRealIndex + 1]); } }

        /// <summary>
        /// 是否是农历的闰月
        /// </summary>
        public bool IsLunarLeapMonth { get { UpdateLunar(); return _lunarIsLeapMonth; } }

        /// <summary>
        /// 是否是农历的大月？
        /// </summary>
        public bool IsLunarBigMonth { get { UpdateLunar(); return _LunarMonthSize[_lunarMonthRealIndex] > 29; } }


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
            string[] ymc = ["十一", "十二", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十"]; //月名称,建寅

            return ymc[((int)(index)) % ymc.Length];
        }




        #endregion



        #region 农历日
        /// <summary>
        /// 农历日，1~30
        /// </summary>
        public int LunarDay { 
            get
            {
                UpdateLunar();
                return _lunarDay;
            }
        }


        /// <summary>
        /// 农历日名
        /// </summary>
        public LunarDayName LunarDayName
        {
            get
            {
                return ((LunarDayName)(LunarDay - 1));
            }
        }



        ///// <summary>
        ///// 日干支
        ///// </summary>
        //public GanZhi ganZhi
        //{
        //    get
        //    {
        //        return new GanZhi(JulianDate + 50);
        //    }
        //}




        #endregion

        #region 农历时
        /// <summary>
        /// 农历时辰
        /// </summary>
        public int LunarHour
        {
            get
            {
                return ((Hour + 1) / 2 % 12) + 1;
            }
        }
        static string[] LunarHourName = ["夜半","鸡鸣","平旦","日出","食时","隅中","日中","日昳","晡时","日入","黄昏","人定"];
        static string[] LunarTickName = ["初", "二", "三", "四"];

        /// <summary>
        /// 农历几刻
        /// </summary>
        public int LunarTick
        {
            get
            {
                return (Minute / 15) + 1;
            }
        }


        /// <summary>
        /// 农历时刻描述串，即子初三刻、午正二刻……
        /// </summary>
        public string LunarShiKe
        {
            get
            {
                var h = (Dizhi)(LunarHour - 1);
                var h2 = Hour % 2 == 0?"初":"正";
                var t = LunarTickName[LunarTick - 1];
                return $"{h.ToString()}{h2}{t}刻";
            }
        }


        #endregion



        #region 节气




        /// <summary>
        /// 所属节气
        /// </summary>
        public JieQi Jieqi
        {
            get
            {
                //var w = AstronomyOld.S_aLon(JulianCentury, 3);
                //w = Math.Floor((w - 0.13) / (Math.PI * 2) * 24) * (Math.PI * 2) / 24;

                ////var D = Math.Floor(Data.LunarData.qi_accurate(w) + 0.5);
                //_jieqiBegin = Math.Floor(Data.LunarData.qi_accurate(w) + 0.5 + JD_2000);
                //var xn = (int)Math.Floor(w / (Math.PI * 2) * 24 + 24000006.01) % 24;

                //return (JieQi)(xn);
                UpdateLunar();
                return (JieQi)(_jieqi);
            }
        }

        


        /// <summary>
        /// 当前节气起始时间
        /// </summary>
        public double JieqiBegin
        {
            get
            {
                UpdateLunar();
                return JulianDate - GetJulianDateOfLunarHour0(ZQ[(int)Jieqi]);
            }
        }


        /// <summary>
        /// 当天是否交节气？
        /// </summary>
        public bool isTodayJieqi
        {
            get
            {
                return Math.Abs(JieqiBegin) <= 1;
            }
        }

        /// <summary>
        /// 距离特定节气过了几天？
        /// </summary>
        /// <param name="jq"></param>
        /// <returns></returns>
        public int LunarToJieqiDays(JieQi jq)
        {
            UpdateLunar();
            return (int)(JulianDate - GetJulianDateOfLunarHour0(ZQ[(int)jq]));
        }


        /// <summary>
        /// 节气当天 子时的儒略日期
        /// </summary>
        /// <param name="jq"></param>
        /// <returns></returns>
        public double JieqiJulianDate(JieQi jq)
        {
            UpdateLunar();
            return GetJulianDateOfLunarHour0(ZQ[(int)jq]);
        }



        #endregion




        /// <summary>
        /// 更新农历数据
        /// </summary>
        private void UpdateLunar()
        {
            if (!_needUpdateLunar) return;
            lock (updateMutex)
            {
                if (!_needUpdateLunar) return;


                // 农历排月序计算,可定出农历,有效范围：两个冬至之间(冬至一 ≤ d ＜ 冬至二)
                ZQ = AstronomyOld.calcJieQiDongzhi(JulianDateFrom2000);

                //ZQ = new List<double>();
                //ZQ.AddRange(Astronomy.Astronomy.AdjustedSolarTermsJd(GerogeYear - 1, 18, 23));
                //ZQ.AddRange(Astronomy.Astronomy.AdjustedSolarTermsJd(GerogeYear, 0, 24));

                HS = new List<double>();
                // 今年"首朔"的日月黄经差,是农历十一月初一
                var winter = AstronomyOld.calShuo(ZQ[0]);
                // 求较靠近冬至的朔日，十一月要把冬至包括在月内
                if (winter > ZQ[0]) winter -= 29.53;
                // 该年所有朔,包含14个月的始末
                for (int i = 0; i < 15; i++)
                {
                    HS.Add(AstronomyOld.calShuo(winter + 29.5306 * i));
                }


                // 计算当前是哪个节气
                for(int i = 1; i < 24; i++)
                {
                    if (JulianDate < GetJulianDateOfLunarHour0(ZQ[i]))
                    {
                        _jieqi = i - 1; 
                        break;
                    }
                }
                

                _UpdateLunarMonths();



                // 以立春为界定年首
                var LunarMatchedGerogeYear = new JDateTime(ZQ[6], true).GerogeYear;
                if (GetJulianDateOfLunarHour0(ZQ[3]) > JulianDate) _lunarYear0 = LunarMatchedGerogeYear - 1;
                else _lunarYear0 = LunarMatchedGerogeYear;
                //var D = ZQ[3] + (JulianDate < ZQ[3] ? -365 : 0) + 365.25 * 16 - 35; //以立春为界定纪年
                //_lunarYear0 = (int)(Math.Floor(D / 365.2422 + 0.5)); //农历纪年(10进制,1984年起算)

                // 以下几行以正月初一定年首，日子如果在十一月、十二月里，算作农历的前一年
                if (_lunarMonthRealIndex < 2) _lunarYear = LunarMatchedGerogeYear - 1;
                else _lunarYear = LunarMatchedGerogeYear;


                _needUpdateLunar = false;
            }

          

            //return (lunarYearIndex, lunarMonthIndex, lunarDayIndex, lunarHourIndex, isLeapMonth);
        }

        /// <summary>
        /// 更新月信息
        /// </summary>
        private void _UpdateLunarMonths()
        {

            var LeapMonth = -1;
            var realMonthNUM = 12;
            for (int i = 0; i < MAXMonthNUM; i++)
            {
                _LunarMonthSize[i] = (int)(HS[i + 1] - HS[i]); // 月大小
                _LunarMonthIndex[i] = i;
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
                    _LunarMonthIndex[i] = i - 1;
                    //LunarMonthName[i] = (LunarMonth[i] - 1).ToString();

                }
            }

            // 名称转换(月建别名)
            for (int i = 0; i < MAXMonthNUM; i++)
            {
                double jdm0 = HS[i] + JD_2000;// 初一的儒略日
                var v = _LunarMonthIndex[i];  //月建序号
                string name = GetLunarMonthName(v);    // 月建对应的默认月名称：建子十一,建丑十二,建寅为正……
                if (jdm0 >= 1724360 && jdm0 <= 1729794) name = GetLunarMonthName(v + 1);        //  8.01.15至 23.12.02 建子为十二,其它顺推
                else if (jdm0 >= 1807724 && jdm0 <= 1808699) name = GetLunarMonthName(v + 1);   // 237.04.12至239.12.13 建子为十二,其它顺推
                else if (jdm0 >= 1999349 && jdm0 <= 1999467) name = name = GetLunarMonthName(v + 2);   // 761.12.02至762.03.30 建子为正月,其它顺推
                else if (jdm0 >= 1973067 && jdm0 <= 1977112) { if (v % 12 == 0) name = "正"; if (v == 2) name = "一"; }    // 689.12.18至701.01.14 建子为正月,建寅为一月,其它不变

                if (jdm0 == 1729794 || jdm0 == 1808699) name = "拾贰";    // 239.12.13及23.12.02均为十二月,为避免两个连续十二月，此处改名

                _LunarMonthName[i] = name;
            }

            // -721年至-104年的后九月及月建的闰月名称有变化,与朔有关，与气无关
            double YY = Math.Floor((ZQ[0] + 10 + 180) / 365.2422) + 2000; // 确定年份
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
                    if (f3 < 12) _LunarMonthName[i] = GetLunarMonthName(f3 + x0); else _LunarMonthName[i] = lName;
                }

            }
            _lunarMonthNumOfYear = LeapMonth > 0 ? 13 : 12;
            _lunarMonth0jd = 0;
            _lunarMonth1jd = 0;
            _lunarMonthRealIndex = 0;
            _lunarMonth = 0;
            _lunarIsLeapMonth = false;
            _lunarDay = 1;
            for (int i = 1; i < MAXMonthNUM; i++)
            {
                
                var month0 = GetJulianDateOfLunarHour0(HS[i - 1]);
                var month0Next = GetJulianDateOfLunarHour0(HS[i]);
                if (JulianDate >= month0 && JulianDate < month0Next)
                {
                    _lunarMonth0jd = month0;
                    _lunarMonth1jd = month0Next;
                    _lunarMonthRealIndex = i - 1;
                    _lunarMonth = _LunarMonthIndex[_lunarMonthRealIndex];
                    
                    _lunarIsLeapMonth = _lunarMonthRealIndex > 0 && _LunarMonthIndex[_lunarMonthRealIndex] == _LunarMonthIndex[_lunarMonthRealIndex - 1];
                    _lunarDay = (int)(JulianDate - month0) + 1;
                    return;
                }

                //    Month.Add(new LunarMonth(
                //        this,
                //        HS[i],
                //        LunarMonthIndex[i],
                //        LunarMonthName[i],
                //        LunarMonthSize[i],
                //        i > 0 && LunarMonthIndex[i] == LunarMonthIndex[i - 1],
                //        i + 1 == realMonthNUM
                //    ));
            }
            // Not Match??
        }


        /// <summary>
        /// 获取儒略日当天子时的时刻，即公历上比当前早的23:00:00
        /// </summary>
        /// <param name="jd2000"></param>
        /// <returns></returns>
        private double GetJulianDateOfLunarHour0(double jd2000)
        {
            var jdt = new JDateTime(jd2000, true);
            jdt = new JDateTime(jdt.GerogeYear, jdt.GerogeMonth, jdt.GerogeDay, 23, 0, 0);
            if (jdt.JulianDateFrom2000 > jd2000) jdt.AddDays(-1);
            return jdt.JulianDate;
        }















        /// <summary>
        /// 将当前 JDateTime 转换为农历格式的字符串
        /// </summary>
        /// <param name="format">农历格式化字符串，例如 "yyyy-MM-dd HH:mm:ss"</param>
        /// <param name="formatProvider">格式化提供程序</param>
        /// <returns>格式化的回历日期字符串</returns>
        public string ToStringLunar(string format, IFormatProvider formatProvider = null)
        {
            return DealTimeToStringTemplate(format, LunarYear, LunarMonth, LunarDay, GerogeWeek, LunarHour, LunarTick, Second);
        }





        public string LunarTest()
        {
            string res = "";

            for (int i = 0; i < HS.Count; i++)
            {
                var moonjd = HS[i];
                res += $"{i} -> {new JDateTime(moonjd,true).ToStringGeroge()}\r\n";
            }

            for (int i = 0; i < ZQ.Count; i += 2)
            {
                var jq = ZQ[i];
                var jq2 = i + 1 < ZQ.Count ? ZQ[i + 1] : 0;
                res += $"{i} -> {new JDateTime(jq, true).ToStringGeroge()}{(jq2 != 0 ? $"\t{new JDateTime(jq2, true).ToStringGeroge()}" : "")}\r\n";
            }

            return res;
        }
        #region YinYang Methods

        ///// <summary>
        ///// 星座名称
        ///// </summary>
        //public Xingzuo Xingzuo { get {
        //        var mk = (int)Math.Floor((JulianDate - _trueNMST.ZQ[0] - 15) / 30.43685);
        //        if (mk < 11 && JulianDate >= _trueNMST.ZQ[2 * mk + 2]) mk++; //星座所在月的序数,(如果j=13,ob.d0不会超过第14号中气)
        //        return (Xingzuo)((int)((mk + 12) % 12));

        //} }


        //public int Jieqi
        //{
        //    get
        //    {

        //    }
        //}





        #endregion


    }
}
