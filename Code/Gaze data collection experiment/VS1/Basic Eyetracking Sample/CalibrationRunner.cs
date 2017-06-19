using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    public class CalibrationRunner
    {
        private readonly CalibrationForm _calibrationForm;
        private readonly Timer _sleepTimer;
        private IEyeTracker _tracker;
        private Queue<Point2D> _calibrationPoints;
        private Calibration _calibrationResult;

        public CalibrationRunner()
        {
            _sleepTimer = new Timer();
            _sleepTimer.Interval = 1000;
            _sleepTimer.Tick += HandleTimerTick;

            _calibrationForm = new CalibrationForm();
            _calibrationForm.Load += CalibrationFormLoaded;
        }

        public Calibration RunCalibration(IEyeTracker tracker)
        {
            CreatePointList();

            try
            {
                _tracker = tracker;
                _tracker.ConnectionError += HandleConnectionError;
                _tracker.ComputeCalibrationCompleted += ComputeCompleted;
                _tracker.AddCalibrationPointCompleted += PointCompleted;

                // Inform the eyetracker that we want to run a calibration
                _tracker.StartCalibration();

                _calibrationForm.ClearCalibrationPoint();
                _calibrationForm.ShowDialog();
            }
            finally
            {
                // Inform the eyetracker that we have finished 
                // the calibration routine
                _tracker.StopCalibration();

                _tracker.ConnectionError -= HandleConnectionError;
                _tracker.ComputeCalibrationCompleted -= ComputeCompleted;
                _tracker.AddCalibrationPointCompleted -= PointCompleted;
                _tracker = null;
            }

            return _calibrationResult;
        }

        private void StartNextOrFinish()
        {
            if (_calibrationPoints.Count > 0)
            {
                var point = _calibrationPoints.Dequeue();
                _calibrationForm.DrawCalibrationPoint(point, Color.Navy);
                _sleepTimer.Start();
            }
            else
            {
                // Use the async version of ComputeCalibration since
                // this call takes some time
                _tracker.ComputeCalibrationAsync();
            }
        }

        private void HandleTimerTick(object sender, EventArgs e)
        {
            _sleepTimer.Stop();
            var point = _calibrationForm.CalibrationPoint;
            _tracker.AddCalibrationPointAsync(point);
        }

        private void PointCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _calibrationForm.ClearCalibrationPoint();

            StartNextOrFinish();
        }

        private void ComputeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _calibrationForm.Close();

            if (e.Error != null)
            {
                _calibrationResult = null;
            }
            else
            {
                _calibrationResult = _tracker.GetCalibration();
            }
        }

        private void CreatePointList()
        {
            _calibrationPoints = new Queue<Point2D>();
            _calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
            _calibrationPoints.Enqueue(new Point2D(0.5, 0.5));
            _calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
            _calibrationPoints.Enqueue(new Point2D(0.9, 0.9));
            _calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
        }

        private void CalibrationFormLoaded(object sender, EventArgs e)
        {
            StartNextOrFinish();
        }

        private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            // Abort calibration if the connection fails
            AbortCalibration();
        }

        private void AbortCalibration()
        {
            _calibrationResult = null;
            _sleepTimer.Stop();
            _calibrationForm.Close();
        }
    }
}
