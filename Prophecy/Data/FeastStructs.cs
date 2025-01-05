using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    /// <summary>
    /// 公历节日，按星期来过的节
    /// </summary>
    public class GerogeWeekFeast : Feast
    {
        public int Month;
        public int WeekNum;
        public int Weekday;

        public override bool IsMatch(JDateTime date)
        {
            return (date.GerogeYear >= beginYear && date.GerogeYear <= endYear && date.GerogeMonth == Month && date.GerogeWeekOfMonth == WeekNum && date.GerogeWeek == Weekday);
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
    }

    internal class FeastStructs
    {
        static readonly List<Feast> feasts;
        static readonly List<Feast> feastsLunar;
        static FeastStructs()
        {
            feasts = [.. getGerogeFeasts(), .. getGerogeWeeklyFeasts(), ..getJieQiFeasts(),];
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

            if (dt.IsLunarLeapMonth)
            {
                res.AddRange(feastsLunar.Where(f => f.IsMatch(dt)).ToList());

            }
            //if (u.Lmc2 == "正")
            //{
            //    // 最后一月
            //    if (d == "十二三十" && u.Ldn == 30) { r.A += "除夕 "; r.Fjia = 1; }
            //    if (d == "十二廿九" && u.Ldn == 29) { r.A += "除夕 "; r.Fjia = 1; }
            //    if (d == "十二廿三") r.B += "小年 ";
            //}
            //if (u.Ljq.Length > 0)
            //{
            //    for (i = 0; i < JieQiFeasts.Count; i++)    // C#: 查找是否有放假的节气
            //    {
            //        if (u.Ljq == JieQiFeasts[i])               // C#: 匹配
            //            break;
            //    }
            //    if (i < JieQiFeasts.Count) { r.A += u.Ljq + " "; r.Fjia = 1; }
            //    else r.B += u.Ljq + " ";
            //}

            //// 农历杂节
            //if (u.cur_dz >= 0 && u.cur_dz < 81)
            //{ // 数九
            //    int nineIndex = (int)Math.Floor(u.cur_dz / 9) + 1;
            //    if (u.cur_dz % 9 == 0) r.B += "『" + nineIndex + "九』 ";
            //    else r.C += nineIndex + "九第" + (u.cur_dz % 9 + 1) + "天 ";
            //}

            //string w = u.Lday2.GanStr;// .Substring(0, 1);
            //string w2 = u.Lday2.ZhiStr;// (1, 1);
            //if (u.cur_xz > 20 && u.cur_xz <= 30 && w == "庚") r.B += "初伏 ";
            //if (u.cur_xz > 30 && u.cur_xz <= 40 && w == "庚") r.B += "中伏 ";
            //if (u.cur_lq > 0 && u.cur_lq <= 10 && w == "庚") r.B += "末伏 ";
            //if (u.cur_mz > 0 && u.cur_mz <= 10 && w == "丙") r.B += "入梅 ";
            //if (u.cur_xs > 0 && u.cur_xs <= 12 && w2 == "未") r.B += "出梅 ";

            return res;
        }




        ///// <summary>
        ///// 取年号
        ///// </summary>
        ///// <param name="y">公历年(天文纪年, 如 -1 表示常规纪年的"公元前2年")</param>
        ///// <returns></returns>
        //public static string getNH(int y)
        //{
        //    int i, j;
        //    string c, s = "";
        //    JnbArrayList ob = JNB;
        //    for (i = 0; i < ob.Count; i += 7)
        //    {
        //        j = (int)ob[i];
        //        if (y < j || y >= j + (int)ob[i + 1]) continue;
        //        c = (string)ob[i + 6] + (y - j + 1 + (int)ob[i + 2]) + "年";   // 年号及年次
        //        s += (s.Length > 0 ? ";" : "") + "[" + ob[i + 3] + "]" + ob[i + 4] + " " + ob[i + 5] + " " + c;   // i为年号元年,i+3朝代,i+4朝号,i+5皇帝,i+6年号
        //    }
        //    return s;
        //}


        ///// <summary>
        ///// 计算农历节日
        ///// </summary>
        ///// <param name="u"></param>
        //public static void getDayNameL(DayInfo u)
        //{
        //    int i;

        //    //// 按农历日期查找重点节假日
        //    //string d = u.Lmc + (u.Lmc.Length < 2 ? "月" : "") + u.Ldc;
        //    //if (u.Lleap != "闰")
        //    //{
        //    //    for (i = 0; i < LunarFeasts.Count; i++)     // C#: 查表, 查找农历节假日
        //    //    {
        //    //        if (d == LunarFeasts[i].Lmc)
        //    //        {
        //    //            r.A += LunarFeasts[i].A;
        //    //            r.B += LunarFeasts[i].B;
        //    //            r.C += LunarFeasts[i].C;
        //    //            r.Fjia = LunarFeasts[i].Fjia;
        //    //        }
        //    //    }
        //    //}
        //    //if (u.Lmc2 == "正")
        //    //{
        //    //    // 最后一月
        //    //    if (d == "十二三十" && u.Ldn == 30) { r.A += "除夕 "; r.Fjia = 1; }
        //    //    if (d == "十二廿九" && u.Ldn == 29) { r.A += "除夕 "; r.Fjia = 1; }
        //    //    if (d == "十二廿三") r.B += "小年 ";
        //    //}
        //    //if (u.Ljq.Length > 0)
        //    //{
        //    //    for (i = 0; i < JieQiFeasts.Count; i++)    // C#: 查找是否有放假的节气
        //    //    {
        //    //        if (u.Ljq == JieQiFeasts[i])               // C#: 匹配
        //    //            break;
        //    //    }
        //    //    if (i < JieQiFeasts.Count) { r.A += u.Ljq + " "; r.Fjia = 1; }
        //    //    else r.B += u.Ljq + " ";
        //    //}

        //    //// 农历杂节
        //    //if (u.cur_dz >= 0 && u.cur_dz < 81)
        //    //{ // 数九
        //    //    int nineIndex = (int)Math.Floor(u.cur_dz / 9) + 1;
        //    //    if (u.cur_dz % 9 == 0) r.B += "『" + nineIndex + "九』 ";
        //    //    else r.C += nineIndex + "九第" + (u.cur_dz % 9 + 1) + "天 ";
        //    //}

        //    //string w = u.Lday2.GanStr;// .Substring(0, 1);
        //    //string w2 = u.Lday2.ZhiStr;// (1, 1);
        //    //if (u.cur_xz > 20 && u.cur_xz <= 30 && w == "庚") r.B += "初伏 ";
        //    //if (u.cur_xz > 30 && u.cur_xz <= 40 && w == "庚") r.B += "中伏 ";
        //    //if (u.cur_lq > 0 && u.cur_lq <= 10 && w == "庚") r.B += "末伏 ";
        //    //if (u.cur_mz > 0 && u.cur_mz <= 10 && w == "丙") r.B += "入梅 ";
        //    //if (u.cur_xs > 0 && u.cur_xs <= 12 && w2 == "未") r.B += "出梅 ";
        //}




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
                "0202,世界湿地日,I",
                "0210,国际气象节",
                "0214,情人节,I",
                "0301,国际海豹日",
                "0303,全国爱耳日",
                "0305,学雷锋纪念日,,1963",
                "0308,妇女节,I",
                "0312,植树节,I",
                "0312,孙中山逝世纪念日,,1925",
                "0314,国际警察日",
                "0315,消费者权益日,I,1983",
                "0317,中国国医节",
                "0317,国际航海日",
                "0321,世界森林日",
                "0321,消除种族歧视国际日",
                "0321,世界儿歌日",
                "0322,世界水日,I",
                "0323,世界气象日,I",
                "0324,世界防治结核病日,,1982",
                "0325,全国中小学生安全教育日",
                "0330,巴勒斯坦国土日",
                "0401,愚人节,I,1564",
                //"0401,全国爱国卫生运动月(四月)",
                //"0401,税收宣传月(四月)",
                "0407,世界卫生日,I",
                "0422,世界地球日,I",
                "0423,世界图书和版权日",
                "0424,亚非新闻工作者日",
                "0501,劳动节,#,1889",
                "0504,青年节,I",
                "0505,碘缺乏病防治日",
                "0508,世界红十字日",
                "0512,国际护士节,I",
                "0515,国际家庭日,I",
                "0517,国际电信日",
                "0518,国际博物馆日",
                "0520,全国学生营养日",
                "0523,国际牛奶日",
                "0531,世界无烟日,I",
                "0601,国际儿童节,I,1925",
                "0605,世界环境保护日",
                "0606,全国爱眼日",
                "0617,防治荒漠化和干旱日",
                "0623,国际奥林匹克日",
                "0625,全国土地日",
                "0626,国际禁毒日,I",
                "0701,香港回归纪念日,I,1997",
                "0701,中共诞辰,I,1921",
                "0701,世界建筑日",
                "0702,国际体育记者日",
                "0707,抗日战争纪念日,I,1937",
                "0711,世界人口日,I",
                "0730,非洲妇女日",
                "0801,建军节,I,1927",
                "0808,中国男子节(爸爸节)",
                "0815,抗日战争胜利纪念,I,1945",
                "0908,国际扫盲日,,1966",
                "0908,国际新闻工作者日",
                "0909,毛泽东逝世纪念",
                "0910,中国教师节,I",
                "0914,世界清洁地球日",
                "0916,国际臭氧层保护日",
                "0918,九·一八事变纪念日,I",
                "0920,国际爱牙日",
                "0927,世界旅游日",
                "0928,孔子诞辰,I",
                "1001,国庆节,#,1949",
                "1001,世界音乐日",
                "1001,国际老人节",
                "1002,国庆节假日,#,1949",
                "1002,国际和平与民主自由斗争日",
                "1003,国庆节假日,#,1949",
                "1004,世界动物日",
                "1006,老人节",
                "1008,全国高血压日",
                "1008,世界视觉日",
                "1009,世界邮政日",
                "1009,万国邮联日",
                "1010,辛亥革命纪念日,I",
                "1010,世界精神卫生日",
                "1013,世界保健日",
                "1013,国际教师节",
                "1014,世界标准日",
                "1015,国际盲人节(白手杖节)",
                "1016,世界粮食日",
                "1017,世界消除贫困日",
                "1022,世界传统医药日",
                "1024,联合国日",
                "1031,世界勤俭日",
                "1107,十月社会主义革命纪念日,,1917",
                "1108,中国记者日",
                "1109,全国消防安全宣传教育日",
                "1110,世界青年节",
                "1111,国际科学与和平周(本日所属的一周)",
                "1112,孙中山诞辰纪念日",
                "1114,世界糖尿病日",
                "1117,国际大学生节",
                "1117,世界学生节",
                "1120,彝族年",
                "1121,彝族年",
                "1121,世界问候日",
                "1121,世界电视日",
                "1122,彝族年",
                "1129,国际声援巴勒斯坦人民国际日",
                "1201,1988-9999世界艾滋病日,I",
                "1203,世界残疾人日",
                "1205,国际经济和社会发展志愿人员日",
                "1208,国际儿童电视日",
                "1209,世界足球日",
                "1210,世界人权日",
                "1212,西安事变纪念日,I",
                "1213,南京大屠杀死难者纪念日,I,1937",
                "1220,澳门回归纪念",
                "1221,国际篮球日",
                "1224,平安夜,I",
                "1225,圣诞节,I",
                "1226,毛泽东诞辰纪念",
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
                    int beginyear = int.MinValue;
                    if (items.Length >= 3)
                    {
                        if (items[2] == "#") { level = 1; isholiday = true; }
                        else if (items[2] == "I") { level = 1; }
                    }
                    if (items.Length >= 4)
                    {
                        int.TryParse(items[3], out beginyear);
                    }
                    result.Add(new GerogeFeast { Level = level, Month = month, Day = day, isHolidy = isholiday, beginYear = beginyear, Name = name });
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
                "0150,世界麻风日,I",
                "0520,国际母亲节",
                "0530,全国助残日,I",
                "0630,父亲节",
                "0730,被奴役国家周",
                "0932,国际和平日,I",
                "0940,国际聋人节",
                "0940,世界儿童日",
                "0950,世界海事日,I",
                "1011,国际住房日",
                "1013,国际减轻自然灾害日(减灾日),I",
                "1144,感恩节,I",

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
                    result.Add(new GerogeWeekFeast { Level = level, Month = month, WeekNum = weeknum, Weekday = weekindex, isHolidy = isholiday, Name = name });
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
                "正月初一,春节 ,,,1 ",
                "正月初二,,大年初二 ,,1 ",
                "五月初五,端午节 ,,,1 ",
                "八月十五,中秋节 ,,,1 ",
                "正月十五,元宵节,上元节,壮族歌墟节 苗族踩山节 达斡尔族卡钦 ,0 ",
                "正月十六,,,侗族芦笙节(至正月二十) ,0 ",
                "正月廿五,,,填仓节,0 ",
                "正月廿九,,,送穷日,0 ",
                "二月初一,,,瑶族忌鸟节 ,0 ",
                "二月初二,,春龙节(龙抬头),畲族会亲节 ,0 ",
                "二月初八,,,傈傈族刀杆节 ,0 ",
                "三月初三,,北帝诞 ,苗族黎族歌墟节 ,0 ",
                "三月十五,,,白族三月街(至三月二十) ,0 ",
                "三月廿三,,天后诞 妈祖诞 ,,0 ",
                "四月初八,,牛王诞 ,,0 ",
                "四月十八,,,锡伯族西迁节 ,0 ",
                "五月十三,,关帝诞 ,阿昌族泼水节 ,0 ",
                "五月廿二,,,鄂温克族米阔鲁节 ,0 ",
                "五月廿九,,,瑶族达努节 ,0 ",
                "六月初六,,姑姑节 天贶节 ,壮族祭田节 瑶族尝新节 ,0 ",
                "六月廿四,,,火把节、星回节(彝、白、佤、阿昌、纳西、基诺族 ) ,0 ",
                "七月初七,,七夕(中国情人节/乞巧节/女儿节),,0 ",
                "七月十三,,,侗族吃新节,0 ",
                "七月十五,,中元节 鬼节,,0 ",
                "九月初九,,重阳节,,0 ",
                "十月初一,,祭祖节(十月朝),,0 ",
                "十月十五,,下元节 ,,0 ",
                "十月十六,,,瑶族盘王节 ,0 ",
                "腊月初八,,腊八节,,0 ",
            };

            List<Feast> result = new List<Feast>();

            foreach (var lunarf in lunarFeasts)
            {
                var items = lunarf.Split(',', StringSplitOptions.TrimEntries);
                if (items.Length == 5)
                {
                    var dateString = items[0].Split('月', StringSplitOptions.RemoveEmptyEntries);
                    if (dateString.Length == 2)
                    {
                        var month = Util.GetNumFromHans(dateString[0]);
                        var day = Util.GetNumFromHans(dateString[1]);
                        var Level1 = items[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var Level2 = items[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var Level3 = items[3].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        bool isHolidy = (items[4] == "1");

                        foreach (var feastName in Level1) result.Add(new LunarFeast { Level = 1, Month = month, Day = day, isHolidy = isHolidy, Name = feastName });
                        foreach (var feastName in Level2) result.Add(new LunarFeast { Level = 2, Month = month, Day = day, isHolidy = isHolidy, Name = feastName });
                        foreach (var feastName in Level3) result.Add(new LunarFeast { Level = 3, Month = month, Day = day, isHolidy = isHolidy, Name = feastName });
                    }


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
