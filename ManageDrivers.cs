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
    public partial class ManageDrivers : Form
    {
        public ManageDrivers()
        {
            InitializeComponent();
            createColumns();
            assginValuesToDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void createColumns()
        {
            // Create a new column1
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "DriverID"; // The name for identifying the column1 programmatically
            column.HeaderText = "DriverID"; // The text displayed in the column1 header
            dataGridView1.Columns.Add(column);

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "PersonID";
            column1.HeaderText = "PersonID";
        
            dataGridView1.Columns.Add(column1);

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "NationalNo";
            column2.HeaderText = "NationalNo";
            dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "FullName";
            column3.HeaderText = "FullName";
            column3.Width = 250;
            dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "Date";
            column4.HeaderText = "Date";
            column4.Width = 150;
            dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "ActiveLicense";
            column5.HeaderText = "ActiveLicenses";
            dataGridView1.Columns.Add(column5);

           


        }

        private void assginValuesToDataGridView()
        {
            DataTable dt = Business.GetDriversInfo();

            foreach (DataRow row in dt.Rows)
            {
                int DriverID = (int)row["DriverID"];
                int PersonID = (int)row["PersonID"];
                string Date = row["CreatedDate"].ToString();

                string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                string FullName = Business.GetPersonFullNameByID(PersonID);
                int NumberOfActiveLicenses = Business.NumberOfActiverLicenses(PersonID, DriverID);


              dataGridView1.Rows.Add(DriverID, PersonID, NationalNo, FullName, Date, NumberOfActiveLicenses);
            }
        }

        private void assginValuesToDataGridViewByDriverID(int DriverIDd)
        {
            DataTable dt = Business.GetDriversInfo();

            foreach (DataRow row in dt.Rows)
            {
                int DriverID = (int)row["DriverID"];
                int PersonID = (int)row["PersonID"];
                string Date = row["CreatedDate"].ToString();

                string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                string FullName = Business.GetPersonFullNameByID(PersonID);
                int NumberOfActiveLicenses = Business.NumberOfActiverLicenses(PersonID, DriverID);

                if(DriverIDd == DriverID) 
                dataGridView1.Rows.Add(DriverID, PersonID, NationalNo, FullName, Date, NumberOfActiveLicenses);
            }
        }

        private void assginValuesToDataGridViewByPersonID(int PersonIDd)
        {
            DataTable dt = Business.GetDriversInfo();

            foreach (DataRow row in dt.Rows)
            {
                int DriverID = (int)row["DriverID"];
                int PersonID = (int)row["PersonID"];
                string Date = row["CreatedDate"].ToString();

                string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                string FullName = Business.GetPersonFullNameByID(PersonID);
                int NumberOfActiveLicenses = Business.NumberOfActiverLicenses(PersonID, DriverID);

                if (PersonIDd == PersonID)
                    dataGridView1.Rows.Add(DriverID, PersonID, NationalNo, FullName, Date, NumberOfActiveLicenses);
            }
        }

        private void assginValuesToDataGridViewByNationalNo(string NationalNoo)
        {
            DataTable dt = Business.GetDriversInfo();

            foreach (DataRow row in dt.Rows)
            {
                int DriverID = (int)row["DriverID"];
                int PersonID = (int)row["PersonID"];
                string Date = row["CreatedDate"].ToString();

                string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                string FullName = Business.GetPersonFullNameByID(PersonID);
                int NumberOfActiveLicenses = Business.NumberOfActiverLicenses(PersonID, DriverID);

                if (NationalNoo == NationalNo)
                    dataGridView1.Rows.Add(DriverID, PersonID, NationalNo, FullName, Date, NumberOfActiveLicenses);
            }
        }

        private void assginValuesToDataGridViewByFullName(string FullNamee)
        {
            DataTable dt = Business.GetDriversInfo();

            foreach (DataRow row in dt.Rows)
            {
                int DriverID = (int)row["DriverID"];
                int PersonID = (int)row["PersonID"];
                string Date = row["CreatedDate"].ToString();

                string NationalNo = Business.GetNationalNumberByPersonID(PersonID);
                string FullName = Business.GetPersonFullNameByID(PersonID);
                int NumberOfActiveLicenses = Business.NumberOfActiverLicenses(PersonID, DriverID);

                if (FullNamee == FullName)
                    dataGridView1.Rows.Add(DriverID, PersonID, NationalNo, FullName, Date, NumberOfActiveLicenses);
            }
        }
        private void _RefreashDataGridView()
        {
            dataGridView1.Rows.Clear();
            assginValuesToDataGridView();
        }
      
        private void ManageDrivers_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                textBox1.Text = "";
                textBox1.Visible = true;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "";
                textBox1.Visible = false;
                _RefreashDataGridView();
            }

            if ((comboBox1.SelectedIndex == 1)) 
            {
              
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {

                case 1:
                    dataGridView1.Rows.Clear();
                    int DriverID;
                    if (int.TryParse(textBox1.Text, out DriverID))
                    {


                    }
                    else
                    {

                       
                    }

                    assginValuesToDataGridViewByDriverID(DriverID);
                    break;
                case 2:
                    int PersonID;
                    if (int.TryParse(textBox1.Text, out PersonID))
                    {


                    }
                    else
                    {

                      
                    }
                    dataGridView1.Rows.Clear();
                    assginValuesToDataGridViewByPersonID(PersonID);
                    break;
                case 3:
                    dataGridView1.Rows.Clear();
                    assginValuesToDataGridViewByNationalNo(textBox1.Text);
                    break;
                case 4:
                    dataGridView1.Rows.Clear();
                    assginValuesToDataGridViewByFullName(textBox1.Text);
                    break;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 2)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true; // إلغاء إدخال المفتاح
                }
            }
        }


    }
}
