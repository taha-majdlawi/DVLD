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
    public partial class UpdateTestType : Form
    {
        int TestTypeID;
        public UpdateTestType(int testID)
        {
            InitializeComponent();
            TestTypeID = testID;
        }

        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); 
            dt = Business.GetTestInfoByID(TestTypeID);

            lblID.Text = TestTypeID.ToString();
            tbTitle.Text = dt.Rows[0]["TestTypeTitle"].ToString();
            tbDiscription.Text = dt.Rows[0]["TestTypeDescription"].ToString();
            tbFees.Text = dt.Rows[0]["TestTypeFees"].ToString();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            float fee;
           float.TryParse(tbFees.Text, out fee);
      
            if (Business.UpdateTestTypeByID(TestTypeID, tbTitle.Text,tbDiscription.Text, fee))
            {
                MessageBox.Show("Updated Successfully-:)");
            }
            else
            {
                MessageBox.Show("Updated Not Successfully-:)");
            }
        }
    }
}
