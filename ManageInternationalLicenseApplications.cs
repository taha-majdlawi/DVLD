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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Presentation
{
    public partial class ManageInternationalLicenseApplications : Form
    {
        public ManageInternationalLicenseApplications()
        {
            InitializeComponent();
            RefreashInternationalLicense();
        }

        private void RefreashInternationalLicense()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = Business.GetInternationalLicensesInfo();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {

        }

        private void showPersonDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
              
                if (dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value != null)
                {
                    int ApplicationID;

                    if (int.TryParse(dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value.ToString(), out ApplicationID))
                    {
                        int PersonID = Business.GetPersonIDByApplicationID(ApplicationID);
                        Person_Info info = new Person_Info(PersonID);
                        info.Show();
                    }
                    else
                    {
                    
                        MessageBox.Show("Unable to convert PersonID to an integer.");
                    }
                }
                else
                {
                  
                    MessageBox.Show("PersonID cell is null.");
                }
            }
            else
            {
             
                MessageBox.Show("No row selected.");
            }



          
          
        }

        private void showLicenseDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalLicense;
            int.TryParse(dataGridView1.SelectedRows[0].Cells["IssuedUsingLocalLicenseID"].Value.ToString(), out LocalLicense);
            int InternationalLicense;
            int.TryParse(dataGridView1.SelectedRows[0].Cells["InternationalLicenseID"].Value.ToString(), out InternationalLicense);

            DriverInternationalLicenseInfo driverInternationalLicenseInfo = new DriverInternationalLicenseInfo(LocalLicense, InternationalLicense);
            driverInternationalLicenseInfo.Show();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID;

            int.TryParse(dataGridView1.SelectedRows[0].Cells["ApplicationID"].Value.ToString(), out ApplicationID);
            
                int PersonID = Business.GetPersonIDByApplicationID(ApplicationID);
                LicenseHistory licenseHistory = new LicenseHistory(PersonID);
            licenseHistory.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseApplication newInternationalLicenseApplication = new NewInternationalLicenseApplication();
            newInternationalLicenseApplication.Show();
        }
    }
}
