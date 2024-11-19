using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy.Data
{
    public static partial class LunarData
    {// 数字 0 - 10 对应的中文名称
        public static string[] numCn = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" }; //中文数字

        // 十天干表
        public static string[] Gan = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

        // 十二地支表
        public static string[] Zhi = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        // 十二属相表
        public static string[] ShX = new string[] { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        // 十二星座表
        public static string[] XiZ = new string[] { "摩羯", "水瓶", "双鱼", "白羊", "金牛", "双子", "巨蟹", "狮子", "处女", "天秤", "天蝎", "射手" };

        // 月相名称表
        public static string[] yxmc = new string[] { "朔", "上弦", "望", "下弦" }; //月相名称表

        // 廿四节气表
        public static string[] jqmc = new string[] { "冬至", "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪" };

        // 农历各月的名称, 从 "十一" 月开始, 即从月建 "子" 开始, 与十二地支的顺序对应
        public static string[] ymc = new string[] { "十一", "十二", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十" }; //月名称,建寅

        // 农历各日的名称
        public static string[] rmc = new string[] { "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十", "卅一" };

        // 历史纪年表
        public static JnbArrayList JNB = new JnbArrayList();

        // 廿四节气对应的月建表, 与 __jqmc 对应
        public static string[] JieQiYueJian = new string[] { "子", "丑", "丑", "寅", "寅", "卯", "卯", "辰", "辰", "巳", "巳", "午", "午", "未", "未", "申", "申", "酉", "酉", "戌", "戌", "亥", "亥", "子" };

        // 日十二建表
        public static string[] RiJian12 = new string[] { "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭" };

        // 双重日十二建表
        public static string[] DoubleRiJian12 = new string[] { "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭",
                                                                  "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭" };
        // 双重十二地支表
        public static string[] DoubleZhi = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥",
                                                             "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        // 农历节日的定义
        public static List<Day> LunarFeasts = getLunarFeasts();

        // 二十四节气假日的定义
        public static List<string> JieQiFeasts = getJieQiFeasts();


    }
}
