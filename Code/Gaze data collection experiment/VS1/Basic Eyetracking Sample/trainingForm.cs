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
        Point locationRight;
        Point locationLeft;

        public Point LocationRight
        {
            get
            {
                return locationRight;
            }

            set
            {
                locationRight = value;
            }
        }

        public Point LocationLeft
        {
            get
            {
                return locationLeft;
            }

            set
            {
                locationLeft = value;
            }
        }

        
        public trainingForm(string rightImagePath, string leftImagePath, int formWidth, int formHeight)
        {
            this.left = leftImagePath;
            this.right = rightImagePath;
            setImagesLoc(formWidth, formHeight);
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
            this.leftPictureBox.Location = this.locationLeft;
            this.rightPictureBox.Location = this.locationRight;

            this.trainingPanel.Show();
        }

        private void trainingForm_Load(object sender, EventArgs e)
        {
            showtrainingImages();
        }
        private void setImagesLoc(int W, int H)
        {
            Image rImg = Image.FromFile(this.right);
            Image lImg = Image.FromFile(this.left);
            this.locationLeft = new Point((W / 2) - (W / 8) - (lImg.Size.Width / 2), (H / 2) - (lImg.Size.Height / 2));
            this.locationRight = new Point((W / 2) + (W / 8) - (rImg.Size.Width / 2), (H / 2) - (rImg.Size.Height / 2));
        }
    }
}
