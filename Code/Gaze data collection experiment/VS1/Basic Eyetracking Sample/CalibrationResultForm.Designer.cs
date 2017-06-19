using BasicEyetrackingSample;

namespace BasicEyetrackingSample
{
    partial class CalibrationResultForm
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
            this._leftPlot = new CalibrationResultPanel();
            this._rightPlot = new CalibrationResultPanel();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _leftPlot
            // 
            this._leftPlot.BackColor = System.Drawing.Color.WhiteSmoke;
            this._leftPlot.EyeOption = EyeOption.Left;
            this._leftPlot.Location = new System.Drawing.Point(12, 12);
            this._leftPlot.Name = "_leftPlot";
            this._leftPlot.Size = new System.Drawing.Size(366, 275);
            this._leftPlot.TabIndex = 0;
            this._leftPlot.Text = "calibrationResultPanel1";
            // 
            // _rightPlot
            // 
            this._rightPlot.BackColor = System.Drawing.Color.WhiteSmoke;
            this._rightPlot.EyeOption = EyeOption.Right;
            this._rightPlot.Location = new System.Drawing.Point(397, 12);
            this._rightPlot.Name = "_rightPlot";
            this._rightPlot.Size = new System.Drawing.Size(366, 275);
            this._rightPlot.TabIndex = 1;
            this._rightPlot.Text = "calibrationResultPanel2";
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(680, 310);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(82, 27);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // CalibrationResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 349);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._rightPlot);
            this.Controls.Add(this._leftPlot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalibrationResultForm";
            this.Text = "Calibration Result";
            this.ResumeLayout(false);

        }

        #endregion

        private CalibrationResultPanel _leftPlot;
        private CalibrationResultPanel _rightPlot;
        private System.Windows.Forms.Button _okButton;
    }
}