using MaterialSkin.Controls;
using System;
using System.IO;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SettingsDialog : MaterialForm
    {
        public SettingsDialog()
        {
            InitializeComponent();

            if (!LoggedAccount.IsAdmin)
            {
                ChangePassword.Visible = false;
            }
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog FBD = new FolderBrowserDialog())
            {
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    File.Copy("AUTOLANDIA.db", Path.Combine(FBD.SelectedPath, "AUTOLANDIA.autlndia"));
                }
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Filter = "AUTOLANDIA Database Backup File (*.autlndia)|*.autlndia";
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(OFD.FileName, "AUTOLANDIA.db", true);
                    Application.Restart();
                }
            }
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            new ChangePasswordDialog().ShowDialog();
        }
    }
}
