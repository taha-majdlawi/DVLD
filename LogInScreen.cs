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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.Win32;

namespace DVLD_Presentation
{
    public partial class LogInScreen : Form
    {
        public LogInScreen()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            if (Business.rememberMe)
            {
                tbUserName.Text = Business.CurrentUserUserName;
                
            }
            _setUserInfo();
        }

        private void _setUserInfo()
        {
            string userName= null;
            string password= null;

            //get user name
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\UserInfo";
            string valueName = "UserName";
            try
            {
                 userName = Registry.GetValue(keyPath, valueName, null) as string;

                if (userName != null) Console.WriteLine(userName);
                else Console.WriteLine("null");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //get user password
            string keyPath2 = @"HKEY_CURRENT_USER\SOFTWARE\UserInfo";
            string valueName2 = "Password";
            try
            {
                password = Registry.GetValue(keyPath2, valueName2, null) as string;

                if (password != null) Console.WriteLine(password);
                else Console.WriteLine("null");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if(userName!= null)
            {
                tbUserName.Text = userName;    
                tbPassword.Text = password;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {

            if(Business.IsUserExist(tbUserName.Text))
            {
                Users user = Business.GetUserByUserNameAsUser(tbUserName.Text);
                Console.WriteLine(user.PersonID.ToString());

                if (user.Password != tbPassword.Text) 
                {
                    MessageBox.Show("Enter Valid Password", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if(tbPassword.Text==user.Password && tbUserName.Text==user.UserName && user.IsActive==1)
                    {
                        MainMenu  mainMenu = new MainMenu();
                        mainMenu.Show();
                        Business.CurrentUserUserName = tbUserName.Text;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Account Has User Name\'"+user.UserName+"\' Is NOT Active", "User Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            else
            {
                MessageBox.Show("Enter Valid User Name", "Invalid User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (checkBox1.Checked) {
            Business.rememberMe = true;
                //userName
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\UserInfo";
                string valueName = "UserName";
                string valueData = tbUserName.Text;

                try
                {
                    Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                    Console.WriteLine("User name saved");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                string keyPath2 = @"HKEY_CURRENT_USER\SOFTWARE\UserInfo";
                string valueName2 = "Password";
                string valueData2 = tbPassword.Text;

                try
                {
                    Registry.SetValue(keyPath2, valueName2, valueData2, RegistryValueKind.String);
                    Console.WriteLine("Password saved");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }





        }

        private void LogInScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
