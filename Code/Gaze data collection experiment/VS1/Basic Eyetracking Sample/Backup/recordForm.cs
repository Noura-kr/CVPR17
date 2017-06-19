using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicEyetrackingSample
{
    public partial class recordForm : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        string recImage;
        public recordForm()
        {
            InitializeComponent();
        }
        public recordForm(MainForm main, string imagePath)
        {
            this.recImage = imagePath;
            main.S = new Stopwatch();
            main.ConnectedTracker.StartTracking();
            main.IsTracking = true;
            main.S.Start();
            InitializeComponent();
        }
        private void showImageCollectGazeData()
        {
            Image img = Image.FromFile(this.recImage);
            this.recordPictureBox.Size = img.Size;
            this.recordPictureBox.Image = img;
            this.recordPictureBox.Location = new Point((this.Size.Width / 2) - (img.Size.Width / 2), (this.Size.Height / 2) - (img.Size.Height / 2));
        }

        private void recordForm_Load(object sender, EventArgs e)
        {
            showImageCollectGazeData();
        }

    }
}
