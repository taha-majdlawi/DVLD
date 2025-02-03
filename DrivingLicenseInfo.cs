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
    public partial class DrivingLicenseInfo : UserControl
    {
        int LocalDrivingLicenseApplicationID;
        string NationalNo; 
        string FullName;
        int LicenseID;
        public DrivingLicenseInfo(int LocalDrivingLicenseApplicationID ,string NationalNo , string FullName)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.NationalNo = NationalNo; 
            this.FullName = FullName;
            AssignValues();

        }
        public DrivingLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
            AssignValuesByLicenseID(LicenseID);
        }
             private void AssignValuesByLicenseID(int LicenseID)
             {






            //
            DataTable dt = Business.GetLicenseInfoByLicenseID(LicenseID);
            if (dt.Rows.Count > 0)
            {


                int DriverID = (int)dt.Rows[0]["DriverID"];
                int PersonID = Business.GetPersonIDByDriverID(DriverID);

                int LicenseClass = (int)dt.Rows[0]["LicenseClass"];
                lblClass.Text = LicenseClass.ToString();
                lblLicenseID.Text = LicenseID.ToString();
                NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                lblNationalNo.Text = NationalNo;
                lblGendor.Text = Business.GetGendorByNationalNo(NationalNo);
                lblName.Text = Business.GetPersonFullNameByID(PersonID);

                lblIssueDate.Text = dt.Rows[0]["IssueDate"].ToString().Substring(0, 10);
                if (dt.Rows[0]["Notes"].ToString() == "" || dt.Rows[0]["Notes"].ToString() == null) { lblNotes.Text = "No Notes"; }
                else
                {
                    lblNotes.Text = dt.Rows[0]["Notes"].ToString();
                }
                if ((bool)dt.Rows[0]["IsActive"])
                {
                    lblIsActive.Text = "Yes";
                }
                else { lblIsActive.Text = "No"; }

                lblDateOdBirth.Text = Business.GetBirthOfDateByNationalNo(NationalNo).Substring(0, 10);
                lblDriverID.Text = DriverID.ToString();
                lblExpirationDate.Text = dt.Rows[0]["ExpirationDate"].ToString().Substring(0, 10);
                if ((byte)dt.Rows[0]["IssueReason"] == 1) lblIssueReason.Text = "First Time";
                if ((byte)dt.Rows[0]["IssueReason"] == 2) lblIssueReason.Text = "Renew";
                if ((byte)dt.Rows[0]["IssueReason"] == 3) lblIssueReason.Text = "Replaced For Lost";
                if ((byte)dt.Rows[0]["IssueReason"] == 4) lblIssueReason.Text = "Replaced For Damage";

                 if(Business.IsLicenseDetainedByLicenseID(LicenseID) ) lblDetained.Text = "Yes";
                else lblDetained.Text = "No";

                string imagePath = Business.GetPersonImagePathByNationalNo(NationalNo);
                if (System.IO.File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                Console.WriteLine("reeorrrrr taha");
            }
        }
            private void AssignValues()
            {
            lblClass.Text = Business.GetClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            lblName.Text = FullName;
            //
            int ApplicationID = Business.GetApplicationIDByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            LicenseID = Business.GetLicenseIDByApplicationID(ApplicationID);
            //
            lblLicenseID.Text = LicenseID.ToString();
            lblNationalNo.Text = NationalNo.ToString();
            lblGendor.Text = Business.GetGendorByNationalNo(NationalNo);

            //
            DataTable dt = Business.GetLicenseInfoBuApplicationID(ApplicationID);
            lblIssueDate.Text = dt.Rows[0]["IssueDate"].ToString().Substring(0, 10);
            if (dt.Rows[0]["Notes"].ToString() == "" || dt.Rows[0]["Notes"].ToString() == null) { lblNotes.Text = "No Notes"; }
            else {
            lblNotes.Text = dt.Rows[0]["Notes"].ToString();
        }
            if ((bool)dt.Rows[0]["IsActive"])
            {
                lblIsActive.Text = "Yes";
            }
            else { lblIsActive.Text = "No"; }

            lblDateOdBirth.Text = Business.GetBirthOfDateByNationalNo(NationalNo).Substring(0, 10);
            lblDriverID.Text = dt.Rows[0]["DriverID"].ToString();
            lblExpirationDate.Text = dt.Rows[0]["ExpirationDate"].ToString().Substring(0,10);
           if ((byte)dt.Rows[0]["IssueReason"]==1) lblIssueReason.Text = "First Time";
           if((byte)dt.Rows[0]["IssueReason"] ==2) lblIssueReason.Text = "Renew";
            lblDetained.Text = "No";

            string imagePath = Business.GetPersonImagePathByNationalNo(NationalNo);
            if (System.IO.File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        private void DrivingLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
