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
    public partial class Replacement_for_Damaged_Licenes : Form
    {
        bool isFound = false;
        int PersonID;
        int DriverID;
        int LocalDrivingLicenseApplicationID;
        int LocalDrivingLicenseID;
        int InternationalLicensID;
        int LocalApplicationID;
        int NewLocalDrivingLicenseApplicationID;
        string NationalNo;
        string FullName;
        int tbLicenseID;
        int licenseClassID;
        int newApplication;
        int newLicense;
        int IssueResoan;
        public Replacement_for_Damaged_Licenes()
        {
            InitializeComponent();
        }

        private void Replacement_for_Damaged_Licenes_Load(object sender, EventArgs e)
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
                if (Business.IsLicesnseActiveByLicenseID(tbLicenseID))
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
                        drivingLicenseInfo.Left = 23;
                        drivingLicenseInfo.Top = 170;

                        groupBox2.Visible = true;

                        lblApplicationDate.Text = DateTime.Now.ToString().Substring(0,10);
                        lblApplicationFees.Text = "5 $";
                        lblOldLicenseID.Text = tbLicenseID.ToString();
                        lblCreatedByUserID.Text = Business.CurrentUserID.ToString();


                       linkLabel1.Visible = true;
                       linkLabel2.Visible = true;
                       btnRenew.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("error :" + tbLicenseID + " Is NOT Active License .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("The License Has ID :" + tbLicenseID + " Is NOT Active License .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License NOT Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (isFound)
            {
                if (MessageBox.Show("Are you sure do you want to replace this lic ense ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (Business.MakeLicenseNotActiveByLicenseID(tbLicenseID))
                    {
                        MessageBox.Show("Your License has ID:" + tbLicenseID + " become inactive.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (radioButton1.Checked) IssueResoan = 3;
                    if (radioButton2.Checked) IssueResoan = 4;
                     newApplication = Business.InsertApplicationToDataBase(PersonID,DateTime.Now,1,3, DateTime.Now,5,Business.CurrentUserID);
                     newLicense = Business.InsertLicenseAndGetLicenseID(newApplication,DriverID, licenseClassID,DateTime.Now, DateTime.Now.AddYears(10),"",5,1,IssueResoan,Business.CurrentUserID);
                    if (newLicense >0)
                    {
                        MessageBox.Show("Your License has ID:" + tbLicenseID + " become inactive, And now you have new licese has ID :" + newLicense + " .", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        linkLabel2.Enabled = true;
                        lblLRApplicationID.Text = newApplication.ToString();
                        lblReplacedLicenseID.Text = newLicense.ToString();
                        btnRenew.Enabled = false;
                        groupBox3.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Something get wrong");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense showLicense = new ShowLicense(newLicense);
            showLicense.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(PersonID);
            licenseHistory.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
