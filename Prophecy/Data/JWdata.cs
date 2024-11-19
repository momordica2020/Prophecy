/*-------------------------------------------------------------------------------------------------------------------
 地理经纬度数据: 

    经纬度的压缩编码细节(以经度79度48分 北纬12度49分 为例,):
        字元表 s = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'; 共62个字元
        数字  0 编码为 s 中的第  1 个字符(0)
        数字  1 编码为 s 中的第  2 个字符(1)
        数字 10 编码为 s 中的第 11 个字符(A)
        数字 59 编码为 s 中的第 60 个字符(x)
        ...
        1.将'度'减去73，即79-73 = 6，因为我国各地经度在73到133度之间,减73之后,经度就变为0到60之间的数
        2.使用字元表对 '6度'进行编码,得到字符6,编码后只用一个字母就可以表示"度"
        3.使用字元表对'48分'进行编码,得到字符m,编码后只用一个字母就可以表示"分"
        4.最后，79度48分被编码为 6m
        5.同样方法对纬度编码(但不用减73)得到 Cn
        6.经纬度的总编码为(纬度放在前) Cn6m
        7.解码过程是上面过程的逆运算。通过以上处理，比常规表示法减少60%的数据量
-------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace Prophecy.Data
{
    /// <summary>
    /// 经纬及时区类
    /// </summary>
    public partial class JWdata
    {

        /// <summary>
        /// 经度(弧度值)
        /// </summary>
        public static double J { get; set; }    // = 0;

        /// <summary>
        /// 纬度(弧度值)
        /// </summary>
        public static double W { get; set; }    // = 0;

        /// <summary>
        /// 存储压缩的各地经纬度数据, 功能类似于交错数组 string[][]
        /// </summary>
        public static List<List<string>> JWv = new List<List<string>>();

        /// <summary>
        /// 存储压缩的各时区数据, 功能类似于交错数组 string[][]
        /// </summary>
        public static List<List<string>> SQv = new List<List<string>>();


        private static int nothing = Init();     // C#: 通过一个私有字段的赋值来初始化本类的数组



        /// <summary>
        /// 初始化数据
        /// </summary>
        private static int Init()
        {
            DaylightInfo = string.Empty;
            SQDescription = string.Empty;

            //----------------------------------------------------------------------------------------
            // 加载 Xml 数据:  各地经纬度表, 时区表
            // 注: 加载时自动去除 Xml 各行数据前、后端的所有空白字符, 但对数据内部的空白字符不作处理
            //----------------------------------------------------------------------------------------
            if (Util.SxwnlXmlData != null)
            {
                const string JWvXPath = "SharpSxwnl/SxwnlData/Data[@Id = 'JWdata_JWv']";
                const string SQvXPath = "SharpSxwnl/SxwnlData/Data[@Id = 'JWdata_SQv']";

                XmlNode foundNode;
                Regex regexToTrim = new Regex(@"(^\s*)|(\s*$)");    // C#: 匹配任何前后端的空白字符
                char[] lineFlags = new char[] { '\r', '\n' };


                // 读取并解开各地经纬度表
                foundNode = Util.SxwnlXmlData.SelectSingleNode(JWvXPath);
                if (foundNode != null)
                {
                    string[] strJWv = regexToTrim.Replace(foundNode.InnerText, "").Split(lineFlags, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strJWv.Length; i++)
                    {
                        JWv.Add(new List<string>());
                        JWv[i].AddRange(regexToTrim.Replace(strJWv[i], "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));  //解开清单
                    }
                }


                // 读取并解开各时区表
                foundNode = Util.SxwnlXmlData.SelectSingleNode(SQvXPath);
                if (foundNode != null)
                {
                    string[] strSQv = regexToTrim.Replace(foundNode.InnerText, "").Split(lineFlags, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strSQv.Length; i++)
                    {
                        SQv.Add(new List<string>());
                        SQv[i].AddRange(regexToTrim.Replace(strSQv[i], "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));  //解开各地时区清单
                    }
                }

            }

            return 1;
        }





        /// <summary>
        /// 经纬度解压缩
        /// </summary>
        /// <param name="v">经纬度字符串(压缩格式)</param>
        public static void JWdecode(string v)
        {
            int i;
            char[] vChar = v.ToCharArray();
            int[] vAscii = new int[4];
            for (i = 0; i < 4; i++)    //对经纬度解压缩
            {
                vAscii[i] = vChar[i];
                if (vAscii[i] > 96) vAscii[i] -= 97 - 36;
                else if (vAscii[i] > 64) vAscii[i] -= 65 - 10;
                else vAscii[i] -= 48;
            }
            J = -(vAscii[2] + vAscii[3] / 60d + 73) / 180d * Math.PI;    // C#: 注意数据类型
            W = (vAscii[0] + vAscii[1] / 60d) / 180d * Math.PI;
        }


        /// <summary>
        /// 时差
        /// </summary>
        public static double SQTimeDifference { get; set; }    // = 0;    // 时差

        /// <summary>
        /// 日光参数
        /// </summary>
        public static string DaylightInfo { get; set; }    // = "";       // 日光参数

        /// <summary>
        /// 时区描述
        /// </summary>
        public static string SQDescription { get; set; }    // = "";      // 时区描述


        /// <summary>
        /// 解析时区信息数据, 并保存到静态公共属性中
        /// </summary>
        /// <param name="strSQInfo"></param>
        public static void SQdecode(string strSQInfo)
        {
            string[] SQInfos = strSQInfo.Split('#');
            SQTimeDifference = double.Parse(SQInfos[0]);
            DaylightInfo = SQInfos[1];
            SQDescription = SQInfos[2];
        }



    }
}
