using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using Prophecy.Astronomy;

namespace Prophecy.Data
{
    /// <summary>
    /// 农历基础构件(常数、通用函数等)
    /// </summary>
    public static partial class LunarData
    {


        /// <summary>
        /// 取年号
        /// </summary>
        /// <param name="y">公历年(天文纪年, 如 -1 表示常规纪年的"公元前2年")</param>
        /// <returns></returns>
        public static string getNH(int y)
        {
            int i, j;
            string c, s = "";
            JnbArrayList ob = JNB;
            for (i = 0; i < ob.Count; i += 7)
            {
                j = (int)ob[i];
                if (y < j || y >= j + (int)ob[i + 1]) continue;
                c = (string)ob[i + 6] + (y - j + 1 + (int)ob[i + 2]) + "年";   // 年号及年次
                s += (s.Length > 0 ? ";" : "") + "[" + ob[i + 3] + "]" + ob[i + 4] + " " + ob[i + 5] + " " + c;   // i为年号元年,i+3朝代,i+4朝号,i+5皇帝,i+6年号
            }
            return s;
        }


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
        /// 太阳视黄经计算精气
        /// </summary>
        /// <param name="W">太阳视黄经</param>
        /// <returns>以 J2000.0 为基准的儒略日</returns>
        public static double qi_accurate(double W)
        {
            double t = AstronomyOld.S_aLon_t(W) * 36525;
            return t - AstronomyOld.deltatT2(t) + 8d / 24d;    // 精气
        }


        /// <summary>
        /// 根据月-日视黄经计算精朔
        /// </summary>
        /// <param name="W">月日视黄经</param>
        /// <returns>以 J2000.0 为基准的儒略日</returns>
        public static double so_accurate(double W)
        {
            double t = AstronomyOld.MS_aLon_t(W) * 36525;
            return t - AstronomyOld.deltatT2(t) + 8d / 24d;    // 精朔
        }


        /// <summary>
        /// 精气计算法 jd2000 -> jd2000: 
        /// </summary>
        /// <param name="jd2000">以 J2000.0 为基准的儒略日</param>
        /// <returns>以 J2000.0 为基准的儒略日</returns>
        public static double qi_accurate2(double jd2000)
        {
            return qi_accurate(Math.Floor((jd2000 + 293) / 365.2422 * 24) * Math.PI / 12); 
        }


        /// <summary>
        /// 精朔计算法 jd2000 -> jd2000: 
        /// </summary>
        /// <param name="jd">以 J2000.0 为基准的儒略日</param>
        /// <returns>以 J2000.0 为基准的儒略日</returns>
        public static double so_accurate2(double jd)
        {
            return so_accurate(Math.Floor((jd + 8) / 29.5306) * Math.PI * 2);     // 精朔
        }


    }






    /// <summary>
    /// 纪年表数据
    /// </summary>
    public class JnbArrayList : ArrayList    // 由于存在多种数据类型, 故派生于 ArrayList, 需要装箱和拆箱操作
    {
        /// <summary>
        /// 构造函数, 完成纪年表数据的加载
        /// </summary>
        public JnbArrayList()
        {

            //----------------------------------------------------------------------------------------
            // 加载 Xml 数据:  历史纪年表
            // 注: 加载时自动去除历史纪年表 Xml 数据中所有的空白字符
            //----------------------------------------------------------------------------------------
            if (Util.SxwnlXmlData != null)
            {
                const string JnbXPath = "SharpSxwnl/SxwnlData/Data[@Id = 'obb_JNB']";

                XmlNode foundNode;
                Regex regexToTrim = new Regex(@"\s*");    // C#: 匹配任何空白字符, 用于去除所有空白字符
                int i;

                // 读取并解开历史纪年表
                foundNode = Util.SxwnlXmlData.SelectSingleNode(JnbXPath);
                if (foundNode != null)
                {
                    string[] JNB = regexToTrim.Replace(foundNode.InnerText, "").Split(',');

                    AddRange(JNB);
                    for (i = 0; i < JNB.Length; i += 7)
                    {
                        this[i] = int.Parse((string)this[i]);
                        this[i + 1] = int.Parse((string)this[i + 1]);
                        this[i + 2] = int.Parse((string)this[i + 2]);
                    }
                }
            }
        }

    }
}
