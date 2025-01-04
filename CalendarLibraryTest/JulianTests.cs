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
            // ���Թ���ת������
            double julianDay = JulianConverter.ToJulianDay(2000, 1, 1, 12);
            Assert.AreEqual(2451545.0, julianDay, 1e-10, "��������ת��Ϊ������ʧ��");

            julianDay = JulianConverter.ToJulianDay(2021, 12, 6);
            Assert.AreEqual(2459554.5, julianDay, 1e-10, "��������ת��Ϊ������ʧ��");

            // ����������ת����
            DateTime date = JulianConverter.FromJulianDay(2459554.5);
            Assert.AreEqual(new DateTime(2021, 12, 6, 0, 0, 0), date, "������ת��Ϊ��������ʧ��");

            date = JulianConverter.FromJulianDay(2451545.0);
            Assert.AreEqual(new DateTime(2000, 1, 1, 12, 0, 0), date, "������ת��Ϊ��������ʧ��");
        }

        [TestMethod]
        public void TestJulianCenturyCalculation()
        {
            double julianDay = JulianConverter.ToJulianDay(2000, 1, 1, 12);
            double century = JulianConverter.JulianCentury(julianDay);
            Assert.AreEqual(0.0, century, 1e-10, "�������ͼ���ʧ��");
        }

        [TestMethod]
        public void TestJulianDayFromDateTime()
        {
            DateTime date = new DateTime(2021, 12, 6, 12, 0, 0);
            double julianDay = JulianConverter.ToJulianDay(date);
            Assert.AreEqual(2459555.0, julianDay, 1e-10, "����ʱ��ת��Ϊ������ʧ��");
        }
    }


    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestPow()
        {
            Assert.AreEqual(1, CUtil.Pow(2, 0), "������ʧ��");
            Assert.AreEqual(8, CUtil.Pow(2, 3), "������ʧ��");
            Assert.AreEqual(0.125, CUtil.Pow(2, -3), 1e-10, "������ʧ��");
        }

        [TestMethod]
        public void TestRound()
        {
            Assert.AreEqual(3.14, CUtil.Round(3.1415926, 2), 1e-10, "��������ʧ��");
            Assert.AreEqual(3.1, CUtil.Round(3.1415926, 1), 1e-10, "��������ʧ��");
            Assert.AreEqual(3.142, CUtil.Round(3.1415926, 3), 1e-10, "��������ʧ��");
        }

        [TestMethod]
        public void TestBoolToInt()
        {
            Assert.AreEqual(1, CUtil.BoolToInt(true), "����ֵת����ʧ��");
            Assert.AreEqual(0, CUtil.BoolToInt(false), "����ֵת����ʧ��");
        }

        [TestMethod]
        public void TestIntToBool()
        {
            Assert.IsTrue(CUtil.IntToBool(1), "����ת����ֵʧ��");
            Assert.IsFalse(CUtil.IntToBool(0), "����ת����ֵʧ��");
        }

        [TestMethod]
        public void TestStringSplice()
        {
            string result = CUtil.StringSplice("Hello", " ", "World", "!");
            Assert.AreEqual("Hello World!", result, "�ַ���ƴ��ʧ��");
        }
    }


    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void TestDefaultConfig()
        {
            // ����Ĭ������
            var config = new CalendarConfig();

            // ���Ĭ��ֵ
            Assert.AreEqual(GridMode.Month, config.Grid, "Ĭ��������ʾģʽӦΪ����");
            Assert.AreEqual(0, config.FirstWeek, "Ĭ�ϵ�һ��ӦΪ����");
            Assert.AreEqual(TimeZoneInfo.Local.Id, config.TimeZoneName, "Ĭ��ʱ��ӦΪ����ʱ��");
            Assert.IsTrue(config.EnableSolarTerms, "Ĭ�����ý�������");
            Assert.IsTrue(config.EnableLunar, "Ĭ������ũ������");
            Assert.IsTrue(config.EnableHeavenlyEarthly, "Ĭ��������ɵ�֧����");
            Assert.IsTrue(config.DistinguishZiHour, "Ĭ������������ʱ");
            Assert.IsTrue(config.EnableStarSign, "Ĭ��������������");
        }

        [TestMethod]
        public void TestCloneConfig()
        {
            // �����Զ�������
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

            // ��¡����
            var clonedConfig = config.Clone();

            // ȷ����¡���һ�µ����¶���
            Assert.AreEqual(config.Grid, clonedConfig.Grid, "��¡������ʾģʽʧ��");
            Assert.AreEqual(config.FirstWeek, clonedConfig.FirstWeek, "��¡��һ����ʾ��ʧ��");
            Assert.AreEqual(config.TimeZoneName, clonedConfig.TimeZoneName, "��¡ʱ������ʧ��");
            Assert.AreEqual(config.EnableSolarTerms, clonedConfig.EnableSolarTerms, "��¡��������ʧ��");
            Assert.AreEqual(config.EnableLunar, clonedConfig.EnableLunar, "��¡ũ������ʧ��");
            Assert.AreEqual(config.EnableHeavenlyEarthly, clonedConfig.EnableHeavenlyEarthly, "��¡��ɵ�֧����ʧ��");
            Assert.AreEqual(config.DistinguishZiHour, clonedConfig.DistinguishZiHour, "��¡����������ʱʧ��");
            Assert.AreEqual(config.EnableStarSign, clonedConfig.EnableStarSign, "��¡��������ʧ��");
            Assert.AreNotSame(config, clonedConfig, "��¡���ӦΪ�¶���");
        }
    }


    [TestClass]
    public class AstronomyTests
    {
        [TestMethod]
        public void TestDeltaTDays()
        {
            double deltaT = Astronomy.DeltaTDays(2021, 12); // 0.0008406386097956
            Assert.AreEqual(0.0008406386, Util.Round(deltaT, 10), "DeltaT ��������ʧ��");
        }

        [TestMethod]
        public void TestDeltaTMinutes()
        {
            double deltaTMinutes = Astronomy.DeltaTMinutes(2021, 12);   // 1.2105195981056707
            Assert.AreEqual(1.2105195981, Util.Round(deltaTMinutes, 10), "DeltaT ���Ӽ���ʧ��");
        }

        [TestMethod]
        public void TestPerturbation()
        {
            double perturbation = Astronomy.Perturbation(2298519);
            Assert.AreEqual(-0.0056141749, Util.Round(perturbation, 10), "�㶯ֵ����ʧ��");
        }

        [TestMethod]
        public void TestVernalEquinox()
        {
            double vernalEquinox = Astronomy.VernalEquinox(2021);
            Assert.AreEqual(2459293.8997175973, Util.Round(vernalEquinox, 10), "���ֵ����ʧ��");
        }

        [TestMethod]
        public void TestMeanSolarTerms()
        {
            var mst = Astronomy.MeanSolarTermsJd(2021);
            Assert.AreEqual(2459293.8997175973, Util.Round(mst[0], 10), "���ּ���ʧ��");
            Assert.AreEqual(2459309.060708575, Util.Round(mst[1], 10), "��������ʧ��");
            Assert.AreEqual(2459324.354092278, Util.Round(mst[2], 10), "�������ʧ��");
            Assert.AreEqual(2459659.142093854, Util.Round(mst[24], 10), "��ˮ����ʧ��");
            Assert.AreEqual(2459674.3030848317, Util.Round(mst[25], 10), "���ݼ���ʧ��");
        }

        [TestMethod]
        public void TestTrueNewMoon()
        {
            double jd = Astronomy.ReferenceLunarMonthNum(2298519);
            var tnm = Astronomy.TrueNewMoon(jd);
            Assert.AreEqual(2298493.2989711817, Util.Round(tnm, 10), "���¼���ʧ��");
        }

        [TestMethod]
        public void TestAdjustedSolarTermsJd()
        {
            var jqs = Astronomy.AdjustedSolarTermsJd(2021, 0, 25);
            Assert.AreEqual(2459293.9010286564, Util.Round(jqs[0], 10), "���ּ���ʧ��");
            Assert.AreEqual(2459309.0658356417, Util.Round(jqs[1], 10), "��������ʧ��");
            Assert.AreEqual(2459324.356054907, Util.Round(jqs[2], 10), "�������ʧ��");
            Assert.AreEqual(2459659.1481248834, Util.Round(jqs[24], 10), "��ˮ����ʧ��");
            Assert.AreEqual(2459674.3054912435, Util.Round(jqs[25], 10), "���ݼ���ʧ��");
        }


        [TestMethod]
        public void TestLastYearSolarTerms()
        {
            var jqs = Astronomy.LastYearSolarTerms(2021);
            Assert.AreEqual(0, Util.Round(jqs[0], 10), "���ּ���ʧ��");
            Assert.AreEqual(0, Util.Round(jqs[17], 10), "��������ʧ��");
            Assert.AreEqual(2459204.9184778044, Util.Round(jqs[18], 10), "�������ʧ��");
            Assert.AreEqual(2459278.8707997804, Util.Round(jqs[23], 10), "��ˮ����ʧ��");
        }

    }


    [TestClass]
    public class CalendarTests
    {
        [TestMethod]
        public void TestDefaultCalendar()
        {
            var calendar = new GregorianCalendar();
            Assert.IsNotNull(calendar, "Ĭ����������ʧ��");
        }

        [TestMethod]
        public void TestSolarTerms()
        {
            var config = new CalendarConfig { TimeZoneName = "Asia/Shanghai" };
            var calendar = new GregorianCalendar();
            var solarTerms = calendar.GetSolarTerms(2021);

            Assert.IsNotNull(solarTerms, "��������ʧ��");
        }

        [TestMethod]
        public void TestGenerateWithDate()
        {
            var config = new CalendarConfig { Grid = GridMode.Week };
            var calendar = new GregorianCalendar();
            var result = calendar.GenerateWithDate(2021, 12, 22);

            Assert.IsNotNull(result, "����ָ�����ڵ�����ʧ��");
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
            Assert.IsNotNull(lunarDate, "����תũ��ʧ��");
        }

        [TestMethod]
        public void TestLunarToGregorian()
        {
            var calendar = new GregorianCalendar();
            var lunarDate = calendar.ConvertToLunar(new DateTime(2020, 4, 14));
            var gregorianDate = calendar.ConvertToGregorian(lunarDate);

            Assert.AreEqual(new DateTime(2020, 4, 14), gregorianDate, "ũ��ת����ʧ��");
        }
    }

    [TestClass]
    public class DateTimeUtilsTests
    {
        [TestMethod]
        public void TestIsLeapYear()
        {
            Assert.IsFalse(CUtil.IsLeapYear(1900), "1900�겻������");
            Assert.IsTrue(CUtil.IsLeapYear(2000), "2000��������");
            Assert.IsTrue(CUtil.IsLeapYear(1940), "1940��������");
            Assert.IsFalse(CUtil.IsLeapYear(2100), "2100�겻������");
        }

        [TestMethod]
        public void TestBeginningOfMonth()
        {
            var date = new DateTime(2021, 5, 15);
            var expected = new DateTime(2021, 5, 1);
            Assert.AreEqual(expected, CUtil.BeginningOfMonth(date), "�³�����ʧ��");
        }

        [TestMethod]
        public void TestEndOfMonth()
        {
            var date = new DateTime(2021, 5, 15);
            var expected = new DateTime(2021, 5, 31);
            Assert.AreEqual(expected, CUtil.EndOfMonth(date), "��ĩ����ʧ��");
        }
    }



    [TestClass]
    public class ProphencyTests
    {
        [TestMethod]
        public void TestHuili()
        {
            Assert.AreEqual("1445-07-01", new JDateTime(2024, 1, 12).ToStringIslamic("yyyy-MM-dd"), "��˹���������");
            Assert.AreEqual("1445-08-01", new JDateTime(2024, 2, 11).ToStringIslamic("yyyy-MM-dd"), "��˹���������");
            Assert.AreEqual("1445-09-01", new JDateTime(2024, 3, 11).ToStringIslamic("yyyy-MM-dd"), "��˹���������");

            Assert.AreEqual("2024-01-12", JDateTime.FromIslamicDate(1445, 7, 1).ToStringGeroge("yyyy-MM-dd"), "��˹���������");
        }
    }




}