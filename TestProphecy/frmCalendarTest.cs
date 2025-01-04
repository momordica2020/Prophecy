using CalendarLibrary.Calendars;
using CalendarLibrary.Configuration;
using CalendarLibrary.Julian;
using Prophecy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestProphecy
{
    public partial class frmCalendarTest : Form
    {
        public frmCalendarTest()
        {
            InitializeComponent();
        }

        private void WriteLine(string str)
        {
            textBox1.AppendText(str + Environment.NewLine);
            //textBox2.Focus();
        }


        void TriggerUpdateTime(bool jd = false)
        {
            textBox1.Clear();
            editable = false;
            JDateTime dt;
            if (jd)
            {
                dt = new JDateTime((double)(nJD.Value));
            }
            else
            {
                dt = new JDateTime((int)nYear.Value, (int)nMonth.Value, (int)nDay.Value, (int)nHour.Value, (int)nMinute.Value, (int)nSecond.Value);
            }


            var r = dt.LunarFourPillars;
            WriteLine($"四柱（农历）：{r.Year} {r.Month} {r.Day} {r.Hour}");
            var r0 = dt.LunarFourPillars0;
            WriteLine($"四柱（节气）：{r0.Year} {r0.Month} {r0.Day} {r0.Hour}");
            WriteLine($"儒略日：{dt.JulianDate}   {dt.JulianDateFrom2000}");
            WriteLine($"公历 {dt.ToString()}");

            WriteLine($"回历 {dt.ToStringIslamic("yyyy年MM月dd日")}");
            WriteLine($"农历{dt.ToStringLunar("yyyy年MM月dd日 h时辰")}");
            WriteLine($"农历{dt.LunarShengxiao.ToString()}年 {(dt.IsLunarLeapMonth ? "闰" : "")}{dt.LunarMonthName}月{(dt.IsLunarBigMonth?"大":"小")} {dt.LunarDayName}日 {dt.Jieqi.ToString()}已过{dt.JieqiBegin}天{(dt.isTodayJieqi?"★":"")}");

            WriteLine(dt.LunarTest());
            if (jd)
            {
                nYear.Value = dt.GerogeYear;
                nMonth.Value = dt.GerogeMonth;
                nDay.Value = dt.GerogeDay;
                nHour.Value = dt.Hour;
                nMinute.Value = dt.Minute;
                nSecond.Value = dt.Second;

            }
            else
            {
                nJD.Value = (decimal)dt.JulianDate;
            }

            

            editable = true;
        }

        private void TriggerEvent()
        {
            string input = textBox2.Text.Trim();
            textBox1.Clear();

            WriteLine($"输入是{input}");
            JDateTime dt = new JDateTime(input);
            var r = dt.LunarFourPillars;
            WriteLine($"四柱：{r.Year} {r.Month} {r.Day} {r.Hour}");

            WriteLine($"儒略日：{dt.JulianDate}   {dt.JulianDateFrom2000}");
            WriteLine($"公历 {dt.ToString()}");

            WriteLine($"回历 {dt.ToStringIslamic("yyyy年MM月dd日")}");
            WriteLine($"农历{dt.ToStringLunar("yyyy年MM月dd日 h时辰")}");
            WriteLine($"农历{dt.LunarShengxiao.ToString()}年{(dt.IsLunarLeapMonth ? "闰" : "")}{dt.LunarMonthName}月{dt.LunarDayName}日");

            WriteLine(dt.LunarTest());

            WriteLine("");
            WriteLine("");
            WriteLine("");
            // 示例 1: 公历转儒略日
            WriteLine("=== 示例 1: 公历转儒略日 ===");
            DateTime date = new DateTime(2021, 12, 6, 12, 0, 0);
            double julianDay = JulianConverter.ToJulianDay(date);
            WriteLine($"公历日期 {date:yyyy-MM-dd HH:mm:ss} 转换为儒略日: {julianDay:F10}\n");

            // 示例 2: 儒略日转公历
            WriteLine("=== 示例 2: 儒略日转公历 ===");
            DateTime convertedDate = JulianConverter.FromJulianDay(julianDay);
            WriteLine($"儒略日 {julianDay:F10} 转换为公历日期: {convertedDate:yyyy-MM-dd HH:mm:ss}\n");

            // 示例 3: 公历转农历
            WriteLine("=== 示例 3: 公历转农历 ===");
            var gregorianCalendar = new GregorianCalendar();
            var lunarDate = gregorianCalendar.ConvertToLunar(date);
            WriteLine($"公历日期 {date:yyyy-MM-dd HH:mm:ss} 转换为农历: {lunarDate}\n");

            // 示例 4: 农历转公历
            WriteLine("=== 示例 4: 农历转公历 ===");
            DateTime gregorianDate = gregorianCalendar.ConvertToGregorian(lunarDate);
            WriteLine($"农历日期 {lunarDate} 转换为公历: {gregorianDate:yyyy-MM-dd HH:mm:ss}\n");

            // 示例 5: 星座计算
            WriteLine("=== 示例 5: 星座计算 ===");
            string starSign = gregorianCalendar.GetStarSign(date);
            WriteLine($"公历日期 {date:yyyy-MM-dd} 的星座是: {starSign}\n");

            // 示例 6: 配置管理
            WriteLine("=== 示例 6: 配置管理 ===");
            var config = new CalendarConfig
            {
                Grid = GridMode.Week,
                FirstWeek = 1,
                TimeZoneName = "UTC",
                EnableSolarTerms = true,
                EnableLunar = true
            };
            WriteLine($"当前配置: 网格模式 = {config.Grid}, 第一列显示星期 = {config.FirstWeek}, 时区 = {config.TimeZoneName}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TriggerEvent();

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TriggerEvent();
            }
        }


        private bool editable = true;
        private void nYear_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nMonth_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nDay_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nHour_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nMinute_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nSecond_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime();
            }
        }

        private void nJD_ValueChanged(object sender, EventArgs e)
        {
            if (editable)
            {
                TriggerUpdateTime(true);
            }
        }

        private void frmCalendarTest_Load(object sender, EventArgs e)
        {
            JDateTime dt = JDateTime.Now;
            editable = false;
            nYear.Value = dt.GerogeYear;
            nMonth.Value = dt.GerogeMonth;
            nDay.Value = dt.GerogeDay;
            nHour.Value = dt.Hour;
            nMinute.Value = dt.Minute;
            nSecond.Value = dt.Second;
            nJD.Value = (decimal)dt.JulianDate;

            editable = true;
            TriggerUpdateTime(true);
        }
    }
}
