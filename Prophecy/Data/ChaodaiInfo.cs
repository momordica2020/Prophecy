﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy.Data
{
    /// <summary>
    /// 朝代
    /// </summary>
    public class ChaodaiItem
    {
        public string Name;
        public int BeginYear;
        public int BeginMonth;
        public int LengthYear;
        public int EndMonth;
        public List<string> DynastyPre;   // 王朝名前缀
        public string DynastyName;      // 王朝正式名称
        public string EmperorTitle;
        public string EmperorName;

        public ChaodaiItem(string name, double beginYear, double length, string dynasty, string emperorTitle, string emperorName)
        {
            Name = name;
            BeginYear = (int)beginYear;  // 整数部分是农历年，小数部分是起始的月份/100。1月0.01,12月0.12
            BeginMonth = (int)((beginYear % 1) * 100);
            LengthYear = (int)length; // 整数部分是农历年，小数部分是额外的月份/100。1月0.01,12月0.12
            EndMonth = (int)((length % 1) * 100);
            var Dynasty = dynasty.Split('-',StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            DynastyPre = new List<string>();
            if (dynasty.Length <= 0)
            {
                DynastyName = "";
            }
            else 
            {
                if (Dynasty.Length > 1) DynastyPre = Dynasty.ToList().GetRange(0, Dynasty.Length - 1);
                DynastyName = Dynasty.Last();
            }

            EmperorTitle = emperorTitle;
            EmperorName = emperorName;
        }


    }



    public class ChaodaiInfo
    {
     
        public static ChaodaiItem[] getChaodais(int lunarYear, int lunarMonth)
        {
            return Items.Where( c => 
                (lunarYear > c.BeginYear
                || lunarYear==c.BeginYear && lunarMonth >= c.BeginMonth)
                && 
                (lunarYear < c.BeginYear + c.LengthYear
                || lunarYear == c.BeginYear + c.LengthYear && lunarMonth < c.EndMonth)
            ).ToArray();
        }

        public static string getChaodaiDesc(int lunarYear, int lunarMonth)
        {
            string res = "";
            var c = getChaodais(lunarYear, lunarMonth);
            if (c.Length>0)
            {
                foreach(var cc in c)
                {
                    int last = lunarYear - cc.BeginYear + 1;
                    string lastdesc = last == 1 ? "元" : Util.NumberToHans(last);
                    res += $"{cc.DynastyName}{cc.Name}{lastdesc}年,";
                }

            }
            else
            {
                res = $"{Util.NumberToHansOneByOne(lunarYear)}年";
            }

            return res.TrimEnd(',');
        }

        //public static int TodayGerogeYear = DateTime.Now.Year;



        public static readonly List<ChaodaiItem> Items;
        static ChaodaiInfo()
        {
            int TodayGerogeYear = DateTime.Now.Year;
            Items = new List<ChaodaiItem>
            {
                //new("上古", int.MinValue, Math.Abs(int.MinValue + 2069), "", "", ""),
                //new("未来", TodayGerogeYear + 1, int.MaxValue - TodayGerogeYear - 1, "", "", ""),

                new("禹", -2069, 45, "夏", "禹", ""),
                new("启", -2024, 10, "夏", "启", ""),
                new("太康", -2014, 25, "夏", "太康", ""),
                new("仲康", -1986, 14, "夏", "仲康", ""),
                new("相", -1972, 28, "夏", "相", ""),
                new("后羿", -1944, 2, "夏", "后羿", ""),
                new("寒浞", -1942, 38, "夏", "寒浞", ""),
                new("少康", -1904, 21, "夏", "少康", ""),
                new("杼", -1883, 17, "夏", "杼", ""),
                new("槐", -1866, 26, "夏", "槐", ""),
                new("芒", -1840, 18, "夏", "芒", ""),
                new("泄", -1822, 16, "夏", "泄", ""),
                new("不降", -1806, 59, "夏", "不降", ""),
                new("扃", -1747, 21, "夏", "扃", ""),
                new("廑", -1726, 21, "夏", "廑", ""),
                new("孔甲", -1705, 31, "夏", "孔甲", ""),
                new("皋", -1674, 11, "夏", "皋", ""),
                new("发", -1663, 11, "夏", "发", ""),
                new("桀", -1652, 53, "夏", "桀", ""),

                new("商汤", -1599, 11, "商", "商太祖", "汤"),
                new("商代王", -1588, 1, "商", "商代王", "太乙"),
                new("外丙", -1587, 2, "商", "哀王", "子胜"),
                new("仲壬", -1585, 4, "商", "懿王", "子庸"),
                new("太甲", -1581, 12, "商", "太宗", "子至"),
                new("沃丁", -1569, 29, "商", "昭王", "子绚"),
                new("太庚", -1540, 25, "商", "宣王", "子辩"),
                new("小甲", -1515, 17, "商", "敬王", "子高"),
                new("雍己", -1498, 13, "商", "元王", "子密"),
                new("太戊", -1485, 75, "商", "中宗", "子伷"),
                new("仲丁", -1410, 11, "商", "孝成王", "子庄"),
                new("外壬", -1399, 15, "商", "思王", "子发"),
                new("河亶甲", -1384, 9, "商", "前平王", "子整"),
                new("祖乙", -1375, 19, "商", "穆王", "子滕"),
                new("祖辛", -1356, 16, "商", "桓王", "子旦"),
                new("沃甲", -1340, 5, "商", "僖王", "子逾"),
                new("祖丁", -1335, 9, "商", "庄王", "子新"),
                new("南庚", -1326, 6, "商", "顷王", "子更"),
                new("阳甲", -1320, 7, "商", "悼王", "子和"),
                new("盘庚", -1313, 42, "商", "世祖", "子旬"),
                new("小辛", -1271, 21, "商", "章王", "子颂"),
                new("小乙", -1250, 1, "商", "惠王", "子敛"),
                new("武丁", -1249, 59, "商", "高宗", "子昭"),
                new("祖庚", -1190, 2, "商", "后平王", "子跃"),
                new("祖甲", -1188, 33, "商", "世宗", "子载"),
                new("廪辛", -1155, 8, "商", "甲宗", "子先"),
                new("庚丁", -1147, 1, "商", "康祖", "子嚣"),
                new("武乙", -1146, 35, "商", "武祖", "子瞿"),
                new("文丁", -1111, 11, "商", "匡王", "子托"),
                new("帝乙", -1100, 26, "商", "德王", "子羡"),
                new("帝辛", -1074, 29, "商", "纣王", "子寿"),

                new("武王", -1045, 4, "西-周", "武王", "姬发"),
                new("成王", -1041, 22, "西-周", "成王", "姬诵"),
                new("康王", -1019, 25, "西-周", "康王", "姬钊"),
                new("昭王", -994, 19, "西-周", "昭王", "姬瑕"),
                new("穆王", -975, 54, "西-周", "穆王", "姬满"),
                new("共王", -921, 23, "西-周", "共王", "姬繄"),
                new("懿王", -898, 8, "西-周", "懿王", "姬囏"),
                new("孝王", -890, 6, "西-周", "孝王", "姬辟方"),
                new("夷王", -884, 8, "西-周", "夷王", "姬燮"),
                new("厉王", -876, 36, "西-周", "厉王", "姬胡"),
                new("共和", -840, 14, "西-周", "厉王", "姬胡"),
                new("宣王", -826, 46, "西-周", "宣王", "姬静"),
                new("幽王", -780, 11, "西-周", "幽王", "姬宫湦"),
                new("平王", -769, 51, "东-周", "平王", "姬宜臼"),
                new("桓王", -718, 23, "东-周", "桓王", "姬林"),
                new("庄王", -695, 15, "东-周", "庄王", "姬佗"),
                new("釐王", -680, 5, "东-周", "釐王", "姬胡齐"),
                new("惠王", -675, 25, "东-周", "惠王", "姬阆"),
                new("襄王", -650, 33, "东-周", "襄王", "姬郑"),
                new("顷王", -617, 6, "东-周", "顷王", "姬壬臣"),
                new("匡王", -611, 6, "东-周", "匡王", "姬班"),
                new("定王", -605, 21, "东-周", "定王", "姬瑜"),
                new("简王", -584, 14, "东-周", "简王", "姬夷"),
                new("灵王", -570, 27, "东-周", "灵王", "姬泄心"),
                new("景王", -543, 24, "东-周", "景王", "姬贵"),
                new("悼王", -519, 1, "东-周", "悼王", "姬勐"),
                new("敬王", -518, 44, "东-周", "敬王", "姬匄"),
                new("元王", -474, 7, "东-周", "元王", "姬仁"),
                new("贞定王", -467, 27, "东-周", "贞定王", "姬介"),
                new("哀王-思王", -440, 1, "东-周", "哀王-思王", "姬去疾-姬叔"),
                new("考王", -439, 15, "东-周", "考王", "姬嵬"),
                new("威烈王", -424, 24, "东-周", "威烈王", "姬午"),
                new("安王", -400, 26, "东-周", "安王", "姬骄"),
                new("烈王", -374, 7, "东-周", "烈王", "姬喜"),
                new("显王", -367, 48, "东-周", "显王", "姬扁"),
                new("慎靓王", -319, 6, "东-周", "慎靓王", "姬定"),
                new("赧王", -313, 8, "东-周", "赧王", "姬延"),
                new("昭襄王", -305, 56, "战国-秦", "昭襄王", "嬴则"),
                new("孝文王", -249, 1, "战国-秦", "孝文王", "嬴柱"),
                new("庄襄王", -248, 3, "战国-秦", "庄襄王", "嬴子楚"),

                new("嬴政", -245, 25, "秦", "嬴政", "嬴政"),
                new("始皇", -220, 12, "秦", "始皇帝", "嬴政"),
                new("二世", -208, 3, "秦", "二世皇帝", "嬴胡亥"),

                new("高帝", -205, 12, "西-汉", "高帝", "刘邦"),
                new("惠帝", -193, 7, "西-汉", "惠帝", "刘盈"),
                new("高后", -186, 8, "西-汉", "高后", "吕雉"),
                new("文帝", -178, 16, "西-汉", "文帝", "刘恒"),
                new("后元", -162, 7, "西-汉", "文帝", "刘恒"),
                new("景帝", -155, 7, "西-汉", "景帝", "刘启"),
                new("中元", -148, 6, "西-汉", "景帝", "刘启"),
                new("后元", -142, 3, "西-汉", "景帝", "刘启"),
                new("建元", -139, 6, "西-汉", "武帝", "刘彻"),
                new("元光", -133, 6, "西-汉", "武帝", "刘彻"),
                new("元朔", -127, 6, "西-汉", "武帝", "刘彻"),
                new("元狩", -121, 6, "西-汉", "武帝", "刘彻"),
                new("元鼎", -115, 6, "西-汉", "武帝", "刘彻"),
                new("元封", -109, 6, "西-汉", "武帝", "刘彻"),
                new("太初", -103, 4, "西-汉", "武帝", "刘彻"),
                new("天汉", -99, 4, "西-汉", "武帝", "刘彻"),
                new("太始", -95, 4, "西-汉", "武帝", "刘彻"),
                new("征和", -91, 4, "西-汉", "武帝", "刘彻"),
                new("后元", -87, 2, "西-汉", "武帝", "刘彻"),

                new("始元", -85, 6, "西-汉", "昭帝", "刘弗陵"),
                new("元凤", -79, 6, "西-汉", "昭帝", "刘弗陵"),
                new("元平", -73, 1, "西-汉", "昭帝", "刘弗陵"),
                new("本始", -72, 4, "西-汉", "宣帝", "刘询"),
                new("地节", -68, 4, "西-汉", "宣帝", "刘询"),
                new("元康", -64, 4, "西-汉", "宣帝", "刘询"),
                new("神爵", -60, 4, "西-汉", "宣帝", "刘询"),
                new("五凤", -56, 4, "西-汉", "宣帝", "刘询"),
                new("甘露", -52, 4, "西-汉", "宣帝", "刘询"),
                new("黄龙", -48, 1, "西-汉", "宣帝", "刘询"),
                new("初元", -47, 5, "西-汉", "元帝", "刘奭"),
                new("永光", -42, 5, "西-汉", "元帝", "刘奭"),
                new("建昭", -37, 5, "西-汉", "元帝", "刘奭"),
                new("竟宁", -32, 1, "西-汉", "元帝", "刘奭"),
                new("建始", -31, 4, "西-汉", "成帝", "刘骜"),
                new("河平", -27, 4, "西-汉", "成帝", "刘骜"),
                new("阳朔", -23, 4, "西-汉", "成帝", "刘骜"),
                new("鸿嘉", -19, 4, "西-汉", "成帝", "刘骜"),
                new("永始", -15, 4, "西-汉", "成帝", "刘骜"),
                new("元延", -11, 4, "西-汉", "成帝", "刘骜"),
                new("绥和", -7, 2, "西-汉", "成帝", "刘骜"),
                new("建平", -5, 4, "西-汉", "哀帝", "刘欣"),
                new("太初元将", -4.06, 0.08, "西-汉", "哀帝", "刘欣"),
                new("元寿", -1, 2, "西-汉", "哀帝", "刘欣"),
                new("元始", 1, 5, "西-汉", "平帝", "刘衎"),
                new("居摄", 6, 2.11, "西-汉", "孺子", "刘婴"),
                new("初始", 8.11, 1, "西-汉", "孺子", "刘婴"),

                new("始建国", 9, 5, "新", "", "王莽"),
                new("天凤", 14, 6, "新", "", "王莽"),
                new("地皇", 20, 3, "新", "", "王莽"),
                new("更始", 23.02, 2.10, "新", "更始帝", "刘玄"),
                new("复汉", 23.07, 0.13, "新-隗嚣", "", "隗嚣"),
                new("龙兴", 25.04, 12.11, "新-公孙述", "", "公孙述"),
                new("建始", 25.06, 3.02, "新-赤眉", "", "刘盆子"),

                new("建武", 25.06, 31.04, "东-汉", "光武帝", "刘秀"),
                new("建武中元", 56.04, 2, "东-汉", "光武帝", "刘秀"),
                new("永平", 58, 18, "东-汉", "明帝", "刘庄"),
                new("建初", 76, 8.08, "东-汉", "章帝", "刘炟"),
                new("元和", 84.08, 3.07, "东-汉", "章帝", "刘炟"),
                new("章和", 87.07, 2, "东-汉", "章帝", "刘炟"),
                new("永元", 89, 16.04, "东-汉", "和帝", "刘肇"),
                new("元兴", 105.04, 1, "东-汉", "和帝", "刘肇"),
                new("延平", 106, 1, "东-汉", "殇帝", "刘隆"),
                new("永初", 107, 7, "东-汉", "安帝", "刘祜"),
                new("元初", 114, 6.04, "东-汉", "安帝", "刘祜"),
                new("永宁", 120.04, 1.07, "东-汉", "安帝", "刘祜"),
                new("建光", 121.07, 1.03, "东-汉", "安帝", "刘祜"),
                new("延光", 122.03, 4, "东-汉", "安帝", "刘祜"),
                new("永建", 126, 7.03, "东-汉", "顺帝", "刘保"),
                new("阳嘉", 132.03, 4, "东-汉", "顺帝", "刘保"),
                new("永和", 136, 6, "东-汉", "顺帝", "刘保"),
                new("汉安", 142, 2.04, "东-汉", "顺帝", "刘保"),
                new("建康", 144.04, 1, "东-汉", "顺帝", "刘保"),
                new("永嘉", 145, 1, "东-汉", "冲帝", "刘炳"),
                new("本初", 146, 1, "东-汉", "质帝", "刘缵"),
                new("建和", 147, 3, "东-汉", "桓帝", "刘志"),
                new("和平", 150, 1, "东-汉", "桓帝", "刘志"),
                new("元嘉", 151, 2.05, "东-汉", "桓帝", "刘志"),
                new("永兴", 153.05, 2, "东-汉", "桓帝", "刘志"),
                new("永寿", 155, 3.06, "东-汉", "桓帝", "刘志"),
                new("延熹", 158.06, 9.06, "东-汉", "桓帝", "刘志"),
                new("永康", 167.06, 1, "东-汉", "桓帝", "刘志"),
                new("建宁", 168, 4.05, "东-汉", "灵帝", "刘宏"),
                new("熹平", 172.05, 5.03, "东-汉", "灵帝", "刘宏"),
                new("光和", 178.03, 6, "东-汉", "灵帝", "刘宏"),
                new("中平", 184, 5.04, "东-汉", "灵帝", "刘宏"),
                new("光熹", 189.04, 0.08, "东-汉", "少帝", "刘辩"),
                new("昭宁", 189.08, 0.09, "东-汉", "少帝", "刘辩"),
                new("永汉", 189.09, 0.12, "东-汉", "献帝", "刘协"),
                new("中平", 189.12, 1, "东-汉", "献帝", "刘协"),
                new("初平", 190, 4, "东-汉", "献帝", "刘协"),
                new("兴平", 194, 2, "东-汉", "献帝", "刘协"),
                new("建安", 196, 25.03, "东-汉", "献帝", "刘协"),
                new("延康", 220.03, 0.10, "东-汉", "献帝", "刘协"),
            
                //new("元始-河西", 1, 26, "东-汉", "", "窦融"),
                new("黄巾", 180, 34.10, "三国-黄巾", "", "宋建"),

                new("黄初", 220.10, 7, "三国-魏", "文帝", "曹丕"),
                new("太和", 227, 6.02, "三国-魏", "明帝", "曹叡"),
                new("青龙", 233.02, 4.04, "三国-魏", "明帝", "曹叡"),
                new("景初", 237.04, 3, "三国-魏", "明帝", "曹叡"),
                new("绍汉", 237.07, 1.08, "三国-燕", "", "公孙渊"),
                new("正始", 240, 9.04, "三国-魏", "齐王", "曹芳"),
                new("嘉平", 249.04, 5.10, "三国-魏", "齐王", "曹芳"),
                new("正元", 254.10, 2.06, "三国-魏", "高贵乡公", "曹髦"),
                new("甘露", 256.06, 4.06, "三国-魏", "高贵乡公", "曹髦"),
                new("景元", 260.06, 4.05, "三国-魏", "元帝", "曹奂"),
                new("咸熙", 264.05, 1.12, "三国-魏", "元帝", "曹奂"),
                new("章武", 221.04, 2.05, "三国-蜀-汉", "昭烈帝", "刘备"),
                new("建兴", 223.05, 15, "三国-蜀-汉", "后主", "刘禅"),
                new("延熙", 238, 20, "三国-蜀-汉", "后主", "刘禅"),
                new("景耀", 258, 5.08, "三国-蜀-汉", "后主", "刘禅"),
                new("炎兴", 263.08, 0.11, "三国-蜀-汉", "后主", "刘禅"),
                new("黄武", 222.10, 7.04, "三国-吴", "大帝", "孙权"),
                new("黄龙", 229.04, 3, "三国-吴", "大帝", "孙权"),
                new("嘉禾", 232, 6.08, "三国-吴", "大帝", "孙权"),
                new("赤乌", 238.08, 13.05, "三国-吴", "大帝", "孙权"),
                new("太元", 251.05, 1.02, "三国-吴", "大帝", "孙权"),
                new("神凤", 252.02, 0.04, "三国-吴", "大帝", "孙权"),
                new("建兴", 252.04, 2, "三国-吴", "会稽王", "孙亮"),
                new("五凤", 254, 2.10, "三国-吴", "会稽王", "孙亮"),
                new("太平", 256.10, 2.10, "三国-吴", "会稽王", "孙亮"),
                new("永安", 258.10, 6.07, "三国-吴", "景帝", "孙休"),
                new("元兴", 264.07, 1.04, "三国-吴", "末帝", "孙皓"),
                new("甘露", 265.04, 1.08, "三国-吴", "末帝", "孙皓"),
                new("宝鼎", 266.08, 3.10, "三国-吴", "末帝", "孙皓"),
                new("建衡", 269, 3, "三国-吴", "末帝", "孙皓"),
                new("凤凰", 272, 3, "三国-吴", "末帝", "孙皓"),
                new("天册", 275, 1.07, "三国-吴", "末帝", "孙皓"),
                new("天玺", 276.07, 1, "三国-吴", "末帝", "孙皓"),
                new("天纪", 277, 3.04, "三国-吴", "末帝", "孙皓"),

                new("泰始", 265.12, 10, "西-晋", "武帝", "司马炎"),
                new("咸宁", 275, 5.04, "西-晋", "武帝", "司马炎"),
                new("太康", 280.04, 10, "西-晋", "武帝", "司马炎"),
                new("太熙", 290, 0.04, "西-晋", "武帝", "司马炎"),
                new("永熙", 290.04, 1, "西-晋", "惠帝", "司马衷"),
                new("永平", 291, 0.03, "西-晋", "惠帝", "司马衷"),
                new("元康", 291.03, 9, "西-晋", "惠帝", "司马衷"),
                new("永康", 300, 1.04, "西-晋", "惠帝", "司马衷"),
                new("永宁", 301.04, 1.11, "西-晋", "惠帝", "司马衷"),
                new("太安", 302.12, 2, "西-晋", "惠帝", "司马衷"),
                new("永安", 304, 0.07, "西-晋", "惠帝", "司马衷"),
                new("建武", 304.07, 0.11, "西-晋", "惠帝", "司马衷"),
                new("永安", 304.11, 0.12, "西-晋", "惠帝", "司马衷"),
                new("永兴", 304.12, 2.06, "西-晋", "惠帝", "司马衷"),
                new("光熙", 306.06, 1, "西-晋", "惠帝", "司马衷"),
                new("永嘉", 307, 6.04, "西-晋", "怀帝", "司马炽"),
                new("建兴", 313.04, 4.03, "西-晋", "愍帝", "司马邺"),

                // 五胡十六国
                new("建初", 303, 1.10, "十六国-成-汉", "景帝", "李特"),
                new("建兴", 304.10, 2.06, "十六国-成-汉", "武帝", "李雄"),
                new("晏平", 306.06, 5, "十六国-成-汉", "武帝", "李雄"),
                new("玉衡", 311, 24, "十六国-成-汉", "武帝", "李雄"),// 哀帝 李班
                new("玉恒", 311, 24, "十六国-成-汉", "幽公", "李期"),
                new("汉兴", 338.04, 6, "十六国-成-汉", "昭文帝", "李寿"),
                new("太和", 344, 2.10, "十六国-成-汉", "末主", "李势"),
                new("嘉宁", 346.10, 1.03, "十六国-成-汉", "末主", "李势"),

                new("元熙", 304.10, 4.10, "十六国-赵-汉", "光文帝", "刘渊"),
                new("永凤", 308.10, 1.05, "十六国-赵-汉", "光文帝", "刘渊"),
                new("河瑞", 309.05, 1.07, "十六国-赵-汉", "光文帝", "刘渊"),
                new("光兴", 309.05, 1.06, "十六国-赵-汉", "昭武帝", "刘聪"),
                new("嘉平", 311.06, 4.03, "十六国-赵-汉", "昭武帝", "刘聪"),
                new("建元 ", 315.03, 1.11, "十六国-赵-汉", "昭武帝", "刘聪"),
                new("麟嘉", 316.11, 2.07, "十六国-赵-汉", "昭武帝", "刘聪"),
                new("汉昌", 318.07, 0.10, "十六国-赵-汉", "隐帝", "刘粲"),
                new("光初", 318.10, 11.09, "十六国-前-赵", "帝", "刘曜"),

                new("太和", 328.02, 2.09, "十六国-后-赵", "明帝", "石勒"),
                new("建平", 330.09, 4, "十六国-后-赵", "明帝", "石勒"),
                new("延熙", 334, 1, "十六国-后-赵", "海阳王", "石弘"),
                new("建武", 335, 14, "十六国-后-赵", "武帝", "石虎"),
                new("太宁", 349, 1, "十六国-后-赵", "武帝", "石虎"),
                new("青龙", 350, 0.03, "十六国-后-赵", "", "石鉴"),
                new("永宁", 350.03, 1, "十六国-后-赵", "", "石祗"),
                new("和平", 354, 1.10, "十六国-前-凉", "威王", "张祚"),
                new("凤凰", 386.02, 1.08, "十六国-凉州", "抚军将军", "张大豫"),
                new("元玺", 352.11, 5.02, "十六国-前-燕", "景明帝", "慕容儁"),
                new("光寿", 357.02, 3.02, "十六国-前-燕", "景明帝", "慕容儁"),
                new("建熙", 360.02, 10.12, "十六国-前-燕", "幽帝", "慕容𬀩"),
                new("皇始", 351, 4.06, "十六国-前-秦", "景明帝", "苻健"),
                new("寿光", 355.06, 2.06, "十六国-前-秦", "景明帝", "苻生"),
                new("永兴", 357.06, 2.06, "十六国-前-秦", "宣昭帝", "苻坚"),
                new("甘露", 359.06, 6, "十六国-前-秦", "宣昭帝", "苻坚"),
                new("建元", 365, 20.08, "十六国-前-秦", "宣昭帝", "苻坚"),
                new("太安", 385, 1.11, "十六国-前-秦", "哀平帝", "苻丕"),
                new("太初", 386.11, 8.07, "十六国-前-秦", "高帝", "苻登"),
                new("延初", 394.07, 0.11, "十六国-前-秦", "末主", "苻崇"),
                new("燕元", 384, 2.03, "十六国-后-燕", "成武帝", "慕容垂"),
                new("建兴", 386.03, 10.04, "十六国-后-燕", "成武帝", "慕容垂"),
                new("永康", 396.04, 2.05, "十六国-后-燕", "惠愍帝", "慕容宝"),

                new("建武", 317, 1, "东-晋", "元帝", "司马睿"),
                new("大兴", 318, 4, "东-晋", "元帝", "司马睿"),
                new("永昌", 322, 1, "东-晋", "元帝", "司马睿"),
                new("太宁", 323, 3, "东-晋", "明帝", "司马绍"),
                new("咸和", 326, 9, "东-晋", "成帝", "司马衍"),
                new("咸康", 335, 8, "东-晋", "成帝", "司马衍"),
                new("建元", 343, 2, "东-晋", "康帝", "司马岳"),
                new("永和", 345, 12, "东-晋", "穆帝", "司马聃"),
                new("升平", 357, 5, "东-晋", "穆帝", "司马聃"),
                new("隆和", 362, 1, "东-晋", "哀帝", "司马丕"),
                new("兴宁", 363, 3, "东-晋", "哀帝", "司马丕"),
                new("太和", 366, 5, "东-晋", "海西公", "司马奕"),
                new("咸安", 371, 2, "东-晋", "简文帝", "司马昱"),
                new("甯康", 373, 3, "东-晋", "孝武帝", "司马曜"),
                new("太元", 376, 21, "东-晋", "孝武帝", "司马曜"),
                new("隆安", 397, 5, "东-晋", "安帝", "司马德宗"),
                new("元兴", 402, 3, "东-晋", "安帝", "司马德宗"),
                new("义熙", 405, 14, "东-晋", "安帝", "司马德宗"),
                new("元熙", 419, 1, "东-晋", "恭帝", "司马德文"),
                new("永初", 420, 3, "南朝-前-宋", "武帝", "刘裕"),
                new("景平", 423, 2, "南朝-前-宋", "少帝", "刘义符"),
                new("元嘉", 424, 30, "南朝-前-宋", "文帝", "刘義隆"),
                new("孝建", 454, 3, "南朝-前-宋", "孝武", "帝刘骏"),
                new("大明", 457, 8, "南朝-前-宋", "孝武", "帝刘骏"),
                new("永光", 465, 1, "南朝-前-宋", "废帝", "刘子业"),
                new("景和", 465, 1, "南朝-前-宋", "废帝", "刘子业"),
                new("泰始", 465, 7, "南朝-前-宋", "明帝", "刘彧"),
                new("泰豫", 472, 1, "南朝-前-宋", "明帝", "刘彧"),
                new("元徽", 473, 5, "南朝-前-宋", "废帝", "刘昱"),
                new("升明", 477, 3, "南朝-前-宋", "顺帝", "刘准"),
                new("建元", 479, 4, "南朝-南-齐", "高帝", "萧道成"),
                new("永明", 483, 11, "南朝-南-齐", "武帝", "萧赜"),
                new("隆昌", 494, 1, "南-南-齐", "欎林王", "萧昭业"),
                new("延兴", 494, 1, "南朝-南-齐", "海陵王", "萧昭文"),
                new("建武", 494, 5, "南朝-南-齐", "明帝", "萧鸾"),
                new("永泰", 498, 1, "南朝-南-齐", "明帝", "萧鸾"),
                new("中兴", 499, 3, "南朝-南-齐", "东昏侯", "萧宝"),
                new("中兴", 501, 2, "南朝-南-齐", "和帝", "萧宝融"),
                new("天监", 502, 18, "南朝-前-梁", "武帝", "萧衍"),
                new("普通", 520, 8, "南朝-前-梁", "武帝", "萧衍"),
                new("大通", 527, 3, "南朝-前-梁", "武帝", "萧衍"),
                new("中大通", 529, 6, "南朝-前-梁", "武帝", "萧衍"),
                new("大同", 535, 12, "南朝-前-梁", "武帝", "萧衍"),
                new("中大同", 546, 2, "南朝-前-梁", "武帝", "萧衍"),
                new("太清", 547, 3, "南朝-前-梁", "武帝", "萧衍"),
                new("大宝", 550, 2, "南朝-前-梁", "简文帝", "萧纲"),
                new("天正", 551, 2, "南朝-前-梁", "豫章王", "萧栋"),
                new("承圣", 552, 4, "南朝-前-梁", "元帝", "萧绎"),
                new("天成", 555, 1, "南朝-前-梁", "贞阳侯", "萧渊明"),
                new("绍泰", 555, 2, "南朝-前-梁", "敬帝", "萧方智"),
                new("太平", 556, 2, "南朝-前-梁", "敬帝", "萧方智"),
                new("太平", 557, 3, "南朝-南-陈", "武帝", "陈霸先"),
                new("天嘉", 560, 7, "南朝-南-陈", "文帝", "陈蒨"),
                new("天康", 566, 1, "南朝-南-陈", "文帝", "陈蒨"),
                new("光大", 567, 2, "南朝-南-陈", "废帝", "陈伯宗"),
                new("太建", 569, 14, "南朝-南-陈", "宣帝", "陈顼"),
                new("至德", 583, 4, "南朝-南-陈", "后主", "陈叔宝"),
                new("祯明", 587, 3, "南朝-南-陈", "后主", "陈叔宝"),
                new("大定", 555, 8, "南朝-后-粱", "宣帝", "萧詧"),
                new("天保", 562, 24, "南朝-后-粱", "明帝", "萧岿"),
                new("广运", 586, 2, "南朝-后-粱", "莒公", "萧琮"),
                new("登国", 386, 11, "北朝-北-魏", "道武帝", "拓跋圭"),
                new("皇始", 396, 3, "北朝-北-魏", "道武帝", "拓跋圭"),
                new("天兴", 398, 7, "北朝-北-魏", "道武帝", "拓跋圭"),
                new("天赐", 404, 6, "北朝-北-魏", "道武帝", "拓跋圭"),
                new("永兴", 409, 5, "北朝-北-魏", "明元帝", "拓跋嗣"),
                new("神瑞", 414, 3, "北朝-北-魏", "明元帝", "拓跋嗣"),
                new("泰常", 416, 8, "北朝-北-魏", "明元帝", "拓跋嗣"),
                new("始光", 424, 5, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("神麚", 428, 4, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("延和", 432, 3, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("太延", 435, 6, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("太平真君", 440, 12, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("正平", 451, 2, "北朝-北-魏", "太武帝", "拓跋焘"),
                new("承平", 452, 1, "北朝-北-魏", "南安王", "拓跋余"),
                new("兴安", 452, 3, "北朝-北-魏", "文成帝", "拓跋浚"),
                new("兴光", 454, 2, "北朝-北-魏", "文成帝", "拓跋浚"),
                new("太安", 455, 5, "北朝-北-魏", "文成帝", "拓跋浚"),
                new("和平", 460, 6, "北朝-北-魏", "文成帝", "拓跋浚"),
                new("天安", 466, 2, "北朝-北-魏", "献文帝", "拓跋弘"),
                new("皇兴", 467, 5, "北朝-北-魏", "献文帝", "拓跋弘"),
                new("延兴", 471, 6, "北朝-北-魏", "教文帝", "拓跋宏"),
                new("承明", 476, 1, "北朝-北-魏", "孝文帝", "拓跋宏"),
                new("太和", 477, 23, "北朝-北-魏", "孝文帝", "拓跋宏"),
                new("景明", 500, 4, "北朝-北-魏", "宣武帝", "元恪"),
                new("正始", 504, 5, "北朝-北-魏", "宣武帝", "元恪"),
                new("永平", 508, 5, "北朝-北-魏", "宣武帝", "元恪"),
                new("延昌", 512, 4, "北朝-北-魏", "宣武帝", "元恪"),
                new("熙平", 516, 3, "北朝-北-魏", "孝明帝", "元诩"),
                new("神龟", 518, 3, "北朝-北-魏", "孝明帝", "元诩"),
                new("正光", 520, 6, "北朝-北-魏", "孝明帝", "元诩"),
                new("孝昌", 525, 3, "北朝-北-魏", "孝明帝", "元诩"),
                new("武泰", 528, 1, "北朝-北-魏", "孝明帝", "元诩"),
                new("建义", 528, 1, "北朝-北-魏", "孝庄帝", "元子攸"),
                new("永安", 528, 3, "北朝-北-魏", "孝庄帝", "元子攸"),
                new("建明", 530, 2, "北朝-北-魏", "东海王", "元晔"),
                new("普泰", 531, 2, "北朝-北-魏", "节闵帝", "元恭"),
                new("中兴", 531, 2, "北朝-北-魏", "安定王", "元朗"),
                new("太昌", 532, 1, "北朝-北-魏", "孝武帝", "元修"),
                new("永兴", 532, 1, "北朝-北-魏", "孝武帝", "元修"),
                new("永熙", 532, 3, "北朝-北-魏", "孝武帝", "元修"),
                new("天平", 534, 4, "北朝-东-魏", "孝静帝", "元善见"),
                new("元象", 538, 2, "北朝-东-魏", "孝静帝", "元善见"),
                new("兴和", 539, 4, "北朝-东-魏", "孝静帝", "元善见"),
                new("武定", 543, 8, "北朝-东-魏", "孝静帝", "元善见"),
                new("大统", 535, 17, "北朝-西-魏", "文帝", "元宝炬"),
                new("大统", 552, 3, "北朝-西-魏", "废帝", "元钦"),
                new("大统", 554, 3, "北朝-西-魏", "恭帝", "元廓"),
                new("天保", 550, 10, "北朝-北-齐", "文宣帝", "高洋"),
                new("乾明", 560, 1, "北朝-北-齐", "废帝", "高殷"),
                new("皇建", 560, 2, "北朝-北-齐", "孝昭帝", "高演"),
                new("太宁", 561, 2, "北朝-北-齐", "武成帝", "高湛"),
                new("河清", 562, 4, "北朝-北-齐", "武成帝", "高湛"),
                new("天统", 565, 5, "北朝-北-齐", "温公", "高纬"),
                new("武平", 570, 7, "北朝-北-齐", "温公", "高纬"),
                new("隆化", 576, 2, "北朝-北-齐", "温公", "高纬"),
                new("德昌", 576, 1, "北朝-北-齐", "安德王", "高延宗"),
                new("承光", 577, 1, "北朝-北-齐", "幼主", "高恒"),
                new("空", 557, 1, "北朝-北-周", "闵帝", "宇文觉"),
                new("空", 557, 2, "北朝-北-周", "明帝", "宇文毓"),
                new("武成", 559, 2, "北朝-北-周", "明帝", "宇文毓"),
                new("保定", 561, 5, "北朝-北-周", "武帝", "宇文邕"),
                new("天和", 566, 7, "北朝-北-周", "武帝", "宇文邕"),
                new("建德", 572, 7, "北朝-北-周", "武帝", "宇文邕"),
                new("宣政", 578, 1, "北朝-北-周", "武帝", "宇文邕"),
                new("大成", 579, 1, "北朝-北-周", "宣帝", "宇文贇"),
                new("大象", 579, 2, "北朝-北-周", "静帝", "宇文衍"),
                new("大定", 581, 1, "北朝-北-周", "静帝", "宇文衍"),
                new("开皇", 581, 20, "隋", "文帝", "杨坚"),
                new("仁寿", 601, 4, "隋", "文帝", "杨坚"),
                new("大业", 605, 13, "隋", "炀帝", "杨广"),
                new("义宁", 617, 2, "隋", "恭帝", "杨侑"),
                new("武德", 618, 9, "唐", "高祖", "李渊"),
                new("贞观", 627, 23, "唐", "太宗", "李世民"),
                new("永徽", 650, 6, "唐", "高宗", "李治"),
                new("显庆", 656, 6, "唐", "高宗", "李治"),
                new("龙朔", 661, 3, "唐", "高宗", "李治"),
                new("麟德", 664, 2, "唐", "高宗", "李治"),
                new("乾封", 666, 3, "唐", "高宗", "李治"),
                new("总章", 668, 3, "唐", "高宗", "李治"),
                new("咸亨", 670, 5, "唐", "高宗", "李治"),
                new("上元", 674, 3, "唐", "高宗", "李治"),
                new("仪凤", 676, 4, "唐", "高宗", "李治"),
                new("调露", 679, 2, "唐", "高宗", "李治"),
                new("永隆", 680, 2, "唐", "高宗", "李治"),
                new("开耀", 681, 2, "唐", "高宗", "李治"),
                new("永淳", 682, 2, "唐", "高宗", "李治"),
                new("弘道", 683, 1, "唐", "高宗", "李治"),
                new("嗣圣", 684, 1, "唐", "中宗", "李显"),
                new("文明", 684, 1, "唐", "睿宗", "李旦"),
                new("光宅", 684, 1, "武-周", "则天后", "武曌"),
                new("垂拱", 685, 4, "武-周", "则天后", "武曌"),
                new("永昌", 689, 1, "武-周", "则天后", "武曌"),
                new("载初", 689, 2, "武-周", "则天后", "武曌"),
                new("天授", 690, 3, "武-周", "则天后", "武曌"),
                new("如意", 692, 1, "武-周", "则天后", "武曌"),
                new("长寿", 692, 3, "武-周", "则天后", "武曌"),
                new("延载", 694, 1, "武-周", "则天后", "武曌"),
                new("证圣", 695, 1, "武-周", "则天后", "武曌"),
                new("天册万岁", 695, 2, "武-周", "则天后", "武曌"),
                new("万岁登封", 696, 1, "武-周", "则天后", "武曌"),
                new("万岁通天", 696, 2, "武-周", "则天后", "武曌"),
                new("神功", 697, 1, "武-周", "则天后", "武曌"),
                new("圣历", 698, 3, "武-周", "则天后", "武曌"),
                new("久视", 700, 1, "武-周", "则天后", "武曌"),
                new("大足", 701, 1, "武-周", "则天后", "武曌"),
                new("长安", 701, 4, "武-周", "则天后", "武曌"),
                new("神龙", 705, 1, "武-周", "则天后", "李显"),
                new("神龙", 705, 2, "唐", "中宗", "李显"),
                new("景龙", 707, 4, "唐", "中宗", "李显"),
                new("唐隆", 710, 1, "唐", "温王", "李重茂"),
                new("景云", 710, 2, "唐", "睿宗", "李旦"),
                new("太极", 712, 1, "唐", "睿宗", "李旦"),
                new("延和", 712, 1, "唐", "睿宗", "李旦"),
                new("先天", 712, 2, "唐", "玄宗", "李隆基"),
                new("开元", 713, 29, "唐", "玄宗", "李隆基"),
                new("天宝", 742, 15, "唐", "玄宗", "李隆基"),
                new("至德", 756, 3, "唐", "肃宗", "李亨"),
                new("乾元", 758, 3, "唐", "肃宗", "李亨"),
                new("上元", 760, 3, "唐", "肃宗", "李亨"),
                new("宝应", 762, 2, "唐", "肃宗", "李亨"),
                new("广德", 763, 2, "唐", "代宗", "李豫"),
                new("永泰", 765, 2, "唐", "肃宗", "李亨"),
                new("大历", 766, 14, "唐", "肃宗", "李亨"),
                new("建中", 780, 4, "唐", "德宗", "李适"),
                new("兴元", 784, 1, "唐", "德宗", "李适"),
                new("贞元", 785, 21, "唐", "德宗", "李适"),
                new("永贞", 805, 1, "唐", "顺宗", "李诵"),
                new("元和", 806, 15, "唐", "宪宗", "李纯"),
                new("长庆", 821, 4, "唐", "穆宗", "李恒"),
                new("宝历", 825, 3, "唐", "敬宗", "李湛"),
                new("大和", 827, 9, "唐", "文宗", "李昂"),
                new("开成", 836, 5, "唐", "文宗", "李昂"),
                new("会昌", 841, 6, "唐", "武宗", "李炎"),
                new("大中", 847, 14, "唐", "宣宗", "李忱"),
                new("咸通", 860, 15, "唐", "宣宗", "李忱"),
                new("乾符", 874, 6, "唐", "僖宗", "李儇"),
                new("广明", 880, 2, "唐", "僖宗", "李儇"),
                new("中和", 881, 5, "唐", "僖宗", "李儇"),
                new("光启", 885, 4, "唐", "僖宗", "李儇"),
                new("文德", 888, 1, "唐", "僖宗", "李儇"),
                new("龙纪", 889, 1, "唐", "昭宗", "李晔"),
                new("大顺", 890, 2, "唐", "昭宗", "李晔"),
                new("景福", 892, 2, "唐", "昭宗", "李晔"),
                new("乾宁", 894, 5, "唐", "昭宗", "李晔"),
                new("光化", 898, 4, "唐", "昭宗", "李晔"),
                new("天复", 901, 4, "唐", "昭宗", "李晔"),
                new("天佑", 904, 1, "唐", "昭宗", "李晔"),
                new("天佑", 905, 3, "唐", "昭宣帝", "李祝"),
                new("开平", 907, 5, "五代-后-梁", "太祖", "朱温"),
                new("乾化", 911, 2, "五代-后-梁", "太祖", "朱温"),
                new("凤历", 913, 1, "五代-后-梁", "庶人", "朱友圭"),
                new("乾化", 913, 3, "五代-后-梁", "末帝", "朱友贞"),
                new("贞明", 915, 7, "五代-后-梁", "末帝", "朱友贞"),
                new("龙德", 921, 3, "五代-后-梁", "末帝", "朱友贞"),
                new("同光", 923, 4, "五代-后-唐", "庄宗", "李存勗"),
                new("天成", 926, 5, "五代-后-唐", "明宗", "李嗣源"),
                new("长兴", 930, 4, "五代-后-唐", "明宗", "李嗣源"),
                new("应顺", 934, 1, "五代-后-唐", "闵帝", "李从厚"),
                new("清泰", 934, 3, "五代-后-唐", "潞王", "李从珂"),
                new("天福", 936, 6, "五代-后-晋", "高祖", "石敬瑭"),
                new("天福", 942, 2, "五代-后-晋", "出帝", "石重贵"),
                new("开运", 944, 3, "五代-后-晋", "出帝", "石重贵"),
                new("天福", 947, 12, "五代-后-汉", "高祖", "刘知远"),
                new("乾祐", 948, 1, "五代-后-汉", "隐帝", "刘承祐"),
                new("乾祐", 948, 3, "五代-后-汉", "隐帝", "刘承祐"),
                new("广顺", 951, 3, "五代-后-周", "太祖", "郭威"),
                new("显德", 954, 1, "五代-后-周", "太祖", "郭威"),
                new("显德", 954, 6, "五代-后-周", "世宗", "柴荣"),
                new("显德", 959, 2, "五代-后-周", "恭帝", "郭宗训"),
                new("建隆", 960, 4, "北-宋", "太祖", "赵匡胤"),
                new("乾德", 963, 6, "北-宋", "太祖", "赵匡胤"),
                new("开宝", 968, 9, "北-宋", "太祖", "赵匡胤"),
                new("太平兴国", 976, 9, "北-宋", "太宗", "赵炅"),
                new("雍熙", 984, 4, "北-宋", "太宗", "赵炅"),
                new("端拱", 988, 2, "北-宋", "太宗", "赵炅"),
                new("淳化", 990, 5, "北-宋", "太宗", "赵炅"),
                new("至道", 995, 3, "北-宋", "太宗", "赵炅"),
                new("咸平", 998, 6, "北-宋", "真宗", "赵恒"),
                new("景德", 1004, 4, "北-宋", "真宗", "赵恒"),
                new("大中祥符", 1008, 9, "北-宋", "真宗", "赵恒"),
                new("天禧", 1017, 5, "北-宋", "真宗", "赵恒"),
                new("乾兴", 1022, 1, "北-宋", "真宗", "赵恒"),
                new("天圣", 1023, 10, "北-宋", "仁宗", "赵祯"),
                new("明道", 1032, 2, "北-宋", "仁宗", "赵祯"),
                new("景祐", 1034, 5, "北-宋", "仁宗", "赵祯"),
                new("宝元", 1038, 3, "北-宋", "仁宗", "赵祯"),
                new("康定", 1040, 2, "北-宋", "仁宗", "赵祯"),
                new("庆历", 1041, 8, "北-宋", "仁宗", "赵祯"),
                new("皇祐", 1049, 6, "北-宋", "仁宗", "赵祯"),
                new("至和", 1054, 3, "北-宋", "仁宗", "赵祯"),
                new("嘉祐", 1056, 8, "北-宋", "仁宗", "赵祯"),
                new("治平", 1064, 4, "北-宋", "英宗", "赵曙"),
                new("熙宁", 1068, 10, "北-宋", "神宗", "赵顼"),
                new("元丰", 1078, 8, "北-宋", "神宗", "赵顼"),
                new("元祐", 1086, 9, "北-宋", "哲宗", "赵煦"),
                new("绍圣", 1094, 5, "北-宋", "哲宗", "赵煦"),
                new("元符", 1098, 3, "北-宋", "哲宗", "赵煦"),
                new("建中靖国", 1101, 1, "北-宋", "徽宗", "赵佶"),
                new("崇宁", 1102, 5, "北-宋", "徽宗", "赵佶"),
                new("大观", 1107, 4, "北-宋", "徽宗", "赵佶"),
                new("政和", 1111, 8, "北-宋", "徽宗", "赵佶"),
                new("重和", 1118, 2, "北-宋", "徽宗", "赵佶"),
                new("宣和", 1119, 7, "北-宋", "徽宗", "赵佶"),
                new("靖康", 1126, 2, "北-宋", "钦宗", "赵桓"),
                new("建炎", 1127, 4, "南-宋", "高宗", "赵构"),
                new("绍兴", 1131, 32, "南-宋", "高宗", "赵构"),
                new("隆兴", 1163, 2, "南-宋", "孝宗", "赵慎"),
                new("乾道", 1165, 9, "南-宋", "孝宗", "赵慎"),
                new("淳熙", 1174, 16, "南-宋", "孝宗", "赵慎"),
                new("绍熙", 1190, 5, "南-宋", "光宗", "赵暴"),
                new("庆元", 1195, 6, "南-宋", "宁宗", "赵扩"),
                new("嘉泰", 1201, 4, "南-宋", "宁宗", "赵扩"),
                new("开禧", 1205, 3, "南-宋", "宁宗", "赵扩"),
                new("嘉定", 1208, 17, "南-宋", "宁宗", "赵扩"),
                new("宝庆", 1225, 3, "南-宋", "理宗", "赵昀"),
                new("绍定", 1228, 6, "南-宋", "理宗", "赵昀"),
                new("端平", 1234, 3, "南-宋", "理宗", "赵昀"),
                new("嘉熙", 1237, 4, "南-宋", "理宗", "赵昀"),
                new("淳祐", 1241, 12, "南-宋", "理宗", "赵昀"),
                new("寶祐", 1253, 6, "南-宋", "理宗", "赵昀"),
                new("开庆", 1259, 1, "南-宋", "理宗", "赵昀"),
                new("景定", 1260, 5, "南-宋", "理宗", "赵昀"),
                new("咸淳", 1265, 10, "南-宋", "度宗", "赵禥"),
                new("德祐 ", 1275, 2, "南-宋", "恭宗", "赵㬎"),
                new("景炎", 1276, 3, "南-宋", "端宗", "赵昰"),
                new("祥兴", 1278, 2, "南-宋", "帝昺", "赵昺"),
                new("至元", 1271, 24, "元", "世祖", "孛儿只斤·忽必烈"),
                new("元贞", 1295, 3, "元", "成宗", "孛儿只斤·铁穆耳"),
                new("大德", 1297, 11, "元", "成宗", "孛儿只斤·铁穆耳"),
                new("至大", 1308, 4, "元", "武宗", "孛儿只斤·海山"),
                new("皇庆", 1312, 2, "元", "仁宗", "孛儿只斤·爱育黎拔力八达"),
                new("延祐", 1314, 7, "元", "仁宗", "孛儿只斤·愛育黎拔力八達"),
                new("至治", 1321, 3, "元", "英宗", "孛儿只斤·宗硕德八剌"),
                new("泰定", 1324, 5, "元", "泰定帝", "孛儿只斤·也孙铁木耳"),
                new("至和", 1328, 1, "元", "泰定帝", "孛儿只斤·也孙铁木耳"),
                new("天顺", 1328, 1, "元", "幼主", "孛儿只斤·阿速吉八"),
                new("天历", 1328, 3, "元", "文宗", "孛儿只斤·图贴睦尔"),
                new("至顺", 1330, 3, "元", "文宗", "孛儿只斤·图贴睦尔"),
                new("元统", 1333, 3, "元", "惠宗", "孛儿只斤·妥镤贴睦尔"),
                new("至元", 1335, 6, "元", "惠宗", "孛儿只斤·妥镤贴睦尔"),
                new("至正", 1341, 28, "元", "惠宗", "孛儿只斤·妥镤贴睦尔"),
                new("洪武", 1368, 31, "明", "太祖", "朱元璋"),
                new("建文", 1399, 4, "明", "惠帝", "朱允溫"),
                new("永乐", 1403, 22, "明", "成祖", "朱棣"),
                new("洪熙", 1425, 1, "明", "仁宗", "朱高炽"),
                new("宣德", 1426, 10, "明", "宣宗", "朱瞻基"),
                new("正统", 1436, 14, "明", "英宗", "朱祁镇"),
                new("景泰", 1450, 7, "明", "代宗", "朱祁钰"),
                new("天顺", 1457, 8, "明", "英宗", "朱祁镇"),
                new("成化", 1465, 23, "明", "宪宗", "朱见深"),
                new("弘治", 1488, 18, "明", "孝宗", "朱祐樘"),
                new("正德", 1506, 16, "明", "武宗", "朱厚照"),
                new("嘉靖", 1522, 45, "明", "世宗", "朱厚熜"),
                new("隆庆", 1567, 6, "明", "穆宗", "朱载贺"),
                new("万历", 1573, 48, "明", "神宗", "朱翊钧"),
                new("泰昌", 1620, 1, "明", "光宗", "朱常洛"),
                new("天启", 1621, 7, "明", "熹宗", "朱同校"),
                new("崇祯", 1628, 17, "明", "毅宗", "朱由检"),
                new("顺治", 1644, 18, "清", "世祖", "爱新觉罗福临"),
                new("康熙", 1662, 61, "清", "圣祖", "爱新觉罗玄烨"),
                new("雍正", 1723, 13, "清", "世宗", "爱新觉罗胤禛"),
                new("乾隆", 1736, 60, "清", "高宗", "爱新觉罗弘历"),
                new("嘉庆", 1796, 25, "清", "仁宗", "爱新觉罗颙琰"),
                new("道光", 1821, 30, "清", "宣宗", "爱新觉罗旻宁"),
                new("咸丰", 1851, 11, "清", "文宗", "爱新觉罗奕詝"),
                new("同治", 1862, 13, "清", "穆宗", "爱新觉罗载淳"),
                new("光绪", 1875, 34, "清", "德宗", "爱新觉罗载湉"),
                new("宣统", 1909, 3, "清", "逊帝", "爱新觉罗溥仪"),
                new("民国", 1912, TodayGerogeYear - 1911, "", "", ""),
                new("建国", 1949, TodayGerogeYear - 1948, "中华人民共和国", "", ""),

            };


        }

        
    
    }
}