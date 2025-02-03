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
    public partial class ChangePassword : Form
    {
        int PersonID;
        public ChangePassword(int PersonID)
        {
            InitializeComponent();
            assignValues(PersonID);
            this.PersonID =  PersonID;
           
        }
        Users user = new Users();
        private void assignValues(int ID)
        {
            Person_Details pd = new Person_Details(ID);

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);


            // جعل الـ UserControl يظهر في المنتصف
            // CenterControl(pd);
            DataTable dt = new DataTable();
            dt = Business.FindUserById(ID);
            user.UserID = (int)dt.Rows[0]["UserID"];
            user.UserName = (string)dt.Rows[0]["UserName"];
            if (dt.Rows[0]["IsActive"] != DBNull.Value)
            {
                user.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]) ? 1 : 0;  // Convert bool to int (1 or 0)
            }
            else
            {
                // Handle the case where the value is DBNull (null in database)
                user.IsActive = 0;  // Default to 0 or any other default value
            }


            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName;
            if (user.IsActive == 1)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }
        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            int UserId = Business.GetUserIDByPersonID(PersonID);
            string currentPassword = Business.GetPasswordByUserID(UserId);
            if (tbCurrentPassword.Text != currentPassword&&tbCurrentPassword.Text.Length>0)
            {
                e.Cancel = true;

                errorProvider1.SetError(tbCurrentPassword, "Your Current Password Is Not Matched");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
          // e.Cancel = false;
            this.Close();
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbConfirmPassword.Text != tbPassword.Text)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbConfirmPassword.Text != null && tbPassword.Text != null)
            {
                int UserId = Business.GetUserIDByPersonID(PersonID);

              if(Business.UpdateUser(PersonID, UserId, user.IsActive, user.UserName, tbPassword.Text))
                {
                    MessageBox.Show("Password Changed","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Password Haven't Changed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            tbCurrentPassword.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
        }
    }
}
