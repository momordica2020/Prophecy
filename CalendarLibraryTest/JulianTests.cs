using CalendarLibrary.Astronomy;
using CalendarLibrary.Calendars;
using CalendarLibrary.Configuration;
using CalendarLibrary.Julian;
using CalendarLibrary.Utils;
using Newtonsoft.Json.Converters;
using Prophecy;





namespace CalendarLibraryTest
{
    [TestClass]
    public class JulianTests
    {
        [TestMethod]
        public void TestJulianDayConversion()
        {
            // 测试公历转儒略日
            double julianDay = JulianConverter.ToJulianDay(2000, 1, 1, 12);
            Assert.AreEqual(2451545.0, julianDay, 1e-10, "公历日期转换为儒略日失败");

            julianDay = JulianConverter.ToJulianDay(2021, 12, 6);
            Assert.AreEqual(2459554.5, julianDay, 1e-10, "公历日期转换为儒略日失败");

            // 测试儒略日转公历
            DateTime date = JulianConverter.FromJulianDay(2459554.5);
            Assert.AreEqual(new DateTime(2021, 12, 6, 0, 0, 0), date, "儒略日转换为公历日期失败");

            date = JulianConverter.FromJulianDay(2451545.0);
            Assert.AreEqual(new DateTime(2000, 1, 1, 12, 0, 0), date, "儒略日转换为公历日期失败");
        }

        [TestMethod]
        public void TestJulianCenturyCalculation()
        {
            double julianDay = JulianConverter.ToJulianDay(2000, 1, 1, 12);
            double century = JulianConverter.JulianCentury(julianDay);
            Assert.AreEqual(0.0, century, 1e-10, "儒略世纪计算失败");
        }

        [TestMethod]
        public void TestJulianDayFromDateTime()
        {
            DateTime date = new DateTime(2021, 12, 6, 12, 0, 0);
            double julianDay = JulianConverter.ToJulianDay(date);
            Assert.AreEqual(2459555.0, julianDay, 1e-10, "日期时间转换为儒略日失败");
        }
    }


    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestPow()
        {
            Assert.AreEqual(1, CUtil.Pow(2, 0), "幂运算失败");
            Assert.AreEqual(8, CUtil.Pow(2, 3), "幂运算失败");
            Assert.AreEqual(0.125, CUtil.Pow(2, -3), 1e-10, "幂运算失败");
        }

        [TestMethod]
        public void TestRound()
        {
            Assert.AreEqual(3.14, CUtil.Round(3.1415926, 2), 1e-10, "四舍五入失败");
            Assert.AreEqual(3.1, CUtil.Round(3.1415926, 1), 1e-10, "四舍五入失败");
            Assert.AreEqual(3.142, CUtil.Round(3.1415926, 3), 1e-10, "四舍五入失败");
        }

        [TestMethod]
        public void TestBoolToInt()
        {
            Assert.AreEqual(1, CUtil.BoolToInt(true), "布尔值转整数失败");
            Assert.AreEqual(0, CUtil.BoolToInt(false), "布尔值转整数失败");
        }

        [TestMethod]
        public void TestIntToBool()
        {
            Assert.IsTrue(CUtil.IntToBool(1), "整数转布尔值失败");
            Assert.IsFalse(CUtil.IntToBool(0), "整数转布尔值失败");
        }

        [TestMethod]
        public void TestStringSplice()
        {
            string result = CUtil.StringSplice("Hello", " ", "World", "!");
            Assert.AreEqual("Hello World!", result, "字符串拼接失败");
        }
    }


    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void TestDefaultConfig()
        {
            // 创建默认配置
            var config = new CalendarConfig();

            // 检查默认值
            Assert.AreEqual(GridMode.Month, config.Grid, "默认网格显示模式应为按月");
            Assert.AreEqual(0, config.FirstWeek, "默认第一列应为周日");
            Assert.AreEqual(TimeZoneInfo.Local.Id, config.TimeZoneName, "默认时区应为本地时区");
            Assert.IsTrue(config.EnableSolarTerms, "默认启用节气功能");
            Assert.IsTrue(config.EnableLunar, "默认启用农历功能");
            Assert.IsTrue(config.EnableHeavenlyEarthly, "默认启用天干地支功能");
            Assert.IsTrue(config.DistinguishZiHour, "默认区分早晚子时");
            Assert.IsTrue(config.EnableStarSign, "默认启用星座功能");
        }

        [TestMethod]
        public void TestCloneConfig()
        {
            // 创建自定义配置
            var config = new CalendarConfig
            {
                Grid = GridMode.Week,
                FirstWeek = 1,
                TimeZoneName = "UTC",
                EnableSolarTerms = false,
                EnableLunar = false,
                EnableHeavenlyEarthly = false,
                DistinguishZiHour = false,
                EnableStarSign = false
            };

            // 克隆配置
            var clonedConfig = config.Clone();

            // 确保克隆结果一致但是新对象
            Assert.AreEqual(config.Grid, clonedConfig.Grid, "克隆网格显示模式失败");
            Assert.AreEqual(config.FirstWeek, clonedConfig.FirstWeek, "克隆第一列显示周失败");
            Assert.AreEqual(config.TimeZoneName, clonedConfig.TimeZoneName, "克隆时区名称失败");
            Assert.AreEqual(config.EnableSolarTerms, clonedConfig.EnableSolarTerms, "克隆节气功能失败");
            Assert.AreEqual(config.EnableLunar, clonedConfig.EnableLunar, "克隆农历功能失败");
            Assert.AreEqual(config.EnableHeavenlyEarthly, clonedConfig.EnableHeavenlyEarthly, "克隆天干地支功能失败");
            Assert.AreEqual(config.DistinguishZiHour, clonedConfig.DistinguishZiHour, "克隆区分早晚子时失败");
            Assert.AreEqual(config.EnableStarSign, clonedConfig.EnableStarSign, "克隆星座功能失败");
            Assert.AreNotSame(config, clonedConfig, "克隆结果应为新对象");
        }
    }


    [TestClass]
    public class AstronomyTests
    {
        [TestMethod]
        public void TestDeltaTDays()
        {
            double deltaT = Astronomy.DeltaTDays(2021, 12); // 0.0008406386097956
            Assert.AreEqual(0.0008406386, Util.Round(deltaT, 10), "DeltaT 天数计算失败");
        }

        [TestMethod]
        public void TestDeltaTMinutes()
        {
            double deltaTMinutes = Astronomy.DeltaTMinutes(2021, 12);   // 1.2105195981056707
            Assert.AreEqual(1.2105195981, Util.Round(deltaTMinutes, 10), "DeltaT 分钟计算失败");
        }

        [TestMethod]
        public void TestPerturbation()
        {
            double perturbation = Astronomy.Perturbation(2298519);
            Assert.AreEqual(-0.0056141749, Util.Round(perturbation, 10), "摄动值计算失败");
        }

        [TestMethod]
        public void TestVernalEquinox()
        {
            double vernalEquinox = Astronomy.VernalEquinox(2021);
            Assert.AreEqual(2459293.8997175973, Util.Round(vernalEquinox, 10), "春分点计算失败");
        }

        [TestMethod]
        public void TestMeanSolarTerms()
        {
            var mst = Astronomy.MeanSolarTermsJd(2021);
            Assert.AreEqual(2459293.8997175973, Util.Round(mst[0], 10), "春分计算失败");
            Assert.AreEqual(2459309.060708575, Util.Round(mst[1], 10), "清明计算失败");
            Assert.AreEqual(2459324.354092278, Util.Round(mst[2], 10), "谷雨计算失败");
            Assert.AreEqual(2459659.142093854, Util.Round(mst[24], 10), "雨水计算失败");
            Assert.AreEqual(2459674.3030848317, Util.Round(mst[25], 10), "惊蛰计算失败");
        }

        [TestMethod]
        public void TestTrueNewMoon()
        {
            double jd = Astronomy.ReferenceLunarMonthNum(2298519);
            var tnm = Astronomy.TrueNewMoon(jd);
            Assert.AreEqual(2298493.2989711817, Util.Round(tnm, 10), "新月计算失败");
        }

        [TestMethod]
        public void TestAdjustedSolarTermsJd()
        {
            var jqs = Astronomy.AdjustedSolarTermsJd(2021, 0, 25);
            Assert.AreEqual(2459293.9010286564, Util.Round(jqs[0], 10), "春分计算失败");
            Assert.AreEqual(2459309.0658356417, Util.Round(jqs[1], 10), "清明计算失败");
            Assert.AreEqual(2459324.356054907, Util.Round(jqs[2], 10), "谷雨计算失败");
            Assert.AreEqual(2459659.1481248834, Util.Round(jqs[24], 10), "雨水计算失败");
            Assert.AreEqual(2459674.3054912435, Util.Round(jqs[25], 10), "惊蛰计算失败");
        }


        [TestMethod]
        public void TestLastYearSolarTerms()
        {
            var jqs = Astronomy.LastYearSolarTerms(2021);
            Assert.AreEqual(0, Util.Round(jqs[0], 10), "春分计算失败");
            Assert.AreEqual(0, Util.Round(jqs[17], 10), "清明计算失败");
            Assert.AreEqual(2459204.9184778044, Util.Round(jqs[18], 10), "谷雨计算失败");
            Assert.AreEqual(2459278.8707997804, Util.Round(jqs[23], 10), "雨水计算失败");
        }

    }


    [TestClass]
    public class CalendarTests
    {
        [TestMethod]
        public void TestDefaultCalendar()
        {
            var calendar = new GregorianCalendar();
            Assert.IsNotNull(calendar, "默认日历创建失败");
        }

        [TestMethod]
        public void TestSolarTerms()
        {
            var config = new CalendarConfig { TimeZoneName = "Asia/Shanghai" };
            var calendar = new GregorianCalendar();
            var solarTerms = calendar.GetSolarTerms(2021);

            Assert.IsNotNull(solarTerms, "节气计算失败");
        }

        [TestMethod]
        public void TestGenerateWithDate()
        {
            var config = new CalendarConfig { Grid = GridMode.Week };
            var calendar = new GregorianCalendar();
            var result = calendar.GenerateWithDate(2021, 12, 22);

            Assert.IsNotNull(result, "生成指定日期的日历失败");
        }
    }

    [TestClass]
    public class ChineseCalendarTests
    {
        [TestMethod]
        public void TestGregorianToLunar()
        {
            var calendar = new GregorianCalendar();
            var lunarDate = calendar.ConvertToLunar(new DateTime(1000, 6, 5));
            Assert.IsNotNull(lunarDate, "公历转农历失败");
        }

        [TestMethod]
        public void TestLunarToGregorian()
        {
            var calendar = new GregorianCalendar();
            var lunarDate = calendar.ConvertToLunar(new DateTime(2020, 4, 14));
            var gregorianDate = calendar.ConvertToGregorian(lunarDate);

            Assert.AreEqual(new DateTime(2020, 4, 14), gregorianDate, "农历转公历失败");
        }
    }

    [TestClass]
    public class DateTimeUtilsTests
    {
        [TestMethod]
        public void TestIsLeapYear()
        {
            Assert.IsFalse(CUtil.IsLeapYear(1900), "1900年不是闰年");
            Assert.IsTrue(CUtil.IsLeapYear(2000), "2000年是闰年");
            Assert.IsTrue(CUtil.IsLeapYear(1940), "1940年是闰年");
            Assert.IsFalse(CUtil.IsLeapYear(2100), "2100年不是闰年");
        }

        [TestMethod]
        public void TestBeginningOfMonth()
        {
            var date = new DateTime(2021, 5, 15);
            var expected = new DateTime(2021, 5, 1);
            Assert.AreEqual(expected, CUtil.BeginningOfMonth(date), "月初计算失败");
        }

        [TestMethod]
        public void TestEndOfMonth()
        {
            var date = new DateTime(2021, 5, 15);
            var expected = new DateTime(2021, 5, 31);
            Assert.AreEqual(expected, CUtil.EndOfMonth(date), "月末计算失败");
        }
    }



    [TestClass]
    public class ProphencyTests
    {
        [TestMethod]
        public void TestHuili()
        {
            Assert.AreEqual("1445-07-01", new JDateTime(2024, 1, 12).ToStringIslamic("yyyy-MM-dd"), "伊斯兰历算错了");
            Assert.AreEqual("1445-08-01", new JDateTime(2024, 2, 11).ToStringIslamic("yyyy-MM-dd"), "伊斯兰历算错了");
            Assert.AreEqual("1445-09-01", new JDateTime(2024, 3, 11).ToStringIslamic("yyyy-MM-dd"), "伊斯兰历算错了");

            Assert.AreEqual("2024-01-12", JDateTime.FromIslamicDate(1445, 7, 1).ToStringGeroge("yyyy-MM-dd"), "伊斯兰历算错了");
        }
    }




}