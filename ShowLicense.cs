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
    public partial class ShowLicense : Form
    {
        int LocalDrivingLicenseApplicationID;
        string NationalNo;
        string FullName;
        int LicenseID;
        public ShowLicense(int LocalDrivingLicenseApplicationID ,string NationalNo,string FullName)
        {
            InitializeComponent();
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            addDrivingLicenseInfoUserControl();


        }
        public ShowLicense(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
            addDrivingLicenseInfoUserControlByLicenseID();
        }
        private void addDrivingLicenseInfoUserControlByLicenseID()
        {
            DrivingLicenseInfo pd = new DrivingLicenseInfo(LicenseID);

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);

        }
        private void addDrivingLicenseInfoUserControl()
        {
            DrivingLicenseInfo pd = new DrivingLicenseInfo(LocalDrivingLicenseApplicationID,NationalNo,FullName);

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);

        }
        private void ShowLicense_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
