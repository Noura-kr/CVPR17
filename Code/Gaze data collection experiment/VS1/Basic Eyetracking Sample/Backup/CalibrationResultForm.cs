using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    public partial class CalibrationResultForm : Form
    {
        public CalibrationResultForm()
        {
            InitializeComponent();
        }

        public void SetPlotData(Tobii.EyeTracking.IO.Calibration calibration)
        {
            _leftPlot.Initialize(calibration.Plot);
            _rightPlot.Initialize(calibration.Plot);
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}