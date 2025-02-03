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
    public partial class ScheduleTest : Form

    {
        int LocalDrivingLicenseApplicationID = 0;
        int TestAppointmentID = 0;
        int VisionTrial = 0;
        int WrittenTrial = 0;
        int StreetTrial = 0;
        bool isLocked = false;
        int TestTypeID;
        public ScheduleTest(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            defineValues(LocalDrivingLicenseApplicationID);
            
        }

        /// fOR UPDATE MODE
        public ScheduleTest(int LocalDrivingLicenseApplicationID, int TestAppointmentID , bool isLocked, int TestTypeID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestAppointmentID = TestAppointmentID;
            this.isLocked = isLocked;
            this.TestTypeID = TestTypeID;
            defineValues(LocalDrivingLicenseApplicationID);

        }
        private void defineValues(int LocalDrivingLicenseApplicationID)
        {
            if (TestTypeID == 1)
            {
                if (isLocked)
                {
                    dateTimePicker1.Enabled = false;
                    label11.Visible = true;
                    button1.Enabled = false;
                }
                else
                {
                    VisionTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 1);


                    lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                    lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblTrail.Text = VisionTrial.ToString();
                    lblFees.Text = "10 $";
                    lblRAppFees.Text = "5 $";
                    lblRTestAppID.Text = "Unkown";
                    lblTotalFees.Text = "15 $";
                    if (VisionTrial > 0)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        groupBox1.Enabled = false;
                    }
                }
            }
            if (TestTypeID == 2)
            {
                if (isLocked)
                {
                    dateTimePicker1.Enabled = false;
                    label11.Visible = true;
                    button1.Enabled = false;
                }
                else
                {
                    WrittenTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 2);


                    lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                    lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblTrail.Text = WrittenTrial.ToString();
                    lblFees.Text = "20 $";
                    lblRAppFees.Text = "20 $";
                    lblRTestAppID.Text = "Unkown";
                    lblTotalFees.Text = "40 $";
                    if (WrittenTrial > 0)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        groupBox1.Enabled = false;
                    }
                }
            }
            if (TestTypeID == 3)
            {
                if (isLocked)
                {
                    dateTimePicker1.Enabled = false;
                    label11.Visible = true;
                    button1.Enabled = false;
                }
                else
                {
                    StreetTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 3);


                    lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                    lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                    lblTrail.Text = StreetTrial.ToString();
                    lblFees.Text = "20 $";
                    lblRAppFees.Text = "20 $";
                    lblRTestAppID.Text = "Unkown";
                    lblTotalFees.Text = "40 $";
                    if (StreetTrial > 0)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        groupBox1.Enabled = false;
                    }
                }
            }
        }
        private void ScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TestTypeID == 1)
            {
                int PaidFees = 10;
                if (VisionTrial > 0)
                {
                    PaidFees = 15;
                }

                if (Business.ScheduleTest == "AddNew")
                {

                    if (Business.InsertTestAppointment(1, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Added Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     
                    }
                }
                else
                {

                    if (Business.UpdateTestAppointment(TestAppointmentID, 1, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Updated Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("NOT Updated Successfully", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Business.ScheduleTest = "AddNew";
                }

            }

            if (TestTypeID == 2) 
            {
                int PaidFees = 10;
                if (WrittenTrial > 0)
                {
                    PaidFees = 15;
                }

                if (Business.ScheduleTest == "AddNew")
                {

                    if (Business.InsertTestAppointment(2, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Added Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                }
                else
                {

                    if (Business.UpdateTestAppointment(TestAppointmentID, 2, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Updated Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("NOT Updated Successfully", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Business.ScheduleTest = "AddNew";
                }

            }
            if (TestTypeID == 3) 
            {
                int PaidFees = 10;
                if (VisionTrial > 0)
                {
                    PaidFees = 15;
                }

                if (Business.ScheduleTest == "AddNew")
                {

                    if (Business.InsertTestAppointment(3, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Added Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      
                    }
                }
                else
                {

                    if (Business.UpdateTestAppointment(TestAppointmentID, 3, LocalDrivingLicenseApplicationID, dateTimePicker1.Value, PaidFees, Business.CurrentUserID, 0))
                    {
                        MessageBox.Show("Updated Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("NOT Updated Successfully", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Business.ScheduleTest = "AddNew";
                }
            }
        }
    }
}
