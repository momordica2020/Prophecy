using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy.Data
{
    public static partial class LunarData
    {
        /// <summary>
        /// 月相名称表
        /// </summary>
        public static string[] yxmc = new string[] { "朔", "上弦", "望", "下弦" }; //月相名称表



        /// <summary>
        /// 历史纪年表
        /// </summary>
        public static JnbArrayList JNB = new JnbArrayList();

        /// <summary>
        /// 廿四节气对应的月建表, 与 __jqmc 对应
        /// </summary>
        public static string[] JieQiYueJian = new string[] { "子", "丑", "丑", "寅", "寅", "卯", "卯", "辰", "辰", "巳", "巳", "午", "午", "未", "未", "申", "申", "酉", "酉", "戌", "戌", "亥", "亥", "子" };


        //public static string[] Weeks = new string[] { "日", "一", "二", "三", "四", "五", "六", "七" };    // 星期中文名称

        /// <summary>
        /// 农历节日的定义
        /// </summary>
       // public static List<DayInfo> LunarFeasts = getLunarFeasts();

        /// <summary>
        /// 二十四节气假日的定义
        /// </summary>
        //public static List<string> JieQiFeasts = getJieQiFeasts();


    }
}
