using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
namespace DVLD_Presentation
{
    public partial class PersonCard : UserControl
    {
     
       
        public int PersonID, NationalityCountryID, Gendor;
        public string FirstName, SecondName, ThirdName, LastName, NationalNo, Email, Address, Phone, Country, ImagePath = "";
        public DateTime DateOfBirth;
        bool allChecksTrue = false;
       // string ImagePath = "";
        public PersonCard()
        {
            InitializeComponent();
            cbCountry.SelectedIndex = 193;
           
        }
        public PersonCard(int ID)
        {
            InitializeComponent();
            cbCountry.SelectedIndex = 193;
            PutData( ID);
            this.PersonID = ID;
        }
        private void PutData(int ID) 
        { 
            DataTable dt = new DataTable();
            dt = Business.FindPersonById(ID);
           // Business.PersonMode = "Update";
            button1.Text = "Update";
            lblID.Text = ID.ToString();
            
            tbFirstName.Text =  (string)dt.Rows[0]["FirstName"].ToString();
            tbSecondName.Text = (string)dt.Rows[0]["SecondName"].ToString();
            tbThirdName.Text =  (string)dt.Rows[0]["ThirdName"].ToString();
            tbLastName.Text =   (string)dt.Rows[0]["LastName"].ToString();
            tbNationalNumber.Text = (string)dt.Rows[0]["NationalNo"].ToString();

            if (dt.Rows[0]["Gendor"].ToString() == "0")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            tbEmail.Text = dt.Rows[0]["Email"].ToString();
            tbPhone.Text = dt.Rows[0]["Phone"].ToString();
            tbAddress.Text = dt.Rows[0]["Address"].ToString();
            dateTimePicker1.Value =(DateTime) dt.Rows[0]["DateOfBirth"];
            cbCountry.SelectedIndex = (int)dt.Rows[0]["NationalityCountryID"]-1;
            if (dt.Rows[0]["ImagePath"] != DBNull.Value && !string.IsNullOrEmpty((string)dt.Rows[0]["ImagePath"]))
            {
                // تحميل الصورة من المسار الموجود في قاعدة البيانات
                ImagePath = (string)dt.Rows[0]["ImagePath"];
                pictureBox1.Image = Image.FromFile((string)dt.Rows[0]["ImagePath"]);
            }
            else
            {
                // تحميل صورة افتراضية في حالة عدم وجود مسار أو إذا كانت القيمة فارغة
               // pictureBox1.Image = Image.FromFile("C:\\Users\\hp\\Pictures\\Screenshots\\Screenshot 2024-09-11 211521.png");
            }

        }

        private void PersonCard_Load(object sender, EventArgs e)
        {
            tbAddress.Multiline = true;
            tbAddress.ScrollBars = ScrollBars.Vertical;  // Optionally, add a scrollbar
            tbAddress.WordWrap = true;  // Optionally, enable word wrapping
            tbAddress.Height = 80;
        }
        private int getId()
        {
            return Business.GetLastID();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Select an Image";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                ImagePath = openFileDialog.FileName;// Optional: Set how the image should be displayed
            }
        }

        private void _assginValueToPersonObj()
        {

           PersonID = PersonID;
           FirstName = tbFirstName.Text;
           SecondName = tbSecondName.Text;
           ThirdName = tbThirdName.Text;
           LastName = tbLastName.Text;
           NationalNo = tbNationalNumber.Text;
           DateOfBirth = (DateTime)(dateTimePicker1.Value);
           Gendor = (radioButton1.Checked) ? 0 : 1;
           Phone = tbPhone.Text;
           Email = tbEmail.Text;
           Country = cbCountry.ValueMember.ToString();
           NationalityCountryID = cbCountry.SelectedIndex + 1;
           Address = tbAddress.Text;
        

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            //if (person == null)
            //{
            //    person = new clsPerson();
            //}


            if (Business.PersonMode == "AddNew")
            {


                _assginValueToPersonObj();
                if (Business.InsertPersonData(FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Phone, Email, NationalityCountryID,
                           Address, ImagePath))
                {
                    MessageBox.Show(FirstName + " Has Been Add successfully", "Done", MessageBoxButtons.OKCancel);
                }

                DataTable dtPerson = new DataTable();
                int LastID = Business.GetLastID();
                dtPerson = Business.FindPersonById(LastID);

                lblID.Text = LastID.ToString();
                Business.PersonMode = "Update";
                button1.Text = "Update";

            }


            else
            {
                if (Business.PersonMode == "Update")
                {
                    _assginValueToPersonObj();

                    if (Business.UpdatePersonData(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Phone, Email, NationalityCountryID,
                           Address, ImagePath))
                    {
                        MessageBox.Show("Person Info Was Updeted Successfully -:)");
                    }
                    else
                    {
                        MessageBox.Show("Person Info Was NOT Updeted -:)");
                    }
                }

            }
            
           // Business.PersonMode = "AddPerson";
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                e.Cancel = true;
                tbFirstName.Focus();
                errorProvider1.SetError(tbFirstName,"First Name Is Requierd Feild");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.Clear();
              
            }
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                e.Cancel = true;
                tbSecondName.Focus();
                errorProvider1.SetError(tbSecondName, "Second Name Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void tbThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbThirdName.Text))
            {
                e.Cancel = true;
                tbThirdName.Focus();
                errorProvider1.SetError(tbThirdName, "Third Name Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                e.Cancel = true;
                tbLastName.Focus();
                errorProvider1.SetError(tbLastName, "Last Name Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void tbNationalNumber_Validating(object sender, CancelEventArgs e)
        {///////////////////////////////////////////////////////////////////////////////////////////////////
            if (Business.IsNationalNumFound(tbNationalNumber.Text))
            {
                e.Cancel = true;
                tbNationalNumber.Focus();
                errorProvider1.SetError(tbNationalNumber, "National Number \'"+ tbNationalNumber.Text+ "\' Is Exist, Enter Onther one");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
                allChecksTrue = true;
            }
        }

        private void tbNationalNumber_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void tbNationalNumber_Enter(object sender, EventArgs e)
        {
        
        }

        private void tbNationalNumber_Leave(object sender, EventArgs e)
        {
          
        }

        private void tbNationalNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider1.SetError(tbPhone, "Phone Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValidEmail(tbEmail.Text))
            {
                e.Cancel = true;
                tbEmail.Focus();
                errorProvider1.SetError(tbEmail, "Email Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }

        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }  
        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                e.Cancel = true;
                tbAddress.Focus();
                errorProvider1.SetError(tbAddress, "Address Is Requierd Feild");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }
    }
}
