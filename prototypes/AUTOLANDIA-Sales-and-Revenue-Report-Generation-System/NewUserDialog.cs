using CSSimpleFunctions;
using MaterialSkin.Controls;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class NewUserDialog : MaterialForm
    {
        public NewUserDialog()
        {
            InitializeComponent();

            CB_T.Items.Add("Admin");
            CB_T.Items.Add("User");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
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
            foreach (AccountItem Account in GlobalAccountList)
            {
                if (Account.Username.Equals(TB_U))
                {
                    ErrorMessage += "This username is already taken.\n";
                    break;
                }
            }

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    RecordActivity($"Added new {(CB_T.SelectedIndex == 0 ? "admin" : "user")} [{TB_U.Text}]");

                    InsertToAccount(TB_U.Text, TB_P.Text, CB_T.SelectedIndex).ExecuteNonQuery();

                    OkMessageBox("Successfully added new account!");
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
