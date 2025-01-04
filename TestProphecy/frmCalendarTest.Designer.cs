namespace TestProphecy
{
    partial class frmCalendarTest
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
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            nYear = new System.Windows.Forms.NumericUpDown();
            nMonth = new System.Windows.Forms.NumericUpDown();
            nDay = new System.Windows.Forms.NumericUpDown();
            nHour = new System.Windows.Forms.NumericUpDown();
            nMinute = new System.Windows.Forms.NumericUpDown();
            nSecond = new System.Windows.Forms.NumericUpDown();
            nJD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nMinute).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nSecond).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nJD).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Location = new System.Drawing.Point(343, 31);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox1.Size = new System.Drawing.Size(976, 1040);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(40, 31);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(255, 56);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.SystemColors.WindowFrame;
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.Location = new System.Drawing.Point(40, 93);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(257, 317);
            textBox2.TabIndex = 2;
            textBox2.KeyDown += textBox2_KeyDown;
            // 
            // nYear
            // 
            nYear.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nYear.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nYear.Location = new System.Drawing.Point(40, 438);
            nYear.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nYear.Minimum = new decimal(new int[] { 999999, 0, 0, int.MinValue });
            nYear.Name = "nYear";
            nYear.Size = new System.Drawing.Size(255, 43);
            nYear.TabIndex = 3;
            nYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            nYear.ValueChanged += nYear_ValueChanged;
            // 
            // nMonth
            // 
            nMonth.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nMonth.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nMonth.Location = new System.Drawing.Point(40, 500);
            nMonth.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            nMonth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nMonth.Name = "nMonth";
            nMonth.Size = new System.Drawing.Size(124, 43);
            nMonth.TabIndex = 4;
            nMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nMonth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nMonth.ValueChanged += nMonth_ValueChanged;
            // 
            // nDay
            // 
            nDay.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nDay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nDay.Location = new System.Drawing.Point(173, 500);
            nDay.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nDay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nDay.Name = "nDay";
            nDay.Size = new System.Drawing.Size(124, 43);
            nDay.TabIndex = 5;
            nDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nDay.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nDay.ValueChanged += nDay_ValueChanged;
            // 
            // nHour
            // 
            nHour.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nHour.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nHour.Location = new System.Drawing.Point(28, 560);
            nHour.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            nHour.Name = "nHour";
            nHour.Size = new System.Drawing.Size(92, 43);
            nHour.TabIndex = 6;
            nHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nHour.ValueChanged += nHour_ValueChanged;
            // 
            // nMinute
            // 
            nMinute.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nMinute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nMinute.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nMinute.Location = new System.Drawing.Point(126, 560);
            nMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            nMinute.Name = "nMinute";
            nMinute.Size = new System.Drawing.Size(92, 43);
            nMinute.TabIndex = 7;
            nMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nMinute.ValueChanged += nMinute_ValueChanged;
            // 
            // nSecond
            // 
            nSecond.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nSecond.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nSecond.Location = new System.Drawing.Point(224, 560);
            nSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            nSecond.Name = "nSecond";
            nSecond.Size = new System.Drawing.Size(92, 43);
            nSecond.TabIndex = 8;
            nSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            nSecond.ValueChanged += nSecond_ValueChanged;
            // 
            // nJD
            // 
            nJD.BackColor = System.Drawing.SystemColors.InactiveCaption;
            nJD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            nJD.DecimalPlaces = 10;
            nJD.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            nJD.Location = new System.Drawing.Point(40, 688);
            nJD.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            nJD.Minimum = new decimal(new int[] { 1410065407, 2, 0, int.MinValue });
            nJD.Name = "nJD";
            nJD.Size = new System.Drawing.Size(255, 43);
            nJD.TabIndex = 9;
            nJD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            nJD.ValueChanged += nJD_ValueChanged;
            // 
            // frmCalendarTest
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            ClientSize = new System.Drawing.Size(1342, 1083);
            Controls.Add(nJD);
            Controls.Add(nSecond);
            Controls.Add(nMinute);
            Controls.Add(nHour);
            Controls.Add(nDay);
            Controls.Add(nMonth);
            Controls.Add(nYear);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "frmCalendarTest";
            Text = "frmCalendarTest";
            Load += frmCalendarTest_Load;
            ((System.ComponentModel.ISupportInitialize)nYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)nMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)nDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)nMinute).EndInit();
            ((System.ComponentModel.ISupportInitialize)nSecond).EndInit();
            ((System.ComponentModel.ISupportInitialize)nJD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown nYear;
        private System.Windows.Forms.NumericUpDown nMonth;
        private System.Windows.Forms.NumericUpDown nDay;
        private System.Windows.Forms.NumericUpDown nHour;
        private System.Windows.Forms.NumericUpDown nMinute;
        private System.Windows.Forms.NumericUpDown nSecond;
        private System.Windows.Forms.NumericUpDown nJD;
    }
}