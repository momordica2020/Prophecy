using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Prophecy
{
    public abstract class Feast
    {
        /// <summary>
        /// 节日的类型，1=很重要，2=一般，3=不重要
        /// </summary>
        public int Level = 1;
        public int beginYear = int.MinValue;
        public int endYear = int.MaxValue;
        public bool isHolidy = false;
        public string Name;

        
        public abstract bool IsMatch(JDateTime date);
        public abstract override string ToString();
    }

    /// <summary>
    /// 公历节日
    /// </summary>
    public class GerogeFeast : Feast
    {
        public int Month;
        public int Day;


        public override bool IsMatch(JDateTime date)
        {
            return (date.GerogeYear >= beginYear && date.GerogeYear <= endYear && date.GerogeMonth == Month && date.GerogeDay == Day);
        }

        public override string ToString()
        {
            return $"{Name}(公历{Month}月{Day}日)";
        }
    }

    /// <summary>
    /// 公历节日，按星期来过的节
    /// </summary>
    public class GerogeWeekFeast : Feast
    {
        public int Month;
        public int WeekNum;
        public int Weekday;
        public static string WeekDayNum = "日一二三四五六";
        public override bool IsMatch(JDateTime date)
        {
            return (date.GerogeYear >= beginYear && date.GerogeYear <= endYear && date.GerogeMonth == Month && date.GerogeWeekOfMonth == WeekNum && date.GerogeWeek == Weekday);
        }

        public override string ToString()
        {

            return $"{Name}(公历{Month}月第{WeekNum}个星期{WeekDayNum[Weekday%7]})";
        }
    }

    /// <summary>
    /// 伊斯兰历节日
    /// </summary>
    public class IslamicFeast : Feast
    {
        public int Month;
        public int Day;
        public override bool IsMatch(JDateTime date)
        {
            return (date.IslamicYear >= beginYear && date.IslamicYear <= endYear && date.IslamicMonth == Month && date.IslamicDay == Day);
        }

        public override string ToString()
        {
            return $"{Name}(回历{Month}月{Day}日)";
        }
    }


    /// <summary>
    /// 农历节日
    /// </summary>
    public class LunarFeast : Feast
    {
        public int Month;
        public int Day;

        public override bool IsMatch(JDateTime date)
        {
            return (date.GerogeYear >= beginYear && date.GerogeYear <= endYear && !date.IsLunarLeapMonth && date.LunarMonth == Month && date.LunarDay == Day);

        }

        public override string ToString()
        {
            return $"{Name}(农历{Month}月{Day}日)";
        }
    }


    /// <summary>
    /// 农历节气节日
    /// </summary>
    public class LunarJieqiFeast : Feast
    {
        public int JieqiIndex;

        public override bool IsMatch(JDateTime date)
        {
            // TODO
            return (date.GerogeYear >= beginYear && date.GerogeYear <= endYear && false);
        }

        public override string ToString()
        {
            return $"{Name}(农历{(JieQi)JieqiIndex})";
        }
    }


    /// <summary>
    /// 查询节日信息
    /// </summary>
    internal class FeastInfo
    {
        static readonly List<Feast> feasts;
        static readonly List<Feast> feastsLunar;
        static FeastInfo()
        {
            feasts = [.. getGerogeFeasts(), .. getGerogeWeeklyFeasts(), ..getIslamicFeasts(), .. getJieQiFeasts(),];
            feastsLunar = [.. getLunarFeasts(),];

        }

        /// <summary>
        /// 根据日期筛选当天的节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Feast> MatchFeasts(JDateTime dt)
        {
            var res = feasts.Where(f => f.IsMatch(dt)).ToList();
            res.AddRange(MatchLunarSpecialFeasts(dt));
            return res;
        }

        public static List<Feast> MatchLunarSpecialFeasts(JDateTime dt)
        {
            List<Feast> res = new List<Feast>();

            if (!dt.IsLunarLeapMonth)
            {
                res.AddRange(feastsLunar.Where(f => f.IsMatch(dt)).ToList());
            }

            if (dt.LunarMonth == 12 && !dt.IsLunarNextMonthLeapMonth)
            {
                // 腊月
                if( dt.LunarDay == (dt.IsLunarLeapMonth ? 30 : 29))  res.Add(new LunarFeast { Month= dt.LunarMonth, Day= dt.LunarDay, Name = "除夕", Level = 1, isHolidy = true });
                if (dt.LunarDay == 23) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "小年", Level = 2 });
            }


            // 数九
            var n1 = dt.LunarToJieqiDays(JieQi.冬至);
            if (n1 >= 0 && n1 < 81)
            {
                int nineIndex = (int)(n1 / 9) + 1;
                if (n1 % 9 == 0) if (dt.LunarDay == 23) res.Add(new LunarFeast { Month=dt.LunarMonth, Day=dt.LunarDay, Name = $"{Util.NumberToHans(nineIndex)}九", Level = 2 });
                //    else r.C += nineIndex + "九第" + (u.cur_dz % 9 + 1) + "天 ";
            }

            // 三伏
            var jdxz = dt.JieqiJulianDate(JieQi.夏至);
            if(jdxz >= 0)
            {
                var gz0 = new GanZhi(jdxz + 50);
                var d = (int)((Tiangan.庚 - gz0.Gan + 10) % 10 + jdxz);
                if (d == (int)dt.JulianDateLunar0) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "初伏", Level = 2 });
                if (d + 10 == (int)dt.JulianDateLunar0) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "中伏", Level = 2 });
                if (d + 20 == (int)dt.JulianDateLunar0) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "末伏", Level = 2 });
            }


            // 梅雨
            var jdmz = dt.JieqiJulianDate(JieQi.芒种);
            if(jdmz >= 0)
            {
                var gz1 = new GanZhi(jdmz + 50);
                var d1 = (int)((Tiangan.丙 - gz1.Gan + 10) % 10 + jdmz);
                if (d1 == (int)dt.JulianDateLunar0) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "入梅", Level = 2 });
            }
            var jdxs = dt.JieqiJulianDate(JieQi.小暑);
            if(jdxs >= 0)
            {
                var gz2 = new GanZhi(jdxs + 50);
                var d2 = (int)((Dizhi.未 - gz2.Zhi + 12) % 12 + jdxs);
                if (d2 == (int)dt.JulianDateLunar0) res.Add(new LunarFeast { Month = dt.LunarMonth, Day = dt.LunarDay, Name = "出梅", Level = 2 });
            }
   

            return res;
        }




        ///// <summary>
        ///// 命理八字计算, 并保存到日对象 ob 中
        ///// </summary>
        ///// <param name="jd">格林尼治UT(J2000起算)</param>
        ///// <param name="J">本地经度</param>
        ///// <param name="ob">日对象</param>
        ///// <param name="southernHemisphere">南半球的标志</param>
        //public static (double jdTrue, GanZhi Y, GanZhi M, GanZhi D, GanZhi H) mingLiBaZi(double jd, double J, DayInfo ob, BaZiTypeS baziTypeS)
        //{
        //    double _jdTrue;
        //    GanZhi zY, zM, zD, zH;


        //    double jd2 = jd + Util.deltatT2(jd);      // 力学时
        //    double w = Ephemeris.S_aLon(jd2 / 36525, -1);  // 此刻太阳视黄经
        //    double k = Math.Floor((w / (Math.PI * 2) * 360 + 45 + 15 * 360) / 30);   // 1984年立春起算的节气数(不含中气)
        //    jd += Ephemeris.shiCha2(jd2 / 36525) - J / Math.PI / 2;        // 本地真太阳时(使用低精度算法计算时差)
        //    //jd += 0 - J / Math.PI / 2;     // 将格林尼治UT(J2000起算), 转换为本地时间, 不必考虑真太阳与平太阳时之间的时差
        //    _jdTrue = jd;
        //    //var _jdTrue = new JDateTime(jd,true); // DayJ.timeStr(jd);



        //    jd += 13d / 24d;   // 转为前一日23点起算(原jd为本日中午12点起算)   // C#: 注意数据类型
        //    double D = Math.Floor(jd), SC = Math.Floor((jd - D) * 12);   // 日数与时辰

        //    var v = Math.Floor(k / 12 + 6000000);
        //    zY = new GanZhi(v);
        //    v = k + 2 + 60000000; 
        //    zM= new GanZhi(v);

        //    // 计算南半球八字(仅纪月不同)
        //    switch (baziTypeS)
        //    {
        //        case BaZiTypeS.TCDC:
        //            zM = new GanZhi(v + 4, v + 6);
        //            break;

        //        case BaZiTypeS.TKDC:
        //            zM = new GanZhi(v + 6, v + 6);
        //            break;

        //        case BaZiTypeS.TTDC:
        //            zM = new GanZhi(v, v + 6);
        //            break;

        //        default:
        //            break;
        //    }


        //    v = D - 6 + 9000000; 
        //    zD = new GanZhi(v); 
        //    v = (D - 1) * 12 + 90000000 + SC; 
        //    zH = new GanZhi(v);

        //    //v -= SC;


        //    return (_jdTrue, zY, zM, zD, zH);
        //}




        /// <summary>
        /// 公历节日
        /// </summary>
        /// <returns></returns>
        private static List<Feast> getGerogeFeasts()
        {
            var feasts = new List<string> {
                "0101,元旦,#",
                "0202,世界湿地日,I,1996",
                "0210,国际气象节,,1991",
                "0214,情人节,I,496",
                "0301,国际海豹日,,1983",
                "0303,全国爱耳日,,1999",
                "0305,学雷锋纪念日,,1963",
                "0308,妇女节,I,1975",
                "0312,植树节,I,1915",
                "0312,孙中山逝世纪念日,,1925",
                "0314,国际警察日,,1995",
                "0315,消费者权益日,I,1983",
                "0317,中国国医节,,1929",
                "0317,国际航海日,,1978",
                "0321,世界森林日,,2012",
                "0321,消除种族歧视国际日,,1967",
                "0321,世界儿歌日,,1976",
                "0322,世界水日,I,1993",
                "0323,世界气象日,I,1950",
                "0324,世界防治结核病日,,1996",
                //"0325,全国中小学生安全教育日",
                "0330,巴勒斯坦国土日,,1976",
                "0401,愚人节,I,1565",
                //"0401,全国爱国卫生运动月(四月)",
                //"0401,税收宣传月(四月)",
                "0407,世界卫生日,I,1948",
                "0422,世界地球日,I,1970",
                "0423,世界图书和版权日,,1995",
                "0424,亚非新闻工作者日,,1964",
                "0501,劳动节,#,1889",
                "0504,青年节,I,1950",
                "0505,碘缺乏病防治日,,1993",
                "0508,世界红十字日,,1948",
                "0512,国际护士节,I,1912",
                "0515,国际家庭日,I,1994",
                "0517,国际电信日,,1969",
                "0518,国际博物馆日,,1977",
                "0520,全国学生营养日,,1990",
                "0523,国际牛奶日,,2001",
                "0530,五卅纪念日,,1926",
                "0531,世界无烟日,I,1988",
                "0601,国际儿童节,I,1950",
                "0605,世界环境保护日,,1973",
                "0606,全国爱眼日,,1996",
                "0617,防治荒漠化和干旱日,,1995",
                "0623,国际奥林匹克日,,1948",
                "0625,全国土地日,,1991",
                "0626,国际禁毒日,I,1987",
                "0701,香港回归纪念日,I,1998",
                "0701,中共诞辰,I,1938",
                "0701,世界建筑日,,1985",
                "0702,国际体育记者日,,1995",
                "0707,中国人民抗日战争胜利纪念日,I,1999",
                "0711,世界人口日,I,1990",
                "0730,非洲妇女日,,1962",
                "0801,建军节,I,1933",
                "0808,中国男子节(爸爸节),,1945",
                "0815,日本投降纪念日,I,1946",
                "0908,国际扫盲日,,1967",
                "0908,国际新闻工作者日,,1958",
                "0909,毛泽东逝世纪念日,,1977",
                "0910,中国教师节,I,1985",
                "0914,世界清洁地球日,,1989",
                "0916,国际臭氧层保护日,,1994",
                "0918,九·一八事变纪念日,I,1932",
                "0920,国际爱牙日,,1989",
                "0927,世界旅游日,,1970",
                "0928,孔子诞辰,I,-551",
                "1001,国庆节,#,1949",
                "1001,世界音乐日,,1982",
                "1001,国际老人节,,1991",
                "1002,国庆节假日,#,1949",
                "1002,国际和平与民主自由斗争日,,1949",
                "1003,国庆节假日,#,1949",
                "1004,世界动物日,,1931",
                "1008,全国高血压日,,1998",
                "1008,世界视觉日,,1998",
                "1009,世界邮政日,,1969",
                "1010,辛亥革命纪念日,I,1949",
                "1010,世界精神卫生日,,1992",
                "1013,世界保健日,,1946",
                "1013,国际教师节,,1994",
                "1014,世界标准日,,1970",
                "1015,国际盲人节(白手杖节),,1984",
                "1016,世界粮食日,,1981",
                "1017,世界消除贫困日,,1992",
                "1022,世界传统医药日,,1991",
                "1024,联合国日,,1945",
                "1031,世界勤俭日,,2006",
                "1107,十月社会主义革命纪念日,,1918",
                "1108,中国记者日,,2000",
                "1109,全国消防安全宣传教育日,,1992",
                "1110,世界青年节,,1984",
                "1111,国际科学与和平周(本日所属的一周),,1986",
                "1112,孙中山诞辰纪念日,,1926",
                "1114,世界糖尿病日,,1991",
                "1117,世界学生节,,1942",
                "1120,世界儿童日,,1954",
                //"1120,彝族年",TODO:彝历
                //"1121,彝族年",
                "1121,世界问候日,,1973",
                "1121,世界电视日,,1996",
                //"1122,彝族年",
                "1129,国际声援巴勒斯坦人民国际日,,1977",
                "1201,世界艾滋病日,I,1987",
                "1203,世界残疾人日,,1992",
                "1205,国际经济和社会发展志愿人员日,,1985",
                "1208,国际儿童电视广播日,,1977",
                "1209,世界足球日,,1978",
                "1210,世界人权日,,1950",
                "1212,西安事变纪念日,I,1937",
                "1213,南京大屠杀死难者纪念日,I,1938",
                "1220,澳门回归纪念日,,1999",
                "1221,国际篮球日,,1891",
                "1224,平安夜,I,1818",
                "1225,圣诞节,I,336",
                "1226,毛泽东诞辰纪念日,,1978",
            };

            List<Feast> result = new List<Feast>();

            foreach (var finfo in feasts)
            {
                var items = finfo.Split(',', StringSplitOptions.TrimEntries);
                if (items.Length >= 2)
                {
                    var month = int.Parse(items[0].Substring(0, 2));
                    var day = int.Parse(items[0].Substring(2, 2));
                    var name = items[1];
                    int level = 2;
                    bool isholiday = false;
                    int beginyear = 0;
                    int endyear = int.MaxValue;
                    if (items.Length >= 3)
                    {
                        if (items[2] == "#") { level = 1; isholiday = true; }
                        else if (items[2] == "I") { level = 1; }
                    }
                    if (items.Length >= 4)
                    {
                        int.TryParse(items[3], out beginyear);
                    }
                    if (items.Length >= 5)
                    {
                        int.TryParse(items[4], out endyear);
                    }
                    result.Add(new GerogeFeast { Level = level, Month = month, Day = day, isHolidy = isholiday, beginYear = beginyear, endYear = endyear, Name = name });
                }
            }

            return result;
        }


        /// <summary>
        /// 以星期来确定的节日
        /// </summary>
        /// <returns></returns>
        private static List<Feast> getGerogeWeeklyFeasts()
        {
            var feasts = new List<string> {
                "0150,世界麻风日,I,1954",
                "0351,全国中小学生安全教育日,,1996",
                "0520,母亲节,,1914",
                "0530,全国助残日,I,1991",
                "0630,父亲节,,1910",
                "0730,受奴役民族周,,1959",
                "0932,国际和平日,I,1981",
                "0950,国际聋人节,,1958",
                
                //"0950,世界海事日,I,1979",    9月的最后一个完整周的周四
                "1011,世界人居日,,1986",
                "1023,国际减少自然灾害日(减灾日),I,1990",
                "1144,感恩节,I,1872",

            };

            List<Feast> result = new List<Feast>();

            foreach (var finfo in feasts)
            {
                var items = finfo.Split(',', StringSplitOptions.TrimEntries);
                if (items.Length >= 2)
                {
                    var month = int.Parse(items[0].Substring(0, 2));
                    var weeknum = int.Parse(items[0].Substring(2, 1));
                    var weekindex = int.Parse(items[0].Substring(3, 1));
                    var name = items[1];
                    int level = 2;
                    bool isholiday = false;
                    if (items.Length >= 3)
                    {
                        if (items[2] == "I") { level = 1; }
                    }
                    var beginyear = 0;
                    var endyear = int.MaxValue;
                    if (items.Length >= 4) beginyear = int.Parse(items[3]);
                    if (items.Length >= 5) endyear = int.Parse(items[4]);
                    result.Add(new GerogeWeekFeast { Level = level, Month = month, WeekNum = weeknum, Weekday = weekindex, isHolidy = isholiday, Name = name, beginYear = beginyear, endYear = endyear });
                }
            }

            return result;
        }




        /// <summary>
        /// 伊斯兰历节日
        /// </summary>
        /// <returns></returns>
        private static List<Feast> getIslamicFeasts()
        {
            var feasts = new List<string> {
                "0101,伊斯兰历新年,I",
                "1001,开斋节,I,1",
                "1210,古尔邦节(宰牲节),I",
                "0110,阿舒拉节",
                "0312,圣纪节（逊尼派）,I",
                "0317,圣纪节（什叶派）,I",
                "0727,登霄节",
            };

            List<Feast> result = new List<Feast>();

            foreach (var finfo in feasts)
            {
                var items = finfo.Split(',', StringSplitOptions.TrimEntries);
                if (items.Length >= 2)
                {
                    var month = int.Parse(items[0].Substring(0, 2));
                    var day = int.Parse(items[0].Substring(2, 2));
                    var name = items[1];
                    int level = 2;
                    bool isholiday = false;
                    int beginyear = 0;// int.MinValue;
                    int endyear = int.MaxValue;
                    if (items.Length >= 3)
                    {
                        if (items[2] == "I") { level = 1; isholiday = true; }
                    }
                    if (items.Length >= 4)
                    {
                        int.TryParse(items[3], out beginyear);
                    }
                    if (items.Length >= 5)
                    {
                        int.TryParse(items[4], out endyear);
                    }
                    result.Add(new IslamicFeast { Name = name, Level = level, Month = month, Day = day, isHolidy = isholiday, beginYear = beginyear, endYear = endyear});
                }
            }

            return result;
        }


        /// <summary>
        /// 读取农历节日的定义
        /// </summary>
        /// <returns></returns>
        private static List<Feast> getLunarFeasts()
        {
            var lunarFeasts = new List<string> {
                "正月初一,春节,#",
                "正月初二,大年初二,2",
                "正月十五,元宵节,1,-278",
                "正月十五,苗族踩山节",
                "正月十五,壮族歌墟节",
                "正月十五,达斡尔族卡钦",
                "正月十六,侗族芦笙节(至正月二十) ",
                "正月廿五,填仓节",
                "正月廿九,送穷日",
                "二月初一,瑶族忌鸟节",
                "二月初二,春龙节(龙抬头),2",
                "二月初二,畲族会亲节",
                "二月初八,傈傈族刀杆节",
                "三月初三,北帝诞",
                "三月初三,苗族黎族歌墟节",
                "三月十五,白族三月节(至三月二十)",
                "三月廿三,天后诞",
                "三月廿三,妈祖诞",
                "四月初八,牛王诞",
                "四月十八,锡伯族西迁节",
                "五月初五,端午节,1,-278",
                "五月十三,关帝诞,2",
                "五月十三,阿昌族泼水节",
                "五月廿二,鄂温克族米阔鲁节",
                "五月廿九,瑶族达努节",
                "六月初六,姑姑节,2",
                "六月初六,天贶节,2",
                "六月初六,壮族祭田节",
                "六月初六,瑶族尝新节",
                "六月廿四,火把节/星回节",
                "七月初七,七夕,1,",
                "七月十三,侗族吃新节",
                "七月十五,中元节,2",
                "八月十五,中秋节,1,-278",
                "九月初九,重阳节,2",
                "十月初一,祭祖节(十月朝),2",
                "十月十五,下元节,2",
                "十月十六,瑶族盘王节",
                "腊月初八,腊八节,2",
            };

            List<Feast> result = new List<Feast>();

            foreach (var lunarf in lunarFeasts)
            {
                var items = lunarf.Split(',', StringSplitOptions.TrimEntries);
                if (items.Length >= 2)
                {
                    var dateString = items[0].Split('月', StringSplitOptions.RemoveEmptyEntries);
                    var month = 0;
                    var day = 0;
                    if (dateString.Length == 2)
                    {
                        month = Util.GetNumFromHans(dateString[0]);
                        day = Util.GetNumFromHans(dateString[1]);
                    }
                    var level = 3;
                    bool isHolidy = false;
                    if (items.Length >= 3) 
                    {
                        if (items[2] == "#") { level = 1; isHolidy = true; }
                        else if (!String.IsNullOrWhiteSpace(items[2])) { level = int.Parse(items[2]); }
                    }
                    var beginyear = -2000;
                    var endyear = int.MaxValue;
                    if (items.Length >= 4)
                    {
                        int.TryParse(items[3], out beginyear);
                    }
                    if (items.Length >= 5)
                    {
                        int.TryParse(items[4], out endyear);
                    }
                    result.Add(new LunarFeast { Level = level, Month = month, Day = day, isHolidy = isHolidy, Name = items[1].Trim(), beginYear = beginyear, endYear= endyear });


                }

            }

            return result;
        }


        /// <summary>
        /// 读取农历节气节日
        /// </summary>
        /// <returns></returns>
        private static List<Feast> getJieQiFeasts()
        {
            //List<string> JieqiFeasts = new List<string>
            //{
            //    "清明,清明节",
            //};
            var results = new List<Feast>();

            results.Add(new LunarJieqiFeast
            {
                JieqiIndex = (int)(JieQi.清明),
                Name = "清明节",
            });


            //foreach (var feasts in JieqiFeasts)
            //{
            //    var item = feasts.Split(",",StringSplitOptions.TrimEntries);
            //    if (item.Length == 2)
            //    {

            //    }
            //}

            return results;
        }




    }
}
