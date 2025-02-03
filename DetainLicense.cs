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
    public partial class DetainLicense : Form
    {
        int tbLicenseID;
        int DriverID;
        int PersonID;
        int licenseClassID;
        bool isFound;
        string NationalNo;
        string FullName;

        public DetainLicense()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out tbLicenseID);

            DriverID = Business.GetDriverIDByLicenseID(tbLicenseID);
            PersonID = Business.GetPersonIDByDriverID(DriverID);

            //Get LicenseClass ID 
            licenseClassID = Business.GetLicenseClassIDByLicenseID(tbLicenseID);



            bool isLicenseFound = Business.IsLicesnseExistsByLicenseID(tbLicenseID);
            if (isLicenseFound)
            {
                if (!Business.IsLicenseDetainedByLicenseID(tbLicenseID))
                {

                    DataTable dt = Business.GetLicenseInfoByLicenseID(tbLicenseID);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        isFound = true;
                        NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                        FullName = Business.GetPersonFullNameByID(PersonID);

                        // Remove any existing DrivingLicenseInfo control
                        var existingControl = this.Controls.OfType<DrivingLicenseInfo>().FirstOrDefault();
                        if (existingControl != null)
                        {
                            this.Controls.Remove(existingControl);
                            existingControl.Dispose();  // Optionally dispose to release resources
                        }

                        // Add new DrivingLicenseInfo control with updated information
                        DrivingLicenseInfo drivingLicenseInfo = new DrivingLicenseInfo(tbLicenseID);
                        this.Controls.Add(drivingLicenseInfo);
                        groupBox2.Visible = true;
                        drivingLicenseInfo.Left = 23;
                        drivingLicenseInfo.Top = 170;


                        lblDetainedDate.Text = DateTime.Now.ToString().Substring(0,10);
                        lblFees.Text = "150 $";
                        lblCreatedByUser.Text = Business.CurrentUserUserName;

                        lblLicenseID.Text = tbLicenseID.ToString();


                        linkLabel1.Visible = true;
                        linkLabel2.Visible = true;
                        btnDetain.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("error :" + tbLicenseID + " Is NOT Active License .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("The License Has ID :" + tbLicenseID + " Is already Detained .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License NOT Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
             if (isFound)
            {
                if (MessageBox.Show("Are you sure do you want to detain license has id :" + tbLicenseID + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int DetainedID = Business.DetainLicenseAndGetDetainedID(tbLicenseID,DateTime.Now,150.00f,Business.CurrentUserID,0);
                    if (DetainedID > 0)
                    {
                        MessageBox.Show("Detained Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblDetainedID.Text = DetainedID.ToString(); 
                        linkLabel2.Enabled = true;
                        btnDetain.Enabled = false;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          //  btnDetain.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(PersonID);
            licenseHistory.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense showLicense = new ShowLicense(tbLicenseID);
            showLicense.Show();
        }

        private void DetainLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
