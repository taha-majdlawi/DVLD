using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
namespace DVLD_Presentation
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            // Set default filter
        }

        DataTable dt = new DataTable();

        // This function refreshes the DataGridView based on the provided DataTable
        private void _refreshPeopleData(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;

            

        }

        // On form load, populate the grid with all people
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            dt = Business.GetPeopleData();  // Load all data initially
            _refreshPeopleData(dt);
        }

        // Handle filter selection from ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                textBox1.Visible = true;
            }


            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dt = Business.GetPeopleData();  // Load all data initially
                    _refreshPeopleData(dt);
                    break;

                case 2:
                    _refreshPeopleData(dt);
                    break;
            }

            textBox1.Text = "";      // Clear the previous input
            textBox1.Focus();        // Set focus to the TextBox for the user to enter input
        }

        // Handle when the user presses PersonID key (e.g., Enter) in the TextBox
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // When the user presses the Enter key
            {
                // Call the method to handle search based on the selected filter
                PerformSearch();
            }
        }

        // Method to perform the search
        private void PerformSearch()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Visible = false;
                    dt = Business.GetPeopleData();  // Reload all data
                    _refreshPeopleData(dt);
                    break;
                case 1: // Example filter by ID
                    if (textBox1.Text.Length >= 1)
                    {
                        int num;
                        bool isValidId = int.TryParse(textBox1.Text, out num);

                        if (isValidId)
                        {
                            // Fetch the data for the specific ID
                            dt = Business.FindPersonById(num);

                            if (dt.Rows.Count > 0)
                            {
                                _refreshPeopleData(dt);  // Refresh the DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Person NOT Found!!");
                                dt = Business.GetPeopleData();  // Reload all data
                                _refreshPeopleData(dt);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter PersonID valid ID.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter an ID.");
                    }
                    break;
                case 2:
                    dt = Business.FindPersonByNationalNumber(textBox1.Text);
                    _refreshPeopleData(dt);
                    break;
                case 3:
                    dt = Business.FindPersonByFirstName(textBox1.Text);
                    _refreshPeopleData(dt);
                    break;
                case 4:
                    dt = Business.FindPersonBySecondName(textBox1.Text);
                    _refreshPeopleData(dt);
                    break;
                case 5:
                    dt = Business.FindPersonByThiredName(textBox1.Text);
                    _refreshPeopleData(dt);
                    break;
                case 6:
                    dt = Business.FindPersonByLastName(textBox1.Text);
                    _refreshPeopleData(dt);
                    break;

                default:
                    dt = Business.GetPeopleData();  // Load all data for default or other filters
                    _refreshPeopleData(dt);
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Business.PersonMode = "AddNew";
            AddPerson ap = new AddPerson();
            
            ap.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt = Business.GetPeopleData();
            _refreshPeopleData(dt);
        }


        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0)
                {
                    // اختيار الصف
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        private void edirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Business.PersonMode = "Update";
            var cellValue = dataGridView1.SelectedRows[0].Cells["PersonID"].Value;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["PersonID"].Value != null)
                {
                    int personID;
                    // حاول تحويل القيمة إلى عدد صحيح
                    if (int.TryParse(dataGridView1.SelectedRows[0].Cells["PersonID"].Value.ToString(), out personID))
                    {
                        // نجاح التحويل
                         MessageBox.Show("Person ID: " + personID);
                       
                        // إنشاء كائن من Person_Info باستخدام القيمة PersonID
                        AddPerson addPerson = new AddPerson(personID);
                        addPerson.ShowDialog(); //
                       
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
            
            Business.PersonMode = "AddPerson";
            dt = Business.GetPeopleData();
            _refreshPeopleData(dt);
        }

        private void ManagePeople_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0)
                {
                    // قم بتحديد الصف الذي تم النقر عليه
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;
                }
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // تحقق أولاً من أن هناك صفاً محدداً
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // تحقق من أن القيمة ليست فارغة
                if (dataGridView1.SelectedRows[0].Cells["PersonID"].Value != null)
                {
                    int a;
                    // حاول تحويل القيمة إلى عدد صحيح
                    if (int.TryParse(dataGridView1.SelectedRows[0].Cells["PersonID"].Value.ToString(), out a))
                    {
                        // نجاح التحويل
                      // MessageBox.Show("Person ID: " + PersonID);

                        // إنشاء كائن من Person_Info باستخدام القيمة PersonID
                        Person_Info info = new Person_Info(a);
                        info.Show();
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddPerson ap = new AddPerson();
            ap.ShowDialog();
            

            dt = Business.GetPeopleData();
            _refreshPeopleData(dt);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID;
            int.TryParse(dataGridView1.SelectedRows[0].Cells["PersonID"].Value.ToString(), out PersonID);
          if(MessageBox.Show("Are You Sure For Delete Person Have ID:" + PersonID + " From Database?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                if (Business.DeletePersonByIDFromDataBase(PersonID))
                {

                }
                else
                {
                    MessageBox.Show("Cant Delete This Person Because He/She May Have Some Relations In Database.","Delete Faild",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                }
            }
            else
            {
              
            }

            dt = Business.GetPeopleData();
            _refreshPeopleData(dt);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 7)
            {
                // Check if the key pressed is a number or a control key (like Backspace)
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Block the input if it's not a number
                }

            }
            if( comboBox1.SelectedIndex == 3 || comboBox1.SelectedIndex == 4 ||
                comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 6 || comboBox1.SelectedIndex == 8
                || comboBox1.SelectedIndex == 9)
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Block the input if it's not a letter or control character
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    } 
    }
