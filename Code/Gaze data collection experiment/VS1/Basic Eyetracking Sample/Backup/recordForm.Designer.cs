namespace BasicEyetrackingSample
{
    partial class recordForm
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
            this.recordPanel = new System.Windows.Forms.Panel();
            this.recordPictureBox = new System.Windows.Forms.PictureBox();
            this.recordPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recordPictureBox)).BeginInit();
            this.SuspendLayout();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            // 
            // recordPanel
            // 
            this.recordPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.recordPanel.Controls.Add(this.recordPictureBox);
            this.recordPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordPanel.Location = new System.Drawing.Point(0, 0);
            this.recordPanel.Name = "recordPanel";
            this.recordPanel.Size = new System.Drawing.Size(284, 262);
            this.recordPanel.TabIndex = 6;
            this.recordPanel.Visible = true;
            // 
            // recordPictureBox
            // 
            this.recordPictureBox.Location = new System.Drawing.Point(46, 36);
            this.recordPictureBox.Name = "recordPictureBox";
            this.recordPictureBox.Size = new System.Drawing.Size(183, 200);
            this.recordPictureBox.TabIndex = 5;
            this.recordPictureBox.TabStop = false;
            // 
            // recordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.recordPanel);
            this.Name = "recordForm";
            this.Text = "recordForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.recordForm_Load);
            this.recordPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.recordPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel recordPanel;
        private System.Windows.Forms.PictureBox recordPictureBox;
    }
}