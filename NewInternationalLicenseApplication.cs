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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Presentation
{
    public partial class NewInternationalLicenseApplication : Form
    {
        bool isFound = false;
        int PersonID;
        int DriverID;
        int LocalDrivingLicenseApplicationID;
        int LocalDrivingLicenseID;
        int InternationalLicensID;
        int LocalApplicationID;
        public NewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; // إلغاء إدخال المفتاح
                }
         
        }

        private void NewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            int.TryParse(textBox1.Text, out LocalDrivingLicenseApplicationID);
             LocalApplicationID = Business.IsLocalDrivingLicenseApplicationExists(LocalDrivingLicenseApplicationID);//73

            if (LocalApplicationID == 0)
            {
                MessageBox.Show("The License Has ID:" + LocalDrivingLicenseApplicationID + " Is Not Found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                 PersonID = Business.GetPersonIDByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);//1023
                DataTable dt = Business.CheckIfLicesnseExistsAndClassIdIs3(LocalDrivingLicenseApplicationID, PersonID);
                DriverID = Business.GetDriverIDByPersonID(PersonID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    isFound = true;
                    string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                    string FullName = Business.GetPersonFullNameByID(PersonID);

                    // Remove any existing DrivingLicenseInfo control
                    var existingControl = this.Controls.OfType<DrivingLicenseInfo>().FirstOrDefault();
                    if (existingControl != null)
                    {
                        this.Controls.Remove(existingControl);
                        existingControl.Dispose();  // Optionally dispose to release resources
                    }

                    // Add new DrivingLicenseInfo control with updated information
                    DrivingLicenseInfo drivingLicenseInfo = new DrivingLicenseInfo(LocalDrivingLicenseApplicationID, NationalNo, FullName);
                    this.Controls.Add(drivingLicenseInfo);
                    drivingLicenseInfo.Left = 23;
                    drivingLicenseInfo.Top = 170;
                    lblAppliactionDate.Text = DateTime.Now.ToString().Substring(0,10);
                    lblIssueDate.Text = DateTime.Now.ToString().Substring(0, 10);
                    lblFees.Text = "51 $";

                    lblLocalLicenseID.Text = LocalDrivingLicenseApplicationID.ToString();
                    lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString().Substring(0,10);
                    lblCreatedByUser.Text = Business.CurrentUserUserName;
                    groupBox2.Visible = true;   
                    linkLabel1.Visible = true;
                    linkLabel2.Visible = true;
                }
                else
                {
                    MessageBox.Show("The License Has ID:" + LocalDrivingLicenseApplicationID + " Is Not 'Class 3 - Ordinary driving license', try to enter another ID ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isFound)
            {
                DataTable dt = Business.CheckIfDriverHasInternationalLicense(DriverID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBox.Show("This License has International License with ID : " + dt.Rows[0]["InternationalLicenseID"] + " .",
                               "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int InternationalApplicationID = Business.InsertApplicationToDataBase(PersonID, DateTime.Now, 6, 3, DateTime.Now, 51, Business.CurrentUserID);

                    Console.WriteLine(InternationalApplicationID);

                  int  LicenseID = Business.GetLocalLicenseIDByApplicationID(LocalApplicationID);//20

                    Console.WriteLine(LicenseID);

                    InternationalLicensID = Business.InsertInternationalLicense(InternationalApplicationID, DriverID, LicenseID, DateTime.Now
                        , DateTime.Now.AddYears(1), 1, Business.CurrentUserID);

                    Console.WriteLine(InternationalLicensID);

                    if (InternationalApplicationID > 0 && InternationalLicensID > 0)
                    {
                        MessageBox.Show("Added Successfully with International License ID : " + InternationalLicensID + " .",
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ILApplicationID.Text = InternationalApplicationID.ToString();
                        lblILLicenseID.Text = InternationalLicensID.ToString();
                        linkLabel2.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
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
            DriverInternationalLicenseInfo internationalLicenseInfo = new DriverInternationalLicenseInfo( LocalDrivingLicenseApplicationID, InternationalLicensID);
            internationalLicenseInfo.Show();

        }
    }
}
