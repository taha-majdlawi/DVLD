using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLD_Presentation
{
    public partial class AddPerson : Form
    {
        
        private clsPerson currentPerson;
        public AddPerson()
        {
            InitializeComponent();

            assignValues();
        }
        public AddPerson(int ID)
        {
           
            InitializeComponent();
            if (Business.PersonMode == "Update")
            {
                label2.Text = "Update Person";
            }
            assignValues(ID);
        }
        private void CenterControl(UserControl control)
        {
            // حساب الموقع بناءً على حجم النموذج وحجم الـ UserControl
            int x = (this.ClientSize.Width - control.Width) / 2;
            int y = (this.ClientSize.Height - control.Height) / 2;

            // ضبط الموقع الجديد
            control.Location = new Point(x, y);
        }
        private void assignValues(int ID )
        {
            PersonCard pd = new PersonCard(ID);
          
            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);


            // جعل الـ UserControl يظهر في المنتصف
            CenterControl(pd);
           
        }
        private void assignValues()
        {
            PersonCard pd = new PersonCard();

            pd.Dock = DockStyle.None;

            this.Controls.Add(pd);


            // جعل الـ UserControl يظهر في المنتصف
            CenterControl(pd);

        }
        private void LoadPersonData(clsPerson person)
        {
            int LastID =Business.GetLastID();
           DataTable dt = new DataTable();
            dt = Business.FindPersonById(LastID);
        }

        
        private void AddPerson_Load(object sender, EventArgs e)
        {
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       

        private void personCard1_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void changeToUpdate()
        {
            label2.Text = "Update Person";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
