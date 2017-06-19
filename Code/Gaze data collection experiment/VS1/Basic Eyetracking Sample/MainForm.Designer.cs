namespace BasicEyetrackingSample
{
    partial class MainForm
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
            this._calibrateButton = new System.Windows.Forms.Button();
            this.resultScoresLabel = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _calibrateButton
            // 
            this._calibrateButton.Location = new System.Drawing.Point(12, 12);
            this._calibrateButton.Name = "_calibrateButton";
            this._calibrateButton.Size = new System.Drawing.Size(111, 27);
            this._calibrateButton.TabIndex = 2;
            this._calibrateButton.Text = "Run Calibration";
            this._calibrateButton.UseVisualStyleBackColor = true;
            this._calibrateButton.Visible = false;
            // 
            // resultScoresLabel
            // 
            this.resultScoresLabel.AutoSize = true;
            this.resultScoresLabel.Font = new System.Drawing.Font("Garamond", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultScoresLabel.Location = new System.Drawing.Point(23, 30);
            this.resultScoresLabel.Name = "resultScoresLabel";
            this.resultScoresLabel.Size = new System.Drawing.Size(0, 35);
            this.resultScoresLabel.TabIndex = 3;
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("Garamond", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(208, 123);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(136, 40);
            this.close.TabIndex = 4;
            this.close.Text = "Ok!";
            this.close.UseVisualStyleBackColor = true;
            this.close.Visible = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.close);
            this.panel1.Controls.Add(this.resultScoresLabel);
            this.panel1.Location = new System.Drawing.Point(143, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 195);
            this.panel1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(728, 447);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._calibrateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SDK - Basic Eyetracking Sample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _calibrateButton;
        private System.Windows.Forms.Label resultScoresLabel;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Panel panel1;


    }
}

