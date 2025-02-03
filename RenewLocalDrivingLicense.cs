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
    public partial class RenewLocalDrivingLicense : Form
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
        int NewLicenseID;
        int tbLicenseID;
        public RenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void RenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            int.TryParse(textBox1.Text, out tbLicenseID);
            DataTable dt = Business.GetLicenseInfoByLicenseID(tbLicenseID);
            int licenseClassID = Business.GetLicenseClassIDByLicenseID(tbLicenseID);
            DriverID = (int)dt.Rows[0]["DriverID"];
            PersonID = Business.GetPersonIDByDriverID(DriverID);
            int otherLicense = Business.IsDriverHasActiveLicenseByDrivingIDAndLicenseClassID(DriverID, licenseClassID);

            if (otherLicense > 0)
            {
                MessageBox.Show("The Driver Has ID:" + DriverID + " Has an Active License with ID : " + otherLicense, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                if (dt.Rows.Count > 0)
                {
                    DateTime ExpirationDate = (DateTime)dt.Rows[0]["ExpirationDate"];
                    bool isExpired = (ExpirationDate > DateTime.Now);//  isExpired will give true when license expired

                    if (isExpired) //still not expired
                    {
                        MessageBox.Show("The License Has ID:" + tbLicenseID + " Expired on " + ExpirationDate.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        isFound = true;


                        var existingControl = this.Controls.OfType<DrivingLicenseInfo>().FirstOrDefault();
                        if (existingControl != null)
                        {
                            this.Controls.Remove(existingControl);
                            existingControl.Dispose();
                        }


                        DrivingLicenseInfo drivingLicenseInfo = new DrivingLicenseInfo(tbLicenseID);
                        this.Controls.Add(drivingLicenseInfo);
                        drivingLicenseInfo.Left = 23;
                        drivingLicenseInfo.Top = 170;



                        lblApplicationDate.Text = DateTime.Now.ToString().Substring(0, 10);
                        lblIssueDate.Text = DateTime.Now.ToString().Substring(0, 10);
                        lblApplicationFees.Text = "7 $";
                        lblLicenseFees.Text = "20 $";

                        lblOldLicenseID.Text = dt.Rows[0]["LicenseID"].ToString();
                        lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString().Substring(0, 10);
                        lblCreatedByUser.Text = Business.CurrentUserUserName;
                        lblTotalFees.Text = "27 $";

                        groupBox2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel2.Visible = true;
                        btnRenew.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("The License Has ID:" + tbLicenseID + " Is Not Found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = Business.GetLicenseInfoByLicenseID(tbLicenseID);
            if (MessageBox.Show("Are You Sure Do You Want To Renew License ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               if(Business.MakeLicenseNotActiveByLicenseID(tbLicenseID))
                {
                    MessageBox.Show("License has Id :" + tbLicenseID + " become inactive", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                string Notes;
              
                DriverID = (int)dt.Rows[0]["DriverID"];
                PersonID = Business.GetPersonIDByDriverID(DriverID);
                NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                //Get LicenseClass ID 
                int licenseClassID = Business.GetLicenseClassIDByLicenseID(tbLicenseID);
                

                int NewApplicationID = Business.InsertApplicationToDataBase(PersonID,DateTime.Now,2,3,DateTime.Now,5,Business.CurrentUserID);
                 NewLicenseID = Business.InsertLicenseAndGetLicenseID(NewApplicationID,DriverID, licenseClassID,DateTime.Now,
                     DateTime.Now.AddYears(10), tbNotes.Text,20.00f,1,2,Business.CurrentUserID);
                if(NewApplicationID>0 && NewLicenseID > 0)
                {
                    MessageBox.Show("Your new License has Id :" + NewLicenseID , "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRLApplicationID.Text = NewApplicationID.ToString();
                    lblRenewLicenseID.Text = NewLicenseID.ToString();
                    linkLabel2.Enabled = true;
                    btnRenew.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(PersonID);
            licenseHistory.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense showLicense = new ShowLicense(NewLicenseID);
            showLicense.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
