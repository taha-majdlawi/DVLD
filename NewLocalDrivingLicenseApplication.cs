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
    public partial class NewLocalDrivingLicenseApplication : Form
    {
        public NewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 2;
            lblUserName.Text= Business.CurrentUserUserName;
            lblDate.Text = DateTime.Now.ToString();
            lblFees.Text = "15 $";
            button1.Focus();
            addTaps(-1);
        }
        private Person_Details personDetails;

        private void NewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {

        }
        private void addTaps(int PersonID=-1 )
        {
            if (PersonID < 0 || textBox1.Text=="")
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



            if (PersonID > 0 ) 
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
           

        }
        private void button1_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox1.Text != null)
            {
                tabControl1.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Enter Valiad PersonID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int personID;
            int.TryParse(textBox1.Text, out personID);
            if (Business.IsPersonHaveTheSameApplication(1, personID,1, comboBox2.SelectedIndex + 1))
            {
                MessageBox.Show("This Person Have the Same Application Active","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                
                int UserID;
                Users user = Business.GetUserByUserNameAsUser(Business.CurrentUserUserName);
                UserID = user.UserID;

                bool isAddedToApplications = true;
                int LastApplicationID = Business.InsertApplicationToDataBase(personID, DateTime.Now, 1, 1, DateTime.Now, 15, UserID); ;
                if (LastApplicationID == 0) isAddedToApplications = false;

                if (isAddedToApplications)
                {

                    MessageBox.Show("Application ID :" + LastApplicationID, "Info");
                }

                bool isAdeddToLocalDrivingLicenseAplication = Business.InsertLocalDrivingLicenseAplication(LastApplicationID, comboBox2.SelectedIndex + 1);
                if (isAdeddToLocalDrivingLicenseAplication && isAddedToApplications && LastApplicationID != 0)
                {
                    MessageBox.Show("Added Successfully-:)", "Added",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("NOT Added Successfully-:)", "Added");
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
