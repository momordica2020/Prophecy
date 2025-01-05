using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prophecy
{
    public enum Yao9
    {
        初九, 九二, 九三, 九四, 九五, 上九
    }

    public enum Yao6
    {
        初六, 六二, 六三, 六四, 六五, 上六
    }

    /// <summary>
    /// 先天八卦
    /// </summary>
    public enum Gua8X
    {
        乾, 兑, 离, 震, 巽, 坎, 艮, 坤,
    }

    /// <summary>
    /// 先天八卦对应的事物
    /// </summary>
    public enum Gua8XThing
    {
        天, 泽, 火, 雷, 风, 水, 山, 地,
    }

    /// <summary>
    /// 后天八卦
    /// </summary>
    public enum Gua8H
    {
        坎, 艮, 震, 巽, 离, 坤, 兑, 乾,
    }

    /// <summary>
    /// 先天64卦
    /// </summary>
    public enum Gua64X
    {
        乾, 夬, 大有, 大壮, 小畜, 需, 大畜, 泰,
        履, 兑, 睽, 归妹, 中孚, 节, 损, 临,
        同人, 革, 离, 丰, 家人, 既济, 贲, 明夷,
        无妄, 随, 噬嗑, 震, 益, 屯, 颐, 复,
        姤, 大过, 鼎, 恒, 巽, 井, 蛊, 升,
        讼, 困, 未济, 解, 涣, 坎, 蒙, 师,
        遁, 咸, 旅, 小过, 渐, 蹇, 艮, 谦,
        否, 萃, 晋, 豫, 观, 比, 剥, 坤,
    }

    public enum Wuxing
    {
        水,火,木,金,土
    }


    public enum Tiangan
    {
        甲, 乙, 丙, 丁, 戊, 己, 庚, 辛, 壬, 癸,
    }


    public enum Dizhi
    {
        子, 丑, 寅, 卯, 辰, 巳, 午, 未, 申, 酉, 戌, 亥,
    }


    public enum Shengxiao
    {
        鼠, 牛, 虎, 兔, 龙, 蛇, 马, 羊, 猴, 鸡, 狗, 猪,
    }

    /// <summary>
    /// 十二建
    /// </summary>
    public enum Jian
    {
        建, 除, 满, 平, 定, 执, 破, 危, 成, 收, 开, 闭,
    }

    public enum Bamen
    {
        休, 生, 伤, 杜, 景, 死, 惊, 开,
    }

    /// <summary>
    /// 廿四节气表
    /// </summary>
    public enum JieQi{ 
        冬至,小寒,大寒,立春,雨水,惊蛰, 
        春分,清明,谷雨,立夏,小满,芒种, 
        夏至,小暑,大暑,立秋,处暑,白露, 
        秋分,寒露,霜降,立冬,小雪,大雪,
    };

    /// <summary>
    /// 农历各月的名称
    /// 注意，从十一月建子
    /// </summary>
    public enum LunarMonthName
    {
        十一, 十二, 正, 二, 三, 四, 五, 六, 七, 八, 九, 十
    }

    /// <summary>
    /// 农历各日的名称
    /// </summary>
    public enum LunarDayName
    {
        初一, 初二, 初三, 初四, 初五, 初六, 初七, 初八, 初九, 初十,
        十一, 十二, 十三, 十四, 十五, 十六, 十七, 十八, 十九, 二十,
        廿一, 廿二, 廿三, 廿四, 廿五, 廿六, 廿七, 廿八, 廿九, 三十,
        卅一
    }



    public enum WeekDay
    {
        一, 二, 三, 四, 五, 六, 日,
    }
    public enum Xingzuo
    {
        摩羯, 水瓶, 双鱼, 白羊, 金牛, 双子, 巨蟹, 狮子, 处女, 天秤, 天蝎, 射手,
    }

    /// <summary>
    /// 干支
    /// </summary>
    public class GanZhi
    {
        Tiangan _gan;
        Dizhi _zhi;

        public Tiangan Gan { get { return _gan; } }
        public Dizhi Zhi { get { return _zhi; } }

        public string GanStr { get { return Gan.ToString(); } }
        public string ZhiStr { get { return Zhi.ToString(); } }

        public int Index { get { return ((((int)_gan) - 1) * 12 + ((int)_zhi)); } }


        //public GanZhi()
        //{
        //    _gan = Tiangan.甲;
        //    _zhi = Dizhi.子;
        //}

        public GanZhi(Tiangan Gan, Dizhi Zhi)
        {
            _gan = Gan;
            _zhi = Zhi;
        }
        public GanZhi(double ganIndex, double dizhiIndex)
        {
            try
            {
                if (ganIndex < 0) ganIndex = (Math.Abs(ganIndex / 10) + 1) * 10 + ganIndex;
                ganIndex = ganIndex % 10;
                _gan = (Tiangan)ganIndex;

                if (dizhiIndex < 0) dizhiIndex = (Math.Abs(dizhiIndex / 12) + 1) * 12 + dizhiIndex;
                dizhiIndex = dizhiIndex % 12;
                _zhi = (Dizhi)dizhiIndex;
            }
            catch (Exception ex)
            {
                _gan = Tiangan.甲;
                _zhi = Dizhi.子;
            }

        }

        public GanZhi(double index = 0)
        {
            if (index < 0) index = (Math.Abs((int)(index / 60)) + 1) * 60 + index;
            _gan = (Tiangan)(index % 10);
            _zhi = (Dizhi)(index % 12);
        }


        public void Next(int num = 1)
        {
            if (num < 0) num = (Math.Abs(num / 12) + 1) * 12 + num;
            _gan = (Tiangan)((((int)_gan) + num) % 10);
            _zhi = (Dizhi)((((int)_zhi) + num) % 12);
        }

        public static GanZhi operator +(GanZhi a, GanZhi b)
        {
            return new GanZhi((a.Index + b.Index) % 60);
        }
        public static GanZhi operator -(GanZhi a, GanZhi b)
        {
            return new GanZhi((a.Index - b.Index + 60) % 60);  // 防止负数
        }
        public static bool operator >(GanZhi a, GanZhi b)
        {
            return a.Index > b.Index;
        }

        public static bool operator <(GanZhi a, GanZhi b)
        {
            return a.Index < b.Index;
        }

        public static bool operator >=(GanZhi a, GanZhi b)
        {
            return a.Index >= b.Index;
        }

        public static bool operator <=(GanZhi a, GanZhi b)
        {
            return a.Index <= b.Index;
        }
        public static bool operator ==(GanZhi a, GanZhi b)
        {
            return a.Index == b.Index;
        }

        public static bool operator !=(GanZhi a, GanZhi b)
        {
            return a.Index != b.Index;
        }

        /// <summary>
        /// 自增运算符重载 (前缀)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static GanZhi operator ++(GanZhi a)
        {
            return new GanZhi((a.Index + 1) % 60);
        }

        /// <summary>
        /// 重写Equals方法（用于相等比较）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is GanZhi other)
            {
                return this.Index == other.Index;
            }
            return false;
        }

        public override string ToString()
        {
            return GanStr + ZhiStr;
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }
    }

    public enum ShuoWang
    {
        朔,望, 上弦, 下弦, 无
    }
    /// <summary>
    /// 月相
    /// </summary>
    public class MoonState
    {
        double state;

        public ShuoWang shuowang
        {
            get
            {
                if (Math.Abs(state - 0) < 0.01 || Math.Abs(state - 1) < 0.01) return ShuoWang.朔;
                else if (Math.Abs(state - 0.5) < 0.01) return ShuoWang.望;
                else if (state < 0.5) return ShuoWang.上弦;
                else if (state > 0.5) return ShuoWang.下弦;
                else return ShuoWang.无;
            }
        }

        public MoonState(double state)
        {
            this.state = state;
        }

        public double State { get { return state; } }

        public string ToStringDesc()
        {
            var res = "";
            if (state >= 0 && state < 0.125) res = "新月";
            else if (state >= 0.125 && state < 0.25) res = "蛾眉月";
            else if (state >= 0.25 && state < 0.375) res = "上弦月";
            else if (state >= 0.375 && state < 0.5) res = "盈凸月";
            else if (state >= 0.5 && state < 0.625) res = "满月";
            else if (state >= 0.625 && state < 0.75) res = "亏凸月";
            else if (state >= 0.75 && state < 0.875) res = "下弦月";
            else if (state >= 0.875 && state < 1) res = "残月";
            return res;
        }

        public override string ToString()
        {
            var res = "";
            if (state >= 0 && state < 0.125) res = "🌑";
            else if (state >= 0.125 && state < 0.25) res = "🌒";
            else if (state >= 0.25 && state < 0.375) res = "🌓";
            else if (state >= 0.375 && state < 0.5) res = "🌔";
            else if (state >= 0.5 && state < 0.625) res = "🌕";
            else if (state >= 0.625 && state < 0.75) res = "🌖";
            else if (state >= 0.75 && state < 0.875) res = "🌗";
            else if (state >= 0.875 && state < 1) res = "🌘";
            return res;
        }
    }
}
