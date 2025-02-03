using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Presentation
{
    public partial class ApplicationInfo : UserControl
    {
        int LocalDrivingLicenseApplicationID;
        int personID;
        public ApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            LoadValues(LocalDrivingLicenseApplicationID);
        }
        private void LoadValues(int LocalDrivingLicenseApplicationID)

        {

            DataTable dt = Business.GetApplicationInfoForControl(LocalDrivingLicenseApplicationID);

            DLAppID.Text = dt.Rows[0]["LocalDrivingLicenseApplicationID"].ToString();
            lblLicenseName.Text = dt.Rows[0]["ClassName"].ToString();
            lblNumberOfPassedTests.Text = defineNumberOfPassedTests(LocalDrivingLicenseApplicationID);

            lblApplicationID.Text = dt.Rows[0]["ApplicationID"].ToString();
            lblApplicationStatus.Text = defineStatus((byte)dt.Rows[0]["ApplicationStatus"]);
            lblAppicationFees.Text = dt.Rows[0]["PaidFees"].ToString();
            lblApplicationType.Text = dt.Rows[0]["ApplicationTypeTitle"].ToString();
            ApplicantName.Text = Business.GetPersonFullNameByID((int)dt.Rows[0]["ApplicantPersonID"]);
            lblApplicationDate.Text = dt.Rows[0]["ApplicationDate"].ToString();
            lblStatusDate.Text = dt.Rows[0]["LastStatusDate"].ToString();
            lblUserName.Text = Business.GetUserNameByUserID((int)dt.Rows[0]["CreatedByUserID"]);

            personID = (int)dt.Rows[0]["ApplicantPersonID"];
        }
        private string defineStatus(byte status)
        {
            string Status = "";
            switch (status)
            {
               
                case 1:
                    Status = "New";
                    break;
                case 2:
                    Status = "Cancled";
                    break;
                case 3:
                    Status = "Completed";
                    break;
            }


            return Status;
        }
        private string defineNumberOfPassedTests(int LocalDrivingLicenseApplicationID)
        {
            string NumberOfTests="";
            switch (Business.GetNumberOfPassedTests(LocalDrivingLicenseApplicationID))
            {
                case 0:
                    NumberOfTests = "0/3";
                    break;
                case 1:
                    NumberOfTests = "1/3";
                    break;
                    case 2:
                    NumberOfTests = "2/3";
                    break;
                    case 3:
                    NumberOfTests = "3/3";
                    break;
            }

            return NumberOfTests;
        }
        private void ApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddPerson addPerson = new AddPerson(personID);
            addPerson.ShowDialog();
        }
    }
}
