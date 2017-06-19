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
        string recorded = "";
        Stopwatch s = new Stopwatch();

        public Stopwatch S
        {
            get { return s; }
            set { s = value; }
        }
        StreamWriter sw = new StreamWriter("log.csv");
        System.Windows.Forms.Timer trainingTimer = new System.Windows.Forms.Timer();
        string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        List<dataItem> dataItems = new List<dataItem>();
        dataItem currentDataItem;

        internal List<dataItem> DataItems
        {
            get
            {
                return dataItems;
            }

            set
            {
                dataItems = value;
            }
        }

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
        public MainForm()
        {
            InitializeComponent();

            _clock = new Clock();

            _trackerBrowser = new EyeTrackerBrowser();
            _trackerBrowser.EyeTrackerFound += EyetrackerFound;
            _trackerBrowser.EyeTrackerUpdated += EyetrackerFound;
            _trackerBrowser.EyeTrackerRemoved += EyetrackerRemoved;
        }

        private void EyetrackerFound(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker is found on the network we set tracker
            tracker = e.EyeTrackerInfo;
            ConnectToTracker(tracker);
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
            _trackerBrowser.StartBrowsing();
            
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
            if (s.Elapsed < TimeSpan.FromSeconds(5))
            {
                currentDataItem.GazeData.Add(e.GazeDataItem);
                currentDataItem.Timestamp.Add(DateTime.Now);
                
            }
            else
            {
                _connectedTracker.StopTracking();
                _isTracking = false;
                rc.Close();
            }
        }

        private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            // If the connection goes down we dispose 
            // the IAsyncEyetracker instance. This will release 
            // all resources held by the connection
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




        recordForm rc = new recordForm();
        private void _calibrateButton_Click(object sender, EventArgs e)
        {
            var runner = new CalibrationRunner();
            
            try
            {
                var result = runner.RunCalibration(_connectedTracker);

                // if everything went OK start training
                if (result != null)
                {
                    test t = new test();
                    //get experiment stucture
                    experiment ex = t.create();
                    //loop
                    for (int i = 0; i < ex.Compare.Count; i++) {
                        currentDataItem = new dataItem(Guid.NewGuid());
                        currentDataItem.RightClass = ex.Compare[i][0];
                        currentDataItem.LeftClass = ex.Compare[i][1];
                        currentDataItem.ImgPath = ex.Record[i];
                        trainingForm f = new trainingForm(ex.Compare[i][0], ex.Compare[i][1]);
                        f.ShowDialog();
                        focusCalibrationForm focusCal = new focusCalibrationForm();
                        focusCal.ShowDialog();
                        rc = new recordForm(this, ex.Record[i]);
                        rc.ShowDialog();
                        this.dataItems.Add(currentDataItem);
                    }
                    writeGazeExcel();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not enough data to create a calibration (or calibration aborted).");
                }                
            }
            catch(EyeTrackerException ee)
            {
                MessageBox.Show("Failed to calibrate. Got exception " + ee, 
                    "Calibration Failed", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }            
        }
        

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyData.ToString());
        }

        private void writeGazeExcel() {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add("hi"));

                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                int rowCount = 1;
                for (int i = 0; i < this.dataItems.Count; i++)
                {
                    for (int j = 0; j < this.dataItems[i].GazeData.Count; j++)
                    {

                        oSheet.Cells[rowCount, 1] = this.dataItems[i].ParticipantID;
                        oSheet.Cells[rowCount, 2] = this.dataItems[i].ImgPath;
                        oSheet.Cells[rowCount, 3] = this.dataItems[i].GazeData[j].LeftGazePoint2D.X;
                        oSheet.Cells[rowCount, 4] = this.dataItems[i].GazeData[j].LeftGazePoint2D.Y;
                        oSheet.Cells[rowCount, 5] = this.dataItems[i].GazeData[j].RightGazePoint2D.X;
                        oSheet.Cells[rowCount, 6] = this.dataItems[i].GazeData[j].RightGazePoint2D.Y;
                        oSheet.Cells[rowCount, 7] = this.dataItems[i].GazeData[j].RightGazePoint2D.Y;
                        oSheet.Cells[rowCount, 8] = this.dataItems[i].Timestamp[j].ToString();
                        oSheet.Cells[rowCount, 9] = this.dataItems[i].RightClass;
                        oSheet.Cells[rowCount, 10] = this.dataItems[i].LeftClass;
                        oSheet.Cells[rowCount, 11] = this.dataItems[i].Decision;
                        rowCount++;
                    }
                }

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("test505.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
                MessageBox.Show(this.dataItems.Count.ToString());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
