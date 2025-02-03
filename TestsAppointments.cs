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
    public partial class TestsAppointments : Form
    {
        int LocalDrivingLicenseApplicationID;
        int TestTypeID;
        string FormAddrees;
        public TestsAppointments(int LocalDrivingLicenseApplicationID, int TestTypeID,string FormAddrees)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            this.FormAddrees = FormAddrees;
            InitializeComponent();
            ApplicationInfo applicationInfo = new ApplicationInfo(LocalDrivingLicenseApplicationID);        
            // إضافة عنصر التحكم إلى النموذج
            this.Controls.Add(applicationInfo);
            applicationInfo.Left = 20;
            applicationInfo.Top = 80;
            

        }
        DataTable dt = new DataTable();
        private void TestAppointments_Load(object sender, EventArgs e)
        {
            if(TestTypeID==1)
                refreshVisionAppoinments();

            if(TestTypeID==2)
                refreshWrittenAppoinments();

            if (TestTypeID==3)
                refreshStreetAppoinments();
        }
        private void refreshVisionAppoinments()
        {
            dt = Business.GetAppoinmentsInfoForAllTestTypes(LocalDrivingLicenseApplicationID,1);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            label1.Text = FormAddrees;
        }
        private void refreshWrittenAppoinments()
        {
            dt = Business.GetAppoinmentsInfoForAllTestTypes(LocalDrivingLicenseApplicationID,2);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            label1.Text = FormAddrees;
        }
        private void refreshStreetAppoinments()
        {
            dt = Business.GetAppoinmentsInfoForAllTestTypes(LocalDrivingLicenseApplicationID,3);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            label1.Text = FormAddrees;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TestTypeID == 1)
            {


                if (((Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 1) > 0) || dataGridView1.Rows.Count == 0)
                    && Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 1, 1) == 0)
                {
                    ScheduleTest scheduleTest = new ScheduleTest(LocalDrivingLicenseApplicationID,TestTypeID);
                    scheduleTest.Show();
                    refreshVisionAppoinments();
                }
                else
                {
                    MessageBox.Show("This Application Has An Active Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (TestTypeID == 2)
            {

                if (((Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 2) > 0) || dataGridView1.Rows.Count == 0)
                    && Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 1, 2) == 0)
                {
                    ScheduleTest scheduleTest = new ScheduleTest(LocalDrivingLicenseApplicationID, TestTypeID);
                    scheduleTest.Show();
                    refreshWrittenAppoinments();
                }
                else
                {
                    MessageBox.Show("This Application Has An Active Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (TestTypeID == 3)
            {
                if (((Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 3) > 0) || dataGridView1.Rows.Count == 0)
                   && Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 1, 3) == 0)
                {
                    ScheduleTest scheduleTest = new ScheduleTest(LocalDrivingLicenseApplicationID, TestTypeID);
                    scheduleTest.Show();
                    refreshStreetAppoinments();
                }
                else
                {
                    MessageBox.Show("This Application Has An Active Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TestTypeID == 1)
                refreshVisionAppoinments();

            if (TestTypeID == 2)
                refreshWrittenAppoinments();

            if (TestTypeID == 3)
                refreshStreetAppoinments();
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            Business.ScheduleTest = "Update";
            bool isLocked =(bool)dataGridView1.SelectedRows[0].Cells["IsLocked"].Value;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var cellValue = dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value;

                if (cellValue != null)
                {
                    ScheduleTest scheduleTest = new ScheduleTest(LocalDrivingLicenseApplicationID, (int)cellValue,  isLocked,TestTypeID);
                    scheduleTest.Show();
                }
                else
                {
                    MessageBox.Show("The cell value is empty.");
                }
            }
            else
            {
                MessageBox.Show("No row is selected.");
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int isLocked = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IsLocked"].Value);
            if (isLocked == 0)
            {

                int TestAppointmentID; int IsLocked = 0;
                string AppointmentDate;
                double PaidFees;

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    TestAppointmentID = (int)dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value;
                    PaidFees = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells["PaidFees"].Value);
                    AppointmentDate = dataGridView1.SelectedRows[0].Cells["AppointmentDate"].Value.ToString();




                    if (TestAppointmentID != null)
                    {
                        TakeTest takeTest = new TakeTest(LocalDrivingLicenseApplicationID, TestAppointmentID, AppointmentDate, PaidFees, IsLocked, TestTypeID);
                        takeTest.Show();
                    }
                    else
                    {
                        MessageBox.Show("The cell value is empty.");
                    }
                }
                else
                {
                    MessageBox.Show("No row is selected.");
                }
            }
            else
            {
                MessageBox.Show("This test is Locked or may taken.");
            }
        }
    }
}
