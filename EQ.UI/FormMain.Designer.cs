
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
            iO_View1 = new EQ.UI.UserViews.IO_View();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // iO_View1
            // 
            iO_View1.Dock = DockStyle.Bottom;
            iO_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            iO_View1.Location = new Point(0, 56);
            iO_View1.Margin = new Padding(3, 4, 3, 4);
            iO_View1.Name = "iO_View1";
            iO_View1.Size = new Size(922, 442);
            iO_View1.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 498);
            Controls.Add(iO_View1);
            Controls.Add(button1);
            Name = "FormMain";
            Text = "Form1";
            ResumeLayout(false);
        }



        #endregion

        private Button button1;
        private UserViews.IO_View iO_View1;
    }
}
