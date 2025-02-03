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
    public partial class Person_Details : UserControl
    {
   
        private int _id;
        public Person_Details(int id)
        {
            InitializeComponent();
            
            loadData(id);
          _id = id;
        }
    
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void loadData(int ID)
        {
            DataTable dataTable = new DataTable();
            dataTable = Business.FindPersonById(ID);
            if (dataTable.Rows.Count > 0)
            {
                lblPersonID.Text = dataTable.Rows[0]["PersonID"].ToString();
                lblName.Text = dataTable.Rows[0]["FirstName"].ToString() + " " + 
                    dataTable.Rows[0]["SecondName"].ToString() + " " + dataTable.Rows[0]["ThirdName"].ToString() + " "
                    + dataTable.Rows[0]["LastName"].ToString() + " ";
                lblNationalNo.Text = dataTable.Rows[0]["NationalNo"].ToString();
                  if(dataTable.Rows[0]["Gendor"].ToString()=="0")
                {
                    lblGendor.Text = "Male";
                }
                else
                {
                    lblGendor.Text = "Female";
                }

                lblEmail.Text = dataTable.Rows[0]["Email"].ToString();
                lblAddress.Text = dataTable.Rows[0]["Address"].ToString();
                string dateOfBirth="";
                string dob = dataTable.Rows[0]["DateOfBirth"].ToString();
                for (int i = 0; i < 10; i ++)
                {
                    dateOfBirth += dob[i];
                }
                lblDateOfBirth.Text = dateOfBirth;
                lblPhone.Text = dataTable.Rows[0]["Phone"].ToString();
                lblCountry.Text = Business.GetCountryNameBuID( (int)dataTable.Rows[0]["NationalityCountryID"] );
                string imagePath = dataTable.Rows[0]["ImagePath"].ToString();
                if (System.IO.File.Exists(imagePath)&&imagePath!=""&&imagePath!=null)
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
 
                   pictureBox1.Image = Image.FromFile("C:\\Users\\hp\\Pictures\\Screenshots\\Screenshot 2024-09-11 211521.png");
                }


            }
            else
            {

                int i =1 ;
                if (i > 1)
                {
                    MessageBox.Show("Person NOT Found, Please Enter Valied Info");
                }
                i++;
            }
            if(ID < -1)
            {
                lblPersonID.Text = "???";
                lblName.Text = "???";
                lblNationalNo.Text = "???";
               
                    lblGendor.Text = "???";


               
                lblEmail.Text = "???";
                lblAddress.Text = "???";
               
                lblDateOfBirth.Text = "???";
                lblPhone.Text = "???";
                lblCountry.Text = "???";
               
              
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

           AddPerson addPerson = new AddPerson(_id);
            addPerson.ShowDialog();

        }
    }
}
