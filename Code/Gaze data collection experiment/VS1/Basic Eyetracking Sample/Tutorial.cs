using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BasicEyetrackingSample
{
    public partial class Tutorial : Form
    {
        
        Timer timer = new Timer();
        int tick = 0;
        public Tutorial()
        {
            InitializeComponent(); 
            timer.Interval = 2000;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (tick)
            {                  
                case 0:
                    LabelInformation.Text = "This experiment study will take around one hour. \nBefore getting started, here is a brief description of the task";
                    break;
                case 1:
                    LabelInformation.Text = "First you will see a sequence of dots you have to focus on them";
                    setImg("../../tutorialPics/dot.png");
                    break;
                case 2:
                    LabelInformation.Text = "Then you will see two images of different types of birds (left & right) and you will have to distinguish the differences between them";
                    setImg("../../tutorialPics/training.png");
                    break;
                case 3:
                    LabelInformation.Text = "Again you will see a dot in the center of the screen, focus on it";
                    setImg("../../tutorialPics/dot.png");
                    break;
                case 4:
                    LabelInformation.Text = "Following you will be shown an image for a bird which you should guess to which of the previous classes it belongs (left or right)";
                    setImg("../../tutorialPics/record.jpg");
                    break;
                case 5:
                    LabelInformation.Text = "Enter your desicion by pressing left or right arrow ";
                    setImg("../../tutorialPics/keyboard.png");
                    break;
                case 6:
                    LabelInformation.Text = "Thank you for participating!";
                    setImg("../../tutorialPics/thankyou-1.png");
                    break;
                case 7:
                    this.Close();
                    break;
            }
            tick++;
        }

        private void Tutorial_Load(object sender, EventArgs e)
        {
            this.panel1.Location = new Point((this.Width / 2) - (this.panel1.Size.Width / 2), (this.Height / 2) - (this.panel1.Size.Height / 2));
        }

        private void setImg(string path)
        {
            Image img = Image.FromFile(path);
            pictureBox1.Size = img.Size;
            pictureBox1.Image = img;
            pictureBox1.Location = new Point((this.Width / 2) - (img.Size.Width / 2), (this.Height / 2) - (img.Size.Height / 2));
        }
        
    }
}
