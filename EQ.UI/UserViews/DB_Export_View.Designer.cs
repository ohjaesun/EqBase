namespace EQ.UI.UserViews
{
    partial class DB_Export_View
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            _Button1 = new EQ.UI.Controls._Button();
            _Label1 = new EQ.UI.Controls._Label();
            _ListBox1 = new EQ.UI.Controls._ListBox();
            _Button2 = new EQ.UI.Controls._Button();
            _PanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // _Panel2
            // 
            _PanelMain.Controls.Add(_ListBox1);
            _PanelMain.Controls.Add(_Label1);
            _PanelMain.Controls.Add(_Button2);
            _PanelMain.Controls.Add(_Button1);
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(52, 73, 94);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(13, 6);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(100, 55);
            _Button1.TabIndex = 0;
            _Button1.Text = "DB 선택";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.FromArgb(149, 165, 166);
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(123, 34);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(64, 18);
            _Label1.TabIndex = 1;
            _Label1.Text = "_Label1";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _Label1.TooltipText = null;
            // 
            // _ListBox1
            // 
            _ListBox1.BackColor = Color.FromArgb(149, 165, 166);
            _ListBox1.DrawMode = DrawMode.OwnerDrawFixed;
            _ListBox1.Font = new Font("D2Coding", 12F);
            _ListBox1.ForeColor = Color.Black;
            _ListBox1.FormattingEnabled = true;
            _ListBox1.Location = new Point(13, 78);
            _ListBox1.Name = "_ListBox1";
            _ListBox1.Size = new Size(185, 180);
            _ListBox1.TabIndex = 2;
            _ListBox1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(52, 73, 94);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(214, 78);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(100, 55);
            _Button2.TabIndex = 0;
            _Button2.Text = "복원";
            _Button2.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button2_Click;
            // 
            // DB_Export_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "DB_Export_View";
            _PanelMain.ResumeLayout(false);
            _PanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls._ListBox _ListBox1;
        private Controls._Label _Label1;
        private Controls._Button _Button2;
        private Controls._Button _Button1;
    }
}
