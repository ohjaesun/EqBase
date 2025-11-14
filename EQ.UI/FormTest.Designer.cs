namespace EQ.UI
{
    partial class FormTest
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
            _Label1 = new EQ.UI.Controls._Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            _Button5 = new EQ.UI.Controls._Button();
            _Button4 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button1 = new EQ.UI.Controls._Button();
            tabPage2 = new TabPage();
            recipe_View1 = new EQ.UI.UserViews.Recipe_View();
            tabPage3 = new TabPage();
            interlock_View1 = new EQ.UI.UserViews.Interlock_IO_View();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            _Button6 = new EQ.UI.Controls._Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage5.SuspendLayout();
            SuspendLayout();
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.LightYellow;
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(785, 19);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(192, 18);
            _Label1.TabIndex = 0;
            _Label1.Text = "각종 기능들 테스트용 폼";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.DesignModeOnly;
            _Label1.TooltipText = null;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(993, 872);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(_Button6);
            tabPage1.Controls.Add(_Button5);
            tabPage1.Controls.Add(_Button4);
            tabPage1.Controls.Add(_Button3);
            tabPage1.Controls.Add(_Button2);
            tabPage1.Controls.Add(_Button1);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(985, 841);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // _Button5
            // 
            _Button5.BackColor = Color.FromArgb(52, 73, 94);
            _Button5.Font = new Font("D2Coding", 12F);
            _Button5.ForeColor = Color.White;
            _Button5.Location = new Point(30, 118);
            _Button5.Name = "_Button5";
            _Button5.Size = new Size(100, 55);
            _Button5.TabIndex = 4;
            _Button5.Text = "로그인";
            _Button5.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button5.TooltipText = null;
            _Button5.UseVisualStyleBackColor = false;
            _Button5.Click += _Button5_Click;
            // 
            // _Button4
            // 
            _Button4.BackColor = Color.FromArgb(52, 73, 94);
            _Button4.Font = new Font("D2Coding", 12F);
            _Button4.ForeColor = Color.White;
            _Button4.Location = new Point(399, 25);
            _Button4.Name = "_Button4";
            _Button4.Size = new Size(100, 55);
            _Button4.TabIndex = 3;
            _Button4.Text = "타워램프";
            _Button4.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button4.TooltipText = null;
            _Button4.UseVisualStyleBackColor = false;
            _Button4.Click += _Button4_Click;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(52, 73, 94);
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.White;
            _Button3.Location = new Point(279, 25);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(100, 55);
            _Button3.TabIndex = 2;
            _Button3.Text = "액션 실행";
            _Button3.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            _Button3.Click += _Button3_Click;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(52, 73, 94);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(157, 25);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(100, 55);
            _Button2.TabIndex = 1;
            _Button2.Text = "시퀀스 실행";
            _Button2.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button2_Click;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(52, 73, 94);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(30, 25);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(100, 55);
            _Button1.TabIndex = 0;
            _Button1.Text = "팝업";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(recipe_View1);
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(985, 841);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // recipe_View1
            // 
            recipe_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            recipe_View1.Location = new Point(40, 17);
            recipe_View1.Margin = new Padding(3, 4, 3, 4);
            recipe_View1.Name = "recipe_View1";
            recipe_View1.Size = new Size(799, 484);
            recipe_View1.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(interlock_View1);
            tabPage3.Location = new Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(985, 841);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // interlock_View1
            // 
            interlock_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            interlock_View1.Location = new Point(192, 101);
            interlock_View1.Margin = new Padding(3, 4, 3, 4);
            interlock_View1.Name = "interlock_View1";
            interlock_View1.Size = new Size(478, 355);
            interlock_View1.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 27);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(985, 841);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(_Label1);
            tabPage5.Location = new Point(4, 27);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(985, 841);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "tabPage5";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // _Button6
            // 
            _Button6.BackColor = Color.FromArgb(52, 73, 94);
            _Button6.Font = new Font("D2Coding", 12F);
            _Button6.ForeColor = Color.White;
            _Button6.Location = new Point(30, 179);
            _Button6.Name = "_Button6";
            _Button6.Size = new Size(100, 55);
            _Button6.TabIndex = 5;
            _Button6.Text = "로그인등급";
            _Button6.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button6.TooltipText = null;
            _Button6.UseVisualStyleBackColor = false;
            _Button6.Click += _Button6_Click;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 872);
            Controls.Add(tabControl1);
            Name = "FormTest";
            Text = "FormTest";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls._Label _Label1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Controls._Button _Button1;
        private Controls._Button _Button2;
        private Controls._Button _Button3;
        private UserViews.Recipe_View recipe_View1;
        private UserViews.Interlock_IO_View interlock_View1;
        private Controls._Button _Button4;
        private Controls._Button _Button5;
        private Controls._Button _Button6;
    }
}