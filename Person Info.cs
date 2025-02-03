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
    public partial class Person_Info : Form
    {
      
     //   clsPerson person = new clsPerson();
        public Person_Info(int Id)
        {
            InitializeComponent();
            assignValues(Id);
          
        }

        private void Person_Info_Load(object sender, EventArgs e)
        {
            //assignValues();
        }
      
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
            Person_Details pd = new Person_Details (ID);
            
            pd.Dock = DockStyle.None;
            
            this.Controls.Add(pd);
          

            // جعل الـ UserControl يظهر في المنتصف
            CenterControl(pd);
        }

        private void person_Details1_Load(object sender, EventArgs e)
        {

        }
    }
}
