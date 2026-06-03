using AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.Properties;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class LoginForm : MaterialForm
    {
        public LoginForm(Startup Startup)
        {
            InitializeComponent();

            Startup.Hide();

            Banner.Image = Resources.autolandia_logo;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;
            List<string> TempUsernames = new List<string>();
            for (int a = 0; a < GlobalAccountList.Count; a++)
            {
                TempUsernames.Add(GlobalAccountList[a].Username);
            }
            if (TempUsernames.Contains(TB_Username.Text.Trim()))
            {
                for (int a = 0; a < GlobalAccountList.Count; a++)
                {
                    if (GlobalAccountList[a].Username.Equals(TB_Username.Text.Trim()))
                    {
                        if (GlobalAccountList[a].Password.Equals(TB_Password.Text.Trim()))
                        {
                            TB_Username.Text = string.Empty;
                            TB_Password.Text = string.Empty;
                            LoggedAccount = GlobalAccountList[a];
                            ErrorMessage = string.Empty;
                            new MainForm(this).ShowDialog();
                            break;
                        }
                        else
                        {
                            ErrorMessage = "Incorrect password.";
                        }
                    }
                }
            }
            else
            {
                ErrorMessage = "Account not found.";
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                AlertMessageBox(ErrorMessage);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
