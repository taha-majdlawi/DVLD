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
    public partial class UserInfo : Form
    {
        int PersonID;
        public UserInfo(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            assignValues(PersonID);
            
        }
      Users user = new Users();
        
        
        private void CenterControl(UserControl control)
        {
            // حساب الموقع بناءً على حجم النموذج وحجم الـ UserControl
            int x = (this.ClientSize.Width - control.Width) / 2;
            int y = (this.ClientSize.Height - control.Height) / 2;

            // ضبط الموقع الجديد
            control.Location = new Point(x, y);
        }
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
        private void UserInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
