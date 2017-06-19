using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BasicEyetrackingSample
{
    public partial class FrameRateDialog : Form
    {

        public FrameRateDialog(IList<float> allFrameRates, int currentFrameRateIndex)
        {
            InitializeComponent();

            foreach (var f in allFrameRates)
            {
                _fpsCombo.Items.Add(f);
            }

            _fpsCombo.SelectedItem = allFrameRates[currentFrameRateIndex];
            _fpsCombo.SelectedIndex = currentFrameRateIndex;
        }

        public float CurrentFrameRate
        {
            get
            {
                return (float) _fpsCombo.SelectedItem;
            }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
