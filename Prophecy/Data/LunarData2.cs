using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Prophecy.Data
{
    /// <summary>
    /// 公历基础构件,JD物件的补充(主要用于计算节假日, 纪念日, 回历信息等)
    /// </summary>
    public static partial class LunarData
    {


        /// <summary>
        /// 计算某日节日的信息, 并保存信息到日对象中
        /// </summary>
        /// <param name="jd">jd</param>
        public static void getDayNameJ(double jd)
        {
            //string m0 = (u.Month < 10 ? "0" : "") + u.Month;
            //string d0 = (u.Day < 10 ? "0" : "") + u.Day;
            //int i;
            //string s, s2, type;

            ////if (u.Week == 0 || u.Week == 6)  u.Fjia = 1;   // 星期日或星期六放假

            //// 按公历日期查找
            //for (i = 0; i < sFtv[u.Month - 1].Count; i++)    // C#: 注: 属性 sFtv 在初始化时已经包含了 12 个元素, 以对应 12 个月
            //{
            //    // 公历节日或纪念日,遍历本月节日表
            //    s = sFtv[u.Month - 1][i];
            //    if (s.Length < 3 || s.Substring(0, 2) != d0)    // C#: 新增了第 1 个判断条件
            //        continue;
            //    s = s.Substring(2, s.Length - 2);
            //    type = s.Substring(0, 1);
            //    if (s.Length >= 6 && s.Substring(5, 1) == "-")
            //    {
            //        // 有年限的节日
            //        if (u.Year < int.Parse(s.Substring(1, 4)) || u.Year > int.Parse(s.Substring(6, 4))) continue;
            //        s = s.Substring(10, s.Length - 10);
            //    }
            //    else
            //    {
            //        if (u.Year < 1850) continue;
            //        s = s.Substring(1, s.Length - 1);
            //    }
            //    if (type == "#") { u.A += s + " "; u.Fjia = 1; } //放假的节日
            //    if (type == "I") u.B += s + " "; //主要
            //    if (type == ".") u.C += s + " "; //其它
            //}

            //// 按周规则查找
            //double w = u.WeekOfMonth;
            //if (u.Week >= u.WeekIndexOfMonthStart) w += 1;
            //double w2 = w;
            //if (u.WeekOfMonth == u.WeekNumOfThisMonth - 1) w2 = 5;
            //string w__ = m0 + w + u.Week;    // d日在本月的第几个星期某
            //string w2__ = m0 + w2 + u.Week;

            //for (i = 0; i < wFtv.Count; i++)
            //{
            //    s = wFtv[i];
            //    s2 = s.Substring(0, 4);
            //    if (s2 != w__ && s2 != w2__) continue;
            //    type = s.Substring(4, 1);
            //    s = s.Substring(5, s.Length - 5);
            //    if (type == "#") { u.A += s + " "; r.Fjia = 1; }
            //    if (type == "I") u.B += s + " ";
            //    if (type == ".") u.C += s + " ";
            //}
        }






        ///// <summary>
        ///// 按周规则定义的世界性节日(纪念日)
        ///// </summary>
        //public static List<string> wFtv = LunarData.getwFtvData();

        ///// <summary>
        ///// 各月的节日(纪念日), 功能类似于交错数组
        ///// </summary>
        //public static List<List<string>> sFtv = LunarData.getsFtvData();   // 国历节日,#表示放假日,I表示重要节日或纪念日



        ///// <summary>
        ///// 从 Xml 文档对象加载 wFtv 数据
        ///// </summary>
        ///// <returns></returns>
        //private static List<string> getwFtvData()
        //{
        //    List<string> result = new List<string>();

        //    //----------------------------------------------------------------------------------------
        //    // 加载 Xml 数据:  按周规则定义的节日(纪念日)
        //    // 注: 加载时自动去除各行 Xml 数据前、后端的所有空白字符, 但对数据内部的空白字符不作处理
        //    //----------------------------------------------------------------------------------------
        //    if (Util.SxwnlXmlData != null)
        //    {
        //        const string wFtvXPath = "SharpSxwnl/SxwnlData/Data[@Id = 'oba_wFtv']";
        //        XmlNode foundNode;
        //        Regex regexToTrim = new Regex(@"(^\s*)|(\s*$)");    // C#: 匹配任何空白字符

        //        // 读取并解开数据
        //        foundNode = Util.SxwnlXmlData.SelectSingleNode(wFtvXPath);
        //        if (foundNode != null)
        //        {
        //            string[] wftv = regexToTrim.Replace(foundNode.InnerText, "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //            for (int i = 0; i < wftv.Length; i++)
        //                result.Add(regexToTrim.Replace(wftv[i], ""));
        //        }
        //    }

        //    return result;
        //}



        ///// <summary>
        ///// 从 Xml 文档对象加载 sFtv 数据
        ///// </summary>
        ///// <returns></returns>
        //private static List<List<string>> getsFtvData()
        //{
        //    const int monthNumPerYear = 12;
        //    List<List<string>> result = new List<List<string>>();

        //    for (int i = 0; i < monthNumPerYear; i++)    // C#: 预置 12 个元素
        //        result.Add(new List<string>());

        //    //----------------------------------------------------------------------------------------
        //    // 加载 Xml 数据:  按周规则定义的节日(纪念日)
        //    // 注: 加载时自动去除各行 Xml 数据前、后端的所有空白字符, 但对数据内部的空白字符不作处理
        //    //----------------------------------------------------------------------------------------
        //    if (Util.SxwnlXmlData != null)
        //    {
        //        XmlNode foundNode;
        //        Regex regexToTrim = new Regex(@"(^\s*)|(\s*$)");    // C#: 匹配前、后端的任何空白字符

        //        for (int i = 0; i < monthNumPerYear; i++)
        //        {
        //            string xPath = "SharpSxwnl/SxwnlData/Data[@Id = 'oba_sFtv']/Month[@Id = '" + (i + 1).ToString() + "']";

        //            foundNode = Util.SxwnlXmlData.SelectSingleNode(xPath);
        //            if (foundNode != null)
        //            {
        //                string[] sftv = regexToTrim.Replace(foundNode.InnerText, "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //                for (int j = 0; j < sftv.Length; j++)
        //                    result[i].Add(regexToTrim.Replace(sftv[j], ""));
        //            }

        //        }
        //    }

        //    return result;
        //}

    }
}
