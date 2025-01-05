using System.Linq;

namespace Prophecy
{
    public partial class JDateTime
    {
        // 节日相关


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
    }
}
