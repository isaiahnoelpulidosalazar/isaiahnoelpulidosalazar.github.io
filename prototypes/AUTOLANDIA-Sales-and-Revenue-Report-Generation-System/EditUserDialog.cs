using CSSimpleFunctions;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditUserDialog : MaterialForm
    {
        string Username, Password;
        int Type;

        public EditUserDialog(string Username, string Password, int Type)
        {
            InitializeComponent();

            this.Username = Username;
            this.Password = Password;
            this.Type = Type;

            TB_U.Text = Username;
            TB_P.Text = Password;

            CB_T.Items.Add("Admin");
            CB_T.Items.Add("User");
            CB_T.SelectedIndex = Type;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (NoticeMessageBox("Are you sure you want to delete this account?") == DialogResult.Yes)
            {
                try
                {
                    RecordActivity($"Deleted {(Type == 0 ? "admin" : "user")} [{Username}]");

                    DeleteAccount(Username, Type).ExecuteNonQuery();

                    OkMessageBox("Successfully deleted account!");
                    GlobalPeopleForm.RefreshAccounts();
                    GlobalActivityRecordForm.RefreshActivities();
                    Close();
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                }
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(TB_U.Text))
            {
                ErrorMessage += "Please input a username.\n";
            }
            if (string.IsNullOrEmpty(TB_P.Text))
            {
                ErrorMessage += "Please input a password.\n";
            }
            if (Check.HasSymbols(TB_U.Text) || Check.HasSpaces(TB_U.Text))
            {
                ErrorMessage += "Username cannot contain spaces nor symbols.\n";
            }
            if (Check.HasSymbols(TB_P.Text) || Check.HasSpaces(TB_P.Text))
            {
                ErrorMessage += "Password cannot contain spaces nor symbols.\n";
            }
            if (CB_T.SelectedIndex < 0)
            {
                ErrorMessage += "Please select a type for this account.\n";
            }
            if (!Username.Equals(TB_U.Text))
            {
                foreach (AccountItem Account in GlobalAccountList)
                {
                    if (Account.Username.Equals(TB_U))
                    {
                        ErrorMessage += "This username is already taken.\n";
                        break;
                    }
                }
            }

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    RecordActivity($"Updated {(Type == 0 ? "admin" : "user")} [{Username}] to {(CB_T.SelectedIndex == 0 ? "admin" : "user")} [{TB_U.Text}]");

                    UpdateAccount(TB_U.Text, TB_P.Text, CB_T.SelectedIndex, Type);

                    OkMessageBox("Successfully updated account!");
                    GlobalPeopleForm.RefreshAccounts();
                    GlobalActivityRecordForm.RefreshActivities();
                    Close();
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                }
            }
            else
            {
                AlertMessageBox(ErrorMessage);
            }
        }
    }
}
