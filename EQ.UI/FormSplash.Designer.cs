namespace EQ.UI
{
    partial class FormSplash
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
            lblStatus = new Label();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(56, 103);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(56, 18);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "label1";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(56, 176);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(453, 254);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // FormSplash
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 540);
            Controls.Add(richTextBox1);
            Controls.Add(lblStatus);
            Margin = new Padding(3, 5, 3, 5);
            Name = "FormSplash";
            Text = "FormSplash";
            Shown += FormSplash_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private RichTextBox richTextBox1;
    }
}