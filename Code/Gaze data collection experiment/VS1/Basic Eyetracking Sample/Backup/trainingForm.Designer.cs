namespace BasicEyetrackingSample
{
    partial class trainingForm
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
            this.trainingPanel = new System.Windows.Forms.Panel();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.trainingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            this.SuspendLayout();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            // 
            // trainingPanel
            // 
            this.trainingPanel.Controls.Add(this.rightPictureBox);
            this.trainingPanel.Controls.Add(this.leftPictureBox);
            this.trainingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainingPanel.Location = new System.Drawing.Point(0, 0);
            this.trainingPanel.Name = "trainingPanel";
            this.trainingPanel.Size = new System.Drawing.Size(284, 262);
            this.trainingPanel.TabIndex = 7;
            this.trainingPanel.Visible = false;
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Location = new System.Drawing.Point(282, 186);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(100, 50);
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Location = new System.Drawing.Point(27, 81);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(100, 50);
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.TabStop = false;
            // 
            // trainingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.trainingPanel);
            this.Name = "trainingForm";
            this.Text = "Compare";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.trainingForm_Load);
            this.trainingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel trainingPanel;
        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.PictureBox leftPictureBox;
    }
}