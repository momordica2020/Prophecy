using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy
{
    /// <summary>
    /// 四柱
    /// </summary>
    public class FourPillars
    {
    
        public class 排盘
        {
            /// <summary>
            /// 是否区分 早晚子 时,true则23:00-24:00算成上一天
            /// </summary>
            public bool Zwz { get; set; } = false;
            /// <summary>
            /// 天干
            /// </summary>
            public List<string> Ctg { get; set; } = new List<string> { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            /// <summary>
            /// 五行
            /// </summary>
            public List<string> Cwx { get; set; } = new List<string> { "木", "火", "土", "金", "水" };
            /// <summary>
            /// 地支
            /// </summary>
            
            public List<string> Cdz { get; set; } = new List<string> { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
           /// <summary>
           /// 地支对应五行
           /// </summary>
            public List<int> Dzwx { get; set; } = new List<int> { 4, 2, 0, 0, 2, 1, 1, 2, 3, 3, 2, 4 };
            /// <summary>
            /// 地支转天干对应
            /// </summary>
            public List<int> Dztg { get; set; } = new List<int> { 8, 5, 0, 2, 4, 3, 2, 5, 6, 7, 4, 9 };
            /// <summary>
            /// 地支藏干对应
            /// </summary>
            public List<List<int>> Dzcg { get; set; } = new List<List<int>>
        {
            new List<int> {9}, new List<int> {5,7,9}, new List<int> {0,2,4}, new List<int> {1},
            new List<int> {4,9,1}, new List<int> {2,6,4}, new List<int> {3,5}, new List<int> {5,1,3},
            new List<int> {6,8,4}, new List<int> {7}, new List<int> {4,3,7}, new List<int> {8,0}
        };
            /// <summary>
            /// 十二生肖
            /// </summary>
            public List<string> Csa { get; set; } = new List<string> { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
            /// <summary>
            /// 十二星座
            /// </summary>
            public List<string> Cxz { get; set; } = new List<string> { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座" };
            /// <summary>
            /// 星期
            /// </summary>
            public List<string> Wkd { get; set; } = new List<string> { "日", "一", "二", "三", "四", "五", "六" };
            /// <summary>
            /// 二十四节气（始于春分）
            /// </summary>
            public List<string> Jq { get; set; } = new List<string>
        {
            "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑",
            "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至",
            "小寒", "大寒", "立春", "雨水", "惊蛰"
        };
            /// <summary>
            /// 平均朔望
            /// </summary>
            private double Synmonth = 29.530588853; 
            
            
            /// <summary>
            /// 十神
            /// </summary>
            
            public List<List<string>> TenGod { get; set; } = new List<List<string>>()
        {
            new List<string> { "比", "劫" },  // Pair 1
            new List<string> { "食", "伤" },  // Pair 2
            new List<string> { "财", "才" },  // Pair 3
            new List<string> { "杀", "官" },  // Pair 4
            new List<string> { "枭", "印" }   // Pair 5
        };

            /// <summary>
            /// 十二长生
            /// </summary>
            public List<string> Cs { get; set; } = new List<string>
        {
            "生", "沐", "冠", "临", "旺", "衰", "病", "死", "墓", "绝", "胎", "养"
        };

            /// <summary>
            /// 本气
            /// </summary>
            public List<string> SelfQi { get; set; } = new List<string>
        {
            "禄", "刃", "长生", "墓库", "余气", "死", "绝"
        };
            /// <summary>
            /// 因子1
            /// </summary>
            private double[] Ptsa = new double[]
        {
            485, 203, 199, 182, 156, 136, 77, 74, 70, 58, 52, 50, 45, 44, 29, 18, 17, 16, 14, 12, 12, 12, 9, 8
        };
            /// <summary>
            /// 因子2
            /// </summary>
            private double[] Ptsb = new double[]
            {
            324.96, 337.23, 342.08, 27.85, 73.14, 171.52, 222.54, 296.72, 243.58, 119.81, 297.17, 21.02, 247.54, 325.15, 60.93, 155.12, 288.79, 198.04, 199.76, 95.39, 287.11, 320.81, 227.73, 15.45
            };
            /// <summary>
            /// 因子3
            /// </summary>
            private double[] Ptsc = new double[]
            {
            1934.136, 32964.467, 20.186, 445267.112, 45036.886, 22518.443, 65928.934, 3034.906, 9037.513, 33718.147, 150.678, 2281.226, 29929.562, 31555.956, 4443.417, 67555.328, 4562.452, 62894.029, 31436.921, 14577.848, 31931.756, 34777.259, 1222.114, 16859.074
            };


            /// <summary>
            /// 长生位置,天干对应的地支索引
            /// </summary>
            public List<int> CsTg2Dz { get; set; } = new List<int>
        {
            11, 6, 2, 9, 2, 9, 5, 0, 8, 3
        };




            private double VE(int yy)
            {
                if (yy < -8000 || yy > 8001)
                    return double.NaN;

                double m;
                if (yy >= 1000 && yy <= 8001)
                {
                    m = (yy - 2000) / 1000.0;
                    return 2451623.80984 + 365242.37404 * m + 0.05169 * m * m - 0.00411 * m * m * m - 0.00057 * m * m * m * m;
                }
                else if (yy >= -8000 && yy < 1000)
                {
                    m = yy / 1000.0;
                    return 1721139.29189 + 365242.1374 * m + 0.06134 * m * m + 0.00111 * m * m * m - 0.00071 * m * m * m * m;
                }
                return double.NaN;
            }


            /// <summary>
            /// Method to calculate the perturbation value
            /// </summary>
            /// <param name="jd"></param>
            /// <returns></returns>
            private double Perturbation(double jd)
            {
                double t = (jd - 2451545) / 36525;
                double s = 0;

                // Example loop calculation (you would need to implement the rest)
                for (int k = 0; k <= 23; k++)
                {
                    s += this.Ptsa[k] * Math.Cos(this.Ptsb[k] * 2 * Math.PI / 360 + this.Ptsc[k] * 2 * Math.PI / 360 * t);
                }

                double w = 35999.373 * t - 2.47;
                double l = 1 + 0.0334 * Math.Cos(w * 2 * Math.PI / 360) + 0.0007 * Math.Cos(2 * w * 2 * Math.PI / 360);
                return 0.00001 * s / l;
            }


            

        }
    }


}
