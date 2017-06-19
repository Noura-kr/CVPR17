using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicEyetrackingSample
{
    public partial class Questionnaire : Form
    {
        Guid userID;
        string logFileName = "../../questionnaireFiles/participants.csv";
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public Questionnaire()
        {
            InitializeComponent();
            this.userID= Guid.NewGuid();
            if (!File.Exists(logFileName))
            {
                string header = "UserUD" + "," + "First Name" + "," + "Last Name" + "," + "Age" + "," + "Gender" + "," + "Impaired Eyesight" + "," + "Impaired type" + "," + "Proffession" + "," + "Previous experience" + "," + "Tired" + "," + "Timestamp" + "\n";
                File.WriteAllText(logFileName, header);
            }
        }
        private void submit_Click(object sender, EventArgs e)
        {
            string log = this.userID.ToString()+ ",";
            log += textBoxFirstName.Text + "," + textBoxLastName.Text +"," + textBoxAge.Text + ",";
            if (radioButtonFemale.Checked)
                log += "Female" + ",";
            else if (radioButtonMale.Checked)
                log += "Male" + ",";
            else
                log += " ,";
            if (radioButtonNotImpaired.Checked)
                log += "No" + ",";
            else if (radioButtonImpaired.Checked)            
                log += "Yes" + ",";        
            else
                log += " ,";
            log += comboBoxEyeSight.Text + "," + textBoxProfession.Text + "," + comboBoxEyetrackerBefore.Text + "," + comboBoxTiredness.Text + ","+ DateTime.Now.ToString() + "\n";
            File.AppendAllText(logFileName, log);
            this.Close();
        }

        private void Questionnaire_Load(object sender, EventArgs e)
        {
            this.panel1.Location = new Point((this.Width/ 2) - (this.panel1.Size.Width / 2), ( this.Height / 2) - (this.panel1.Size.Height / 2));
        }
    }
}
