using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if (level < 0) return Prophecy.FeastInfo.MatchFeasts(this).ToArray();
            else return Prophecy.FeastInfo.MatchFeasts(this).Where(f => f.Level == level).ToArray();
        }

        /// <summary>
        /// 显示所有节日信息
        /// </summary>
        public string FeastInfo
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach(var f in Feasts())
                {
                    res.Append($"{f.Name},");
                }
                return res.ToString().TrimEnd(',');
            }
        }

        /// <summary>
        /// 列出days天内即将来到的节日
        /// </summary>
        public (JDateTime date, Feast feast)[] FeastComing(int days = 30)
        {
            var f = new List<(JDateTime, Feast)>();

            for(int i = 0; i < days; i++)
            {
                var jd = JulianDate + i + 1;
                var tdf = Prophecy.FeastInfo.MatchFeasts(new JDateTime(jd));
                foreach(var ff in tdf) f.Add((new JDateTime(jd), ff));
            }

            return f.ToArray();
        }


        /// <summary>
        /// 是否法定休息日
        /// </summary>
        public bool isHolidy
        {
            get
            {
                return GerogeDayInWeek == System.DayOfWeek.Saturday || GerogeDayInWeek == System.DayOfWeek.Sunday || Feasts().Where(f => f.isHolidy == true).Any();
            }
        }
    }
}
