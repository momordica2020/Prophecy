using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Xml.Linq;

namespace Prophecy.Astronomy
{
    /// <summary>
    /// 实朔实气计算器
    /// 排月序(生成实际年历)
    /// 时间系统全部使用北京时，即使是天象时刻的输出，也是使用北京时
    /// 如果天象的输出不使用北京时，会造成显示混乱，更严重的是无法与古历比对
    /// </summary>
    public partial class AstronomyOld
    {
        ///// <summary>
        ///// 各月名称
        ///// </summary>
        //public string[] LunarMonthName = new string[14];
        ///// <summary>
        ///// 各月实际序号，有闰月的话，序号同其本月
        ///// </summary>
        //public int[] LunarMonth = new int[14];

        //public string GetLunarMonthName(double index)
        //{
        //    /// <summary>
        //    /// 农历各月的名称, 从 "十一" 月开始, 即从月建 "子" 开始, 与十二地支的顺序对应
        //    /// </summary>
        //    string[] ymc = new string[] { "十一", "十二", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十" }; //月名称,建寅

        //    return ymc[((int)(index)) %  ymc.Length];
        //}

        ///// <summary>
        ///// 中气表,其中.liqiu是节气立秋的儒略日,计算三伏时用到
        ///// 1~25 节气时刻(北京时间),从冬至开始到下一个冬至以后
        ///// 26~27:补全二气
        ///// </summary>
        //public List<double> ZQ;

        ///// <summary>
        ///// 合朔表
        ///// </summary>
        //public double[] HS;

        ///// <summary>
        ///// 各月大小
        ///// </summary>
        //public double[] LunarMonthSize = new double[32];  


        ////public double[] Yn = new double[1];   //年计数

        ///// <summary>
        ///// 闰月位置
        ///// </summary>
        //public int LeapMonth { get; set; }


        #region 修正表、拟合参数
        /// <summary>
        /// 朔修正表
        /// </summary>
        private static string SB = decode(suoS);    // 朔修正表

        /// <summary>
        /// 气修正表
        /// </summary>
        private static string QB = decode(qiS);    // 气修正表

        /// <summary>
        /// 朔直线拟合参数
        /// </summary>
        private static double[] suoKB = new double[]{ //朔直线拟合参数
              1457698.231017,29.53067166, // -721-12-17 h=0.00032 古历·春秋
              1546082.512234,29.53085106, // -479-12-11 h=0.00053 古历·战国
              1640640.735300,29.53060000, // -221-10-31 h=0.01010 古历·秦汉
              1642472.151543,29.53085439, // -216-11-04 h=0.00040 古历·秦汉

              1683430.509300,29.53086148, // -104-12-25 h=0.00313 汉书·律历志(太初历)平气平朔
              1752148.041079,29.53085097, //   85-02-13 h=0.00049 后汉书·律历志(四分历)
              1807665.420323,29.53059851, //  237-02-12 h=0.00033 晋书·律历志(景初历)
              1883618.114100,29.53060000, //  445-01-24 h=0.00030 宋书·律历志(何承天元嘉历)
              1907360.704700,29.53060000, //  510-01-26 h=0.00030 宋书·律历志(祖冲之大明历)
              1936596.224900,29.53060000, //  590-02-10 h=0.01010 随书·律历志(开皇历)
              1939135.675300,29.53060000, //  597-01-24 h=0.00890 随书·律历志(大业历)
              1947168.00//  619-01-21
         };

        /// <summary>
        /// 气直线拟合参数
        /// </summary>
        private static double[] qiKB = new double[]{ //气直线拟合参数
              1640650.479938,15.21842500, // -221-11-09 h=0.01709 古历·秦汉
              1642476.703182,15.21874996, // -216-11-09 h=0.01557 古历·秦汉

              1683430.515601,15.218750011, // -104-12-25 h=0.01560 汉书·律历志(太初历)平气平朔 回归年=365.25000
              1752157.640664,15.218749978, //   85-02-23 h=0.01559 后汉书·律历志(四分历) 回归年=365.25000
              1807675.003759,15.218620279, //  237-02-22 h=0.00010 晋书·律历志(景初历) 回归年=365.24689
              1883627.765182,15.218612292, //  445-02-03 h=0.00026 宋书·律历志(何承天元嘉历) 回归年=365.24670
              1907369.128100,15.218449176, //  510-02-03 h=0.00027 宋书·律历志(祖冲之大明历) 回归年=365.24278
              1936603.140413,15.218425000, //  590-02-17 h=0.00149 随书·律历志(开皇历) 回归年=365.24220
              1939145.524180,15.218466998, //  597-02-03 h=0.00121 随书·律历志(大业历) 回归年=365.24321
              1947180.798300,15.218524844, //  619-02-03 h=0.00052 新唐书·历志(戊寅元历)平气定朔 回归年=365.24460
              1964362.041824,15.218533526, //  666-02-17 h=0.00059 新唐书·历志(麟德历) 回归年=365.24480
              1987372.340971,15.218513908, //  729-02-16 h=0.00096 新唐书·历志(大衍历,至德历) 回归年=365.24433
              1999653.819126,15.218530782, //  762-10-03 h=0.00093 新唐书·历志(五纪历) 回归年=365.24474
              2007445.469786,15.218535181, //  784-02-01 h=0.00059 新唐书·历志(正元历,观象历) 回归年=365.24484
              2021324.917146,15.218526248, //  822-02-01 h=0.00022 新唐书·历志(宣明历) 回归年=365.24463
              2047257.232342,15.218519654, //  893-01-31 h=0.00015 新唐书·历志(崇玄历) 回归年=365.24447
              2070282.898213,15.218425000, //  956-02-16 h=0.00149 旧五代·历志(钦天历) 回归年=365.24220
              2073204.872850,15.218515221, //  964-02-16 h=0.00166 宋史·律历志(应天历) 回归年=365.24437
              2080144.500926,15.218530782, //  983-02-16 h=0.00093 宋史·律历志(乾元历) 回归年=365.24474
              2086703.688963,15.218523776, // 1001-01-31 h=0.00067 宋史·律历志(仪天历,崇天历) 回归年=365.24457
              2110033.182763,15.218425000, // 1064-12-15 h=0.00669 宋史·律历志(明天历) 回归年=365.24220
              2111190.300888,15.218425000, // 1068-02-15 h=0.00149 宋史·律历志(崇天历) 回归年=365.24220
              2113731.271005,15.218515671, // 1075-01-30 h=0.00038 李锐补修(奉元历) 回归年=365.24438
              2120670.840263,15.218425000, // 1094-01-30 h=0.00149 宋史·律历志 回归年=365.24220
              2123973.309063,15.218425000, // 1103-02-14 h=0.00669 李锐补修(占天历) 回归年=365.24220
              2125068.997336,15.218477932, // 1106-02-14 h=0.00056 宋史·律历志(纪元历) 回归年=365.24347
              2136026.312633,15.218472436, // 1136-02-14 h=0.00088 宋史·律历志(统元历,乾道历,淳熙历) 回归年=365.24334
              2156099.495538,15.218425000, // 1191-01-29 h=0.00149 宋史·律历志(会元历) 回归年=365.24220
              2159021.324663,15.218425000, // 1199-01-29 h=0.00149 宋史·律历志(统天历) 回归年=365.24220
              2162308.575254,15.218461742, // 1208-01-30 h=0.00146 宋史·律历志(开禧历) 回归年=365.24308
              2178485.706538,15.218425000, // 1252-05-15 h=0.04606 淳祐历 回归年=365.24220
              2178759.662849,15.218445786, // 1253-02-13 h=0.00231 会天历 回归年=365.24270
              2185334.020800,15.218425000, // 1271-02-13 h=0.00520 宋史·律历志(成天历) 回归年=365.24220
              2187525.481425,15.218425000, // 1277-02-12 h=0.00520 本天历 回归年=365.24220
              2188621.191481,15.218437484, // 1280-02-13 h=0.00013 元史·历志(郭守敬授时历) 回归年=365.24250
              2321919.49// 1645-02-04
         };


        /// <summary>
        ///  619-01-21开始16598个朔日修正表 d0=1947168
        /// </summary>
        const string suoS =
            "EqoFscDcrFpmEsF2DfFideFelFpFfFfFiaipqti1ksttikptikqckstekqttgkqttgkqteksttikptikq2fjstgjqttjkqttgkqt" +
            "ekstfkptikq2tijstgjiFkirFsAeACoFsiDaDiADc1AFbBfgdfikijFifegF1FhaikgFag1E2btaieeibggiffdeigFfqDfaiBkF" +
            "1kEaikhkigeidhhdiegcFfakF1ggkidbiaedksaFffckekidhhdhdikcikiakicjF1deedFhFccgicdekgiFbiaikcfi1kbFibef" +
            "gEgFdcFkFeFkdcfkF1kfkcickEiFkDacFiEfbiaejcFfffkhkdgkaiei1ehigikhdFikfckF1dhhdikcfgjikhfjicjicgiehdik" +
            "cikggcifgiejF1jkieFhegikggcikFegiegkfjebhigikggcikdgkaFkijcfkcikfkcifikiggkaeeigefkcdfcfkhkdgkegieid" +
            "hijcFfakhfgeidieidiegikhfkfckfcjbdehdikggikgkfkicjicjF1dbidikFiggcifgiejkiegkigcdiegfggcikdbgfgefjF1" +
            "kfegikggcikdgFkeeijcfkcikfkekcikdgkabhkFikaffcfkhkdgkegbiaekfkiakicjhfgqdq2fkiakgkfkhfkfcjiekgFebicg" +
            "gbedF1jikejbbbiakgbgkacgiejkijjgigfiakggfggcibFifjefjF1kfekdgjcibFeFkijcfkfhkfkeaieigekgbhkfikidfcje" +
            "aibgekgdkiffiffkiakF1jhbakgdki1dj1ikfkicjicjieeFkgdkicggkighdF1jfgkgfgbdkicggfggkidFkiekgijkeigfiski" +
            "ggfaidheigF1jekijcikickiggkidhhdbgcfkFikikhkigeidieFikggikhkffaffijhidhhakgdkhkijF1kiakF1kfheakgdkif" +
            "iggkigicjiejkieedikgdfcggkigieeiejfgkgkigbgikicggkiaideeijkefjeijikhkiggkiaidheigcikaikffikijgkiahi1" +
            "hhdikgjfifaakekighie1hiaikggikhkffakicjhiahaikggikhkijF1kfejfeFhidikggiffiggkigicjiekgieeigikggiffig" +
            "gkidheigkgfjkeigiegikifiggkidhedeijcfkFikikhkiggkidhh1ehigcikaffkhkiggkidhh1hhigikekfiFkFikcidhh1hit" +
            "cikggikhkfkicjicghiediaikggikhkijbjfejfeFhaikggifikiggkigiejkikgkgieeigikggiffiggkigieeigekijcijikgg" +
            "ifikiggkideedeijkefkfckikhkiggkidhh1ehijcikaffkhkiggkidhh1hhigikhkikFikfckcidhh1hiaikgjikhfjicjicgie" +
            "hdikcikggifikigiejfejkieFhegikggifikiggfghigkfjeijkhigikggifikiggkigieeijcijcikfksikifikiggkidehdeij" +
            "cfdckikhkiggkhghh1ehijikifffffkhsFngErD1pAfBoDd1BlEtFqA2AqoEpDqElAEsEeB2BmADlDkqBtC1FnEpDqnEmFsFsAFn" +
            "llBbFmDsDiCtDmAB2BmtCgpEplCpAEiBiEoFqFtEqsDcCnFtADnFlEgdkEgmEtEsCtDmADqFtAFrAtEcCqAE1BoFqC1F1DrFtBmF" +
            "tAC2ACnFaoCgADcADcCcFfoFtDlAFgmFqBq2bpEoAEmkqnEeCtAE1bAEqgDfFfCrgEcBrACfAAABqAAB1AAClEnFeCtCgAADqDoB" +
            "mtAAACbFiAAADsEtBqAB2FsDqpFqEmFsCeDtFlCeDtoEpClEqAAFrAFoCgFmFsFqEnAEcCqFeCtFtEnAEeFtAAEkFnErAABbFkAD" +
            "nAAeCtFeAfBoAEpFtAABtFqAApDcCGJ";

        /// <summary>
        /// 1645-02-03开始7582个节气修正表
        /// </summary>
        const string qiS =
            "EmcFs22AFsckF2tsDtFqEtF1posFdFgiFseFtmelpsEfhkF2anmelpFlF1ikrotcnEqEq2FfqmcDsrFor22FgFrcgDscFs22FgEe" +
            "FtE2sfFs22sCoEsaF2tsD1FpeE2eFsssEciFsFnmelpFcFhkF2tcnEqEpFgkrotcnEqrEtFermcDsrE222FgBmcmr22DaEfnaF22" +
            "2sD1FpeForeF2tssEfiFpEoeFssD1iFstEqFppDgFstcnEqEpFg11FscnEqrAoAF2ClAEsDmDtCtBaDlAFbAEpAAAAAD2FgBiBqo" +
            "BbnBaBoAAAAAAAEgDqAdBqAFrBaBoACdAAf1AACgAAAeBbCamDgEifAE2AABa1C1BgFdiAAACoCeE1ADiEifDaAEqAAFe1AcFbcA" +
            "AAAAF1iFaAAACpACmFmAAAAAAAACrDaAAADG0";

        /// <summary>
        /// 低精度定朔计算,在2000年至600，误差在2小时以内(仍比古代日历精准很多)
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        private static double so_low(double W)
        {
            double v = 7771.37714500204;
            double t = (W + 1.08472) / v;
            t -= (-0.0000331 * t * t
              + 0.10976 * Math.Cos(0.785 + 8328.6914 * t)
              + 0.02224 * Math.Cos(0.187 + 7214.0629 * t)
              - 0.03342 * Math.Cos(4.669 + 628.3076 * t)) / v
              + (32 * (t + 1.8) * (t + 1.8) - 20) / 86400 / 36525;
            return t * 36525 + 8d / 24d;
        }


        /// <summary>
        /// 低精度定气计算, 最大误差小于30分钟，平均5分
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        private static double qi_low(double W)
        {
            double t, L, v = 628.3319653318;
            t = (W - 4.895062166) / v; //第一次估算,误差2天以内
            t -= (53 * t * t + 334116 * Math.Cos(4.67 + 628.307585 * t) + 2061 * Math.Cos(2.678 + 628.3076 * t) * t) / v / 10000000; //第二次估算,误差2小时以内

            L = 48950621.66 + 6283319653.318 * t + 53 * t * t    //平黄经
                + 334166 * Math.Cos(4.669257 + 628.307585 * t)   //地球椭圆轨道级数展开
                + 3489 * Math.Cos(4.6261 + 1256.61517 * t)       //地球椭圆轨道级数展开
                + 2060.6 * Math.Cos(2.67823 + 628.307585 * t) * t  //一次泊松项
                - 994 - 834 * Math.Sin(2.1824 - 33.75705 * t);     //光行差与章动修正

            t -= (L / 10000000 - W) / 628.332 + (32 * (t + 1.8) * (t + 1.8) - 20) / 86400 / 36525;
            return t * 36525 + 8d / 24d;
        }


        /// <summary>
        /// 较高精度定气计算 
        /// </summary>
        /// <param name="W">从2000年起算的太阳视黄经</param>
        /// <returns>从2000年起算的儒略时</returns>
        private static double qi_high(double W)
        {
            // 根据太阳视黄经（W）反推出时间，转化为从2000年起算的儒略日
            double t = S_aLon_t2(W) * 36525;
            // 减去 deltatT2(t)，将 t 从地球时 (TD) 转换为世界时 (UT) 并转换到北京UTC+8 时区
            t = t - deltatT2(t) + 8d / 24d;
            // 计算时间 t 的小数部分（0.5即当天的中午12时）并转换为秒
            double v = (t + 0.5) % 1 * 86400;
            //如果时间过于接近 0 点或 24 点（误差范围为 600 秒内），重新计算 t，使用更精确的 Ephemeris.S_aLon_t(W)
            // + 8d / 24d是转换到北京UTC+8 时区
            if (v < 600 || v > 86400 - 600) t = S_aLon_t(W) * 36525 - deltatT2(t) + 8d / 24d;
            return t;
        }


        /// <summary>
        /// 较高精度朔计算
        /// </summary>
        /// <param name="W"></param>
        /// <returns></returns>
        private static double so_high(double W)
        {
            double t = MS_aLon_t2(W) * 36525;
            t = t - deltatT2(t) + 8d / 24d;
            double v = (t + 0.5) % 1 * 86400;
            if (v < 600 || v > 86400 - 600) t = MS_aLon_t(W) * 36525 - deltatT2(t) + 8d / 24d;
            return t;
        }



        /// <summary>
        /// 气朔解压缩
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string decode(string s)
        {
            string o = "0000000000", o2 = o + o;
            StringBuilder sb = new StringBuilder(s, 32768);
            sb.Replace("J", "00");
            sb.Replace("I", "000");
            sb.Replace("H", "0000");
            sb.Replace("G", "00000");
            sb.Replace("t", "02");
            sb.Replace("s", "002");
            sb.Replace("r", "0002");
            sb.Replace("q", "00002");
            sb.Replace("p", "000002");
            sb.Replace("o", "0000002");
            sb.Replace("n", "00000002");
            sb.Replace("m", "000000002");
            sb.Replace("l", "0000000002");
            sb.Replace("k", "01");
            sb.Replace("j", "0101");
            sb.Replace("i", "001");
            sb.Replace("h", "001001");
            sb.Replace("g", "0001");
            sb.Replace("f", "00001");
            sb.Replace("e", "000001");
            sb.Replace("d", "0000001");
            sb.Replace("c", "00000001");
            sb.Replace("b", "000000001");
            sb.Replace("a", "0000000001");
            sb.Replace("A", o2 + o2 + o2);
            sb.Replace("B", o2 + o2 + o);
            sb.Replace("C", o2 + o2);
            sb.Replace("D", o2 + o);
            sb.Replace("E", o2);
            sb.Replace("F", o);
            return sb.ToString();
        }



        #endregion



        /// <summary>
        /// 节气的儒略日计算 jd 应靠近所要取得的气朔日, 
        /// </summary>
        /// <param name="jd2000">j2000儒略日</param>
        /// <returns>朔日的儒略日数值</returns>
        public static double calQi(double jd2000)
        {
            var jd = jd2000 + JDateTime.JD_2000;
            double resultJD = 0;
            double pc = 7;  // 偏差?
            double f1 = qiKB[0] - pc;
            double f2 = qiKB[qiKB.Length - 1] - pc;
            double f3 = 2436935;    // 1960.1.1 12:00

            if (jd < f1 || jd >= f3)
            {
                // 平气朔表中首个之前，使用现代天文算法。
                // 1960.1.1以后，使用现代天文算法 (这一部分调用了qi_high和so_high,所以需星历表支持)
                // 2451259是1999.3.21,太阳视黄经为0,春分.定气计算
                resultJD = qi_high(Math.Floor((jd + pc - 2451259) / 365.2422 * 24) * Math.PI / 12) + 0.5;

            }
            else if (jd >= f1 && jd < f2)
            {
                // 平气或平朔
                int i = 0;
                for (i = 0; i < qiKB.Length; i += 2)
                {
                    if (jd + pc < qiKB[i + 2]) break;
                }

                resultJD = qiKB[i] + qiKB[i + 1] * Math.Floor((jd + pc - qiKB[i]) / qiKB[i + 1]);
                resultJD = resultJD + 0.5;


                //如果使用太初历计算-103年1月24日的朔日,结果得到的是23日,这里修正为24日(实历)。
                //修正后仍不影响-103的无中置闰。
                //如果使用秦汉历，得到的是24日，本行D不会被执行。
                if (Math.Floor(resultJD)== 1683460) resultJD++;
            }
            else if (jd >= f2 && jd < f3)
            {
                resultJD = qi_low(Math.Floor((jd + pc - 2451259) / 365.2422 * 24) * Math.PI / 12) + 0.5;
                //2451259是1999.3.21,太阳视黄经为0,春分.定气计算
                var n = QB.Substring((int)Math.Floor((jd - f2) / 365.2422 * 24), 1);
                //找定气修正值

                if (n == "1") resultJD += 1;
                else if (n == "2") resultJD -= 1;
            }

            return Math.Floor(resultJD);// Util.Round(resultJD, 10);
        }
        /// <summary>
        /// 计算儒略日最近的朔日
        /// </summary>
        /// <param name="jd2000">完整儒略日</param>
        /// <returns>朔日的完整儒略日数值</returns>
        public static double calShuo(double jd2000)
        {
            var jd = jd2000 + JDateTime.JD_2000;
            double resultJD = 0;
            double pc = 14;
            double f1 = suoKB[0] - pc;
            double f2 = suoKB[suoKB.Length - 1] - pc;
            double f3 = 2436935;


            if (jd < f1 || jd >= f3)
            {
                // 平气朔表中首个之前，使用现代天文算法。
                // 1960.1.1以后，使用现代天文算法 (这一部分调用了qi_high和so_high,所以需星历表支持)
                //2451551是2000.1.7的那个朔日,黄经差为0.定朔计算
                resultJD = so_high(Math.Floor((jd + pc - 2451551) / 29.5306) * Math.PI * 2) +0.5;
            }
            else if (jd >= f1 && jd < f2)
            {
                // 平朔
                int i;
                for (i = 0; i < suoKB.Length; i += 2)
                {
                    if (jd + pc < suoKB[i + 2]) break;
                }
                resultJD = suoKB[i] + suoKB[i + 1] * Math.Floor((jd + pc - suoKB[i]) / suoKB[i + 1]);
                resultJD = resultJD + 0.5;


                //如果使用太初历计算-103年1月24日的朔日,结果得到的是23日,这里修正为24日(实历)。
                //修正后仍不影响-103的无中置闰。
                //如果使用秦汉历，得到的是24日，跳过本行。
                if (Math.Floor(resultJD) == 1683460) resultJD++;
            }
            else if (jd >= f2 && jd < f3)
            {
                //2451551是2000.1.7的那个朔日,黄经差为0.定朔计算
                resultJD = so_low(Math.Floor((jd + pc - 2451551) / 29.5306) * Math.PI * 2) + 0.5;

                var n = SB.Substring((int)Math.Floor((jd - f2) / 29.5306), 1);
                //找定朔修正值

                if (n == "1") resultJD += 1;
                else if (n == "2") resultJD -= 1;
            }

            return Math.Floor(resultJD);
        }












        /// <summary>
        /// 计算从霜降至下 2 个大寒之后的总共31个节气时刻(北京时间),从霜降至开始到下 2 个大寒以后,
        ///  用于计算指定日期的所属节气, 上一节气, 下一节气等信息
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static List<double> calcJieQiShuangjiang(double jd)
        {
            var JQ = new List<double>();
            var jd2000 = jd - JDateTime.JD_2000;
            // 需计算从霜降至下 2 个大寒之后的节气, 用于计算指定日期的所属节气, 上一节气, 下一节气等信息
            // 296是2000.10.23霜降(距2000.1.1的天数),得到较靠近jd的霜降估计值
            var W = Math.Floor((jd2000 - 355 + 183) / 365.2422) * 365.2422 + 296 + JDateTime.JD_2000;
            if (calQi(W) > jd) W -= 365.2422;

            // 31个节气时刻(北京时间),从霜降至开始到下 2 个大寒以后
            for (int i = 0; i < 31; i++) JQ.Add(calQi(W + 15.2184 * i));


            return JQ;

        }


        /// <summary>
        /// 计算一年25个节气时刻(北京时间),从冬至开始到下一个冬至以后
        /// </summary>
        /// <param name="jd">完整儒略日</param>
        /// <returns>节气时刻的完整儒略日列表</returns>
        public static List<double> calcJieQiDongzhi(double jd2000)
        {
            var JQ = new List<double>();
            // 只计算某年的节气
            // 355是2000.12冬至,得到较靠近jd的冬至估计值
            var Winter = Math.Floor((jd2000 - 355) / 365.2422) * 365.2422 + 355;
            if (calQi(Winter) > jd2000) Winter -= 365.2422;


            // 25个节气时刻(北京时间),从冬至开始到下一个冬至以后
            for (int i = 0; i < 25; i++) JQ.Add(calQi(Winter + 15.2184 * i));
            // 补算二气：上一年的小雪、大雪。确保一年中所有月份的“气”全部被计算在内
            JQ.Add(calQi(Winter - 15.2));
            JQ.Add(calQi(Winter - 30.4));

            return JQ;
        }



        //public static double Y = 2000;        // 年
        //public static double M = 1;           // 月
        //public static double D = 1;           // 日
        //public static double h = 12;          // 时
        //public static double m = 0;           // 分
        //public static double s = 0;           // 秒




        /// <summary>
        /// TD - UT1 计算表
        /// </summary>
        private static double[] dts = new double[] {
            -4000,108371.7,-13036.80,392.000, 0.0000, -500, 17201.0,  -627.82, 16.170,-0.3413,
            -150, 12200.6,  -346.41,  5.403,-0.1593,  150,  9113.8,  -328.13, -1.647, 0.0377,
             500,  5707.5,  -391.41,  0.915, 0.3145,  900,  2203.4,  -283.45, 13.034,-0.1778,
            1300,   490.1,   -57.35,  2.085,-0.0072, 1600,   120.0,    -9.81, -1.532, 0.1403,
            1700,    10.2,    -0.91,  0.510,-0.0370, 1800,    13.4,    -0.72,  0.202,-0.0193,
            1830,     7.8,    -1.81,  0.416,-0.0247, 1860,     8.3,    -0.13, -0.406, 0.0292,
            1880,    -5.4,     0.32, -0.183, 0.0173, 1900,    -2.3,     2.06,  0.169,-0.0135,
            1920,    21.2,     1.69, -0.304, 0.0167, 1940,    24.2,     1.22, -0.064, 0.0031,
            1960,    33.2,     0.51,  0.231,-0.0109, 1980,    51.0,     1.29, -0.026, 0.0032,
            2000,    63.87,    0.1,   0,     0,      2005
        };




        /// <summary>
        /// 二次曲线外推
        /// </summary>
        /// <param name="y"></param>
        /// <param name="jsd"></param>
        /// <returns></returns>
        public static double deltatExt(double y, double jsd)
        {
            double dy = (y - 1820) / 100;
            return -20 + jsd * dy * dy;
        }



        /// <summary>
        /// 计算世界时与原子时之差,传入年
        /// 该函数的目的是计算世界时（UT）与原子时（TAI）之间的差值，通常用于天文计算。
        /// 具体来说，它计算了基于年份的 UTC 和 TAI 之间的时间差（单位：秒）。
        /// 此函数根据不同的年份范围采取不同的计算方式：
        ///对于 2005 年及以后的年份，使用外推公式。函数会根据 2005 年之后的速度和加速度来估计。
        ///对于 2005 年之前的年份，使用已有的时间差数据（例如来自 dts 数组）进行插值计算。
        /// </summary>
        /// <param name="y">年数</param>
        /// <returns>世界时（UT）与原子时（TAI）之间的差值（单位：秒）</returns>
        static double deltatT(double y)
        {
            if (y >= 2005)
            {
                //sd 是2005年之后几年（一值到y1年）的速度估计。
                //jsd 是y1年之后的加速度估计。瑞士星历表jsd=31,NASA网站jsd=32,skmap的jsd=29
                double y1 = 2014, sd = 0.4, jsd = 31;
                if (y <= y1)
                    return 64.7 + (y - 2005) * sd;    //直线外推
                double v = deltatExt(y, jsd);        //二次曲线外推
                double dv = deltatExt(y1, jsd) - (64.7 + (y1 - 2005) * sd); //y1年的二次外推与直线外推的差
                if (y < y1 + 100)
                    v -= dv * (y1 + 100 - y) / 100;
                return v;
            }
            int i;
            double[] d = dts;

            for (i = 0; i < d.Length; i += 5)
                if (y < d[i + 5])
                    break;
            double t1 = (y - d[i]) / (d[i + 5] - d[i]) * 10, t2 = t1 * t1, t3 = t2 * t1;
            return d[i + 1] + d[i + 2] * t1 + d[i + 3] * t2 + d[i + 4] * t3;
        }



        /// <summary>
        /// 传入儒略日(J2000起算),计算地球时 (TD) 和世界时 (UT) 的时间差（ΔT=TD-UT） (单位:日)
        /// </summary>
        /// <param name="jd2000">J2000起算的儒略日</param>
        /// <returns>ΔT (单位:日)</returns>
        public static double deltatT2(double jd2000)
        {
            return deltatT(jd2000 / 365.2425 + 2000) / 86400.0;
        }


        /// <summary>
        /// 星期计算
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static double GetWeekFromJD(double jd)
        {
            return Math.Floor(jd + 1.5) % 7;
        }


        /// <summary>
        /// 求y年m月的第n个星期w的儒略日数
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="n"></param>
        /// <param name="w">星期w</param>
        /// <returns></returns>
        public static double GetJDFromYMNW(double y, double m, double n, double w)
        {
            // 获取该月1号的 DateTime 对象
            JDateTime firstDayOfMonth = new JDateTime((int)y, (int)m, 1);
            double jd = firstDayOfMonth.JulianDate; // 月首儒略日数
            double w0 = (jd + 1) % 7;  // 月首的星期几
            double r = jd - w0 + 7 * n + w;  // 计算第n个星期w的儒略日数

            if (w >= w0) r -= 7;  // 第1个星期w可能落在上个月，造成多算1周，减去1周
            if (n == 5)
            {
                m++;
                if (m > 12) { m = 1; y++; }  // 下个月
                JDateTime nextMonthFirstDay = new JDateTime((int)y, (int)m, 1);
                if (r >= nextMonthFirstDay.JulianDate) r -= 7; // 如果r超出本月范围，减去1周
            }

            return r;
        }

        ///// <summary>
        ///// 公历转儒略日
        ///// </summary>
        ///// <param name="y">年</param>
        ///// <param name="m">月</param>
        ///// <param name="d">日</param>
        ///// <returns></returns>
        //public static double GD2JD(double y, double m, double d)
        //{
        //    double n = 0, G = 0;
        //    if (y * 372 + m * 31 + Math.Floor(d) >= 588829) 
        //        G = 1;   //判断是否为格里高利历日1582*372+10*31+15
        //    if (m <= 2) { m += 12; y--; }
        //    if (G != 0)
        //    {
        //        n = Math.Floor(y / 100); 
        //        n = 2 - n + Math.Floor(n / 4);
        //    }    //加百年闰
        //    return Math.Floor(365.25 * (y + 4716) + 0.01) + Math.Floor(30.6001 * (m + 1)) + d + n - 1524.5;
        //}



        ///// <summary>
        ///// 公历转儒略日
        ///// </summary>
        ///// <returns></returns>
        //public static double ToJD()
        //{
        //    return GD2JD(Y, M, D + ((s / 60 + m) / 60 + h) / 24);
        //}



        /////// <summary>
        /////// 儒略日数转公历
        /////// </summary>
        /////// <param name="jd">儒略日</param>
        ////public static void InitFromJD(double jd)
        ////{




        ////}



        /////// <summary>
        /////// 日期转为串
        /////// </summary>
        /////// <returns></returns>
        ////public static string toStr()
        ////{ 
        ////    string Y = "     " + Y, M = "0" + M, D = "0" + D;
        ////    double h = h, m = m, s = Math.Floor(s + .5);
        ////    if (s >= 60) { s -= 60; m++; }
        ////    if (m >= 60) { m -= 60; h++; }
        ////    string hStr = "0" + h, mStr = "0" + m, sStr = "0" + s;
        ////    Y = Y.Substring(Y.Length - 5, 5); 
        ////    M = M.Substring(M.Length - 2, 2); 
        ////    D = D.Substring(D.Length - 2, 2);
        ////    hStr = hStr.Substring(hStr.Length - 2, 2); 
        ////    mStr = mStr.Substring(mStr.Length - 2, 2); 
        ////    sStr = sStr.Substring(sStr.Length - 2, 2);
        ////    return Y + "-" + M + "-" + D + " " + hStr + ":" + mStr + ":" + sStr;
        ////}


        ///// <summary>
        ///// 儒略日数转公历, 并且返回对应的 DateTime 对象
        ///// </summary>
        ///// <param name="jd">儒略日数</param>
        ///// <returns>返回对应的 DateTime 对象</returns>
        //public static DateTime JD2Date(double jd)
        //{
        //    double Y = 2000;        // 年
        //    double M = 1;           // 月
        //    double D = 1;           // 日
        //    double h = 12;          // 时
        //    double m = 0;           // 分
        //    double s = 0;           // 秒
        //    jd += 0.5; // 对JD加0.5，确保我们从午夜12点开始计算
        //    double A = Math.Floor(jd), F = jd - A;  // 取得日数的整数部分A及小数部分F

        //    // 如果JD >= 2299161，说明是格里高利历日期
        //    if (A >= 2299161)
        //    {
        //        double D = Math.Floor((A - 1867216.25) / 36524.25); // 计算闰年修正
        //        A += 1 + D - Math.Floor(D / 4); // 修正JD，得到实际日期
        //    }
        //    A += 1524; // 将JD向前移动4年2个月
        //    Y = Math.Floor((A - 122.1) / 365.25); // 计算年份
        //    D = A - Math.Floor(365.25 * Y); // 获取剩余的日数
        //    M = Math.Floor(D / 30.6001); // 计算月份
        //    D = D - Math.Floor(M * 30.6001); // 计算日期

        //    Y -= 4716; M--; // 调整年份和月份
        //    if (M > 12) M -= 12; // 如果月份大于12，说明是新的一年
        //    if (M <= 2) Y++; // 调整年份

        //    // 小数部分转为时分秒
        //    F *= 24; h = Math.Floor(F); F -= h;  // 计算小时
        //    F *= 60; m = Math.Floor(F); F -= m;  // 计算分钟
        //    F *= 60; s = Math.Floor(F + 0.5);    // 计算秒，四舍五入

        //    // 处理小时、分钟、秒的溢出
        //    if (s >= 60) { s -= 60; m++; }
        //    if (m >= 60) { m -= 60; h++; }

        //    // 返回 DateTime 对象
        //    return new DateTime((int)Y, (int)M, (int)D, (int)h, (int)m, (int)s);
        //}


        ///// <summary>
        ///// 提取jd中的时间(去除日期)
        ///// </summary>
        ///// <param name="jd"></param>
        ///// <returns></returns>
        //public static string GetHHMMSSFromJD(double jd)
        //{ 
        //    double h, m, s;
        //    jd += 0.5; jd = (jd - Math.Floor(jd));
        //    jd *= 24; h = Math.Floor(jd); jd -= h;
        //    jd *= 60; m = Math.Floor(jd); jd -= m;
        //    jd *= 60; s = Math.Floor(jd + 0.5);
        //    if (s >= 60) { s -= 60; m++; }
        //    if (m >= 60) { m -= 60; h++; }
        //    string hStr = "0" + h, mStr = "0" + m, sStr = "0" + s;
        //    return hStr.Substring(hStr.Length - 2, 2) + ':' + mStr.Substring(mStr.Length - 2, 2) + ':' + sStr.Substring(sStr.Length - 2, 2);
        //}













        /// <summary>
        /// 章动(黄经章动和交角章动)计算,t是世纪数
        /// </summary>
        /// <param name="t">世纪数</param>
        /// <returns>黄经章动 (dL)：黄经方向上的变化。交角章动(dE)：黄道与赤道交角（即黄赤交角）的变化。</returns>
        public static (double dL, double dE) Nutation(double t)
        {
            double L = 0;
            double E = 0;

            /// <summary>
            /// 章动计算时使用的数据
            /// 每 5 个元素表示一个谐调项的参数：
            /// B[i]：初始相位（单位为弧度）。
            /// B[i + 1]：频率（单位为弧度 / 世纪）。
            /// B[i + 2]：频率的长期变化率（二次项修正）。
            /// B[i + 3]：黄经章动的系数（单位为毫角秒）。
            /// B[i + 4]：交角章动的系数（单位为毫角秒）。
            /// </summary>
            double[] B = new double[] {
                2.1824,  -33.75705, 36e-6,-1720,920,
                3.5069, 1256.66393, 11e-6,-132, 57,
                1.3375,16799.4182, -51e-6, -23, 10,
                4.3649,  -67.5141,  72e-6,  21, -9,
                0.04,   -628.302,   0,     -14,  0,
                2.36,   8328.691,   0,       7,  0,
                3.46,   1884.966,   0,      -5,  2,
                5.44,  16833.175,   0,      -4,  2,
                3.69,  25128.110,   0,      -3,  0,
                3.55,    628.362,   0,       2,  0
            };

            for (int i = 0; i < B.Length; i += 5)
            {
                var c = B[i] + B[i + 1] * t + B[i + 2] * Math.Pow(t, 2);
                var a = 0d;
                if (i == 0) a = -1.742 * t;
                L += (B[i + 3] + a) * Math.Sin(c);
                E += B[i + 4] * Math.Cos(c);
            }
            L = L / 100 / Util.rad;
            E = E / 100 / Util.rad;
            return (L, E);
        }








        /// <summary>
        /// 球面坐标旋转  黄道赤道坐标变换
        /// E>0：实现从黄道坐标系到赤道坐标系的变换
        /// E<0：实现从赤道坐标系到黄道坐标系的变换
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="E"></param>
        public static Coordinate llrConv(Coordinate coordinate, double E)
        {
            Coordinate newC = new Coordinate(coordinate);
            //黄道赤道坐标变换,赤到黄E取负
            var sinE = Math.Sin(E);
            var cosE = Math.Cos(E);
            var sinJ = Math.Sin(coordinate.J);
            var cosJ = Math.Cos(coordinate.J);
            var sinW = Math.Sin(coordinate.W);
            var cosW = Math.Cos(coordinate.W);
            var tanW = Math.Tan(coordinate.W);
            var j = Math.Atan2(sinJ * cosE - tanW * sinE, cosJ);
            j = Util.rad2mrad(j);
            var w = Math.Asin(cosE * sinW + sinE * cosW * sinJ);

            newC.J = j;
            newC.W = w;
            return newC;
            //return (j, w);
        }


        ///// <summary>
        ///// 球面转直角坐标
        ///// </summary>
        ///// <param name="J"></param>
        ///// <param name="W"></param>
        ///// <param name="R"></param>
        ///// <param name="z"></param>
        //public static (double x, double y, double z) llr2xyz(double J, double W, double R)
        //{
        //    return (
        //        R * Math.Cos(W) * Math.Cos(J),
        //        R * Math.Cos(W) * Math.Sin(J),
        //        R * Math.Sin(W)
        //        );
        //}



        ///// <summary>
        ///// 直角坐标转球
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="z"></param>
        ///// <param name="q"></param>
        //public static (double j, double w, double r) xyz2llr(double x, double y, double z)
        //{
        //    var R = Math.Sqrt(x * x + y * y + z * z);
        //    var W = Math.Asin(z / R);
        //    var J = Util.rad2mrad(Math.Atan2(y, x));
        //    return ( J,W,R );
        //}







        /// <summary>
        /// 返回黄赤交角,t是世纪数
        /// </summary>
        /// <param name="t">世纪数</param>
        /// <returns></returns>
        public static double ObliquityOfEcliptic(double t)
        {
            return
                84381.4088
                - 46.836051 * t
                - 0.0001667 * Math.Pow(t, 2)
                - 0.00199911 * Math.Pow(t, 3)
                - 0.000000523 * Math.Pow(t, 4)
                / Util.rad
            ;
        }



        /// <summary>
        /// 求角度差(未测试)
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static double j1_j2(Coordinate c1, Coordinate c2)
        {
            double dJ = Util.rad2rrad(c1.J - c2.J);
            double dW = c1.W - c2.W;
            if (Math.Abs(dJ) < 1 / 1000 && Math.Abs(dW) < 1 / 1000)
            {
                dJ *= Math.Cos((c1.W + c2.W) / 2);
                return Math.Sqrt(dJ * dJ + dW * dW);
            }
            else
            {
                return Math.Acos(Math.Sin(c1.W) * Math.Sin(c2.W) + Math.Cos(c1.W) * Math.Cos(c2.W) * Math.Cos(dJ));
            }
        }


        /// <summary>
        /// 传入T是2000年首起算的日数(UT),dt是deltatT(日),精度要求不高时dt可取值为0
        /// 返回格林尼治恒星时(不含赤经章动及非多项式部分),即格林尼治子午圈的平春风点起算的赤经
        /// </summary>
        /// <param name="UT">2000年首起算的日数(UT)</param>
        /// <param name="dt">deltatT(日)</param>
        /// <returns></returns>
        public static double gst(double UT, double dt)
        {
            double t = (UT + dt) / 36525;
            return Math.PI * 2 * (0.7790572732640 + 1.00273781191135448 * UT) //严格说这里的T是UT,下一行的t是力学时(世纪数)
                + (0.014506 + 4612.15739966 * t + 1.39667721 * Math.Pow(t, 2) - 0.00009344 * Math.Pow(t, 3) + 0.00001882 * Math.Pow(t, 4)) / Util.rad;
        }


        /// <summary>
        /// 太阳黄经光行差,t是世纪数
        /// </summary>
        /// <param name="t">世纪数</param>
        /// <returns>黄经光行差</returns>
        public static double gxc_sunLon(double t)
        {
            double v = -0.043126 + 628.301955 * t - 0.000002732 * Math.Pow(t, 2); //平近点角
            double e = 0.016708634 - 0.000042037 * t - 0.0000001267 * Math.Pow(t, 2);
            return -20.49552 * (1 + e * Math.Cos(v)) / Util.rad; //黄经光行差
        }


        /// <summary>
        /// 黄纬光行差
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static double gxc_sunLat(double t)
        {
            return 0;
        }


        /// <summary>
        /// 月球经度光行差,误差0.07"
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static double gxc_moonLon(double t)
        {
            return -3.4E-6;
        }


        /// <summary>
        /// 月球纬度光行差,误差0.006"
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static double gxc_moonLat(double t)
        {
            return 0.063 * Math.Sin(0.057 + 8433.4662 * t + 0.000064 * t * t) / Util.rad;
        }


        /// <summary>
        /// 大气折射,ho是视高度
        /// </summary>
        /// <param name="ho"></param>
        /// <returns></returns>
        public static double AR(double ho)
        {
            return -0.0002909 / Math.Tan(ho + 0.002227 / (ho + 0.07679));
        }


        /// <summary>
        /// 大气折射,h是真高度
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public static double AR2(double h)
        {
            return 0.0002967 / Math.Tan(h + 0.003138 / (h + 0.08919));
        }


        /// <summary>
        /// 视差修正
        /// </summary>
        /// <param name="z">赤道坐标</param>
        /// <param name="H">时角</param>
        /// <param name="fa">地理纬度</param>
        /// <param name="high">海拔(千米)</param>
        public static Coordinate parallax(Coordinate c, double H, double fa, double high)
        {
            double dw = 1d;
            var C = new Coordinate(c);
            if (C.R < 500) dw = Util.cs_AU;
            C.R *= dw;
            double r0, x0, y0, z0, f = 0.99664719;
            double u = Math.Atan(f * Math.Tan(fa));
            double g = C.J + H;
            r0 = Util.cs_rEar * Math.Cos(u) + high * Math.Cos(fa); //站点与地地心向径的赤道投影长度
            z0 = Util.cs_rEar * Math.Sin(u) * f + high * Math.Sin(fa); //站点与地地心向径的轴向投影长度
            x0 = r0 * Math.Cos(g);
            y0 = r0 * Math.Sin(g);

            C.X -= x0;
            C.Y -= y0;
            C.Z -= z0;

            C.W /= dw;
            return C;
        }



        /// <summary>
        /// 求空间两点连线与地球的交点(靠近点1的交点),返回在ZB.le_J,le_W,赤道坐标。R1,R2单位是千米
        /// </summary>
        /// <param name="J1"></param>
        /// <param name="W1"></param>
        /// <param name="R1"></param>
        /// <param name="J2"></param>
        /// <param name="W2"></param>
        /// <param name="R2"></param>
        public static Coordinate line_earth(Coordinate c1, Coordinate c2)
        {
            double x1 = c1.X;
            double y1 = c1.Y;
            double z1 = c1.Z;
            double x2 = c2.X;
            double y2 = c2.Y;
            double z2 = c2.Z;
            double dx = x2 - x1;
            double dy = y2 - y1;
            double dz = z2 - z1;
            //直线参数及地球参数
            double f = 0.99664719;
            double r = Util.cs_rEar;
            double x, y, z, lh = 0;
            if (Math.Abs(dx) < Math.Abs(dy)) //必要时仑换
            { lh = dx; dx = dy; dy = lh; lh = x1; x1 = y1; y1 = lh; lh = 1; }
            double a = dy / dx, b = y1 - a * x1, c = dz / dx / f, d = z1 / f - c * x1;
            double A = a * a + c * c + 1, B = a * b + c * d, C = b * b + d * d - r * r, D = B * B - A * C;
            if (D < 0)
            {
                //返回100表示无解
                return new Coordinate { J = 100, W = 100, R = 0 };
                //{ var le_J = le_W = 100; return; } 
            }
            else
            {
                D = Math.Sqrt(D);
                if (x1 + B / A < 0) D = -D;
                x = (-B + D) / A;
                y = a * x + b;
                z = (c * x + d) / f;
                return new Coordinate { X = x, Y = y, Z = z };



                //var le_R1 = Math.Sqrt(Math.Pow((x - x1),2) + Math.Pow((y - y1), 2) + Math.Pow((z - z1), 2));
                //if (lh != 0) { lh = x; x = y; y = lh; }
                //// 空间两点连线与地球的交点: 纬
                //var le_W = Math.Atan(z / Math.Sqrt(x * x + y * y));
                //// 空间两点连线与地球的交点
                //var le_J = Util.rad2mrad(Math.Atan2(y, x));
                //return new Coordinate { J = le_J, W = le_W, R = le_R1 };
            }

        }


    }
}
