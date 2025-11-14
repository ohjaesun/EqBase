
namespace EQ.UI
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            statusStrip1 = new StatusStrip();
            _Panel1 = new EQ.UI.Controls._Panel();
            _Label_Title = new EQ.UI.Controls._Label();
            _Panel2 = new EQ.UI.Controls._Panel();
            _Panel3 = new EQ.UI.Controls._Panel();
            panelMain = new EQ.UI.Controls._Panel();
            _Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(985, 13);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(86, 28);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 1002);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1083, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Controls.Add(_Label_Title);
            _Panel1.Controls.Add(button1);
            _Panel1.Dock = DockStyle.Top;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 0);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(1083, 65);
            _Panel1.TabIndex = 2;
            // 
            // _Label_Title
            // 
            _Label_Title.AutoSize = true;
            _Label_Title.BackColor = Color.FromArgb(149, 165, 166);
            _Label_Title.Font = new Font("D2Coding", 12F);
            _Label_Title.ForeColor = Color.Black;
            _Label_Title.Location = new Point(12, 13);
            _Label_Title.Name = "_Label_Title";
            _Label_Title.Size = new Size(64, 18);
            _Label_Title.TabIndex = 1;
            _Label_Title.Text = "_Label1";
            _Label_Title.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _Label_Title.TooltipText = null;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Dock = DockStyle.Bottom;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 937);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(1083, 65);
            _Panel2.TabIndex = 3;
            // 
            // _Panel3
            // 
            _Panel3.BackColor = SystemColors.Control;
            _Panel3.Dock = DockStyle.Right;
            _Panel3.ForeColor = SystemColors.ControlText;
            _Panel3.Location = new Point(993, 65);
            _Panel3.Name = "_Panel3";
            _Panel3.Size = new Size(90, 872);
            _Panel3.TabIndex = 4;
            // 
            // panelMain
            // 
            panelMain.BackColor = SystemColors.Control;
            panelMain.Dock = DockStyle.Fill;
            panelMain.ForeColor = SystemColors.ControlText;
            panelMain.Location = new Point(0, 65);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(993, 872);
            panelMain.TabIndex = 5;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 1024);
            Controls.Add(panelMain);
            Controls.Add(_Panel3);
            Controls.Add(_Panel2);
            Controls.Add(_Panel1);
            Controls.Add(statusStrip1);
            Margin = new Padding(3, 5, 3, 5);
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            Shown += FormMain_Shown;
            _Panel1.ResumeLayout(false);
            _Panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Button button1;
        private StatusStrip statusStrip1;
        private Controls._Panel _Panel1;
        private Controls._Panel _Panel2;
        private Controls._Panel _Panel3;
        private Controls._Panel panelMain;
        private Controls._Label _Label_Title;
    }
}
