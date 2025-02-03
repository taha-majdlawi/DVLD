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
    public partial class AddUser : Form
    {
       // Person_Details userControl1 = new Person_Details(-1);
        public AddUser()
        {
            InitializeComponent();
            assignValues();
        }
        public AddUser(int PersonID)
        {
            InitializeComponent();
            assignValues(PersonID);
        }
        private void CenterControl(UserControl control)
        {
            // حساب الموقع بناءً على حجم النموذج وحجم الـ UserControl
            int x = (this.ClientSize.Width - control.Width) / 2;
            int y = (this.ClientSize.Height - control.Height) / 2;

            // ضبط الموقع الجديد
            control.Location = new Point(x, y);
        }
        private void assignValues(int PersonID)
        {
            FilterPeople pd = new FilterPeople(PersonID);

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);


            // جعل الـ UserControl يظهر في المنتصف
            CenterControl(pd);

        }
        private void assignValues()
        {
            FilterPeople pd = new FilterPeople();

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);


            // جعل الـ UserControl يظهر في المنتصف
            CenterControl(pd);

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
           
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void AddUser_Load_1(object sender, EventArgs e)
        {

        }
    }
}
