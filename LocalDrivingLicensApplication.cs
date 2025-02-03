using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class LocalDrivingLicensApplication : Form
    {
        public LocalDrivingLicensApplication()
        {
            InitializeComponent();
            createColumns();
            assginValuesToDataGridView();
        }
        private void createColumns()
        {
            // Create a new column1
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "L.D.AppID"; // The name for identifying the column1 programmatically
            column.HeaderText = "L.D.AppID"; // The text displayed in the column1 header
            dataGridView1.Columns.Add(column);

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "DrivingClass"; 
            column1.HeaderText = "DrivingClass";
            column1.Width = 250;
            dataGridView1.Columns.Add(column1);

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "NationalNo";
            column2.HeaderText = "NationalNo";
            dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "FullName";
            column3.HeaderText = "FullName";
            column3.Width = 250;
            dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "ApplicationDate";
            column4.HeaderText = "ApplicationDate";
            column4.Width = 150;
            dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "PassedTests";
            column5.HeaderText = "PassedTests";
            dataGridView1.Columns.Add(column5);

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.Name = "Status";
            column6.HeaderText = "Status";
            dataGridView1.Columns.Add(column6);
           

        }

        private void assginValuesToDataGridView()
        {
            DataTable dt = Business.GetLDLApplicationInfo();

            foreach (DataRow row in dt.Rows)
            {
                int LDLAppID = (int)row["LocalDrivingLicenseApplicationID"];
                string DrivingClassName = row["ClassName"].ToString();
                string NationalNo = Business.GetNationalNumberByPersonID((int)row["ApplicantPersonID"]);
                string FullName = Business.GetPersonFullNameByID((int)row["ApplicantPersonID"]);
                DateTime dateTime = (DateTime)row["ApplicationDate"];
                string ApplicationStatus="";
                byte intApplicatoinStatus = (byte)row["ApplicationStatus"];
                if (intApplicatoinStatus == 1)ApplicationStatus = "New";
                if (intApplicatoinStatus == 2)ApplicationStatus = "Canceled";
                if (intApplicatoinStatus == 3)ApplicationStatus = "Completed";
                int NumberOfPassedTests = Business.GetNumberOfPassedTests(LDLAppID);

                dataGridView1.Rows.Add(LDLAppID, DrivingClassName, NationalNo, FullName, dateTime, NumberOfPassedTests, ApplicationStatus);
            }
        }
        private void assginValuesToDataGridView(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = Business.GetLDLApplicationInfo();

            foreach (DataRow row in dt.Rows)
            {
                int LDLAppID = (int)row["LocalDrivingLicenseApplicationID"];
                string DrivingClassName = row["ClassName"].ToString();
                string NationalNo = Business.GetNationalNumberByPersonID((int)row["ApplicantPersonID"]);
                string FullName = Business.GetPersonFullNameByID((int)row["ApplicantPersonID"]);
                DateTime dateTime = (DateTime)row["ApplicationDate"];
                string ApplicationStatus = "";
                byte intApplicatoinStatus = (byte)row["ApplicationStatus"];
                if (intApplicatoinStatus == 1) ApplicationStatus = "New";
                if (intApplicatoinStatus == 2) ApplicationStatus = "Canceld";
                if (intApplicatoinStatus == 3) ApplicationStatus = "Completed";
                int NumberOfPassedTests = Business.GetNumberOfPassedTests(LDLAppID);
                if (LocalDrivingLicenseApplicationID == LDLAppID)
                {
                    dataGridView1.Rows.Add(LDLAppID, DrivingClassName, NationalNo, FullName, dateTime, NumberOfPassedTests, ApplicationStatus);
                }
            }
        }
        private void assginValuesToDataGridView(string NationalNo)
        {
            DataTable dt = Business.GetLDLApplicationInfo();

            foreach (DataRow row in dt.Rows)
            {
                int LDLAppID = (int)row["LocalDrivingLicenseApplicationID"];
                string DrivingClassName = row["ClassName"].ToString();
                string NationalNo1 = Business.GetNationalNumberByPersonID((int)row["ApplicantPersonID"]);
                string FullName = Business.GetPersonFullNameByID((int)row["ApplicantPersonID"]);
                DateTime dateTime = (DateTime)row["ApplicationDate"];
                string ApplicationStatus = "";
                byte intApplicatoinStatus = (byte)row["ApplicationStatus"];
                if (intApplicatoinStatus == 1) ApplicationStatus = "New";
                if (intApplicatoinStatus == 2) ApplicationStatus = "Canceld";
                if (intApplicatoinStatus == 3) ApplicationStatus = "Completed";
                int NumberOfPassedTests = Business.GetNumberOfPassedTests(LDLAppID);
                if (NationalNo == NationalNo1)
                {
                    dataGridView1.Rows.Add(LDLAppID, DrivingClassName, NationalNo1, FullName, dateTime, NumberOfPassedTests, ApplicationStatus);
                }
            }
        }
        private void assginValuesToDataGridView(string FullName,int a=-1)
        {
            DataTable dt = Business.GetLDLApplicationInfo();

            foreach (DataRow row in dt.Rows)
            {
                int LDLAppID = (int)row["LocalDrivingLicenseApplicationID"];
                string DrivingClassName = row["ClassName"].ToString();
                string NationalNo1 = Business.GetNationalNumberByPersonID((int)row["ApplicantPersonID"]);
                string FullName1 = Business.GetPersonFullNameByID((int)row["ApplicantPersonID"]);
                DateTime dateTime = (DateTime)row["ApplicationDate"];
                string ApplicationStatus = "";
                byte intApplicatoinStatus = (byte)row["ApplicationStatus"];
                if (intApplicatoinStatus == 1) ApplicationStatus = "New";
                if (intApplicatoinStatus == 2) ApplicationStatus = "Canceld";
                if (intApplicatoinStatus == 3) ApplicationStatus = "Completed";
                int NumberOfPassedTests = Business.GetNumberOfPassedTests(LDLAppID);
                if (FullName == FullName1)
                {
                    dataGridView1.Rows.Add(LDLAppID, DrivingClassName, NationalNo1, FullName1, dateTime, NumberOfPassedTests, ApplicationStatus);
                }
            }
        }
        private void LocalDrivingLicensApplication_Load(object sender, EventArgs e)
        {
         
           

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_TabIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void _RefreashDataGridView()
        {
            dataGridView1.Rows.Clear();
            assginValuesToDataGridView();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                textBox1.Visible = true;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "";
                textBox1.Visible = false;
                _RefreashDataGridView();
            }
        }    


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
               
                case 1:
                    dataGridView1.Rows.Clear();
                    int LdlappID;
                    if (int.TryParse(textBox1.Text, out LdlappID))
                    {

                     
                    }
                    else
                    {

                        MessageBox.Show("Invalid input. Please enter a valid number.");
                    }

                    assginValuesToDataGridView(LdlappID);
                    break;
                case 2:
                    dataGridView1.Rows.Clear();
                    assginValuesToDataGridView(textBox1.Text);
                    break;
                case 3:
                dataGridView1.Rows.Clear();                 
                assginValuesToDataGridView(textBox1.Text, a:5); 
                break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplication newLocalDrivingLicenseApplication = new NewLocalDrivingLicenseApplication();
            newLocalDrivingLicenseApplication.Show();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRoww = dataGridView1.SelectedRows[0];
            string status = selectedRoww.Cells["Status"].Value.ToString();
            if (status == "Completed")
            {
                MessageBox.Show("This Application was Completed, Could not Cansel it ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                int LocalDrivingLicenseApplicationID;
              
                if (dataGridView1.SelectedRows.Count > 0)
                {
                  
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                 
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(selectedRow.Cells["L.D.AppID"].Value);
                    if (MessageBox.Show("Are You Sure Do You Want To Cancel This Application?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int ApplicationID = Business.GetApplicationIDByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                        if (Business.UpdateApplicationStatus(ApplicationID))
                        {
                            MessageBox.Show("Application Canceled");

                        }
                    }
                    
                    MessageBox.Show("The selected LocalDrivingLicenseApplicationID is: " + LocalDrivingLicenseApplicationID);
                }
                else
                {
                    MessageBox.Show("No row is selected.");

                }

            }
        }

        private void LocalDrivingLicensApplication_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value) ==0)
            {
                int LocalDrivingLicenseApplicationID;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    LocalDrivingLicenseApplicationID = Convert.ToInt32(selectedRow.Cells["L.D.AppID"].Value);
                    MessageBox.Show("The selected LocalDrivingLicenseApplicationID is: " + LocalDrivingLicenseApplicationID);
                    TestsAppointments visionTestAppointments = new TestsAppointments(LocalDrivingLicenseApplicationID,1, "Vision Test Appointments");
                    visionTestAppointments.Show();
                    MessageBox.Show("The selected LocalDrivingLicenseApplicationID is: " + LocalDrivingLicenseApplicationID);
                }
                else
                {
                    MessageBox.Show("No row is selected.");

                }
            }
            else
            {
                MessageBox.Show("You Have Passed This Test-:)","Info",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _RefreashDataGridView();
        }

        private void sechduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value) == 1)
            {
                int LocalDrivingLicenseApplicationID;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    LocalDrivingLicenseApplicationID = Convert.ToInt32(selectedRow.Cells["L.D.AppID"].Value);
                    MessageBox.Show("The selected LocalDrivingLicenseApplicationID is: " + LocalDrivingLicenseApplicationID);
                    TestsAppointments visionTestAppointments = new TestsAppointments(LocalDrivingLicenseApplicationID, 2, "Written Test Appointments");
                    visionTestAppointments.Show();
                    _RefreashDataGridView();

                }
                else
                {
                    MessageBox.Show("No row is selected.");

                }
            }
            else
            {
                MessageBox.Show("You Have Passed This Test-:)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value) ==2)
            {
                int LocalDrivingLicenseApplicationID;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    LocalDrivingLicenseApplicationID = Convert.ToInt32(selectedRow.Cells["L.D.AppID"].Value);
                    MessageBox.Show("The selected LocalDrivingLicenseApplicationID is: " + LocalDrivingLicenseApplicationID);
                    TestsAppointments visionTestAppointments = new TestsAppointments(LocalDrivingLicenseApplicationID, 3, "Street Test Appointments");
                    visionTestAppointments.Show();
                    _RefreashDataGridView();

                }
                else
                {
                    MessageBox.Show("No row is selected.");

                }
            }
            else
            {
                MessageBox.Show("You Have Passed This Test-:)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(selectedRow.Cells["L.D.AppID"].Value);
            if(MessageBox.Show("Are you sure do you want to delete Local Driving License Application has ID = " + LocalDrivingLicenseApplicationID, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (Business.DeleteLocalDrivingApplicationByApplicationID(LocalDrivingLicenseApplicationID))
                {
                    MessageBox.Show("Local Driving License Application Has ID: " + LocalDrivingLicenseApplicationID + " Was Deleted Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreashDataGridView();
                }
                else
                {
                    MessageBox.Show("Local Driving License Application Has ID: " + LocalDrivingLicenseApplicationID + " Can't be Deleted ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString() == "Completed")
            {
                MessageBox.Show("You Have Issued a License before-:)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PassedTests"].Value) == 3)
                {
                    int LocalDrivingLicenseApplicationID;
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["L.D.AppID"].Value);
                    Issue_Driver_License_For_First_Time issue_Driver_License_For_First_Time = new Issue_Driver_License_For_First_Time(LocalDrivingLicenseApplicationID);
                    issue_Driver_License_For_First_Time.Show();
                }
                else
                {
                    MessageBox.Show("You Have to Pass all Tests-:)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString() == "Completed")
            {
                DataGridViewRow selectedRoww = dataGridView1.SelectedRows[0];
                int LocalDrivingLicenesApplicationID = (int)selectedRoww.Cells["L.D.AppID"].Value;
                string NationalNo = selectedRoww.Cells["NationalNo"].Value.ToString();
                string FullName = selectedRoww.Cells["FullName"].Value.ToString();
                ShowLicense showLicense = new ShowLicense(LocalDrivingLicenesApplicationID,NationalNo, FullName);
                showLicense.Show();
            }
            else
            {
                MessageBox.Show("You Have to Pass all Tests and Issue an License-:)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = Business.GetPersonIDByLocalDrivingLicenseApplicationID(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["L.D.AppID"].Value));
            LicenseHistory licenseHistory = new LicenseHistory(personID);
            licenseHistory.Show();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationInfo showApplicationInfo = new ShowApplicationInfo(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["L.D.AppID"].Value));
            showApplicationInfo.Show();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
