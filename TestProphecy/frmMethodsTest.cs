using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Prophecy;
using Prophecy.Data;

namespace TestSharpSxwnl
{
    public partial class frmMethodsTest : Form
    {
        public frmMethodsTest()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("*" + Util.trim("\t 123 \r") + "*", "返回结果:");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.year2Ayear(-1) + "\r" + Util.year2Ayear("B2009") + "\r" +
                            Util.year2Ayear(10000) + "\r" + Util.year2Ayear("-4713") + "\r" +
                            Util.year2Ayear("B0"), "返回结果:");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.Ayear2year("-1") + "\r" + Util.Ayear2year("0") + "\r" +
                            Util.Ayear2year(2009), "返回结果:");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.timeStr2hour("12") + "\r" + Util.timeStr2hour("12:30") + "\r" +
                            Util.timeStr2hour("12:30:36"), "返回结果:");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.rad2str(3.14, 0) + "\r" + Util.rad2str(-3.14, 1) +
                            "\r\r " + Util.str2rad("+179°54'31.49\"") +
                            "\r" + Util.str2rad("-179°54'31.49\""), "返回结果:");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.rad2str2(3.14) + "\r" + Util.rad2str2(-3.14), "返回结果:");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.m2fm(121, 2, 0) + "\r" + Util.m2fm(121, 0, 1), "返回结果:");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.rad2mrad(0) + "\r" + Util.rad2mrad(Math.PI / 2) + "\r" +
                            Util.rad2mrad(Math.PI * 2) + "\r" + Util.rad2mrad(Math.PI * 3) + "\r" +
                            Util.rad2mrad(Math.PI * 4), "返回结果:");
        }

        private void label9_Click(object sender, EventArgs e)
        {
           MessageBox.Show(Util.rad2rrad(0) + "\r" + Util.rad2rrad(-Math.PI/2) + "\r" + 
                           Util.rad2rrad(Math.PI) + "\r" + Util.rad2rrad(-Math.PI) + "\r" + 
                           Util.rad2rrad(Math.PI * 2) + "\r" + Util.rad2rrad(-Math.PI * 2) + "\r" +
                           Util.rad2rrad(-Math.PI * 3), "返回结果:");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.mod2(1, 2) + "\r" + Util.mod2(1.2, 2.2) + "\r" +
                            Util.mod2(2, 1) + "\r" + Util.mod2(2.2, 1.2), "返回结果:");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            JWdata.JWdecode("P3Tg昆明");
            MessageBox.Show("经: " + Util.rad2str2(JWdata.J) + "\r纬: " + Util.rad2str2(JWdata.W), "返回结果:");

        }

        private void label12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("_" + Util.SUBSTRING("1", 0, 2) + "_" + "\r" +
                            "_" + Util.SUBSTRING("1", -1, 2) + "_" + "\r" +
                            "_" + Util.SUBSTRING("1", 0, -1) + "_" + "\r" +
                            "_" + Util.SUBSTRING("abc", 1, 2) + "_", "返回结果(注意前后均增加了下划线):");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.VAL("1") + "\r" +
                            Util.VAL("$1") + "\r" +
                            Util.VAL("$-1") + "\r" +
                            Util.VAL("-1E3") + "\r" +
                            Util.VAL("$1E-3") + "\r" +
                            Util.VAL("-123%") + "\r" +
                            Util.VAL("ABC") + "\r" +
                            Util.VAL("123D") + "\r" +
                            Util.VAL("$+.E+%"), "返回结果:");
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.VAL("￥+1") + "\r" +
                            Util.VAL("￥+-1") + "\r" +
                            Util.VAL("￥-1.2") + "\r" +
                            Util.VAL("￥$") + "\r" +
                            Util.VAL("￥-.E-%") + "\r" +
                            Util.VAL("￥-12.3%") + "\r" +
                            Util.VAL("A100") + "\r" +
                            Util.VAL("1.2") + "\r", "返回结果:");
        }

        private void label15_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.VAL("001.88") + "\r" +
                            Util.VAL("001.88", 1) + "\r" +
                            Util.VAL("001.88", 1L) + "\r" +
                            Util.VAL(".22") + "\r" +
                            Util.VAL(".88.22") + "\r" +
                            Util.VAL(".22E2") + "\r" +
                            Util.VAL("  2") + "\r" +
                            Util.VAL("$  2") + "\r", "返回结果:");
        }

        private void label16_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.VAL("") + "\r" +
                            Util.VAL("E3") + "\r" +
                            Util.VAL("3E") + "\r" +
                            Util.VAL("3E+") + "\r" +
                            Util.VAL("3E-") + "\r" +
                            Util.VAL("3E2") + "\r" +
                            Util.VAL("3E.2") + "\r" +
                            Util.VAL(".E2") + "\r" +
                            Util.VAL("+1") + "\r", "返回结果:");
        }

    }
}
