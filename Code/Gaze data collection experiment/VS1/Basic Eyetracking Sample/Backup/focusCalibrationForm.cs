using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    public partial class focusCalibrationForm : Form
    {
        public focusCalibrationForm()
        {
            timer.Interval = 3000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            InitializeComponent();
        }
        private const int PixelRadius = 22;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Point2D CalibrationPoint { get; set; }

        public Color PointColor { get; set; }

        public void DrawCalibrationPoint(Point2D point,Color color)
        {
            CalibrationPoint = point;
            PointColor = color;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw calibration circle
            Size monitorSize = Screen.PrimaryScreen.Bounds.Size;
            Rectangle circleBounds = new Rectangle();
            circleBounds.X = (int) (monitorSize.Width * CalibrationPoint.X - PixelRadius);
            circleBounds.Y = (int) (monitorSize.Height * CalibrationPoint.Y - PixelRadius);
            circleBounds.Width = 2*PixelRadius;
            circleBounds.Height = 2*PixelRadius;
      
            Rectangle smallCircleBounds = new Rectangle();
            smallCircleBounds.X = (int)(monitorSize.Width * CalibrationPoint.X - 1);
            smallCircleBounds.Y = (int)(monitorSize.Height * CalibrationPoint.Y - 1);
            smallCircleBounds.Width = 2;
            smallCircleBounds.Height = 2;

            using(var brush = new SolidBrush(PointColor))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Console.WriteLine("Drawing circle " + circleBounds + " color " + PointColor);
                e.Graphics.FillEllipse(brush, circleBounds);

                brush.Color = PointColor == Color.Transparent ? Color.Transparent : Color.Black;
                e.Graphics.FillEllipse(brush, smallCircleBounds);
            }
        }

        private void focusCalibrationForm_Load(object sender, EventArgs e)
        {
            DrawCalibrationPoint(new Point2D(0.5, 0.5), Color.Crimson);
        }
    }
}
