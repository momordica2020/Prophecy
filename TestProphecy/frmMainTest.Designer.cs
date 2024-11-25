namespace TestSharpSxwnl
{
    partial class frmMainTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            webBrowserMonth = new System.Windows.Forms.WebBrowser();
            label1 = new System.Windows.Forms.Label();
            Cal_y = new System.Windows.Forms.TextBox();
            Cal_m = new System.Windows.Forms.TextBox();
            btnGo = new System.Windows.Forms.Button();
            btnBazi = new System.Windows.Forms.Button();
            Cml_m = new System.Windows.Forms.TextBox();
            Cml_y = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            Cml_d = new System.Windows.Forms.TextBox();
            Cml_his = new System.Windows.Forms.TextBox();
            txtBazi = new System.Windows.Forms.TextBox();
            Sel_zhou = new System.Windows.Forms.ComboBox();
            Sel_dq = new System.Windows.Forms.ComboBox();
            Sel_Region = new System.Windows.Forms.ComboBox();
            Sel_Province = new System.Windows.Forms.ComboBox();
            Sel_sqsm = new System.Windows.Forms.Label();
            txtLongitude = new System.Windows.Forms.TextBox();
            Cal_zdzb = new System.Windows.Forms.Label();
            Cal5 = new System.Windows.Forms.TextBox();
            Cal_pan = new System.Windows.Forms.TextBox();
            timerTick = new System.Windows.Forms.Timer(components);
            Cal_pause = new System.Windows.Forms.CheckBox();
            Cal_zb = new System.Windows.Forms.TextBox();
            lblLocalClock = new System.Windows.Forms.Label();
            lblSQClock = new System.Windows.Forms.Label();
            tabControlCal = new System.Windows.Forms.TabControl();
            tabPageMonthCal = new System.Windows.Forms.TabPage();
            tabPageTextMonthCal = new System.Windows.Forms.TabPage();
            txtPg0_Text = new System.Windows.Forms.TextBox();
            txtPg2_Text = new System.Windows.Forms.TextBox();
            txtPg1_Text = new System.Windows.Forms.TextBox();
            tabPageYearCal = new System.Windows.Forms.TabPage();
            Caly_y = new System.Windows.Forms.TextBox();
            btnMakeCaly = new System.Windows.Forms.Button();
            lblCaly = new System.Windows.Forms.Label();
            webBrowserYearCal = new System.Windows.Forms.WebBrowser();
            tabPageBaZi = new System.Windows.Forms.TabPage();
            cmbBaziTypeS = new System.Windows.Forms.ComboBox();
            chkCalcQi = new System.Windows.Forms.CheckBox();
            chkCalcJie = new System.Windows.Forms.CheckBox();
            btnBaZiBeijing = new System.Windows.Forms.Button();
            btnTestNewMethod = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            btnBaZiNormal = new System.Windows.Forms.Button();
            tabPageOthers = new System.Windows.Forms.TabPage();
            btndingSuoV = new System.Windows.Forms.Button();
            btndingQiV = new System.Windows.Forms.Button();
            btnTestOthers2 = new System.Windows.Forms.Button();
            btnGetRi12Jian = new System.Windows.Forms.Button();
            btnTestOthers = new System.Windows.Forms.Button();
            btnTestHelperClass = new System.Windows.Forms.Button();
            tabPageTools = new System.Windows.Forms.TabPage();
            btnLoadXmlSQ = new System.Windows.Forms.Button();
            btnLoadXmlJieqiFjia = new System.Windows.Forms.Button();
            btnLoadXmlJW = new System.Windows.Forms.Button();
            bntLoadXmlLunarJieri = new System.Windows.Forms.Button();
            btnLoadXmlsFtv = new System.Windows.Forms.Button();
            btnLoadwFtv = new System.Windows.Forms.Button();
            btnLoadXML = new System.Windows.Forms.Button();
            txtOutput = new System.Windows.Forms.TextBox();
            btnOutputJnbData = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            tabControlCal.SuspendLayout();
            tabPageMonthCal.SuspendLayout();
            tabPageTextMonthCal.SuspendLayout();
            tabPageYearCal.SuspendLayout();
            tabPageBaZi.SuspendLayout();
            tabPageOthers.SuspendLayout();
            tabPageTools.SuspendLayout();
            SuspendLayout();
            // 
            // webBrowserMonth
            // 
            webBrowserMonth.Location = new System.Drawing.Point(18, 74);
            webBrowserMonth.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            webBrowserMonth.MinimumSize = new System.Drawing.Size(37, 40);
            webBrowserMonth.Name = "webBrowserMonth";
            webBrowserMonth.Size = new System.Drawing.Size(869, 1042);
            webBrowserMonth.TabIndex = 0;
            webBrowserMonth.DocumentCompleted += webBrowserMonth_DocumentCompleted;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 26);
            label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(106, 24);
            label1.TabIndex = 1;
            label1.Text = "        年    月";
            // 
            // Cal_y
            // 
            Cal_y.Location = new System.Drawing.Point(18, 20);
            Cal_y.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal_y.Name = "Cal_y";
            Cal_y.Size = new System.Drawing.Size(77, 30);
            Cal_y.TabIndex = 2;
            // 
            // Cal_m
            // 
            Cal_m.Location = new System.Drawing.Point(136, 20);
            Cal_m.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal_m.Name = "Cal_m";
            Cal_m.Size = new System.Drawing.Size(35, 30);
            Cal_m.TabIndex = 3;
            // 
            // btnGo
            // 
            btnGo.Location = new System.Drawing.Point(214, 16);
            btnGo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnGo.Name = "btnGo";
            btnGo.Size = new System.Drawing.Size(90, 46);
            btnGo.TabIndex = 4;
            btnGo.Text = "确定";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // btnBazi
            // 
            btnBazi.Location = new System.Drawing.Point(473, 20);
            btnBazi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnBazi.Name = "btnBazi";
            btnBazi.Size = new System.Drawing.Size(218, 46);
            btnBazi.TabIndex = 9;
            btnBazi.Text = "当地真太阳时八字";
            btnBazi.UseVisualStyleBackColor = true;
            btnBazi.Click += btnBazi_Click;
            // 
            // Cml_m
            // 
            Cml_m.Location = new System.Drawing.Point(136, 24);
            Cml_m.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cml_m.Name = "Cml_m";
            Cml_m.Size = new System.Drawing.Size(35, 30);
            Cml_m.TabIndex = 8;
            // 
            // Cml_y
            // 
            Cml_y.Location = new System.Drawing.Point(18, 24);
            Cml_y.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cml_y.Name = "Cml_y";
            Cml_y.Size = new System.Drawing.Size(77, 30);
            Cml_y.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 30);
            label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(149, 24);
            label2.TabIndex = 6;
            label2.Text = "        年    月     日";
            // 
            // Cml_d
            // 
            Cml_d.Location = new System.Drawing.Point(205, 24);
            Cml_d.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cml_d.Name = "Cml_d";
            Cml_d.Size = new System.Drawing.Size(35, 30);
            Cml_d.TabIndex = 10;
            // 
            // Cml_his
            // 
            Cml_his.Location = new System.Drawing.Point(292, 24);
            Cml_his.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cml_his.Name = "Cml_his";
            Cml_his.Size = new System.Drawing.Size(147, 30);
            Cml_his.TabIndex = 11;
            // 
            // txtBazi
            // 
            txtBazi.Location = new System.Drawing.Point(18, 298);
            txtBazi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtBazi.Multiline = true;
            txtBazi.Name = "txtBazi";
            txtBazi.ReadOnly = true;
            txtBazi.Size = new System.Drawing.Size(866, 830);
            txtBazi.TabIndex = 12;
            // 
            // Sel_zhou
            // 
            Sel_zhou.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Sel_zhou.FormattingEnabled = true;
            Sel_zhou.Location = new System.Drawing.Point(13, 58);
            Sel_zhou.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Sel_zhou.MaxDropDownItems = 40;
            Sel_zhou.Name = "Sel_zhou";
            Sel_zhou.Size = new System.Drawing.Size(217, 32);
            Sel_zhou.TabIndex = 13;
            Sel_zhou.SelectedIndexChanged += Sel_zhou_SelectedIndexChanged;
            // 
            // Sel_dq
            // 
            Sel_dq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Sel_dq.FormattingEnabled = true;
            Sel_dq.Location = new System.Drawing.Point(244, 58);
            Sel_dq.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Sel_dq.MaxDropDownItems = 40;
            Sel_dq.Name = "Sel_dq";
            Sel_dq.Size = new System.Drawing.Size(248, 32);
            Sel_dq.TabIndex = 14;
            Sel_dq.SelectedIndexChanged += Sel_dq_SelectedIndexChanged;
            // 
            // Sel_Region
            // 
            Sel_Region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Sel_Region.FormattingEnabled = true;
            Sel_Region.Location = new System.Drawing.Point(244, 144);
            Sel_Region.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Sel_Region.MaxDropDownItems = 40;
            Sel_Region.Name = "Sel_Region";
            Sel_Region.Size = new System.Drawing.Size(248, 32);
            Sel_Region.TabIndex = 16;
            Sel_Region.SelectedIndexChanged += Sel_Region_SelectedIndexChanged;
            // 
            // Sel_Province
            // 
            Sel_Province.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Sel_Province.FormattingEnabled = true;
            Sel_Province.Location = new System.Drawing.Point(13, 144);
            Sel_Province.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Sel_Province.MaxDropDownItems = 40;
            Sel_Province.Name = "Sel_Province";
            Sel_Province.Size = new System.Drawing.Size(217, 32);
            Sel_Province.TabIndex = 15;
            Sel_Province.SelectedIndexChanged += Sel_Province_SelectedIndexChanged;
            // 
            // Sel_sqsm
            // 
            Sel_sqsm.Location = new System.Drawing.Point(9, 104);
            Sel_sqsm.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            Sel_sqsm.Name = "Sel_sqsm";
            Sel_sqsm.Size = new System.Drawing.Size(304, 32);
            Sel_sqsm.TabIndex = 17;
            Sel_sqsm.Text = "时区说明";
            Sel_sqsm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLongitude
            // 
            txtLongitude.Location = new System.Drawing.Point(226, 88);
            txtLongitude.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtLongitude.Name = "txtLongitude";
            txtLongitude.Size = new System.Drawing.Size(165, 30);
            txtLongitude.TabIndex = 18;
            txtLongitude.Text = "-102°42'0\" 昆明";
            // 
            // Cal_zdzb
            // 
            Cal_zdzb.AutoSize = true;
            Cal_zdzb.Location = new System.Drawing.Point(13, 196);
            Cal_zdzb.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            Cal_zdzb.Name = "Cal_zdzb";
            Cal_zdzb.Size = new System.Drawing.Size(100, 24);
            Cal_zdzb.TabIndex = 19;
            Cal_zdzb.Text = "经纬度坐标";
            // 
            // Cal5
            // 
            Cal5.Location = new System.Drawing.Point(13, 228);
            Cal5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal5.Multiline = true;
            Cal5.Name = "Cal5";
            Cal5.ReadOnly = true;
            Cal5.Size = new System.Drawing.Size(497, 114);
            Cal5.TabIndex = 20;
            // 
            // Cal_pan
            // 
            Cal_pan.Location = new System.Drawing.Point(13, 842);
            Cal_pan.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal_pan.Multiline = true;
            Cal_pan.Name = "Cal_pan";
            Cal_pan.ReadOnly = true;
            Cal_pan.Size = new System.Drawing.Size(497, 370);
            Cal_pan.TabIndex = 21;
            // 
            // timerTick
            // 
            timerTick.Interval = 1000;
            timerTick.Tick += timerTick_Tick;
            // 
            // Cal_pause
            // 
            Cal_pause.AutoSize = true;
            Cal_pause.Location = new System.Drawing.Point(363, 20);
            Cal_pause.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal_pause.Name = "Cal_pause";
            Cal_pause.Size = new System.Drawing.Size(108, 28);
            Cal_pause.TabIndex = 22;
            Cal_pause.Text = "时钟暂停";
            Cal_pause.UseVisualStyleBackColor = true;
            // 
            // Cal_zb
            // 
            Cal_zb.Location = new System.Drawing.Point(13, 352);
            Cal_zb.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Cal_zb.Multiline = true;
            Cal_zb.Name = "Cal_zb";
            Cal_zb.ReadOnly = true;
            Cal_zb.Size = new System.Drawing.Size(497, 470);
            Cal_zb.TabIndex = 23;
            // 
            // lblLocalClock
            // 
            lblLocalClock.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            lblLocalClock.ForeColor = System.Drawing.Color.Firebrick;
            lblLocalClock.Location = new System.Drawing.Point(13, 12);
            lblLocalClock.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lblLocalClock.Name = "lblLocalClock";
            lblLocalClock.Size = new System.Drawing.Size(350, 40);
            lblLocalClock.TabIndex = 24;
            lblLocalClock.Text = "当前本地时间";
            lblLocalClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSQClock
            // 
            lblSQClock.AutoSize = true;
            lblSQClock.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            lblSQClock.ForeColor = System.Drawing.Color.Blue;
            lblSQClock.Location = new System.Drawing.Point(324, 104);
            lblSQClock.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lblSQClock.Name = "lblSQClock";
            lblSQClock.Size = new System.Drawing.Size(84, 18);
            lblSQClock.TabIndex = 25;
            lblSQClock.Text = "时区时间";
            // 
            // tabControlCal
            // 
            tabControlCal.Controls.Add(tabPageMonthCal);
            tabControlCal.Controls.Add(tabPageTextMonthCal);
            tabControlCal.Controls.Add(tabPageYearCal);
            tabControlCal.Controls.Add(tabPageBaZi);
            tabControlCal.Controls.Add(tabPageOthers);
            tabControlCal.Controls.Add(tabPageTools);
            tabControlCal.Location = new System.Drawing.Point(541, 12);
            tabControlCal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabControlCal.Name = "tabControlCal";
            tabControlCal.SelectedIndex = 0;
            tabControlCal.Size = new System.Drawing.Size(920, 1204);
            tabControlCal.TabIndex = 26;
            // 
            // tabPageMonthCal
            // 
            tabPageMonthCal.Controls.Add(webBrowserMonth);
            tabPageMonthCal.Controls.Add(Cal_y);
            tabPageMonthCal.Controls.Add(Cal_m);
            tabPageMonthCal.Controls.Add(btnGo);
            tabPageMonthCal.Controls.Add(label1);
            tabPageMonthCal.Location = new System.Drawing.Point(4, 33);
            tabPageMonthCal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageMonthCal.Name = "tabPageMonthCal";
            tabPageMonthCal.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageMonthCal.Size = new System.Drawing.Size(912, 1167);
            tabPageMonthCal.TabIndex = 0;
            tabPageMonthCal.Text = "月历";
            tabPageMonthCal.UseVisualStyleBackColor = true;
            // 
            // tabPageTextMonthCal
            // 
            tabPageTextMonthCal.Controls.Add(txtPg0_Text);
            tabPageTextMonthCal.Controls.Add(txtPg2_Text);
            tabPageTextMonthCal.Controls.Add(txtPg1_Text);
            tabPageTextMonthCal.Location = new System.Drawing.Point(4, 33);
            tabPageTextMonthCal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageTextMonthCal.Name = "tabPageTextMonthCal";
            tabPageTextMonthCal.Size = new System.Drawing.Size(912, 1167);
            tabPageTextMonthCal.TabIndex = 2;
            tabPageTextMonthCal.Text = "文本月历";
            tabPageTextMonthCal.UseVisualStyleBackColor = true;
            // 
            // txtPg0_Text
            // 
            txtPg0_Text.Location = new System.Drawing.Point(28, 26);
            txtPg0_Text.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtPg0_Text.Multiline = true;
            txtPg0_Text.Name = "txtPg0_Text";
            txtPg0_Text.ReadOnly = true;
            txtPg0_Text.Size = new System.Drawing.Size(855, 44);
            txtPg0_Text.TabIndex = 26;
            // 
            // txtPg2_Text
            // 
            txtPg2_Text.Location = new System.Drawing.Point(28, 1004);
            txtPg2_Text.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtPg2_Text.Multiline = true;
            txtPg2_Text.Name = "txtPg2_Text";
            txtPg2_Text.ReadOnly = true;
            txtPg2_Text.Size = new System.Drawing.Size(855, 122);
            txtPg2_Text.TabIndex = 25;
            // 
            // txtPg1_Text
            // 
            txtPg1_Text.Location = new System.Drawing.Point(28, 94);
            txtPg1_Text.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtPg1_Text.Multiline = true;
            txtPg1_Text.Name = "txtPg1_Text";
            txtPg1_Text.ReadOnly = true;
            txtPg1_Text.Size = new System.Drawing.Size(855, 894);
            txtPg1_Text.TabIndex = 24;
            // 
            // tabPageYearCal
            // 
            tabPageYearCal.Controls.Add(Caly_y);
            tabPageYearCal.Controls.Add(btnMakeCaly);
            tabPageYearCal.Controls.Add(lblCaly);
            tabPageYearCal.Controls.Add(webBrowserYearCal);
            tabPageYearCal.Location = new System.Drawing.Point(4, 33);
            tabPageYearCal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageYearCal.Name = "tabPageYearCal";
            tabPageYearCal.Size = new System.Drawing.Size(912, 1167);
            tabPageYearCal.TabIndex = 3;
            tabPageYearCal.Text = "年历";
            tabPageYearCal.UseVisualStyleBackColor = true;
            // 
            // Caly_y
            // 
            Caly_y.Location = new System.Drawing.Point(22, 16);
            Caly_y.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Caly_y.Name = "Caly_y";
            Caly_y.Size = new System.Drawing.Size(77, 30);
            Caly_y.TabIndex = 6;
            // 
            // btnMakeCaly
            // 
            btnMakeCaly.Location = new System.Drawing.Point(178, 12);
            btnMakeCaly.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnMakeCaly.Name = "btnMakeCaly";
            btnMakeCaly.Size = new System.Drawing.Size(90, 46);
            btnMakeCaly.TabIndex = 7;
            btnMakeCaly.Text = "确定";
            btnMakeCaly.UseVisualStyleBackColor = true;
            btnMakeCaly.Click += btnMakeCaly_Click;
            // 
            // lblCaly
            // 
            lblCaly.AutoSize = true;
            lblCaly.Location = new System.Drawing.Point(22, 22);
            lblCaly.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            lblCaly.Name = "lblCaly";
            lblCaly.Size = new System.Drawing.Size(68, 24);
            lblCaly.TabIndex = 5;
            lblCaly.Text = "        年";
            // 
            // webBrowserYearCal
            // 
            webBrowserYearCal.Location = new System.Drawing.Point(22, 70);
            webBrowserYearCal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            webBrowserYearCal.MinimumSize = new System.Drawing.Size(37, 40);
            webBrowserYearCal.Name = "webBrowserYearCal";
            webBrowserYearCal.ScrollBarsEnabled = false;
            webBrowserYearCal.Size = new System.Drawing.Size(860, 1064);
            webBrowserYearCal.TabIndex = 1;
            // 
            // tabPageBaZi
            // 
            tabPageBaZi.Controls.Add(cmbBaziTypeS);
            tabPageBaZi.Controls.Add(chkCalcQi);
            tabPageBaZi.Controls.Add(chkCalcJie);
            tabPageBaZi.Controls.Add(btnBaZiBeijing);
            tabPageBaZi.Controls.Add(btnTestNewMethod);
            tabPageBaZi.Controls.Add(label3);
            tabPageBaZi.Controls.Add(btnBaZiNormal);
            tabPageBaZi.Controls.Add(txtBazi);
            tabPageBaZi.Controls.Add(Cml_his);
            tabPageBaZi.Controls.Add(Cml_d);
            tabPageBaZi.Controls.Add(btnBazi);
            tabPageBaZi.Controls.Add(txtLongitude);
            tabPageBaZi.Controls.Add(Cml_y);
            tabPageBaZi.Controls.Add(Cml_m);
            tabPageBaZi.Controls.Add(label2);
            tabPageBaZi.Location = new System.Drawing.Point(4, 33);
            tabPageBaZi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageBaZi.Name = "tabPageBaZi";
            tabPageBaZi.Size = new System.Drawing.Size(912, 1167);
            tabPageBaZi.TabIndex = 4;
            tabPageBaZi.Text = "八字计算";
            tabPageBaZi.UseVisualStyleBackColor = true;
            // 
            // cmbBaziTypeS
            // 
            cmbBaziTypeS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBaziTypeS.FormattingEnabled = true;
            cmbBaziTypeS.Items.AddRange(new object[] { "常规(北半球八字)", "南半球八字: 天冲地冲(月天干地支均与北半球的取法相冲)", "南半球八字: 天克地冲(月地支与北半球的取法相冲, 按五虎遁月法排月天干)", "南半球八字: 天同地冲(月地支与北半球的取法相冲, 月天干与北半球的取法相同)" });
            cmbBaziTypeS.Location = new System.Drawing.Point(24, 152);
            cmbBaziTypeS.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            cmbBaziTypeS.Name = "cmbBaziTypeS";
            cmbBaziTypeS.Size = new System.Drawing.Size(860, 32);
            cmbBaziTypeS.TabIndex = 24;
            // 
            // chkCalcQi
            // 
            chkCalcQi.AutoSize = true;
            chkCalcQi.Location = new System.Drawing.Point(810, 230);
            chkCalcQi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            chkCalcQi.Name = "chkCalcQi";
            chkCalcQi.Size = new System.Drawing.Size(54, 28);
            chkCalcQi.TabIndex = 23;
            chkCalcQi.Text = "气";
            chkCalcQi.UseVisualStyleBackColor = true;
            // 
            // chkCalcJie
            // 
            chkCalcJie.AutoSize = true;
            chkCalcJie.Checked = true;
            chkCalcJie.CheckState = System.Windows.Forms.CheckState.Checked;
            chkCalcJie.Location = new System.Drawing.Point(733, 230);
            chkCalcJie.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            chkCalcJie.Name = "chkCalcJie";
            chkCalcJie.Size = new System.Drawing.Size(54, 28);
            chkCalcJie.TabIndex = 22;
            chkCalcJie.Text = "节";
            chkCalcJie.UseVisualStyleBackColor = true;
            // 
            // btnBaZiBeijing
            // 
            btnBaZiBeijing.Location = new System.Drawing.Point(695, 76);
            btnBaZiBeijing.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnBaZiBeijing.Name = "btnBaZiBeijing";
            btnBaZiBeijing.Size = new System.Drawing.Size(192, 46);
            btnBaZiBeijing.TabIndex = 21;
            btnBaZiBeijing.Text = "北京时间八字";
            btnBaZiBeijing.UseVisualStyleBackColor = true;
            btnBaZiBeijing.Click += btnBaZiBeijing_Click;
            // 
            // btnTestNewMethod
            // 
            btnTestNewMethod.Location = new System.Drawing.Point(473, 222);
            btnTestNewMethod.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnTestNewMethod.Name = "btnTestNewMethod";
            btnTestNewMethod.Size = new System.Drawing.Size(249, 46);
            btnTestNewMethod.TabIndex = 10;
            btnTestNewMethod.Text = "计算该日期的节气信息";
            btnTestNewMethod.UseVisualStyleBackColor = true;
            btnTestNewMethod.Click += btnTestNewMethod_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(18, 96);
            label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(152, 24);
            label3.TabIndex = 20;
            label3.Text = "经度(负数为东经):";
            // 
            // btnBaZiNormal
            // 
            btnBaZiNormal.Location = new System.Drawing.Point(473, 76);
            btnBaZiNormal.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnBaZiNormal.Name = "btnBaZiNormal";
            btnBaZiNormal.Size = new System.Drawing.Size(218, 46);
            btnBaZiNormal.TabIndex = 19;
            btnBaZiNormal.Text = "当地平太阳时八字";
            btnBaZiNormal.UseVisualStyleBackColor = true;
            btnBaZiNormal.Click += btnBaZiNormal_Click;
            // 
            // tabPageOthers
            // 
            tabPageOthers.Controls.Add(btndingSuoV);
            tabPageOthers.Controls.Add(btndingQiV);
            tabPageOthers.Controls.Add(btnTestOthers2);
            tabPageOthers.Controls.Add(btnGetRi12Jian);
            tabPageOthers.Controls.Add(btnTestOthers);
            tabPageOthers.Controls.Add(btnTestHelperClass);
            tabPageOthers.Location = new System.Drawing.Point(4, 33);
            tabPageOthers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageOthers.Name = "tabPageOthers";
            tabPageOthers.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageOthers.Size = new System.Drawing.Size(912, 1167);
            tabPageOthers.TabIndex = 1;
            tabPageOthers.Text = "其他测试";
            tabPageOthers.UseVisualStyleBackColor = true;
            // 
            // btndingSuoV
            // 
            btndingSuoV.Location = new System.Drawing.Point(363, 374);
            btndingSuoV.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btndingSuoV.Name = "btndingSuoV";
            btndingSuoV.Size = new System.Drawing.Size(301, 46);
            btndingSuoV.TabIndex = 14;
            btndingSuoV.Text = "定朔速度测试";
            btndingSuoV.UseVisualStyleBackColor = true;
            btndingSuoV.Click += btndingSuoV_Click;
            // 
            // btndingQiV
            // 
            btndingQiV.Location = new System.Drawing.Point(31, 374);
            btndingQiV.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btndingQiV.Name = "btndingQiV";
            btndingQiV.Size = new System.Drawing.Size(301, 46);
            btndingQiV.TabIndex = 13;
            btndingQiV.Text = "定气速度测试";
            btndingQiV.UseVisualStyleBackColor = true;
            btndingQiV.Click += btndingQiV_Click;
            // 
            // btnTestOthers2
            // 
            btnTestOthers2.Enabled = false;
            btnTestOthers2.Location = new System.Drawing.Point(363, 86);
            btnTestOthers2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnTestOthers2.Name = "btnTestOthers2";
            btnTestOthers2.Size = new System.Drawing.Size(301, 46);
            btnTestOthers2.TabIndex = 12;
            btnTestOthers2.Text = "杂项测试2";
            btnTestOthers2.UseVisualStyleBackColor = true;
            btnTestOthers2.Click += btnTestOthers2_Click;
            // 
            // btnGetRi12Jian
            // 
            btnGetRi12Jian.Location = new System.Drawing.Point(31, 86);
            btnGetRi12Jian.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnGetRi12Jian.Name = "btnGetRi12Jian";
            btnGetRi12Jian.Size = new System.Drawing.Size(301, 46);
            btnGetRi12Jian.TabIndex = 11;
            btnGetRi12Jian.Text = "测试新增的方法: 十二日建";
            btnGetRi12Jian.UseVisualStyleBackColor = true;
            btnGetRi12Jian.Click += btnGetRi12Jian_Click;
            // 
            // btnTestOthers
            // 
            btnTestOthers.Location = new System.Drawing.Point(363, 28);
            btnTestOthers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnTestOthers.Name = "btnTestOthers";
            btnTestOthers.Size = new System.Drawing.Size(301, 46);
            btnTestOthers.TabIndex = 9;
            btnTestOthers.Text = "杂项测试";
            btnTestOthers.UseVisualStyleBackColor = true;
            btnTestOthers.Click += btnTestOthers_Click;
            // 
            // btnTestHelperClass
            // 
            btnTestHelperClass.Location = new System.Drawing.Point(31, 28);
            btnTestHelperClass.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnTestHelperClass.Name = "btnTestHelperClass";
            btnTestHelperClass.Size = new System.Drawing.Size(301, 46);
            btnTestHelperClass.TabIndex = 8;
            btnTestHelperClass.Text = "测试 LunarHelper 类的方法";
            btnTestHelperClass.UseVisualStyleBackColor = true;
            btnTestHelperClass.Click += btnTestHelperClass_Click;
            // 
            // tabPageTools
            // 
            tabPageTools.Controls.Add(btnLoadXmlSQ);
            tabPageTools.Controls.Add(btnLoadXmlJieqiFjia);
            tabPageTools.Controls.Add(btnLoadXmlJW);
            tabPageTools.Controls.Add(bntLoadXmlLunarJieri);
            tabPageTools.Controls.Add(btnLoadXmlsFtv);
            tabPageTools.Controls.Add(btnLoadwFtv);
            tabPageTools.Controls.Add(btnLoadXML);
            tabPageTools.Controls.Add(txtOutput);
            tabPageTools.Controls.Add(btnOutputJnbData);
            tabPageTools.Location = new System.Drawing.Point(4, 33);
            tabPageTools.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            tabPageTools.Name = "tabPageTools";
            tabPageTools.Size = new System.Drawing.Size(912, 1167);
            tabPageTools.TabIndex = 5;
            tabPageTools.Text = "辅助工具";
            tabPageTools.UseVisualStyleBackColor = true;
            // 
            // btnLoadXmlSQ
            // 
            btnLoadXmlSQ.Location = new System.Drawing.Point(293, 78);
            btnLoadXmlSQ.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadXmlSQ.Name = "btnLoadXmlSQ";
            btnLoadXmlSQ.Size = new System.Drawing.Size(258, 46);
            btnLoadXmlSQ.TabIndex = 20;
            btnLoadXmlSQ.Text = "加载 XML 时区数据";
            btnLoadXmlSQ.UseVisualStyleBackColor = true;
            btnLoadXmlSQ.Click += btnLoadXmlSQ_Click;
            // 
            // btnLoadXmlJieqiFjia
            // 
            btnLoadXmlJieqiFjia.Location = new System.Drawing.Point(563, 172);
            btnLoadXmlJieqiFjia.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadXmlJieqiFjia.Name = "btnLoadXmlJieqiFjia";
            btnLoadXmlJieqiFjia.Size = new System.Drawing.Size(258, 46);
            btnLoadXmlJieqiFjia.TabIndex = 18;
            btnLoadXmlJieqiFjia.Text = "加载 XML 廿四节气假日";
            btnLoadXmlJieqiFjia.UseVisualStyleBackColor = true;
            btnLoadXmlJieqiFjia.Click += btnLoadXmlJieqiFjia_Click;
            // 
            // btnLoadXmlJW
            // 
            btnLoadXmlJW.Location = new System.Drawing.Point(20, 76);
            btnLoadXmlJW.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadXmlJW.Name = "btnLoadXmlJW";
            btnLoadXmlJW.Size = new System.Drawing.Size(262, 46);
            btnLoadXmlJW.TabIndex = 19;
            btnLoadXmlJW.Text = "加载 XML 经纬度数据";
            btnLoadXmlJW.UseVisualStyleBackColor = true;
            btnLoadXmlJW.Click += btnLoadXmlJW_Click;
            // 
            // bntLoadXmlLunarJieri
            // 
            bntLoadXmlLunarJieri.Location = new System.Drawing.Point(293, 174);
            bntLoadXmlLunarJieri.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            bntLoadXmlLunarJieri.Name = "bntLoadXmlLunarJieri";
            bntLoadXmlLunarJieri.Size = new System.Drawing.Size(258, 46);
            bntLoadXmlLunarJieri.TabIndex = 17;
            bntLoadXmlLunarJieri.Text = "加载 XML 农历节假日";
            bntLoadXmlLunarJieri.UseVisualStyleBackColor = true;
            bntLoadXmlLunarJieri.Click += bntLoadXmlLunarJieri_Click;
            // 
            // btnLoadXmlsFtv
            // 
            btnLoadXmlsFtv.Location = new System.Drawing.Point(20, 172);
            btnLoadXmlsFtv.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadXmlsFtv.Name = "btnLoadXmlsFtv";
            btnLoadXmlsFtv.Size = new System.Drawing.Size(262, 46);
            btnLoadXmlsFtv.TabIndex = 16;
            btnLoadXmlsFtv.Text = "加载 XML 公历节假日";
            btnLoadXmlsFtv.UseVisualStyleBackColor = true;
            btnLoadXmlsFtv.Click += btnLoadXmlsFtv_Click;
            // 
            // btnLoadwFtv
            // 
            btnLoadwFtv.Location = new System.Drawing.Point(293, 126);
            btnLoadwFtv.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadwFtv.Name = "btnLoadwFtv";
            btnLoadwFtv.Size = new System.Drawing.Size(258, 46);
            btnLoadwFtv.TabIndex = 15;
            btnLoadwFtv.Text = "加载 XML 周规则节假日";
            btnLoadwFtv.UseVisualStyleBackColor = true;
            btnLoadwFtv.Click += btnLoadwFtv_Click;
            // 
            // btnLoadXML
            // 
            btnLoadXML.Location = new System.Drawing.Point(20, 126);
            btnLoadXML.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnLoadXML.Name = "btnLoadXML";
            btnLoadXML.Size = new System.Drawing.Size(262, 46);
            btnLoadXML.TabIndex = 14;
            btnLoadXML.Text = "加载 XML 纪年表数据";
            btnLoadXML.UseVisualStyleBackColor = true;
            btnLoadXML.Click += btnLoadXML_Click;
            // 
            // txtOutput
            // 
            txtOutput.Location = new System.Drawing.Point(20, 232);
            txtOutput.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtOutput.Size = new System.Drawing.Size(866, 898);
            txtOutput.TabIndex = 13;
            txtOutput.WordWrap = false;
            // 
            // btnOutputJnbData
            // 
            btnOutputJnbData.Location = new System.Drawing.Point(20, 20);
            btnOutputJnbData.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            btnOutputJnbData.Name = "btnOutputJnbData";
            btnOutputJnbData.Size = new System.Drawing.Size(330, 46);
            btnOutputJnbData.TabIndex = 0;
            btnOutputJnbData.Text = "输出全部硬编码数据至 XML";
            btnOutputJnbData.UseVisualStyleBackColor = true;
            btnOutputJnbData.Click += btnOutputJnbData_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(1469, 275);
            textBox1.Margin = new System.Windows.Forms.Padding(6);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(404, 907);
            textBox1.TabIndex = 27;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(1473, 220);
            button1.Margin = new System.Windows.Forms.Padding(6);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(218, 46);
            button1.TabIndex = 25;
            button1.Text = "test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(1469, 45);
            textBox2.Margin = new System.Windows.Forms.Padding(6);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(397, 175);
            textBox2.TabIndex = 25;
            // 
            // frmMainTest
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1881, 1228);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(tabControlCal);
            Controls.Add(lblSQClock);
            Controls.Add(lblLocalClock);
            Controls.Add(Cal_zb);
            Controls.Add(Cal_pause);
            Controls.Add(Cal_pan);
            Controls.Add(Cal5);
            Controls.Add(Cal_zdzb);
            Controls.Add(Sel_sqsm);
            Controls.Add(Sel_Region);
            Controls.Add(Sel_Province);
            Controls.Add(Sel_dq);
            Controls.Add(Sel_zhou);
            Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            Name = "frmMainTest";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "调试寿星万年历核心算法的转换";
            FormClosed += frmMainTest_FormClosed;
            Load += frmMainTest_Load;
            tabControlCal.ResumeLayout(false);
            tabPageMonthCal.ResumeLayout(false);
            tabPageMonthCal.PerformLayout();
            tabPageTextMonthCal.ResumeLayout(false);
            tabPageTextMonthCal.PerformLayout();
            tabPageYearCal.ResumeLayout(false);
            tabPageYearCal.PerformLayout();
            tabPageBaZi.ResumeLayout(false);
            tabPageBaZi.PerformLayout();
            tabPageOthers.ResumeLayout(false);
            tabPageTools.ResumeLayout(false);
            tabPageTools.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Cal_y;
        private System.Windows.Forms.TextBox Cal_m;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnBazi;
        private System.Windows.Forms.TextBox Cml_m;
        private System.Windows.Forms.TextBox Cml_y;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Cml_d;
        private System.Windows.Forms.TextBox Cml_his;
        private System.Windows.Forms.TextBox txtBazi;
        private System.Windows.Forms.ComboBox Sel_zhou;
        private System.Windows.Forms.ComboBox Sel_dq;
        private System.Windows.Forms.ComboBox Sel_Region;
        private System.Windows.Forms.ComboBox Sel_Province;
        private System.Windows.Forms.Label Sel_sqsm;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Label Cal_zdzb;
        private System.Windows.Forms.TextBox Cal5;
        private System.Windows.Forms.TextBox Cal_pan;
        private System.Windows.Forms.Timer timerTick;
        private System.Windows.Forms.CheckBox Cal_pause;
        private System.Windows.Forms.TextBox Cal_zb;
        private System.Windows.Forms.Label lblLocalClock;
        private System.Windows.Forms.Label lblSQClock;
        private System.Windows.Forms.TabControl tabControlCal;
        private System.Windows.Forms.TabPage tabPageMonthCal;
        private System.Windows.Forms.TabPage tabPageOthers;
        private System.Windows.Forms.TabPage tabPageTextMonthCal;
        private System.Windows.Forms.TextBox txtPg0_Text;
        private System.Windows.Forms.TextBox txtPg2_Text;
        private System.Windows.Forms.TextBox txtPg1_Text;
        private System.Windows.Forms.TabPage tabPageYearCal;
        private System.Windows.Forms.WebBrowser webBrowserYearCal;
        private System.Windows.Forms.TextBox Caly_y;
        private System.Windows.Forms.Button btnMakeCaly;
        private System.Windows.Forms.Label lblCaly;
        private System.Windows.Forms.Button btnTestHelperClass;
        private System.Windows.Forms.Button btnBaZiNormal;
        private System.Windows.Forms.TabPage tabPageBaZi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBaZiBeijing;
        private System.Windows.Forms.TabPage tabPageTools;
        private System.Windows.Forms.Button btnOutputJnbData;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.Button btnLoadwFtv;
        private System.Windows.Forms.Button btnLoadXmlsFtv;
        private System.Windows.Forms.Button bntLoadXmlLunarJieri;
        private System.Windows.Forms.Button btnLoadXmlJieqiFjia;
        private System.Windows.Forms.Button btnTestOthers;
        private System.Windows.Forms.Button btnLoadXmlSQ;
        private System.Windows.Forms.Button btnLoadXmlJW;
        private System.Windows.Forms.Button btnTestNewMethod;
        private System.Windows.Forms.Button btnGetRi12Jian;
        private System.Windows.Forms.CheckBox chkCalcQi;
        private System.Windows.Forms.CheckBox chkCalcJie;
        private System.Windows.Forms.Button btnTestOthers2;
        private System.Windows.Forms.ComboBox cmbBaziTypeS;
        private System.Windows.Forms.Button btndingSuoV;
        private System.Windows.Forms.Button btndingQiV;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
    }
}