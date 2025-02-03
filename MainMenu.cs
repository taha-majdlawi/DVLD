using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople frm = new ManagePeople();
            frm.ShowDialog();


        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void sginOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Are you sure to sign out?", "Warning!",MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Close();
                LogInScreen logInScreen = new LogInScreen();
                logInScreen.Show();
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Users manage_Users = new Manage_Users();
            manage_Users.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user = Business.GetUserByUserNameAsUser(Business.CurrentUserUserName);
            UserInfo userInfo = new UserInfo(user.PersonID);
            userInfo.Show();
        }

        private void changePaswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user = Business.GetUserByUserNameAsUser(Business.CurrentUserUserName);
            ChangePassword changePassword = new ChangePassword(user.PersonID);
            changePassword.Show();

        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestTypes manageTestTypes = new ManageTestTypes();
            manageTestTypes.Show();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypes manageApplicationTypes = new ManageApplicationTypes();
            manageApplicationTypes.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplication newLocalDrivingLicenseApplication = new NewLocalDrivingLicenseApplication();
            newLocalDrivingLicenseApplication.Show();
        }

        private void driveingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void detainToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicensApplication localDrivingLicensApplication = new LocalDrivingLicensApplication();
            localDrivingLicensApplication.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDrivers manageDrivers = new ManageDrivers();
            manageDrivers.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseApplication newInternationalLicenseApplication = new NewInternationalLicenseApplication();
            newInternationalLicenseApplication.Show();
        }

        private void internationalLicensesApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInternationalLicenseApplications manageInternationalLicenseApplications = new ManageInternationalLicenseApplications();
            manageInternationalLicenseApplications.Show();
        }

        private void renewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLicense renewLocalDrivingLicense = new RenewLocalDrivingLicense();
            renewLocalDrivingLicense.Show();
        }

        private void replacementForLostOrDamgedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replacement_for_Damaged_Licenes replacement_For_Damaged_Licenes = new Replacement_for_Damaged_Licenes();
            replacement_For_Damaged_Licenes.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicense detainLicense = new DetainLicense();
            detainLicense.Show();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense releaseDetainedLicense = new ReleaseDetainedLicense();
            releaseDetainedLicense.Show();  
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List_Detaind_Licenses list_Detaind_Licenses = new List_Detaind_Licenses();
            list_Detaind_Licenses.Show();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            List_Detaind_Licenses list_Detaind_Licenses = new List_Detaind_Licenses();
            list_Detaind_Licenses.Show();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense releaseDetainedLicense = new ReleaseDetainedLicense();
            releaseDetainedLicense.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicensApplication localDrivingLicensApplication = new LocalDrivingLicensApplication();
            localDrivingLicensApplication.Show();
        }
    }
}
