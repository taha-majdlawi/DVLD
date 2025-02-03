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
    public partial class Update_Application_Types : Form
    {
        int ApplicationID;
        public Update_Application_Types(int applictionID)
        {
            InitializeComponent();
            ApplicationID = applictionID;
        }

        private void Update_Application_Types_Load(object sender, EventArgs e)
        {
          
            DataTable dt = new DataTable();
            dt = Business.GetApplicationTypes(ApplicationID);
            if (dt != null)
            {
                lblID.Text = ApplicationID.ToString();
                tbTitle.Text = dt.Rows[0]["ApplicationTypeTitle"].ToString();
                tbFees.Text = dt.Rows[0]["ApplicationFees"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            float fees;
            float.TryParse(tbFees.Text, out fees);

            MessageBox.Show(fees + "");
           
            if (Business.UpdateApplicationInfo(ApplicationID, tbTitle.Text, fees))
            {
                MessageBox.Show("Application Updated Successfully-:)");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
