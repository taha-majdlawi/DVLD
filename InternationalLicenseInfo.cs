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
    public partial class InternationalLicenseInfo : UserControl
    {
        int InternationalLicenseID;
        int LocalDrivingLicenseID;
        public InternationalLicenseInfo(int InternationalLicenseID, int LocalDrivingLicenseID)
        {
            InitializeComponent();
            this.InternationalLicenseID = InternationalLicenseID;
            this.LocalDrivingLicenseID = LocalDrivingLicenseID;
            AddLicenseInfo();
        }
        private void AddLicenseInfo()
        {
            DataTable dt = Business.GetInternationalLicenseInfoByInternationalLicenseID(InternationalLicenseID);
            lblName.Text = Business.GetPersonFullNameByID((int)dt.Rows[0]["PersonID"]);
            lblIntLicenseID.Text = InternationalLicenseID.ToString();
            lblLicenseID.Text = LocalDrivingLicenseID.ToString();
            lblNationalNo.Text = dt.Rows[0]["NationalNo"].ToString();
            if ((byte)dt.Rows[0]["Gendor"] == 0) lblGendor.Text = "Male";
            else lblGendor.Text = "Female";
            lblIssueDate.Text = dt.Rows[0]["IssueDate"].ToString().Substring(0, 10);

            lblApplicationID.Text = dt.Rows[0]["ApplicationID"].ToString();
            if ((bool)dt.Rows[0]["IsActive"]) lblIsActive.Text = "Yes";
            else lblIsActive.Text = "No";
            lblDateOfBirth.Text = dt.Rows[0]["DateOfBirth"].ToString().Substring(0,10);
            lblDriverID.Text = dt.Rows[0]["DriverID"].ToString();
            lblExpirationDate.Text = dt.Rows[0]["ExpirationDate"].ToString().Substring(0, 10);

            string ImagePath = Business.GetPersonImagePathByNationalNo(dt.Rows[0]["NationalNo"].ToString());
            if (System.IO.File.Exists(ImagePath))
            {
                pictureBox1.Image = Image.FromFile(ImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
