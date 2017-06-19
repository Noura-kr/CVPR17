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
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        string recImage;
        string correctClass;
        Point location;
        MainForm main;
        public static int correctScore;
        public static int wrongScore;
        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public recordForm(MainForm main, string imagePath, string realClass, int formWidth, int formHeight)
        {
            this.main = main;
            this.recImage = imagePath;
            this.correctClass = realClass;
            setImageLoc(formWidth, formHeight);
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            InitializeComponent();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void showImageCollectGazeData()
        {
            Image img = Image.FromFile(this.recImage);
            this.recordPictureBox.Size = img.Size;
            this.recordPictureBox.Image = img;
            this.recordPictureBox.Location = this.location;
        }

        private void recordForm_Load(object sender, EventArgs e)
        {
            showImageCollectGazeData();
        }

        private void recordForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.main.CurrentDataItem.Decision = e.KeyData.ToString();
            this.main.sw.Write(e.KeyData.ToString()+",");
            if (e.KeyData.ToString()==correctClass)
            {
                player.SoundLocation = @"..\..\sounds\correct.wav";
                player.Play();
                this.main.sw.Write("Correct \n");
                correctScore++;
                this.Close();
            }
            else 
            {
                player.SoundLocation = @"..\..\sounds\wrong.wav";
                player.Play();
                this.main.sw.Write("Wrong \n");
                wrongScore++;
                this.Close();
            }
                
        }
        private void setImageLoc(int W, int H)
        {
            Image img = Image.FromFile(this.recImage);
            this.location = new Point((W / 2) - (img.Size.Width / 2), (H / 2) - (img.Size.Height / 2));
        }
    }
}
