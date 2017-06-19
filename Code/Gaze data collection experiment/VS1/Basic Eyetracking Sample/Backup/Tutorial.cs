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
            test t = new test();
            t.create();
            InitializeComponent();
            timer.Interval = 3000;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Image img;
            switch (tick)
            {
                case 0:
                    LabelInformation.Text = "Before starting with the experiment you have to fill out some personal information";
                    break;
                case 1:
                    LabelInformation.Text = "The first step is to calibrate the tracking device by keeping your head still and look at the center of the dots (similar to this one) that will appear and disappear on the screen ";
                    img = Image.FromFile("../../tutorialPics/dot.png");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image= img;
                    break;
                case 2:
                    LabelInformation.Text = "The main study first step: you will see two images of birds from two different classes (left & right) and you will have 5 seconds to distinguish the differences between them";
                    img = Image.FromFile("../../tutorialPics/training.png");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image = img;
                    break;
                case 3:
                    LabelInformation.Text = "Again you will be asked to calibrate the tracker by looking at the center of the dot, while doing so keep you head still";
                    img = Image.FromFile("../../tutorialPics/dot.png");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image = img;
                    break;
                case 4:
                    LabelInformation.Text = "Following you will be shown a test image for 5 seconds, which you should guess to which class it belongs (left or right) by looking for differences that you have learned previously (Please keep your head still)";
                    img = Image.FromFile("../../tutorialPics/test.jpg");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image = img;
                    break;
                case 5:
                    LabelInformation.Text = "You can enter your desicion by pressing left arrow (to choose the left class) or right arrow (to choose the right class)";
                    img = Image.FromFile("../../tutorialPics/keyboard.png");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image = img;
                    break;
                case 6:
                    LabelInformation.Text = "After this you will repeat from the first step of the main study for quit some time";
                    img = Image.FromFile("../../tutorialPics/thankyou.jpg");
                    pictureBox1.Size = img.Size;
                    pictureBox1.Image = img;
                    break;

            }
            tick++;

        }

        
    }
}
