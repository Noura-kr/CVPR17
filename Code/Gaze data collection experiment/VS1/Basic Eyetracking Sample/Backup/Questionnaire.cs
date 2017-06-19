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
        string userID;
        public Questionnaire()
        {
            InitializeComponent();
            this.userID= Guid.NewGuid().ToString();
        }
        private void submit_Click(object sender, EventArgs e)
        {
            
            string logFileName = "../../questionnaireFiles/users"+userID+".csv";
            //StreamWriter sw = new StreamWriter(logFileName);
            string log = "First Name: " + textBoxFirstName.Text + "\n";
            log += "Last Name: " + textBoxLastName.Text + "\n";
            log += "Age: " + textBoxAge.Text + "\n";
            if (radioButtonFemale.Checked)
                log += "Gender: Female" + "\n";
            else if (radioButtonMale.Checked)
                log += "Gender: Male" + "\n";
            if (radioButtonNotImpaired.Checked)
                log += "Impaired Eyesight: No" + "\n";
            else if (radioButtonImpaired.Checked)
            {
                log += "Impaired: " + comboBoxEyeSight.Text + "\n";
            }
            log += "Tired: " + comboBoxTiredness.Text + "\n";
            log += "Eye sight: " + comboBoxEyetrackerBefore.Text + "\n";
            log += "TimeStamp: " + DateTime.Now.ToString() + "\n";

            File.WriteAllText(logFileName,log);
            this.Close();
        }
    }
}
