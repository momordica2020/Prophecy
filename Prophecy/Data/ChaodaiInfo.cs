using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy.Data
{
    /// <summary>
    /// 朝代
    /// </summary>
    public class ChaodaiItem
    {
        public string Name;
        public int BeginYear;            // 年号起始年
        public int _BeginYearOrigin = 0;           // 年号的计算起始年，例如后朝继承（恢复）前朝年号时，这个数值会比起始年小
        public int BeginMonth;
        public int LengthYear;
        public int EndMonth;
        public List<string> DynastyPre;   // 王朝名前缀
        public string DynastyName;      // 王朝正式名称
        public string EmperorTitle;
        public string EmperorName;

        public bool IsLunar = true;
        

        public ChaodaiItem(
            string name,
            double beginYear,
            double length,
            string dynasty,
            string emperorTitle,
            string emperorName,
            bool isLunar = true,
            int originBegin = 0 // 这个年号的起始值，不为0时，年数会从这个参数指定的年起算
        )
        {
            Name = name;
            BeginYear = (int)beginYear;  // 整数部分是农历年，小数部分是起始的月份/100。1月0.01,12月0.12
            BeginMonth = (int)((beginYear % 1) * 100);
            LengthYear = (int)length; // 整数部分是农历年，小数部分是额外的月份/100。1月0.01,12月0.12
            EndMonth = (int)((length % 1) * 100);
            var Dynasty = dynasty.Split('-',StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            DynastyPre = new List<string>();
            if (dynasty.Length <= 0)
            {
                DynastyName = "";
            }
            else 
            {
                if (Dynasty.Length > 1) DynastyPre = Dynasty.ToList().GetRange(0, Dynasty.Length - 1);
                DynastyName = Dynasty.Last();
            }

            EmperorTitle = emperorTitle;
            EmperorName = emperorName;
            IsLunar = isLunar;
            _BeginYearOrigin = originBegin != 0 ? originBegin : BeginYear;
        }


    }



    public partial class ChaodaiInfo
    {

        public static ChaodaiItem[] getChaodais(JDateTime dt)
        {
            return Items.Where(c =>  (
                c.IsLunar && (
                (dt.LunarYear > c.BeginYear  || dt.LunarYear == c.BeginYear && dt.LunarMonth >= c.BeginMonth)
                &&
                (dt.LunarYear < c.BeginYear + c.LengthYear || dt.LunarYear == c.BeginYear + c.LengthYear && dt.LunarMonth < c.EndMonth))
                || ( !c.IsLunar &&(
                (dt.GerogeYear > c.BeginYear || dt.GerogeYear == c.BeginYear && dt.GerogeMonth >= c.BeginMonth)
                &&
                (dt.GerogeYear < c.BeginYear + c.LengthYear || dt.GerogeYear == c.BeginYear + c.LengthYear && dt.GerogeMonth < c.EndMonth))
                ))   
            ).ToArray();
        }

        public static string getChaodaiDesc(JDateTime jdt)
        {
            string res = "";
            var c = getChaodais(jdt);
            if (c.Length>0)
            {
                foreach(var cc in c)
                {
                    int last = (cc.IsLunar ? jdt.LunarYear : jdt.GerogeYear) - cc._BeginYearOrigin + 1;
                    string lastdesc = last == 1 ? "元" : Util.NumberToHans(last);
                    var yearname = $"{cc.Name}{lastdesc}年";
                    var dname = string.IsNullOrWhiteSpace(cc.DynastyName) ? "" : $"[{cc.DynastyName}]";
                    
                    var ddesc = $"{(cc.DynastyPre.Count > 0 ? string.Join(" ", cc.DynastyPre) + " " + cc.DynastyName:"")},";
                    var edesc = $"{cc.EmperorTitle}{cc.EmperorName}";
                    var desc = "";
                    if (ddesc.Length > 2) desc = ddesc.Trim();
                    desc += edesc;
                    if (desc.Length > 0) desc = $"({desc})";
                    
                                        
                    res += $"{dname.Trim()}{yearname}{desc}\r\n";

                }
            }
            else
            {
                res = $"{Util.NumberToHansOneByOne(jdt.GerogeYear)}年";
            }

            return res.TrimEnd(',');
        }

        //public static int TodayGerogeYear = DateTime.Now.Year;



        public static readonly List<ChaodaiItem> Items = new List<ChaodaiItem>();
        static ChaodaiInfo()
        {
            int pYear = 100; // 将至今依然计算的年号往后续一些年份
            int TodayGerogeYear = DateTime.Now.Year;
            Items.AddRange(ItemCH);
            Items.AddRange(ItemJP);
            Items.AddRange(ItemVN);
            Items.AddRange(ItemKR);
            Items.Add(new("民国", 1912, TodayGerogeYear - 1911 + pYear, "", "", "", false));
            Items.Add(new("建国", 1949, TodayGerogeYear - 1948 + pYear, "中华人民共和国", "", "", false));
            //Items.Add(new("黄帝", -2697, TodayGerogeYear + 2698 + pYear, "中华", "", "", false));
            Items.Add(new("令和", 2019.05, TodayGerogeYear - 2018 + pYear, "日本", "", "德仁天皇"));
            
            //new("上古", int.MinValue, Math.Abs(int.MinValue + 2069), "", "", ""),
        }



    }
}
