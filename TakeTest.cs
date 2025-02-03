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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLD_Presentation
{
    public partial class TakeTest : Form
    {
        int LocalDrivingLicenseApplicationID = 0;
        int TestAppointmentId;
        string Date;
        double PaidFees;
        int IsLocked;
        int VisionTrial = 0;
        int WrittenTrial = 0;
        int StreetTrial = 0;
        int TestTypeID;
        public TakeTest(int LocalDrivingLicenseApplicationID,int TestAppointmentId , string Date ,double PaidFees , int IsLocked,int TestTypeID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestAppointmentId = TestAppointmentId;
            this.Date = Date;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.TestTypeID = TestTypeID;   
            defineValues( LocalDrivingLicenseApplicationID);
        }
        private void defineValues(int LocalDrivingLicenseApplicationID)
        {
            if (TestTypeID == 1)
            {
                VisionTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 1);


                lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblTrail.Text = VisionTrial.ToString();
                lblFees.Text = PaidFees.ToString() + " $";
                lblDate.Text = Date.ToString();
            }
            if (TestTypeID == 2)
            {
                VisionTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0, 2);


                lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblTrail.Text = VisionTrial.ToString();
                lblFees.Text = PaidFees.ToString() + " $";
                lblDate.Text = Date.ToString();
            }
            if (TestTypeID == 3)
            {
                VisionTrial = Business.GetNumberOfFailerTrialofTest(LocalDrivingLicenseApplicationID, 0,3);


                lblDLAppID.Text = LocalDrivingLicenseApplicationID.ToString();
                lblDClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblName.Text = Business.GetFullPersonNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
                lblTrail.Text = VisionTrial.ToString();
                lblFees.Text = PaidFees.ToString() + " $";
                lblDate.Text = Date.ToString();
            }


        }
        private void TakeTest_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestTypeID == 1)
            {
                int couunt = 1;
                if (couunt == 1)
                {
                    bool result;
                    if (rbPass.Checked)
                        result = true;
                    else
                        result = false;

                    // تحويل النتيجة إلى `int` عند استدعاء `InsertTestInfo`
                    int resultAsInt = result ? 1 : 0;

                    int testID = Business.InsertTestInfo(TestAppointmentId, resultAsInt, textBox1.Text, Business.CurrentUserID);
                    if (testID > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblTestID.Text = testID.ToString();
                        Business.LockTheAppointment(TestAppointmentId);

                    }
                    couunt = 0;
                }
            }
            if (TestTypeID == 2)
            {
                int couunt = 1;
                if (couunt == 1)
                {
                    bool result;
                    if (rbPass.Checked)
                        result = true;
                    else
                        result = false;

                    // تحويل النتيجة إلى `int` عند استدعاء `InsertTestInfo`
                    int resultAsInt = result ? 1 : 0;

                    int testID = Business.InsertTestInfo(TestAppointmentId, resultAsInt, textBox1.Text, Business.CurrentUserID);
                    if (testID > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblTestID.Text = testID.ToString();
                        Business.LockTheAppointment(TestAppointmentId);
                    }
                    couunt = 0;
                }
            }
            if (TestTypeID == 3)
            {
                int couunt = 1;
                if (couunt == 1)
                {
                    bool result;
                    if (rbPass.Checked)
                        result = true;
                    else
                        result = false;

                    // تحويل النتيجة إلى `int` عند استدعاء `InsertTestInfo`
                    int resultAsInt = result ? 1 : 0;

                    int testID = Business.InsertTestInfo(TestAppointmentId, resultAsInt, textBox1.Text, Business.CurrentUserID);
                    if (testID > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblTestID.Text = testID.ToString();
                        Business.LockTheAppointment(TestAppointmentId);
                    }
                    couunt = 0;
                }
            }
        }
    }
}
