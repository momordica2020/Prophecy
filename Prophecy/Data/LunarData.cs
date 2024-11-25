using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

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


        /// <summary>
        /// 计算农历节日
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        public static void getDayNameL(DayInfo u, DayInfo r)
        {
            int i;

            // 按农历日期查找重点节假日
            string d = u.Lmc + (u.Lmc.Length < 2 ? "月" : "") + u.Ldc;
            if (u.Lleap != "闰")
            {
                for (i = 0; i < LunarFeasts.Count; i++)     // C#: 查表, 查找农历节假日
                {
                    if (d == LunarFeasts[i].Lmc)
                    {
                        r.A += LunarFeasts[i].A;
                        r.B += LunarFeasts[i].B;
                        r.C += LunarFeasts[i].C;
                        r.Fjia = LunarFeasts[i].Fjia;
                    }
                }
            }
            if (u.Lmc2 == "正")
            {
                // 最后一月
                if (d == "十二三十" && u.Ldn == 30) { r.A += "除夕 "; r.Fjia = 1; }
                if (d == "十二廿九" && u.Ldn == 29) { r.A += "除夕 "; r.Fjia = 1; }
                if (d == "十二廿三") r.B += "小年 ";
            }
            if (u.Ljq.Length > 0)
            {
                for (i = 0; i < JieQiFeasts.Count; i++)    // C#: 查找是否有放假的节气
                {
                    if (u.Ljq == JieQiFeasts[i])               // C#: 匹配
                        break;
                }
                if (i < JieQiFeasts.Count) { r.A += u.Ljq + " "; r.Fjia = 1; }
                else r.B += u.Ljq + " ";
            }

            // 农历杂节
            string w, w2;
            if (u.cur_dz >= 0 && u.cur_dz < 81)
            { // 数九
                w = numCn[(int)Math.Floor(u.cur_dz / 9) + 1];
                if (u.cur_dz % 9 == 0) r.B += "『" + w + "九』 ";
                else r.C += w + "九第" + (u.cur_dz % 9 + 1) + "天 ";
            }

            w = u.Lday2.Substring(0, 1);
            w2 = u.Lday2.Substring(1, 1);
            if (u.cur_xz > 20 && u.cur_xz <= 30 && w == "庚") r.B += "初伏 ";
            if (u.cur_xz > 30 && u.cur_xz <= 40 && w == "庚") r.B += "中伏 ";
            if (u.cur_lq > 0 && u.cur_lq <= 10 && w == "庚") r.B += "末伏 ";
            if (u.cur_mz > 0 && u.cur_mz <= 10 && w == "丙") r.B += "入梅 ";
            if (u.cur_xs > 0 && u.cur_xs <= 12 && w2 == "未") r.B += "出梅 ";
        }




        /// <summary>
        /// 命理八字计算, 并保存到日对象 ob 中
        /// </summary>
        /// <param name="jd">格林尼治UT(J2000起算)</param>
        /// <param name="J">本地经度</param>
        /// <param name="ob">日对象</param>
        /// <param name="southernHemisphere">南半球的标志</param>
        public static void mingLiBaZi(double jd, double J, DayInfo ob, BaZiTypeS baziTypeS)
        {
            int i;
            string c;
            double v;
            double jd2 = jd + DayInfo.deltatT2(jd);      // 力学时
            double w = Ephemeris.S_aLon(jd2 / 36525, -1);  // 此刻太阳视黄经
            double k = Math.Floor((w / Util.pi2 * 360 + 45 + 15 * 360) / 30);   // 1984年立春起算的节气数(不含中气)
            jd += Ephemeris.shiCha2(jd2 / 36525) - J / Math.PI / 2;        // 本地真太阳时(使用低精度算法计算时差)
            ob.bz_zty = new JDateTime(jd,true).ToString(); // DayJ.timeStr(jd);

            jd += 13d / 24d;   // 转为前一日23点起算(原jd为本日中午12点起算)   // C#: 注意数据类型
            double D = Math.Floor(jd), SC = Math.Floor((jd - D) * 12);   // 日数与时辰

            v = Math.Floor(k / 12 + 6000000); ob.bz_jn = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];
            v = k + 2 + 60000000; ob.bz_jy = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];

            // C#: 新增的代码段, 计算南半球八字(仅纪月不同)
            switch (baziTypeS)
            {
                case BaZiTypeS.TCDC:
                    ob.bz_jy = Gan[(int)((v + 4) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                case BaZiTypeS.TKDC:
                    ob.bz_jy = Gan[(int)((v + 6) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                case BaZiTypeS.TTDC:
                    ob.bz_jy = Gan[(int)((v + 0) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                default:
                    break;
            }


            v = D - 6 + 9000000; ob.bz_jr = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];
            v = (D - 1) * 12 + 90000000 + SC; ob.bz_js = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];

            v -= SC;
            ob.bz_JS = "";    // 全天纪时表
            for (i = 0; i < 13; i++)
            {
                // 一天中包含有13个纪时
                c = Gan[(int)((v + i) % 10)] + Zhi[(int)((v + i) % 12)];  // 各时辰的八字
                if (SC == i)
                {
                    ob.bz_js = c;
                    // c = "<font color=red>" + c + "</font>";   //红色显示这时辰   // C#: 注释, 即取消格式显示
                }
                ob.bz_JS += (i != 0 ? " " : "") + c;
            }
        }


        /// <summary>
        /// 精气计算
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        public static double qi_accurate(double W)
        {
            double t = Ephemeris.S_aLon_t(W) * 36525;
            return t - DayInfo.deltatT2(t) + 8d / 24d;    // 精气
        }


        /// <summary>
        /// 精朔计算
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        public static double so_accurate(double W)
        {
            double t = Ephemeris.MS_aLon_t(W) * 36525;
            return t - DayInfo.deltatT2(t) + 8d / 24d;    // 精朔
        }


        /// <summary>
        /// 精气计算法 2: 
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static double qi_accurate2(double jd)
        {
            return qi_accurate(Math.Floor((jd + 293) / 365.2422 * 24) * Math.PI / 12);     //精气
        }


        /// <summary>
        /// 精朔计算法 2: 
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static double so_accurate2(double jd)
        {
            return so_accurate(Math.Floor((jd + 8) / 29.5306) * Math.PI * 2);     // 精朔
        }




        /// <summary>
        /// 命理八字计算(普通计算, 不转换为当地真太阳时), 并保存到日对象 ob 中
        /// </summary>
        /// <param name="jd">格林尼治UT(J2000起算)</param>
        /// <param name="J">本地经度</param>
        /// <param name="ob">日对象</param>
        /// <param name="southernHemisphere">南半球的标志</param>
        public static void mingLiBaZiNormal(double jd, double J, DayInfo ob, BaZiTypeS baziTypeS = BaZiTypeS.Default)
        {
            int i;
            string c;
            double v;
            double jd2 = jd + DayInfo.deltatT2(jd);      // 力学时
            double w = Ephemeris.S_aLon(jd2 / 36525, -1);  // 此刻太阳视黄经
            double k = Math.Floor((w / Util.pi2 * 360 + 45 + 15 * 360) / 30);   // 1984年立春起算的节气数(不含中气)

            //----------------------------------------------------------------------------------------------
            // C#: 注: 仅有下列代码段与 mingLiBaZi 方法中的代码不同, 其余部分都是相同的
            //----------------------------------------------------------------------------------------------
            jd += 0 - J / Math.PI / 2;     // 将格林尼治UT(J2000起算), 转换为本地时间, 不必考虑真太阳与平太阳时之间的时差
            ob.bz_zty = "";                // 真太阳时置空串
            ob.bz_pty = new JDateTime(jd, true).ToString();    // 计算平太阳时

            jd += 13d / 24d;   // 转为前一日23点起算(原jd为本日中午12点起算)   // C#: 注意数据类型
            double D = Math.Floor(jd), SC = Math.Floor((jd - D) * 12);   // 日数与时辰

            v = Math.Floor(k / 12 + 6000000); ob.bz_jn = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];
            v = k + 2 + 60000000; ob.bz_jy = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];

            // C#: 新增的代码段, 计算南半球八字(仅纪月不同)
            switch (baziTypeS)
            {
                case BaZiTypeS.TCDC:
                    ob.bz_jy = Gan[(int)((v + 4) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                case BaZiTypeS.TKDC:
                    ob.bz_jy = Gan[(int)((v + 6) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                case BaZiTypeS.TTDC:
                    ob.bz_jy = Gan[(int)((v + 0) % 10)] + Zhi[(int)((v + 6) % 12)];
                    break;

                default:
                    break;
            }


            v = D - 6 + 9000000; ob.bz_jr = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];
            v = (D - 1) * 12 + 90000000 + SC; ob.bz_js = Gan[(int)(v % 10)] + Zhi[(int)(v % 12)];

            v -= SC;
            ob.bz_JS = "";    // 全天纪时表
            for (i = 0; i < 13; i++)
            {
                // 一天中包含有13个纪时
                c = Gan[(int)((v + i) % 10)] + Zhi[(int)((v + i) % 12)];  // 各时辰的八字
                if (SC == i)
                {
                    ob.bz_js = c;
                    // c = "<font color=red>" + c + "</font>";   //红色显示这时辰   // C#: 注释, 即取消格式显示
                }
                ob.bz_JS += (i != 0 ? " " : "") + c;
            }
        }


        /// <summary>
        /// 从 Xml 对象中读取农历节日的定义
        /// </summary>
        /// <returns></returns>
        private static List<DayInfo> getLunarFeasts()
        {
            const string xPath = "SharpSxwnl/SxwnlData/Data[@Id = 'obb_getDayName']";
            List<DayInfo> result = new List<DayInfo>();

            if (Util.SxwnlXmlData != null)
            {
                XmlNodeList foundNodeList = Util.SxwnlXmlData.SelectNodes(xPath);
                if (foundNodeList.Count > 0)
                {
                    for (int i = 0; i < foundNodeList.Count; i++)
                        for (int j = 0; j < foundNodeList[i].ChildNodes.Count; j++)
                        {
                            result.Add(new DayInfo());    // 添加日对象来记录节点信息
                            XmlAttributeCollection xmlAttr = foundNodeList[i].ChildNodes[j].Attributes;
                            result[result.Count - 1].Lmc = xmlAttr.GetNamedItem("Day").InnerText;
                            result[result.Count - 1].A = xmlAttr.GetNamedItem("A").InnerText;
                            result[result.Count - 1].B = xmlAttr.GetNamedItem("B").InnerText;
                            result[result.Count - 1].C = xmlAttr.GetNamedItem("C").InnerText;
                            result[result.Count - 1].Fjia = Util.VAL(xmlAttr.GetNamedItem("Fjia").InnerText) == 0 ? 0 : 1;
                        }
                }

            }

            return result;
        }


        /// <summary>
        /// 从 Xml 对象中读取农历节日的定义
        /// </summary>
        /// <returns></returns>
        private static List<string> getJieQiFeasts()
        {
            const string xPath = "SharpSxwnl/SxwnlData/Data[@Id = 'obb_JieqiFjia']";
            List<string> result = new List<string>();

            if (Util.SxwnlXmlData != null)
            {
                XmlNode foundNode;
                Regex regexToTrim = new Regex(@"\s*");    // C#: 匹配任何空白字符, 用于去除所有空白字符

                // 读取并解开历史纪年表
                foundNode = Util.SxwnlXmlData.SelectSingleNode(xPath);
                if (foundNode != null)
                {
                    string[] jieqiFeasts = regexToTrim.Replace(foundNode.InnerText, "").Split(',');
                    result.AddRange(jieqiFeasts);
                }

            }

            return result;
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
