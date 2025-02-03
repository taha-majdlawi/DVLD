using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Presentation
{
    public partial class Manage_Users : Form
    {
        public Manage_Users()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        DataTable dt = new DataTable();

        // This function refreshes the DataGridView based on the provided DataTable
        int a = 1;
        private void LoadDataIntoDataGridView()
        {
          
            // استبدل بـ connection string الخاص بك
            string connectionString = "Server=.;Database=DVLD;User Id=sa;Password=123456;";
            string query = "SELECT UserID, PersonID, UserName, IsActive FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // ربط البيانات بـ DataGridView
                    dataGridView1.DataSource = dataTable;

                    // تخصيص الأعمدة إذا لزم الأمر
                    dataGridView1.Columns["IsActive"].HeaderText = "Is Active";

                    if (a == 1)
                    {
                        // إضافة عمود FullName
                        DataGridViewTextBoxColumn fullNameColumn = new DataGridViewTextBoxColumn();
                        fullNameColumn.Name = "FullName"; // Set the name of the fullNameColumn
                        fullNameColumn.HeaderText = "Full Name"; // Set the header text
                        fullNameColumn.Width = 210;
                        dataGridView1.Columns.Insert(2, fullNameColumn);

                        // تعيين القيم في عمود FullName
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["PersonID"].Value != null)
                            {
                                int personID = (int)row.Cells["PersonID"].Value;
                                row.Cells["FullName"].Value = Business.GetPersonFullNameByID(personID);

                            }
                        }
                    }
                    a++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }
        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // تحديث القيم الخاصة بعمود FullName بعد تحميل البيانات
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["PersonID"].Value != null)
                {
                    int personID = (int)row.Cells["PersonID"].Value;
                    row.Cells["FullName"].Value = Business.GetPersonFullNameByID(personID);
                }
            }
        }

        private void LoadDataIntoDataGridView(int UserID = 0, string UserName = "", int PersonID = 0, int IsActive = -1, string query = "")
        {
            // استبدل بـ connection string الخاص بك
            string connectionString = "Server=.;Database=DVLD;User Id=sa;Password=123456;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    // إضافة المعاملات إلى الـ command
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    connection.Open();

                    // استخدام command بدلاً من query في SqlDataAdapter
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // ربط البيانات بـ DataGridView
                    dataGridView1.DataSource = dataTable;

                    // تخصيص الأعمدة إذا لزم الأمر
                    dataGridView1.Columns["IsActive"].HeaderText = "Is Active";

                    if (a == 1)
                    {
                        // إضافة عمود FullName
                        DataGridViewTextBoxColumn fullNameColumn = new DataGridViewTextBoxColumn();
                        fullNameColumn.Name = "FullName"; // Set the name of the fullNameColumn
                        fullNameColumn.HeaderText = "Full Name"; // Set the header text
                        fullNameColumn.Width = 210;
                        dataGridView1.Columns.Insert(2, fullNameColumn);

                        // تعيين القيم في عمود FullName
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["PersonID"].Value != null)
                            {
                                int personID = (int)row.Cells["PersonID"].Value;
                                row.Cells["FullName"].Value = Business.GetPersonFullNameByID(personID);
                            }
                        }
                    }
                    a++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally { connection.Close(); }
            }
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }



        private void Manage_Users_Load(object sender, EventArgs e)
        {
            // Set the binding property if using data binding

            // Add the fullNameColumn to the DataGridView
           
         //   dt = Business.GetAllUsers();  // Load all data initially
          //  _refreshUserData(dt);

        //    _DefinetionOfColumns();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0&& comboBox1.SelectedIndex <4)
            {
                textBox1.Visible = true;
               
               
            }
            if(comboBox1.SelectedIndex == 4)
            {
                comboBox2.Visible = true;
            }

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dt = Business.GetAllUsers();
                    // Load all data initially
                    LoadDataIntoDataGridView();
                    PerformSearch();
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
            textBox1.KeyDown += textBox1_KeyDown;

        }

        // Method to perform the search
        private void PerformSearch()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    textBox1.Visible = false;
                    LoadDataIntoDataGridView();
                   // Reload all data


                    break;
                case 1: // Example filter by ID
                    if (textBox1.Text.Length >= 1)
                    {
                        int UserID;
                        bool isValidId = int.TryParse(textBox1.Text, out UserID);

                        if (isValidId)
                        {
                            // Fetch the data for the specific ID
                            LoadDataIntoDataGridView(UserID: UserID , query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where UserID=@UserID" );

                            if (dt.Rows.Count > 0)
                            {
                                 // Refresh the DataGridView
                            }
                           
                        }
                        else
                        {
                            MessageBox.Show("Please ente valid ID.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter an ID.");
                    }
                    break;
                case 2:
                    if (textBox1.Text.Length >= 1)
                    {
                        string UserName=textBox1.Text;            
                            // Fetch the data for the specific ID
                            LoadDataIntoDataGridView(UserName: UserName, query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where UserName=@UserName");
                    }
                    else
                    {
                        MessageBox.Show("Please enter an ID.");
                    }
                    break;
                case 3:
                    if (textBox1.Text.Length >= 1)
                    {
                        int PersonID;
                        bool isValidId = int.TryParse(textBox1.Text, out PersonID);

                        if (isValidId)
                        {
                            // Fetch the data for the specific ID
                            LoadDataIntoDataGridView(PersonID: PersonID, query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where PersonID=@PersonID");

                           

                        }
                        else
                        {
                            MessageBox.Show("Please ente valid ID.");
                         
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter an ID.");
                    }

                    break;
                case 4:
                     textBox1.Visible = false;
                    comboBox2.Visible = true;
                    if (comboBox2.SelectedIndex == 0)
                    {
                        LoadDataIntoDataGridView( query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where IsActive=1");
                    }
                    break;
                    
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
           AddUser addUser = new AddUser();
            Business.UserMode = "AddNew";
            addUser.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

       
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.SelectedRows[0].Cells["UserID"].Value;
            if (MessageBox.Show("Are You Sure Do You Want to Delete User Has UserID \'" + UserID + "\' ?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                if (Business.DeletedUserByUserID(UserID))
                {
                    MessageBox.Show("User Has UserID \'" + UserID + "\' Deleted Successfully-:)");

                }
                else
                {
                    MessageBox.Show("User Has UserID \'" + UserID + "\' DOSNT Deleted, It may Have Some Conection In Database");
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
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
                        UserInfo info = new UserInfo(a);
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

        private void toolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {

            // Check if the right mouse button is clicked
            if (e.Button == MouseButtons.Right)
            {
                // Get the current mouse position in relation to the DataGridView
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);

                // Ensure the user clicked on a row, not the column header or outside the grid
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Clear current selection and select the row under the mouse
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;
                }
            }
           

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Business.UserMode = "AddNew";
            AddUser user = new AddUser();
            user.Show();

        }

        private void edirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Business.UserMode = "Update";
            var cellValue = dataGridView1.SelectedRows[0].Cells["PersonID"].Value;
            AddUser user = new AddUser((int)cellValue);
            user.Show();
           
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
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

                        ChangePassword changePassword = new ChangePassword((int)dataGridView1.SelectedRows[0].Cells["PersonID"].Value);
                        changePassword.Show();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 3)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Block the input if it's not a number
                }
            }
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                LoadDataIntoDataGridView(query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where IsActive=1");
            }
            else if (comboBox2.SelectedIndex == 1)
            
            {
                LoadDataIntoDataGridView(query: @"SELECT UserID, PersonID, UserName, IsActive FROM Users Where IsActive=0");

            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // When the user presses the Enter key
            {
                // Call the method to handle search based on the selected filter

                PerformSearch();


            }
            textBox1.KeyDown += textBox1_KeyDown;
        }
    }
}
