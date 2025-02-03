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
    public partial class FilterPeople : UserControl
    {
        private TabControl tabControl;          // المتغير الذي سيخزن TabControl
        private Person_Details personDetails;   // المتغير الذي سيخزن Person_Details
        private Button updateButton;            // زر التحديث
        int ID;
        public FilterPeople()
        {
            InitializeComponent();

            addTaps( -1);


        }

        public FilterPeople(int PersonID)
        {
            InitializeComponent();

            addTaps(PersonID);

            this.ID = PersonID;
        }

        // الدالة التي يتم استدعاؤها عند الضغط على الزر


        private void addTaps(int PersonID = -1)
        {
            if (PersonID< 0)
            {
                if (personDetails != null)
                {
                    tabControl1.TabPages[0].Controls.Remove(personDetails);  // إزالة الـ UserControl الحالي
                }

                // إنشاء UserControl جديد وإضافته إلى التبويب الأول
                personDetails = new Person_Details(PersonID);
                personDetails.Size = new Size(650, 250);

                // ضبط موقع الـ UserControl ليكون في منتصف التبويب
                personDetails.Location = new Point(
                    (tabControl1.TabPages[0].ClientSize.Width - personDetails.Width) / 2,
                    (tabControl1.TabPages[0].ClientSize.Height - personDetails.Height) / 2
                );

                // إضافة UserControl إلى التبويب
                tabControl1.TabPages[0].Controls.Add(personDetails);
            }
            if (PersonID == 0)
            {
                MessageBox.Show("Please enter a valid ID.");
            }



               if (PersonID > 0 && Business.UserMode=="AddNew") //id <0 we are in addnew mode
            {
                // تحقق مما إذا كان Person_Details موجودًا بالفعل في التبويب الأول، ثم قم بتحديثه
                if (personDetails != null)
                {
                    tabControl1.TabPages[0].Controls.Remove(personDetails);  // إزالة الـ UserControl الحالي
                }

                // إنشاء UserControl جديد وإضافته إلى التبويب الأول
                personDetails = new Person_Details(PersonID);
                personDetails.Size = new Size(650, 250);

                // ضبط موقع الـ UserControl ليكون في منتصف التبويب
                personDetails.Location = new Point(
                    (tabControl1.TabPages[0].ClientSize.Width - personDetails.Width) / 2,
                    (tabControl1.TabPages[0].ClientSize.Height - personDetails.Height) / 2
                );

                // إضافة UserControl إلى التبويب
                tabControl1.TabPages[0].Controls.Add(personDetails);


            }
            else
            {
                if (Business.UserMode=="Update") {// تحقق مما إذا كان Person_Details موجودًا بالفعل في التبويب الأول، ثم قم بتحديثه
                    if (personDetails != null)
                    {
                        tabControl1.TabPages[0].Controls.Remove(personDetails);  // إزالة الـ UserControl الحالي
                    }

                    // إنشاء UserControl جديد وإضافته إلى التبويب الأول
                    personDetails = new Person_Details(PersonID);
                    personDetails.Size = new Size(650, 250);

                    // ضبط موقع الـ UserControl ليكون في منتصف التبويب
                    personDetails.Location = new Point(
                        (tabControl1.TabPages[0].ClientSize.Width - personDetails.Width) / 2,
                        (tabControl1.TabPages[0].ClientSize.Height - personDetails.Height) / 2
                    );

                    // إضافة UserControl إلى التبويب
                    tabControl1.TabPages[0].Controls.Add(personDetails);


                    groupBox1.Enabled = false;
                    textBox1.Text = PersonID.ToString();
                    DataTable dt = new DataTable();
                    dt = Business.FindUserById(PersonID);
                    lblUserID.Text = Business.GetUserIDByPersonID(PersonID).ToString();
                    tbUserName.Text = dt.Rows[0]["UserName"].ToString();
                    tbPassword.Text = dt.Rows[0]["Password"].ToString();
                    tbConfirmPassword.Text = dt.Rows[0]["Password"].ToString();
                    int isActive = 0;  // Default value

                    if (dt.Rows[0]["IsActive"] != DBNull.Value)  // Check for DBNull
                    {
                        // If the value is a boolean (as is common for IsActive), convert to int (1 for true, 0 for false)
                        isActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]) ? 1 : 0;
                    }
                    if (isActive == 0)
                    {
                        checkBox1.Checked = false;
                    }
                    else
                    {
                        checkBox1.Checked = true;

                    }
                }
            }
          
        }

        private void FilterPeople_Load(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            
        }

        private void FilterPeople_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int PersonID;
            bool isValidId = int.TryParse(textBox1.Text, out PersonID);
            bool personIsFound = Business.IsPersonFound(PersonID);
            bool userIsFound = Business.IsUserIxistByPersonID(PersonID);


            if (personIsFound)
            {


                if (userIsFound)
                {
                    MessageBox.Show("This Person Is Already a User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                else
                {
                    tabControl1.SelectedIndex = 1;
                }
            }
            else
            {
              
                MessageBox.Show("This Person Is NOT Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // استدعاء دالة addTaps لتمرير ID جديد (يمكنك تخصيص PersonID بناءً على إدخال المستخدم)
            int newPersonID;

            // حاول تحويل النص المدخل إلى عدد صحيح
            if (int.TryParse(textBox1.Text, out newPersonID))
            {
                // إذا كان التحويل ناجحًا، استدعِ الدالة addTaps مع PersonID الجديد
                addTaps(newPersonID);
            }
            else
            {
                // إذا لم يكن النص المدخل صالحًا، عرض رسالة خطأ للمستخدم
                MessageBox.Show("Please enter a valid ID.");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Business.UserMode == "AddNew")
            {
                bool isActive = checkBox1.Checked;
                int isActiv;
                if (isActive) isActiv = 1;
                else isActiv = 0;
                int PersonID = int.Parse(textBox1.Text);

                if (Business.InsertUser(PersonID, isActiv, tbUserName.Text, tbPassword.Text))
                {
                    MessageBox.Show("User Added Saccessfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Something Wrong happend While Adding User! please try again-:)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label6.Text = "Update User";
                Business.UserMode = "Update";
             
                int UserID = Business.GetUserIDByPersonID(PersonID);
                lblUserID.Text = UserID.ToString();
            }
            else
            {
                bool isActive = checkBox1.Checked;
                int isActiv;
                if (isActive) isActiv = 1;
                else isActiv = 0;
                int PersonID = int.Parse(textBox1.Text);
                int UserID = Business.GetUserIDByPersonID(PersonID);
                if (Business.UpdateUser(PersonID,UserID,isActiv, tbUserName.Text, tbPassword.Text))
                {
                    MessageBox.Show("User Updated Saccessfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Something Wrong happend While Updating User! please try again-:)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbConfirmPassword.Text!=tbPassword.Text)
            {
                e.Cancel = true;
               
                errorProvider1.SetError(tbConfirmPassword, "Password NOT Matched");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }
    }
}
