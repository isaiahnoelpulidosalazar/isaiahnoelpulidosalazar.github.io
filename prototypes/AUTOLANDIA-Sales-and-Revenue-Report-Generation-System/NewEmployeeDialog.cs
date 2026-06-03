using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class NewEmployeeDialog : MaterialForm
    {
        public NewEmployeeDialog()
        {
            InitializeComponent();

            DocumentListCheckBox.Items.Add("Tax Identification Number (TIN)");
            DocumentListCheckBox.Items.Add("Department of Labor and Employment (DOLE)");
            DocumentListCheckBox.Items.Add("Home Development Mutual Fund (HDMF)");
            DocumentListCheckBox.Items.Add("PhilHealth");
            DocumentListCheckBox.Items.Add("Social Security System (SSS)");
            DocumentListCheckBox.Items.Add("Valid ID");
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            string ErrorMessage = "";

            if (Check.HasNumbers(TB_FName.Text) || Check.HasSymbols(TB_FName.Text))
            {
                ErrorMessage += "First name cannot contain numbers nor symbols." + Environment.NewLine;
            }
            if (Check.HasNumbers(TB_MName.Text) || Check.HasSymbols(TB_MName.Text))
            {
                ErrorMessage += "Middle name cannot contain numbers nor symbols." + Environment.NewLine;
            }
            if (Check.HasNumbers(TB_LName.Text) || Check.HasSymbols(TB_LName.Text))
            {
                ErrorMessage += "Last name cannot contain numbers nor symbols." + Environment.NewLine;
            }
            if (!Check.IsAValidPhilippineMobileNumber(TB_MobileNumber.Text) && !string.IsNullOrEmpty(TB_MobileNumber.Text))
            {
                ErrorMessage += "Mobile number is not valid. Use the prefixes \"09\", \"+639\", or \"639\"." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_FName.Text))
            {
                ErrorMessage += "Please enter the employee's first name." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_LName.Text))
            {
                ErrorMessage += "Please enter the employee's last name." + Environment.NewLine;
            }
            List<string> LastNames = new List<string>();
            List<string> FirstNames = new List<string>();
            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                LastNames.Add(Employee.LastName);
                FirstNames.Add(Employee.FirstName);
            }

            for (int a = 0; a < LastNames.Count; a++)
            {
                if ($"{LastNames[a].ToUpper().Trim()}:{FirstNames[a].ToUpper().Trim()}".Equals($"{TB_LName.Text.ToUpper().Trim()}{TB_FName.Text.ToUpper().Trim()}"))
                {
                    ErrorMessage += "This employee already exists." + Environment.NewLine;
                    break;
                }
            }

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    string FName = TB_FName.Text.ToUpper().Trim();
                    string MName = TB_MName.Text.ToUpper().Trim();
                    string LName = TB_LName.Text.ToUpper().Trim();
                    string Address = TB_Address.Text.ToUpper().Trim();
                    string MobileNumber = TB_MobileNumber.Text.ToUpper().Trim();
                    string Documents = string.Empty;

                    foreach (CheckBox Item in DocumentListCheckBox.Items)
                    {
                        if (Item.Checked)
                        {
                            Documents += Item.Text.First();
                        }
                    }

                    Documents = Documents.Trim();

                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Added new employee: {LName}, {FName}{(!string.IsNullOrEmpty(MName) ? $" {MName}" : string.Empty)} ({(string.IsNullOrEmpty(MobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(Address) ? "Address not set" : "Address set and hidden")})");

                    SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_EmployeeList(EmployeeId, FirstName, MobileNumber, EmployeeAddress, LastName, MiddleName, EmployeeDocuments) VALUES ('{GlobalEmployeeList.Count + 1}', '{FName}', '{(string.IsNullOrEmpty(MobileNumber) ? "(Mobile number not set)" : MobileNumber)}', '{(string.IsNullOrEmpty(Address) ? "(Address not set)" : Address)}', '{LName}', '{MName}', '{CSSimpleFunctions.Convert.Reverse(Documents)}')", SQL);

                    Command.ExecuteNonQuery();

                    MaterialMessageBox.Show("Successfully added new employee!", "Notice");
                    GlobalPeopleForm.RefreshEmployees();
                    GlobalActivityRecordForm.RefreshActivities();
                    Close();
                }
                catch (Exception exception)
                {
                    MaterialMessageBox.Show(exception.Message, "Alert");
                    DoneButton.Enabled = true;
                    CancelButton.Enabled = true;
                }
            }
            else
            {
                MaterialMessageBox.Show(ErrorMessage, "Alert");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
