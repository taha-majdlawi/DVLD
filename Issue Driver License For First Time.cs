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
    public partial class Issue_Driver_License_For_First_Time : Form
    {
        int LocalDrivingLicenseApplicationID;
        public Issue_Driver_License_For_First_Time(int LocalDrivingLicenseApplicationID=1037)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            ApplicationInfo applicationInfo = new ApplicationInfo(LocalDrivingLicenseApplicationID);
            // إضافة عنصر التحكم إلى النموذج
            this.Controls.Add(applicationInfo);
            applicationInfo.Left = 20;
            applicationInfo.Top = 10;
        }

        private void Issue_Driver_License_For_First_Time_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int PersonID = Business.GetPersonIDByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            int DriverID = Business.InsertDriverAndGetDriverID(PersonID, Business.CurrentUserID,DateTime.Now);
            int ApplicationID = Business.GetApplicationIDByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            int LicenseClass = Business.GettClassNameByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            DateTime currentDate = DateTime.Now;
            DateTime futureDate = currentDate.AddYears(10);

            int LicenseID = Business.InsertLicenseAndGetLicenseID(ApplicationID, DriverID, LicenseClass, currentDate, futureDate,
               textBox1.Text,15.00f,1,1,Business.CurrentUserID);
            if (LicenseID > 0 && DriverID > 0) 
            {
                MessageBox.Show("License Issued Successfully with ID = " + LicenseID + " .", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Business.CompleteTheApplication(ApplicationID);
            this.Close();
        }
    }
}
