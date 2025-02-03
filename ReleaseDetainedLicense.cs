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
    public partial class ReleaseDetainedLicense : Form
    {
        int tbLicenseID;
        int DriverID;
        int PersonID;
        int licenseClassID;
        bool isFound;
        string NationalNo;
        string FullName;

        public ReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                if (Business.IsLicenseDetainedByLicenseID(tbLicenseID))
                {

                    DataTable dt = Business.GetDetainedInfoByLicenseID(tbLicenseID);

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


                        lblDetainedID.Text = dt.Rows[0]["DetainID"].ToString();
                        lblDetaineDate.Text = dt.Rows[0]["DetainDate"].ToString().Substring(0, 10);
                        lblApplicationFees.Text = "15 $";
                        lblTotalFees.Text = "165 $";

                        lblLicenseID.Text = dt.Rows[0]["LicenseID"].ToString();
                        lblCreatedByUser.Text = Business.CurrentUserUserName;
                        lblFineFees.Text = "150 $";


                       


                        linkLabel1.Visible = true;
                        linkLabel2.Visible = true;
                        btnDetain.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("error :" + tbLicenseID + "  error .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("The License Has ID :" + tbLicenseID + " Is NOT Detained .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License NOT Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (isFound)
            {
                if (MessageBox.Show("Are you sure do you want to release detain license has id :" + tbLicenseID + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ApplicationID = Business.InsertApplicationToDataBase(PersonID,DateTime.Now,5,3,DateTime.Now,165,Business.CurrentUserID);
                    if(ApplicationID > 0)
                    {
                        if (Business.ReleaseDetainedLicenseByLicenseID(tbLicenseID, true, DateTime.Now, Business.CurrentUserID, ApplicationID))
                        {
                            MessageBox.Show("Released Detained Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lblApplicationID.Text = ApplicationID.ToString();
                            linkLabel2.Enabled = true;
                            btnDetain.Enabled = false;
                        }
                    }
                }
            }
        }

        private void ReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
