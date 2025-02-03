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
    public partial class List_Detaind_Licenses : Form
    {
        public List_Detaind_Licenses()
        {
            InitializeComponent();
            refreshDataGrideView();
        }
     
        private void List_Detaind_Licenses_Load(object sender, EventArgs e)
        {
            // Assuming you have NationalNo DataGridView named dataGridView1
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void refreshDataGrideView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = Business.GetListDetainedLicenses();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["NationalNo"].Value != null)
                {
                    string NationalNo=dataGridView1.SelectedRows[0].Cells["NationalNo"].Value.ToString();
                  

                    int PersonId = Business.GetPersonIDByNationalNo(NationalNo);
                    if (PersonId > 0)
                    {
                        Person_Info person_Info = new Person_Info(PersonId);
                        person_Info.Show();
                    }
                    else
                    {
                        MessageBox.Show("Something wrong may happend, try again");
                    }
                }
                else
                {
                    // الخلية فارغة
                    MessageBox.Show("PersonID cell is null.");
                }
            }
            else
            {
                // لا يوجد صف محدد
                MessageBox.Show("No row selected.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["LicenseID"].Value != null)
                {
                   int LicenseID = (int)dataGridView1.SelectedRows[0].Cells["LicenseID"].Value;


                 
                    if (LicenseID > 0)
                    {
                        ShowLicense showLicense = new ShowLicense(LicenseID);
                        showLicense.Show();
                    }
                    else
                    {
                        MessageBox.Show("Something wrong may happend, try again");
                    }
                }
                else
                {
                    // الخلية فارغة
                    MessageBox.Show("PersonID cell is null.");
                }
            }
            else
            {
                // لا يوجد صف محدد
                MessageBox.Show("No row selected.");
            }
          
        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["NationalNo"].Value != null)
                {
                    string NationalNo = dataGridView1.SelectedRows[0].Cells["NationalNo"].Value.ToString();


                    int PersonId = Business.GetPersonIDByNationalNo(NationalNo);
                    if (PersonId > 0)
                    {
                        LicenseHistory licenseHistory = new LicenseHistory(PersonId);
                        licenseHistory.Show();
                    }
                    else
                    {
                        MessageBox.Show("Something wrong may happend, try again");
                    }
                }
                else
                {
                    // الخلية فارغة
                    MessageBox.Show("PersonID cell is null.");
                }
            }
            else
            {
                // لا يوجد صف محدد
                MessageBox.Show("No row selected.");
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetainLicense detainLicense = new DetainLicense();
            detainLicense.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense releaseLicense = new ReleaseDetainedLicense();
            releaseLicense.Show();
        }
    }
}
