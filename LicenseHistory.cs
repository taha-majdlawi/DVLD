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
    public partial class LicenseHistory : Form
    {
        int PersonID;
        public LicenseHistory(int personID=1026 )
        {
            InitializeComponent();
            this.PersonID = personID;
            addPersonDetailsUserControl();
        }
        private void addPersonDetailsUserControl()
        {
           
            Person_Details PersonDetails = new Person_Details(PersonID);        
            // إضافة عنصر التحكم إلى النموذج
            this.Controls.Add(PersonDetails);
            PersonDetails.Left = 20;
            PersonDetails.Top = 50;

            DriverLicenses driverlicenses = new DriverLicenses(PersonID);
            this.Controls.Add(driverlicenses);
            driverlicenses.Left = 15;
            driverlicenses.Top = 300;
        }
        private void LicenseHistory_Load(object sender, EventArgs e)
        {

        }
    }
}
