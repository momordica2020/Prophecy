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

        /// <summary>
        /// 农历月序号，1~12，闰月和上一个月相同
        /// </summary>
        public int LunarMonth { 
            get
            {
                UpdateLunar();
                return ((_lunarMonth + 10) % 12) + 1;
            }
        }

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
        /// 农历日
        /// </summary>
        public int LunarDay { 
            get
            {
                UpdateLunar();
                return _lunarDay;
            }
        }

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
        /// <summary>
        /// 是否是农历的闰月
        /// </summary>
        public bool IsLunarLeapMonth { get
            {
                UpdateLunar();
                return _lunarIsLeapMonth;
            }
        }

        /// <summary>
        /// 是否是农历的大月？
        /// </summary>
        public bool IsLunarBigMonth
        {
            get
            {
                UpdateLunar();
                return _LunarMonthSize[_lunarMonthRealIndex] > 29;
            }
        }


        


        ///// <summary>
        ///// 距冬至的天数
        ///// </summary>
        //public int toDongzhi { get { UpdateLunar(); return (int)(JulianDateFrom2000 - ZQ[0]); } }

        ///// <summary>
        ///// 距芒种的天数
        ///// </summary>
        //public int toMangzhong { get { UpdateLunar(); return (int)(JulianDateFrom2000 - ZQ[11]); } }

        ///// <summary>
        ///// 距夏至的天数
        ///// </summary>
        //public int toXiazhi { get { UpdateLunar(); return (int)(JulianDateFrom2000 - ZQ[12]); } }


        ///// <summary>
        ///// 距小暑的天数
        ///// </summary>
        //public int toXiaoshu { get { UpdateLunar(); return (int)(JulianDateFrom2000 - ZQ[13]); } }

        ///// <summary>
        ///// 距立秋的天数
        ///// </summary>
        //public int toLiqiu { get { UpdateLunar(); return (int)(JulianDateFrom2000 - ZQ[15]); } }


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

        ///// <summary>
        /////  当日星座
        ///// </summary>
        //public Xingzuo xingzuo
        //{
        //    get
        //    {

        //        var mk = (int)Math.Floor((jd0 - Year.ZQ[0] - 15) / 30.43685);
        //        if (mk < 11 && jd0 >= Year.ZQ[2 * mk + 2]) mk++; //星座所在月的序数,(如果j=13,ob.d0不会超过第14号中气)
        //        return (Xingzuo)((int)((mk + 12) % 12));
        //    }
        //}


        /// <summary>
        /// 当日所属节日
        /// </summary>
        /// <param name="level">节日等级，-1：全部，1：重要，2：一般，3：不重要</param>
        /// <returns></returns>
        public Feast[] Feasts(int level = -1)
        {
            if (level < 0) return FeastStructs.MatchFeasts(this).ToArray();
            else return FeastStructs.MatchFeasts(this).Where(f => f.Level == level).ToArray();
        }


        ///// <summary>
        ///// 是否放假
        ///// </summary>
        //public bool isHolidy
        //{
        //    get
        //    {
        //        return WeekNum == 5 || WeekNum == 6 || Feasts().Where(f => f.isHolidy == true).Any();
        //    }
        //}

        /// <summary>
        /// 月相，值在0~1之间，0是新月，0.5是满月
        /// </summary>
        public MoonState MoonState
        {
            get
            {
                double w = AstronomyOld.MS_aLon(JulianDateFrom2000 / 36525, 10, 3);
                w = Math.Floor((w - 0.78) / Math.PI * 2) * Math.PI / 2;

                // 使用精确月相时间函数计算月相
                // double D = Math.Floor(LunarData.so_accurate(w) + 0.5); 

                // 计算月相位置（0~1 之间的值）
                double phaseValue = (w % Math.PI * 2) / (Math.PI * 2); // 将 w 归一化到 [0, 1)
                return new MoonState(phaseValue < 0 ? phaseValue + 1 : phaseValue); // 确保值在 [0, 1)

            }
        }

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
        public int LunarYear{ get { UpdateLunar(); return _lunarYear; }}


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





        /// <summary>
        /// 农历日名
        /// </summary>
        public LunarDayName LunarDayName
        {
            get
            {
                return ((LunarDayName)(LunarDay-1));
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



        #region Lunar Storage

        private int _lunarYear; 
        private int _lunarYear0;

        /// <summary>
        /// 农历月的序号，即0表示十一月（子），2表示正月（寅），闰月的序号和相应平月相同。
        /// </summary>
        private int _lunarMonth;
        private int _lunarMonthRealIndex;   // 真实序号是为了查月信息，因为闰月和平月可能用同一个_lunarMonth值
        private int _jieqi;
        private int _lunarDay;
        //private int _lunarHour;
        private bool _lunarIsLeapMonth;

        private List<double> HS;

        private List<double> ZQ;
        //private List<double> ZQ2;
        

        const int MAXMonthNUM = 14;
        int[] _LunarMonthSize = new int[MAXMonthNUM];
        int[] _LunarMonthIndex = new int[MAXMonthNUM];
        string[] _LunarMonthName = new string[MAXMonthNUM];

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
                // 求较靠近冬至的朔日
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
                

                initLunarMonths();



                // 以立春为界定年首
                var LunarMatchedGerogeYear = new JDateTime(ZQ[6], true).GerogeYear;
                if (GetJulianDateOfLunarHour0(ZQ[3]) > JulianDate) _lunarYear0 = LunarMatchedGerogeYear - 1;
                else _lunarYear0 = LunarMatchedGerogeYear;
                //var D = ZQ[3] + (JulianDate < ZQ[3] ? -365 : 0) + 365.25 * 16 - 35; //以立春为界定纪年
                //_lunarYear0 = (int)(Math.Floor(D / 365.2422 + 0.5)); //农历纪年(10进制,1984年起算)

                // 以下几行以正月初一定年首
                if (_lunarMonthRealIndex < 2) _lunarYear = LunarMatchedGerogeYear - 1;
                else _lunarYear = LunarMatchedGerogeYear;
                //D = HS[2];     // 一般第3个月为春节
                //for (int j = 0; j < 14; j++)
                //{
                //    // 找春节
                //    if (_LunarMonthName[j] != "正") continue;
                //    D = HS[j];
                //    if (JulianDate < D) { D -= 365; break; }   // 无需再找下一个正月
                //}
                //D = D + 5810;    // 计算该年春节与1984年平均春节(立春附近)相差天数估计
                //_lunarYear = (int)(Math.Floor(D / 365.2422 + 0.5));   // 农历纪年(10进制,1984年起算)




                //// 农历年序号，应在农历冬至开始至下一个冬至日为止
                //_lunarYear = Year;
                //double winterSolsticeStart = _trueNMST.ZQ[0]; // 获取当前农历年初的冬至节气日期
                //double winterSolsticeEnd = _trueNMST.ZQ[24];  // 获取上一年年底的冬至节气日期
                //if (_julianDate < winterSolsticeStart) _lunarYear -= 1;
                //else if (_julianDate >= winterSolsticeEnd) _lunarYear += 1;


                //// 确定农历月
                //int lunarMonthIndex = (int)Math.Floor((JulianDate - _trueNMST.HS[0]) / 30);
                //if (lunarMonthIndex < 13 && _trueNMST.HS[lunarMonthIndex + 1] <= JulianDate) lunarMonthIndex++;
                //int lunarDayIndex = (int)(JulianDate - _trueNMST.HS[lunarMonthIndex]);
                //int lunarHourIndex = (int)(((_julianDate - Math.Floor(_julianDate)) * 24 + 25) / 2) % 12;

                //// 计算农历信息
                ////string lunarDayName = Data.LunarData.rmc[(int)(JulianDate - trueNMST.HS[lunarMonthIndex])];
                ////string lunarMonthName = trueNMST.LunarMonthName[lunarMonthIndex];
                //// 当前是否处于农历闰月里
                //bool isLeapMonth = (_trueNMST.LeapMonth != 0 && _trueNMST.LeapMonth == lunarMonthIndex);

                //// 计算干支纪年和生肖
                ////double yearOffset = Year - 1984 + 9000;
                ////string ganZhiYear = Data.LunarData.Gan[(int)(yearOffset % 10)] + Data.LunarData.Zhi[(int)(yearOffset % 12)];
                ////string shx = Data.LunarData.ShX[(int)(yeaOffset % 12)];


                _needUpdateLunar = false;
            }

          

            //return (lunarYearIndex, lunarMonthIndex, lunarDayIndex, lunarHourIndex, isLeapMonth);
        }

        void initLunarMonths()
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
            //realMonthNUM = LeapMonth > 0 ? 13 : 12;
            for (int i = 1; i < MAXMonthNUM; i++)
            {
                
                var month0 = GetJulianDateOfLunarHour0(HS[i - 1]);
                var month0Next = GetJulianDateOfLunarHour0(HS[i]);
                if (JulianDate >= month0 && JulianDate < month0Next)
                {
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
            _lunarMonthRealIndex = 0;
            _lunarMonth = 0;
            _lunarIsLeapMonth = false;
            _lunarDay = 1;
        }


        /// <summary>
        /// 获取儒略日当天子时的时刻，即公历上比当前早的23:00:00
        /// </summary>
        /// <param name="jd2000"></param>
        /// <returns></returns>
        double GetJulianDateOfLunarHour0(double jd2000)
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
            // 如果未提供格式化器，使用默认的文化信息
            var culture = formatProvider as CultureInfo ?? CultureInfo.InvariantCulture;


            // 构造虚拟的 DateTime 对象来格式化时间部分
            DateTime fakeDateTime = new DateTime(
                LunarYear==0?1:Math.Abs(LunarYear),
                LunarMonth,
                LunarDay,
                LunarHour,//LunarHour,
                Minute,
                Second,
                Millisecond
            );

            // 使用格式化器格式化时间部分
            string result = fakeDateTime.ToString(format, culture);

            // 如果是公元前的年份，手动替换年份部分
            if (LunarYear <= 0)
            {
                string yearString = $"-{Math.Abs(LunarYear - 1):D4}";
                result = result.Replace(fakeDateTime.Year.ToString("D4"), yearString);
            }

            return result;
        }


        public string LunarTest()
        {
            string res = "";

            for (int i = 0; i < HS.Count; i++)
            {
                var moonjd = HS[i];
                res += $"{i} -> {new JDateTime(moonjd,true).ToStringGeroge("yyyy-MM-dd HH:mm:ss")}\r\n";
            }

            for (int i = 0; i < ZQ.Count; i += 2)
            {
                var jq = ZQ[i];
                var jq2 = i + 1 < ZQ.Count ? ZQ[i + 1] : 0;
                res += $"{i} -> {new JDateTime(jq, true).ToStringGeroge("yyyy-MM-dd HH:mm:ss")}{(jq2 > 0 ? $"\t{new JDateTime(jq2, true).ToStringGeroge("yyyy-MM-dd HH:mm:ss")}" : "")}\r\n";
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
