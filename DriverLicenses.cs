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
    public partial class DriverLicenses : UserControl
    {
        int PersonID;
        public DriverLicenses(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            refreashDataGridView();
        }
        private void refreashDataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = Business.GetLicensesHistory(PersonID);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
             

            dataGridView2.Rows.Clear();
            int DriverID =Business.GetDriverIDByPersonID(PersonID);
            dataGridView2.DataSource = Business.GetInternationalLicenseInfo(DriverID);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void DriverLicenses_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["LicenseID"].Value != null)
                {
                 
                    // حاول تحويل القيمة إلى عدد صحيح
                    if (int.TryParse(dataGridView1.SelectedRows[0].Cells["LicenseID"].Value.ToString(), out LicenseID))
                    {
                       ShowLicense showLicense = new ShowLicense(LicenseID);
                        showLicense.ShowDialog();
                    }
                    else
                    {
                        // فشل التحويل
                        MessageBox.Show("Unable to convert PersonID to an integer.");
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
    }
}
