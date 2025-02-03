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
    public partial class ManageTestTypes : Form
    {
        public ManageTestTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable dt = new DataTable();

        // This function refreshes the DataGridView based on the provided DataTable
        private void _refreshPeopleData(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["TestTypeDescription"].Width = 400;
            dataGridView1.Columns["TestTypeID"].Width = 30;
            dataGridView1.Columns["TestTypeID"].HeaderText = "ID";
        }






        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            dt = Business.GetTestsTypes();  // Load all data initially
            _refreshPeopleData(dt);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Id = 0;
            if (dataGridView1.SelectedRows.Count > 0) // التأكد من تحديد صف واحد على الأقل
            {
                var cellValue = dataGridView1.SelectedRows[0].Cells["TestTypeID"].Value;

                if (cellValue != null && int.TryParse(cellValue.ToString(), out int applicationTypeID))
                {
                    Id = applicationTypeID;
                    // استخدام applicationTypeID هنا بعد التأكد من عدم وجود null وتحويله بنجاح إلى int
                }
                else
                {
                    MessageBox.Show("ApplicationTypeID is either null or not a valid integer.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row first.");
            }



            UpdateTestType q = new UpdateTestType(Id);
            q.Show();
        }
    }
}
