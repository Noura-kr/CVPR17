using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicEyetrackingSample
{
    public partial class trainingForm : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        string left;
        string right;

        public trainingForm()
        { }
        public trainingForm(string rightImagePath, string leftImagePath)
        {
            this.left = leftImagePath;
            this.right = rightImagePath;
            timer.Interval = 6000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            InitializeComponent();
        }
        void timer_Tick(object sender, EventArgs e)
        {
           this.Close();

        }
        private void showtrainingImages()
        {
            Image rImg = Image.FromFile(this.right);
            Image lImg = Image.FromFile(this.left);
            this.rightPictureBox.Size = rImg.Size;
            this.leftPictureBox.Size = lImg.Size;
            this.rightPictureBox.Image = rImg;
            this.leftPictureBox.Image = lImg;
            this.leftPictureBox.Location = new Point((this.Size.Width / 2) - (this.Size.Width / 4) - (lImg.Size.Width / 2), (this.Size.Height / 2) - (lImg.Size.Height / 2));
            this.rightPictureBox.Location = new Point((this.Size.Width / 2) + (this.Size.Width / 4) - (rImg.Size.Width / 2), (this.Size.Height / 2) - (rImg.Size.Height / 2));

            this.trainingPanel.Show();

        }

        private void trainingForm_Load(object sender, EventArgs e)
        {
            showtrainingImages();
        }
    }
}
