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
    public partial class ShowApplicationInfo : Form
    {
        int LocalDrivingLicenseApplicationID;
        public ShowApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            ApplicationInfo pd = new ApplicationInfo( LocalDrivingLicenseApplicationID);

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);
        }

        private void ShowApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
