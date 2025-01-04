using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Prophecy
{
    /// <summary>
    /// 助理类
    /// </summary>
    public partial class Util
    {


        #region 常量定义

        /// <summary>
        /// 地球赤道半径(千米)
        /// </summary>
        public const double cs_rEar = 6378.1366;              // 地球赤道半径(千米)

        /// <summary>
        /// 平均半径
        /// </summary>
        public const double cs_rEarA = 0.99834 * cs_rEar;     // 平均半径

        /// <summary>
        /// 天文单位长度(千米)
        /// </summary>
        public const double cs_AU = 1.49597870691e8;          // 天文单位长度(千米)

        /// <summary>
        /// Sin(太阳视差)
        /// </summary>
        public const double cs_sinP = cs_rEar / cs_AU;        // sin(太阳视差)

        /// <summary>
        /// 太阳视差
        /// </summary>
        public const double cs_PI = 0.0000426352097959108;    // 太阳视差, 即 Math.Asin(cs_sinP)

        /// <summary>
        /// 每弧度的角秒数
        /// </summary>
        public const double rad = 180 * 3600 / Math.PI;    // 每弧度的角秒数


        /// <summary>
        /// 2000年1月1日 12:00:00 的儒略日数
        /// </summary>
        //public const double J2000 = 2451545;               // 2000年1月1日 12:00:00 的儒略日数
        
        #endregion 常量定义


        /// <summary>
        /// 将弧度转为指定格式的字符串(度分秒, 或时分秒)
        /// </summary>
        /// <param name="d">要转换的弧度</param>
        /// <param name="tim">指明返回值的格式类型</param>
        /// <returns>tim = 0 输出格式示例: -23°59' 48.23"
        /// tim = 1 输出格式示例:  18h 29m 44.52s
        /// </returns>
        public static string rad2str(double d, int tim)
        {
            string s = " ";
            string w1 = "°", w2 = "'", w3 = "\"";
            if (d < 0)          // C#: 要转换的弧度值为负数
            {
                d = -d; s = "-";
            }
            if (tim != 0)       // C#: 要返回值的格式为"时分秒"
            {
                d *= 12 / Math.PI; w1 = "h "; w2 = "m "; w3 = "s";
            }
            else d *= 180 / Math.PI;       // C#: 要返回值的格式为"度分秒"

            double a = Math.Floor(d); d = (d - a) * 60;
            double b = Math.Floor(d); d = (d - b) * 60;
            double c = Math.Floor(d); d = (d - c) * 100;
            d = Math.Floor(d + 0.5);
            if (d >= 100) { d -= 100; c++; }
            if (c >= 60) { c -= 60; b++; }
            if (b >= 60) { b -= 60; a++; }

            string aStr = "   " + a, bStr = "0" + b, cStr = "0" + c, dStr = "0" + d;
            s += aStr.Substring(aStr.Length - 3, 3) + w1 +
                 bStr.Substring(bStr.Length - 2, 2) + w2 +
                 cStr.Substring(cStr.Length - 2, 2) + "." +
                 dStr.Substring(dStr.Length - 2, 2) + w3;
            return s;
        }



        /// <summary>
        /// 将弧度转为字串,精确到分
        /// </summary>
        /// <param name="d">要转换的弧度</param>
        /// <returns>输出格式示例: -23°59'</returns>
        public static string rad2str2(double d)
        {
            string s = "+";
            string w1 = "°", w2 = "'";
            if (d < 0) { d = -d; s = "-"; }
            d *= 180 / Math.PI;
            double a = Math.Floor(d);
            double b = Math.Floor((d - a) * 60 + 0.5);
            if (b >= 60) { b -= 60; a++; }
            string aStr = "   " + a, bStr = "0" + b;
            s += aStr.Substring(aStr.Length - 3, 3) + w1 +
                 bStr.Substring(bStr.Length - 2, 2) + w2;
            return s;
        }



        /// <summary>
        /// 秒转为分秒
        /// </summary>
        /// <param name="v">要转换的秒</param>
        /// <param name="fx">小数点位数</param>
        /// <param name="fs">为 1 转为"分秒"格式, 否则转为"角分秒"格式</param>
        /// <returns></returns>
        public static string m2fm(double v, int fx, int fs)
        {
            string gn = "";
            if (v < 0) { v = -v; gn = "-"; }
            double f = Math.Floor(v / 60), m = v - f * 60;
            if (fs != 0) 
                return gn + f + "分" + m.ToString("F" + fx) + "秒";
            else  
                return gn + f + "'" + m.ToString("F" + fx) + "\"";
        }



        /// <summary>
        /// 对超过0-2PI的角度转为0-2PI
        /// </summary>
        /// <param name="v">要转换的角度</param>
        /// <returns></returns>
        public static double rad2mrad(double v)
        {
            v = v % (2 * Math.PI);
            if (v < 0)  v += 2 * Math.PI;
            return v;
        }



        /// <summary>
        /// 对超过-PI到PI的角度转为-PI到PI
        /// </summary>
        /// <param name="v">要转换的角度</param>
        /// <returns></returns>
        public static double rad2rrad(double v)
        {
            v = v % (2 * Math.PI);
            if (v <= -Math.PI) return v + 2 * Math.PI;
            if (v > Math.PI) return v - 2 * Math.PI;
            return v;
        }



        /// <summary>
        /// 临界余数(a与最近的整倍数b相差的距离)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double mod2(double a, double b)
        {
            double c = (a / b);
            c -= Math.Floor(c);
            if (c > 0.5) c -= 1;
            return c * b;
        }


        /// <summary>
        /// 四舍五入到指定小数位
        /// </summary>
        /// <param name="value">需要四舍五入的数值</param>
        /// <param name="decimalPlaces">小数位数</param>
        /// <returns>四舍五入结果</returns>
        public static double Round(double value, int decimalPlaces)
        {
            double factor = Math.Pow(10, decimalPlaces);
            return Math.Round(value * factor) / factor;
        }



        /// <summary>
        /// 传入普通纪年或天文纪年，传回天文纪年
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c">普通纪年</param>
        /// <returns></returns>
        public static int year2Ayear(int y)
        {
            //int y;
            //Regex regexToReplace = new Regex(@"[^0-9Bb\*-]");          // C#: 匹配字符: 数字0-9, B, b, *, -
            //string strC = regexToReplace.Replace(c.ToString(), "");    // C#: 去除无效字符

           // string q = strC.Substring(0, 1);
            //if (q == "B" || q == "b" || q == "*")     //通用纪年法(公元前)
            //{
            //    y = 1 - Util.VAL(strC.Substring(1), 1);
            //    if (y > 0)
            //    {
            //        //MessageBox.Show("通用纪法的公元前纪法从 B.C.1 年开始，并且没有公元 0 年！", 
            //        //                "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return -10000;
            //    }
            //}
            //else y = Util.VAL(strC, 1);

            if (y < -4712)
            {
                //MessageBox.Show("不得小于 B.C.4713 年！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return -10000;
            }
            //if (y > 9999)
            //{
            //    //MessageBox.Show("超过9999年的农历计算很不准。", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            return y;
        }
        


        ///// <summary>
        ///// 传入天文纪年，传回显示用的常规纪年
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="y">天文纪年, 泛型, 支持数值或字符串</param>
        ///// <returns></returns>
        //public static string Ayear2year<T>(T y)
        //{
        //    int result = Util.VAL(y.ToString(), 1);
        //    if (result <= 0) return "B" + (-result + 1);
        //    return result.ToString();
        //}



        /// <summary>
        /// 时间串转为小时
        /// </summary>
        /// <param name="s">时间串</param>
        /// <returns></returns>
        public static double timeStr2hour(string s)
        {
            Regex regexToReplace = new Regex(@"[^0-9:]");    // C#: 匹配字符: 数字0-9, :
            int a, b, c;
            string[] timeStr = regexToReplace.Replace(s, "").Split(':');    // C#: 去除无效字符后, 按 : 分隔字符串
            for (int i = 0; i < timeStr.Length; i++) {                      // C#: 即使参数 s 为空串, 也会产生一个数组元素
                if (timeStr[i].Length == 0)      // C#: 把空串设置为 "0"
                    timeStr[i] = "0";
            }
            switch (timeStr.Length)
            {
                case 1:
                    {
                        // C#: 为避免 Substring 方法超出范围取子串引发异常, 改用本类中的静态方法 SUBSTRING
                        a = Util.VAL(Util.SUBSTR(timeStr[0], 0, 2), 1);
                        b = Util.VAL(Util.SUBSTR(timeStr[0], 2, 2), 1);
                        c = Util.VAL(Util.SUBSTR(timeStr[0], 4, 2), 1);
                        break;
                    }
                case 2:
                    {
                        a = Util.VAL(timeStr[0], 1);
                        b = Util.VAL(timeStr[1], 1);
                        c = 0;
                        break;
                    }
                default:
                    {
                        a = Util.VAL(timeStr[0], 1);
                        b = Util.VAL(timeStr[1], 1);
                        c = Util.VAL(timeStr[2], 1);
                        break;
                    }
            }
            return a + b / 60d + c / 3600d;
        }
        

     

        ///// <summary>
        ///// 计算八字
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="ob">日对象</param>
        ///// <param name="type">八字类型</param>
        ///// <param name="TimeZoneNum">时区</param>
        ///// <param name="year">年</param>
        ///// <param name="month">月</param>
        ///// <param name="day">日</param>
        ///// <param name="time">时间串</param>
        ///// <param name="longitudeStr">经度(度分秒格式)</param>
        ///// <returns>八字字符串</returns>
        //public static string ML_calc<T>(DayInfo ob, BaZiType type, double TimeZoneNum, T year, T month, T day, string time, string longitudeStr, BaZiTypeS baziTypes)
        //{
        //    double y = Util.year2Ayear(year);
        //    if (y == -10000) return String.Empty;

        //    string timeName = (type == BaZiType.TrueLocalSolar ? "真太阳 " :  (type == BaZiType.PingLocalSolar ? "平太阳 " : "北京时间 "));

        //    double t = Util.timeStr2hour(time);

        //    double longitude;
        //    if (type == BaZiType.TrueBJ) longitude = Util.str2rad("-120°");    // 解析东经120°经度为弧度
        //    else longitude = Util.str2rad(longitudeStr);                        // 解析经度为弧度

        //    var jdt = new JDateTime((int)y, int.Parse(month.ToString()),  (int)(int.Parse(day.ToString()) + t / 24));

        //    LunarData.mingLiBaZi(jdt.JulianDateFrom2000 + TimeZoneNum / 24, longitude, ob, baziTypes);    // 八字计算, 独立于 Lunar 类
        //    //if (type == BaZiType.TrueLocalSolar) {
                
        //    //} else {
        //    //    LunarData.mingLiBaZiNormal(jdt.JulianDateFrom2000 + TimeZoneNum / 24, longitude, ob, baziTypes);    // 八字计算, 独立于 Lunar 类
        //    //}

        //    return $"[日标]：公历 {jdt.Year}-{jdt.Month}-{jdt.Day} 儒略日数 {Math.Floor(jdt.JulianDate + 0.5)}" 
        //           + $" 距2000年首{Math.Floor(jdt.JulianDateFrom2000 + 0.5)}日\r\n"
        //           + $"[八字]：{ob.bz_jn}年 {ob.bz_jy}月 {ob.bz_jr}日 {ob.bz_js}时 {timeName}{(type == BaZiType.TrueLocalSolar ? ob.bz_zty?.ToString():ob.bz_pty?.ToString())}\r\n"
        //           + $"[纪时]：{ob.bz_JS}\r\n"
        //           + $"[时标]：" + "23　 01　 03　 05　 07　 09　 11　 13　 15　 17　 19　 21　 23";

        //}
        
  

        /// <summary>
        /// 将度分秒转换为弧度值(只作简单转化, 要求传递的格式严格遵守"度分秒"的格式, 如: 0°0'31.49"
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double str2rad(string d)
        {
            double result = 0;
            string strSpliter = "°'\"";
            char[] spliters = strSpliter.ToCharArray();
            string[] strD = d.Split(spliters, StringSplitOptions.RemoveEmptyEntries);

            if (strD.Length > 0)
            {
                double a = 0, b = 0, c = 0;
                a = Util.VAL(strD[0]) / 180 * Math.PI;                     // 1°= 1/180*PI ≈ 0.017453292519943 弧度
                if (strD.Length > 1)
                {
                    b = Util.VAL(strD[1]) / 60 / 180 * Math.PI;            // 1' = (1/60)°≈ 0.000290888208666 弧度
                    if (strD.Length > 2)
                    {
                        c = Util.VAL(strD[2]) / 60 / 180 / 60 * Math.PI;   // 1" = (1/60)' ≈ 0.000004848136811 弧度
                    }
                }
                if (a > 0)
                    result = a + b + c;
                else
                    result = a - b - c;
            }
            return result;
        }


        /// <summary>
        /// 将弧度转化为相等的度
        /// </summary>
        /// <param name="nRadians"></param>
        /// <returns></returns>
        public static double RTOD(double nRadians)
        {
            return ((nRadians * 180) / Math.PI);
        }


        /// <summary>
        /// 将度转换为弧度
        /// </summary>
        /// <param name="nDegrees"></param>
        /// <returns></returns>
        public static double DTOR(double nDegrees)
        {
            return ((nDegrees * Math.PI) / 180);
        }

        
        /// <summary>
        /// 返回指定时间(缺省则为现在时刻)的 UTC 时间, 从 1970-1-1午夜开始所经过的毫秒数
        /// </summary>
        /// <returns></returns>
        public static double NowUTCmsSince19700101(DateTime? nowDT = null)
        {
            if(nowDT == null) nowDT = DateTime.Now;
            DateTime DT19700101 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTimeOffset nowDTO = new DateTimeOffset((DateTime)nowDT);
            return (nowDTO.UtcDateTime - DT19700101).TotalMilliseconds;
        }

        /// <summary>
        /// 根据中文日期表示来取得数字。例如初三=3，二三=23，二十三=23，廿四=24，正=1，腊=12
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetNumFromHans(string input)
        {
            int res = 0;

            Dictionary<char, int> ChineseDigitMap = new()
            {   
                { '〇', 0 }, { '零', 0 }, { '初', 0 },
                { '一', 1 }, { '壹', 1 }, { '壱', 1 }, { '〡', 1 }, { '正', 1 },
                { '二', 2 }, { '贰', 2 }, { '弐', 2 }, { '貮', 2 }, { '两', 2 },
                { '三', 3 }, { '叁', 3 }, { '参', 3 }, { '仨', 3 },
                { '四', 4 }, { '肆', 4 }, { '〤', 4 },
                { '五', 5 }, { '伍', 5 }, { '〥', 5 }, 
                { '六', 6 }, { '陆', 6 }, { '〦', 6 }, 
                { '七', 7 }, { '柒', 7 }, { '〧', 7 },
                { '八', 8 }, { '捌', 8 }, { '〨', 8 },
                { '九', 9 }, { '玖', 9 }, { '〩', 9 },
                { '十', 10 }, { '拾', 10 },
                { '腊', 12 },
                { '廿', 20 }, 
                { '卅', 30 }, 
                { '卌', 40 }, 
            };
            int tempNumber = 0;
            foreach (char ch in input)
            {
                if (ChineseDigitMap.TryGetValue(ch, out int value))
                {
                    if (value >= 10) // 遇到“十”
                    {
                        if (tempNumber == 0)
                        {
                            tempNumber = value; // “十”单独出现表示10
                        }
                        else
                        {
                            tempNumber *= value; // 如“二十”表示2*10
                        }
                    }
                    else
                    {
                        if (tempNumber >= 10)
                        {
                            tempNumber += value;
                            res += tempNumber;
                            tempNumber = 0;
                        }
                        else
                        {
                            tempNumber = tempNumber * 10 + value; // 普通数字接在后面
                        }
                    }
                }
                else
                {
                    res = 0;
                    break;
                }
            }

            return res;
        }


        


        /// <summary>
        /// 取子字符串, 允许起始位置超过整个字符串长度(此时返回空串), 弥补 String.Substring 方法的不足
        /// </summary>
        /// <param name="cExpression">被取子串的字符串</param>
        /// <param name="nStartPosition">起始位置（从零开始）</param>
        /// <param name="nLength">子串的长度</param>
        /// <returns></returns>
        public static string SUBSTR(string cExpression, int nStartPosition, int nLength)
        {
            if (nStartPosition >= cExpression.Length || nStartPosition < 0 || nLength <= 0)
                return String.Empty;
            if (nLength + nStartPosition >= cExpression.Length)
                return cExpression.Substring(nStartPosition);
            else
                return cExpression.Substring(nStartPosition, nLength);
        }
        /// <summary>
        /// 截串(网页设计对过长的文字做截处理)
        /// </summary>
        /// <param name="s">被截取的字符串</param>
        /// <param name="n">要截取的长度</param>
        /// <param name="end">对超长字符串, 在截取的子串尾部添加的内容</param>
        /// <returns></returns>
        public static string SUBSTR2(string s, int n, string end)
        {
            Regex regToReplace = new Regex(@"(^\s*)|(\s*$)", RegexOptions.ECMAScript);   // 去除首尾空白字符的正则表达式模板
            s = regToReplace.Replace(s, "");
            if (s.Length > n + 1) return s.Substring(0, n) + end;
            return s;
        }


        /// <summary>
        /// 将字符串解析为数值, 允许字符串中含有非数值类型的字符
        /// </summary>
        /// <param name="strExpression">要解析的字符串</param>
        /// <returns></returns>
        public static double VAL(string strExpression)
        {
            // 数值字符串的正则表达式(注: "|"表示"或者", n >= 0):
            //   允许的字符:        ￥|$   +|-  [0-9]   .     [0-9]   E|e   +|-   [0-9]   %
            //   允许出现的次数:     0,1   0,1    n     0,1     n     0,1   0,1     n     0,1
            //   正则式表示的次数:    ?     ?     *      ?      *      ?     ?      *      ?
            const string numberPattern = @"(((￥|\$)?)((\+|\-)?)([0-9]*)((\.)?)([0-9]*)((E|e)?)((\+|\-)?)([0-9]*))((%)?)";

            double result = 0;
            string strResult = "";
            double percentPow = 1;

            char[] toTrimStart = new char[] { ' ' };                 // 允许在前面出现的空白字符: 空格符
            strExpression = strExpression.TrimStart(toTrimStart);    // 去除前面的空白字符

            Regex numberRegPattern = new Regex(numberPattern);
            Match matched = numberRegPattern.Match(strExpression);
            if (matched.Success)
            {
                strResult = matched.ToString().ToUpper();    // 匹配结果转换为大写的字符串
                if (strResult.EndsWith("%"))    // 如果字符串以某符号(本行为 "%") 结尾, 去除此符号, 以下类推
                {
                    strResult = strResult.Substring(0, strResult.Length > 1 ? strResult.Length - 1 : 0);
                    if (strResult.IndexOf("E", StringComparison.CurrentCultureIgnoreCase) < 0)     // 不同时存在 % 和 E 字符
                        percentPow = 0.01;                                                            // 百分数, 除以 100
                }
                if (strResult.EndsWith("+") || strResult.EndsWith("-"))    // 第 1 次
                    strResult = strResult.Substring(0, strResult.Length > 1 ? strResult.Length - 1 : 0);
                if (strResult.EndsWith("E"))
                    strResult = strResult.Substring(0, strResult.Length > 1 ? strResult.Length - 1 : 0);
                if (strResult.EndsWith("."))
                    strResult = strResult.Substring(0, strResult.Length > 1 ? strResult.Length - 1 : 0);
                if (strResult.EndsWith("+") || strResult.EndsWith("-"))    // 第 2 次
                    strResult = strResult.Substring(0, strResult.Length > 1 ? strResult.Length - 1 : 0);

                if (strResult.StartsWith("￥") || strResult.StartsWith("$"))    // 扫描字符串的开始, 以下类推
                    strResult = strResult.Substring(1);
                if (strResult.Length <= 0 || strResult.StartsWith("E") || strResult.StartsWith(".E") ||
                                             strResult.StartsWith("+-") || strResult.StartsWith("-+") ||
                                             strResult.StartsWith("+E") || strResult.StartsWith("-E") ||
                                             strResult.StartsWith("+.E") || strResult.StartsWith("-.E"))
                    strResult = "0";      // 尽量避免让下面的 double.Parse 方法产生异常(补注: 已改写为 TryParse 方法)

                double.TryParse(strResult, out result);     // 在解析失败时, TryParse 方法不会产生异常
                result *= percentPow;
            }
            return result;
        }

        public static int VAL(string strExpression, int getIntegerFlag)
        {
            return (int)Util.VAL(strExpression);
        }

        public static long VAL(string strExpression, long getIntegerFlag)
        {
            return (long)Util.VAL(strExpression);
        }
        


        /// <summary>
        /// 加载 Xml 数据, 返回 XmlDocument 对象引用
        /// </summary>
        /// <returns></returns>
        private static XmlDocument LoadXmlData()
        {
            const string xmlFileName = "SharpSxwnl.xml";
            
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFileName);
            }
            catch
            {
                xmlDoc = null;
            }

            return xmlDoc;
        }

   


        // 保存 Xml 数据
        private static XmlDocument __SxwnlXmlData = Util.LoadXmlData();

        /// <summary>
        /// XmlDocument 对象, 用于读写类的 Xml 数据
        /// </summary>
        public static XmlDocument SxwnlXmlData
        {
            get { return Util.__SxwnlXmlData; }
            set { Util.__SxwnlXmlData = value; }
        }

    }








    /// <summary>
    /// 八字类型(时间)
    /// </summary>
    public enum BaZiType
    {
        /// <summary>
        /// 当地真太阳时八字
        /// </summary>
        TrueLocalSolar,

        /// <summary>
        /// 当地平太阳时八字
        /// </summary>
        PingLocalSolar,

        /// <summary>
        /// 北京时间八字(东经120度)
        /// </summary>
        TrueBJ
    }


    /// <summary>
    /// 八字类型(空间)
    /// </summary>
    public enum BaZiTypeS
    {
        /// <summary>
        /// 常规(北半球八字)
        /// </summary>
        Default,

        /// <summary>
        /// 南半球八字: 天冲地冲(月天干地支均与北半球的取法相冲)
        /// </summary>
        TCDC,

        /// <summary>
        /// 南半球八字: 天克地冲(月地支与北半球的取法相冲, 按五虎遁月法排月天干)
        /// </summary>
        TKDC,

        /// <summary>
        /// 南半球八字: 天同地冲(月地支与北半球的取法相冲, 月天干与北半球的取法相同)
        /// </summary>
        TTDC

    }




   
}


