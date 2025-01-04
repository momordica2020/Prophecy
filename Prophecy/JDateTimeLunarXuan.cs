using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy
{
    public partial class JDateTime
    {
        ///四柱玄学等信息


        /// <summary>
        /// 四柱，以立春作为年柱定界，用节气来计算月柱
        /// </summary>
        public FourPillars LunarFourPillars0
        {
            get
            {
                //if (!_needUpdateLunar) return _LunarFourPillars;
                //UpdateLunar();

                // 计算此刻的四柱
                var lfp = new FourPillars();
                lfp.Year = new GanZhi(LunarYear0 - 4);
                int yuejian = ((int)Jieqi + 1) / 2;
                lfp.Month = new GanZhi((int)lfp.Year.Gan * 2 + (((yuejian+9)%12)+3), yuejian);
                lfp.Day = new GanZhi(GetJulianDateOfLunarHour0(JulianDateFrom2000) + 50);
                lfp.Hour = new GanZhi((int)lfp.Day.Gan * 2 + LunarHour - 1, LunarHour - 1);

                return lfp;
            }
        }


        /// <summary>
        /// 四柱，以大年初一作为年柱定界，用农历月份来计算月柱
        /// </summary>
        public FourPillars LunarFourPillars
        {
            get
            {
                //if (!_needUpdateLunar) return _LunarFourPillars;
                //UpdateLunar();

                // 计算此刻的四柱
                var lfp = new FourPillars();
                lfp.Year = new GanZhi(LunarYear - 4);
                int yuejian = LunarMonth + 1;
                lfp.Month = new GanZhi((int)lfp.Year.Gan * 2 + yuejian, yuejian);
                lfp.Day = new GanZhi(GetJulianDateOfLunarHour0(JulianDateFrom2000) + 50);
                lfp.Hour = new GanZhi((int)lfp.Day.Gan * 2 + LunarHour - 1, LunarHour - 1);

                return lfp;
            }
        }




        /// <summary>
        /// 日十二建
        /// </summary>
        public Jian jian
        {
            get
            {
                int NUM = Enum.GetValues(typeof(Dizhi)).Length;
                var delta = ((LunarFourPillars.Day.Zhi - LunarFourPillars.Month.Zhi)) + NUM % NUM;
                return (Jian)delta;
            }
        }


    }
}
