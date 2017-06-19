using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;



using Microsoft.Office.Interop.Excel;

using Microsoft.CSharp.RuntimeBinder;


namespace BasicEyetrackingSample
{
    public partial class MainForm : Form
    {
        private readonly EyeTrackerBrowser _trackerBrowser;
        private readonly Clock _clock;

        private IEyeTracker _connectedTracker;

        public IEyeTracker ConnectedTracker
        {
            get { return _connectedTracker; }
            set { _connectedTracker = value; }
        }
        private ISyncManager _syncManager; 
        private string _connectionName;
        private bool _isTracking;

        public bool IsTracking
        {
            get { return _isTracking; }
            set { _isTracking = value; }
        }

        EyeTrackerInfo tracker;
        string type;
        public StreamWriter sw;
        System.Windows.Forms.Timer trainingTimer = new System.Windows.Forms.Timer();
        Guid userID;

        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        dataItem currentDataItem;

        internal dataItem CurrentDataItem
        {
            get
            {
                return currentDataItem;
            }

            set
            {
                currentDataItem = value;
            }
        }

        public MainForm(string t)
        {
            this.type = t;
            Questionnaire q = new Questionnaire();
            q.ShowDialog();
            userID = q.UserID;
            //Tutorial t = new Tutorial();
            //t.ShowDialog();
            InitializeComponent();
            _clock = new Clock();

            _trackerBrowser = new EyeTrackerBrowser();
            _trackerBrowser.EyeTrackerFound += EyetrackerFound;
            _trackerBrowser.EyeTrackerUpdated += EyetrackerFound;
            _trackerBrowser.EyeTrackerRemoved += EyetrackerRemoved;

            sw = new StreamWriter("../../gazeLogs/"+this.type+"-"+"gazeLog" + userID.ToString() + ".csv"); 
        }

        private void EyetrackerFound(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker is found on the network we set tracker
            tracker = e.EyeTrackerInfo;
            ConnectToTracker(tracker);
            start();
        }

        private void EyetrackerRemoved(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker disappears from the network we unset tracker
            tracker = null;
            DisconnectTracker();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Start browsing for eyetrackers on the network
            this.panel1.Location = new System.Drawing.Point((this.Width / 2) - (this.panel1.Size.Width / 2), (this.Height / 2) - (this.panel1.Size.Height / 2));
            _trackerBrowser.StartBrowsing();
            //
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Shutdown browser service
            _trackerBrowser.StopBrowsing();
            // Cleanup connections
            DisconnectTracker();
        }

        private void ConnectToTracker(EyeTrackerInfo info)
        {
            try
            {
                _connectedTracker = info.Factory.CreateEyeTracker();
                _connectedTracker.ConnectionError += HandleConnectionError;
                _connectionName = info.ProductId;

                _syncManager = info.Factory.CreateSyncManager(_clock);

                _connectedTracker.GazeDataReceived += _connectedTracker_GazeDataReceived;
            }
            catch (EyeTrackerException ee)
            {
                if(ee.ErrorCode == 0x20000402)
                {
                    MessageBox.Show("Failed to upgrade protocol. " + 
                        "This probably means that the firmware needs" +
                        " to be upgraded to a version that supports the new sdk.","Upgrade Failed",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Eyetracker responded with error " + ee, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }

                DisconnectTracker();
            }
            catch(Exception)
            {
                MessageBox.Show("Could not connect to eyetracker.","Connection Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                DisconnectTracker();
            }

            
        }

        private void _connectedTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            if (currentDataItem != null)
            {
                string s1 = e.GazeDataItem.LeftEyePosition3D.X.ToString();
                string s2 = e.GazeDataItem.LeftEyePosition3D.Y.ToString();
                string s3 = e.GazeDataItem.LeftEyePosition3D.Z.ToString();

                string s4 = e.GazeDataItem.LeftEyePosition3DRelative.X.ToString();
                string s5 = e.GazeDataItem.LeftEyePosition3DRelative.Y.ToString();
                string s6 = e.GazeDataItem.LeftEyePosition3DRelative.Z.ToString();

                string s7 = e.GazeDataItem.LeftGazePoint2D.X.ToString();
                string s8 = e.GazeDataItem.LeftGazePoint2D.Y.ToString();

                string s9 = e.GazeDataItem.LeftGazePoint3D.X.ToString();
                string s10 = e.GazeDataItem.LeftGazePoint3D.Y.ToString();
                string s11 = e.GazeDataItem.LeftGazePoint3D.Z.ToString();

                string s12 = e.GazeDataItem.LeftPupilDiameter.ToString();
                string s13 = e.GazeDataItem.LeftValidity.ToString();

                s1 = s1.Replace(',', '.');
                s2 = s2.Replace(',', '.');
                s3 = s3.Replace(',', '.');
                s4 = s4.Replace(',', '.');
                s5 = s5.Replace(',', '.');
                s6 = s6.Replace(',', '.');
                s7 = s7.Replace(',', '.');
                s8 = s8.Replace(',', '.');
                s9 = s9.Replace(',', '.');
                s10 = s10.Replace(',', '.');
                s11 = s11.Replace(',', '.');
                s12 = s12.Replace(',', '.');
                s13 = s13.Replace(',', '.');

                string s14 = e.GazeDataItem.RightEyePosition3D.X.ToString();
                string s15 = e.GazeDataItem.RightEyePosition3D.Y.ToString();
                string s16 = e.GazeDataItem.RightEyePosition3D.Z.ToString();

                string s17 = e.GazeDataItem.RightEyePosition3DRelative.X.ToString();
                string s18 = e.GazeDataItem.RightEyePosition3DRelative.Y.ToString();
                string s19 = e.GazeDataItem.RightEyePosition3DRelative.Z.ToString();

                string s20 = e.GazeDataItem.RightGazePoint2D.X.ToString();
                string s21 = e.GazeDataItem.RightGazePoint2D.Y.ToString();

                string s22 = e.GazeDataItem.RightGazePoint3D.X.ToString();
                string s23 = e.GazeDataItem.RightGazePoint3D.Y.ToString();
                string s24 = e.GazeDataItem.RightGazePoint3D.Z.ToString();

                string s25 = e.GazeDataItem.RightPupilDiameter.ToString();
                string s26 = e.GazeDataItem.RightValidity.ToString();

                s14 = s14.Replace(',', '.');
                s15 = s15.Replace(',', '.');
                s16 = s16.Replace(',', '.');
                s17 = s17.Replace(',', '.');
                s18 = s18.Replace(',', '.');
                s19 = s19.Replace(',', '.');
                s20 = s20.Replace(',', '.');
                s21 = s21.Replace(',', '.');
                s22 = s22.Replace(',', '.');
                s23 = s23.Replace(',', '.');
                s24 = s24.Replace(',', '.');
                s25 = s25.Replace(',', '.');
                s26 = s26.Replace(',', '.');

                sw.Write(currentDataItem.State + ",");
                sw.Write(DateTime.Now.ToString("hh.mm.ss.ffffff") + ",");
                sw.Write(s1 + "," + s2 + "," + s3 + "," + s4 + "," + s5 + "," + s6 + "," + s7 + "," + s8 + "," + s9 + "," + s10 + "," + s11 + "," + s12 + "," + s13 + ",");
                sw.Write(s14 + "," + s15 + "," + s16 + "," + s17 + "," + s18 + "," + s19 + "," + s20 + "," + s21 + "," + s22 + "," + s23 + "," + s24 + "," + s25 + "," + s26 + ",");
                sw.Write(currentDataItem.getLog() + "\n");
            }
        }

        private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            DisconnectTracker();
        }

        private void DisconnectTracker()
        {
            if(_connectedTracker != null)
            {
                _connectedTracker.GazeDataReceived -= _connectedTracker_GazeDataReceived;
                _connectedTracker.Dispose();
                _connectedTracker = null;
                _connectionName = string.Empty;
                _isTracking = false;
                _syncManager.Dispose();
            }
        }


        private void start()
        {
            var runner = new CalibrationRunner();
            bool calibratedSuccessfully = false;

            while (!calibratedSuccessfully)
            {
                try
                {
                    var result = runner.RunCalibration(_connectedTracker);

                    // if everything went OK start training
                    if (result != null)
                    {
                        calibratedSuccessfully = true;
                        experimentRunner t = new experimentRunner();
                        //get experiment stucture
                        experiment ex = t.create(this.type);
                        //loop
                        if (ex != null)
                        {
                            this.IsTracking = true;
                            this.ConnectedTracker.StartTracking();
                            int correctLabelPos = 0;
                            string correctClass ="";
                            Random rand = new Random();
                            for (int i = 0; i < ex.Compare.Count; i++)
                            //for (int i = 0; i < 3; i++)
                            {
                                trainingForm f = null;
                                correctLabelPos = rand.Next(2);
                                if (correctLabelPos==0)
                                {
                                    f = new trainingForm(ex.Compare[i][0], ex.Compare[i][1], Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                                    correctClass = "Right";
                                    currentDataItem = new dataItem(ex.Compare[i][0], ex.Compare[i][1], f.LocationRight, f.LocationLeft);
                                }
                                else
                                {
                                    f = new trainingForm(ex.Compare[i][1], ex.Compare[i][0], Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                                    correctClass = "Left";
                                    currentDataItem = new dataItem(ex.Compare[i][1], ex.Compare[i][0], f.LocationRight, f.LocationLeft);
                                }
                                f.ShowDialog();
                                focusCalibrationForm focusCal = new focusCalibrationForm();
                                currentDataItem = new dataItem();
                                focusCal.ShowDialog();
                                recordForm rc = new recordForm(this, ex.Record[i], correctClass, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                                currentDataItem = new dataItem(ex.Compare[i][0], ex.Compare[i][1], rc.Location, ex.Record[i]);
                                rc.ShowDialog();
                            }
                            sw.Flush();                            
                        }
                        this.resultScoresLabel.Text = "Correct guess= " + recordForm.correctScore + ", " + "Wrong guess= " + recordForm.wrongScore;
                        this.close.Visible = true;
                        
                    }
                    else
                    {
                        MessageBox.Show("Calibration failed! please try again (focus on the dots)");
                    }
                }
                catch (EyeTrackerException ee)
                {
                    MessageBox.Show("Failed to calibrate. Got exception " + ee,
                        "Calibration Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
