namespace CalendarLibrary.Utils
{
    /// <summary>
    /// 工具模块，包含数学和字符串操作等通用功能
    /// </summary>
    public static class CUtil
    {
        /// <summary>
        /// 计算浮点数的整数次幂
        /// </summary>
        /// <param name="x">基数</param>
        /// <param name="n">指数</param>
        /// <returns>结果</returns>
        public static double Pow(double x, int n)
        {
            if (x == 0) return 0;
            if (n == 0) return 1;

            double result = 1;
            bool isNegative = n < 0;
            n = Math.Abs(n);

            while (n > 0)
            {
                if ((n & 1) == 1) result *= x;
                x *= x;
                n >>= 1;
            }

            return isNegative ? 1 / result : result;
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
        /// 将布尔值转换为整数
        /// </summary>
        /// <param name="b">布尔值</param>
        /// <returns>1 表示 true，0 表示 false</returns>
        public static int BoolToInt(bool b)
        {
            return b ? 1 : 0;
        }

        /// <summary>
        /// 将整数转换为布尔值
        /// </summary>
        /// <param name="i">整数</param>
        /// <returns>True 表示非零，False 表示零</returns>
        public static bool IntToBool(int i)
        {
            return i != 0;
        }

        /// <summary>
        /// 拼接多个字符串
        /// </summary>
        /// <param name="strings">要拼接的字符串数组</param>
        /// <returns>拼接后的字符串</returns>
        public static string StringSplice(params string[] strings)
        {
            return string.Concat(strings);
        }


        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }


        public static DateTime BeginningOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, date.Hour, date.Minute, date.Second);
        }


        public static DateTime EndOfMonth(DateTime date)
        {
            DateTime firstDayOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            return firstDayOfNextMonth.AddDays(-1);
        }


    }
}
