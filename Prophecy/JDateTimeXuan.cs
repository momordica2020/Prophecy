using Prophecy.Astronomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy
{
    public partial class JDateTime
    {
        // 西方占星等


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
    }
}
