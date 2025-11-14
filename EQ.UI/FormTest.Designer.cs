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
            userOption_View1 = new EQ.UI.UserViews.UserOption_View();
            SuspendLayout();
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.LightYellow;
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(36, 15);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(192, 18);
            _Label1.TabIndex = 0;
            _Label1.Text = "각종 기능들 테스트용 폼";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.DesignModeOnly;
            _Label1.TooltipText = null;
            // 
            // userOption_View1
            // 
            userOption_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            userOption_View1.Location = new Point(65, 64);
            userOption_View1.Margin = new Padding(3, 4, 3, 4);
            userOption_View1.Name = "userOption_View1";
            userOption_View1.Size = new Size(813, 525);
            userOption_View1.TabIndex = 1;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 872);
            Controls.Add(userOption_View1);
            Controls.Add(_Label1);
            Name = "FormTest";
            Text = "FormTest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls._Label _Label1;
        private UserViews.UserOption_View userOption_View1;
    }
}