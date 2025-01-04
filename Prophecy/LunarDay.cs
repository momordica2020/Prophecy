using Prophecy.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using Prophecy.Astronomy;

namespace Prophecy
{
    public class LunarDay
    {
        /// <summary>
        /// 本日所属农历月
        /// </summary>
        public readonly LunarMonth Month;



        private JDateTime _jDate;
        /// <summary>
        /// 本日的公历、回历信息
        /// </summary>
        public  JDateTime jDate { get {
                if (this._jDate == null) _jDate = new JDateTime(jd0, true);
                return _jDate;
            } }

        /// <summary>
        /// 本日所属农历年
        /// </summary>
        public LunarYear Year { get { return Month.Year; } }

        /// <summary>
        /// 本日初jd
        /// </summary>
        public double jd0 { get { return Month.jd0 + Index; } }

        /// <summary>
        /// 在农历月内的日序数
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// 距冬至的天数
        /// </summary>
        public int toDongzhi { get { return (int)(jd0 - Year.ZQ[0]); } }

        /// <summary>
        /// 距芒种的天数
        /// </summary>
        public int toMangzhong { get { return (int)(jd0 - Year.ZQ[11]); } }

        /// <summary>
        /// 距夏至的天数
        /// </summary>
        public int toXiazhi { get { return (int)(jd0 - Year.ZQ[12]); } }


        /// <summary>
        /// 距小暑的天数
        /// </summary>
        public int toXiaoshu { get { return (int)(jd0 - Year.ZQ[13]); } }

        /// <summary>
        /// 距立秋的天数
        /// </summary>
        public int toLiqiu { get { return (int)(jd0 - Year.ZQ[15]); } }

        /// <summary>
        ///  当日星座
        /// </summary>
        public Xingzuo xingzuo { get
            {
               
                var mk = (int)Math.Floor((jd0 - Year.ZQ[0] - 15) / 30.43685); 
                if (mk < 11 && jd0 >= Year.ZQ[2 * mk + 2]) mk++; //星座所在月的序数,(如果j=13,ob.d0不会超过第14号中气)
                return (Xingzuo)((int)((mk + 12) % 12));
            }
        }


        /// <summary>
        /// 当日所属节日
        /// </summary>
        /// <param name="level">节日等级，-1：全部，1：重要，2：一般，3：不重要</param>
        /// <returns></returns>
        public List<Feast> Feasts(int level = -1)
        {
            if(level < 0) return FeastStructs.MatchFeasts(jDate).ToList();
            else return FeastStructs.MatchFeasts(jDate).Where(f=>f.Level == level).ToList(); 
        }


        /// <summary>
        /// 是否放假
        /// </summary>
        public bool isHolidy
        {
            get
            {
                return  WeekNum==5 || WeekNum==6 || Feasts().Where(f=>f.isHolidy==true).Any();
            }
        }

        /// <summary>
        /// 月相，值在0~1之间，0是新月，0.5是满月
        /// </summary>
        public MoonState MoonState
        {
            get
            {
                double w = AstronomyOld.MS_aLon(jd0 / 36525, 10, 3);
                w =  Math.Floor((w - 0.78) / Math.PI * 2) * Math.PI / 2;

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
                var w = AstronomyOld.S_aLon(jd0 / 36525, 3);
                w = Math.Floor((w - 0.13) / (Math.PI * 2) * 24) * (Math.PI * 2) / 24;

                //var D = Math.Floor(Data.LunarData.qi_accurate(w) + 0.5);
                _jieqiBegin = Math.Floor(Data.LunarData.qi_accurate(w) + 0.5);
                var xn = (int)Math.Floor(w / (Math.PI * 2) * 24 + 24000006.01) % 24;

                return (JieQi)(xn);
            }
        }

        double _jieqiBegin = 0;


        /// <summary>
        /// 当前节气起始时间
        /// </summary>
        public double JieqiBegin
        {
            get
            {
                return _jieqiBegin;
            }
        }


        /// <summary>
        /// 当天是否交节气？
        /// </summary>
        public bool isTodayJieqi
        {
            get
            {
                return ((int)jd0) == ((int)JieqiBegin);
            }
        }

        /// <summary>
        /// 本日所属以立春为界定的农历年
        /// </summary>
        public int LunarYear0
        {
            get
            {
                // 岁首
                // 干支纪年处理
                // 以立春为界定年首
                var D = Year.ZQ[3] + (jd0 < Year.ZQ[3] ? -365 : 0) + 365.25 * 16 - 35; //以立春为界定纪年
                var Lyear = Math.Floor(D / 365.2422 + 0.5); //农历纪年(10进制,1984年起算)
                return (int)Lyear;


                

                //ob.Lyear2 = new GanZhi(ob.Lyear + 9000);    // 干支纪年(立春)
                //ob.Lyear3 = new GanZhi(ob.Lyear0 + 9000);   // 干支纪年(正月)
                //ob.Lyear4 = ob.Lyear0 + 1984 + 2698;    // 黄帝纪年
            }
        }

        /// <summary>
        /// 本日所属以正月初一为界定的农历年
        /// </summary>
        public int LunarYear0Zheng
        {
            get
            {
                // 以下几行以正月初一定年首
                var D = Year.HS[2];     // 一般第3个月为春节
                for (int j = 0; j < 14; j++)
                {
                    // 找春节
                    if (Year.Month[j].Name != "正") continue;
                    D = Year.HS[j];
                    if (jd0 < D) { D -= 365; break; }   // 无需再找下一个正月
                }
                D = D + 5810;    // 计算该年春节与1984年平均春节(立春附近)相差天数估计
                return (int)( Math.Floor(D / 365.2422 + 0.5));   // 农历纪年(10进制,1984年起算)
            }
        }



        ///// <summary>
        ///// 日十二建
        ///// </summary>
        //public Jian jian
        //{
        //    get
        //    {
        //        int NUM = Enum.GetValues(typeof(Dizhi)).Length;
        //        var delta = ((int)ganZhi.Zhi - (int)Month.jian + NUM) % NUM;
        //        return (Jian)delta;
        //    }
        //}

        /// <summary>
        /// 星期序数
        /// </summary>
        public int WeekNum { get { return (Month.WeekOfMonth0 + Index) % 7; } }


        /// <summary>
        /// 星期几
        /// </summary>
        public WeekDay WeekDay { get { return (WeekDay)(WeekNum); } }


        /// <summary>
        /// 在本月第几周
        /// </summary>
        public int WeekIndex { get { return (Month.WeekOfMonth0 + Index) / 7; } }


        /// <summary>
        /// 农历日名
        /// </summary>
        public string Name { get
            {
                return ((LunarDayName)Index).ToString();
            } 
        }

        /// <summary>
        /// 日干支
        /// </summary>
        public GanZhi ganZhi
        {
            get
            {
                //// 纪日,2000年1月7日起算
                return new GanZhi(jd0 - 6);
            }
        }



        public LunarDay(LunarMonth month, int index)
        {
            Month = month;
            Index = index;
        }


        /// <summary>
        /// 取得时辰的干支
        /// </summary>
        /// <param name="ShichenIndex"></param>
        /// <returns></returns>
        public GanZhi getShichenGanzhi(int ShichenIndex)
        {
            return null;/// TODO
        }


        
    }


    
}