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
    public partial class DriverInternationalLicenseInfo : Form
    {
        int LocalDrivingLicenseID;
        int InternationalLicenseID;
        public DriverInternationalLicenseInfo(int LocalDrivingLicenseID , int InternationalLicenseID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseID = LocalDrivingLicenseID;
            this.InternationalLicenseID = InternationalLicenseID;
            InternationalLicenseInfo internationalLicenseInfo = new InternationalLicenseInfo(InternationalLicenseID, LocalDrivingLicenseID);

        
           

            // Add the UserControl to the Form's Controls collection
            this.Controls.Add(internationalLicenseInfo);
        }

        private void DriverInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
