using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ChangePasswordDialog : MaterialForm
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TB_OldPassword.Text.Trim()) || string.IsNullOrEmpty(TB_NewPassword.Text.Trim()))
            {
                AlertMessageBox("Current and new passwords cannot be empty.");
            }
            else
            {
                if (LoggedAccount.Password.Equals(TB_OldPassword.Text.Trim()))
                {
                    if (NoticeMessageBox("Are you sure you want to change your password?") == DialogResult.Yes)
                    {
                        try
                        {
                            ChangePassword(LoggedAccount.Username, TB_NewPassword.Text.Trim()).ExecuteNonQuery();
                            OkMessageBox("Successfully changed password!");
                            RecreateGlobalAccountList();
                            Close();
                        }
                        catch (Exception Exception)
                        {
                            AlertMessageBox(Exception.Message);
                        }
                    }
                }
                else
                {
                    AlertMessageBox("Incorrect password.");
                }
            }
        }
    }
}
