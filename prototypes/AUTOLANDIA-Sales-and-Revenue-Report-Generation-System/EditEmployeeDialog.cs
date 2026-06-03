using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditEmployeeDialog : MaterialForm
    {
        PreviewEmployeeDialog PreviewEmployeeDialog;
        string EmployeeID, PreviousEmployeeFirstName, PreviousEmployeeMiddleName, PreviousEmployeeLastName, PreviousAddress, PreviousMobileNumber;

        public EditEmployeeDialog(PreviewEmployeeDialog PreviewEmployeeDialog, string EmployeeID)
        {
            InitializeComponent();

            this.PreviewEmployeeDialog = PreviewEmployeeDialog;
            this.EmployeeID = EmployeeID;

            DocumentListCheckBox.Items.Add("Tax Identification Number (TIN)");
            DocumentListCheckBox.Items.Add("Department of Labor and Employment (DOLE)");
            DocumentListCheckBox.Items.Add("Home Development Mutual Fund (HDMF)");
            DocumentListCheckBox.Items.Add("PhilHealth");
            DocumentListCheckBox.Items.Add("Social Security System (SSS)");
            DocumentListCheckBox.Items.Add("Valid ID");

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                if (Employee.ID.Equals(EmployeeID))
                {
                    TB_FName.Text = Employee.FirstName;
                    TB_MName.Text = Employee.MiddleName;
                    TB_LName.Text = Employee.LastName;
                    PreviousEmployeeFirstName = Employee.FirstName;
                    PreviousEmployeeMiddleName = Employee.MiddleName;
                    PreviousEmployeeLastName = Employee.LastName;
                    if (!Employee.MobileNumber.Equals("(Mobile number not set)"))
                    {
                        TB_MobileNumber.Text = Employee.MobileNumber;
                        PreviousMobileNumber = Employee.MobileNumber;
                    }
                    if (!Employee.Address.Equals("(Address not set)"))
                    {
                        TB_Address.Text = Employee.Address;
                        PreviousAddress = Employee.Address;
                    }
                    foreach (char Document in Employee.Documents)
                    {
                        foreach (CheckBox Item in DocumentListCheckBox.Items)
                        {
                            if (Item.Text.First() == Document)
                            {
                                Item.Checked = true;
                            }
                        }
                    }
                }
            }
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

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    string FName = TB_FName.Text.ToUpper();
                    string MName = TB_MName.Text.ToUpper();
                    string LName = TB_LName.Text.ToUpper();
                    string Address = TB_Address.Text.ToUpper();
                    string MobileNumber = TB_MobileNumber.Text.ToUpper();
                    string Documents = string.Empty;

                    foreach (CheckBox Item in DocumentListCheckBox.Items)
                    {
                        if (Item.Checked)
                        {
                            Documents += Item.Text.First();
                        }
                    }

                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Updated employee details from [{PreviousEmployeeLastName}, {PreviousEmployeeFirstName}{(!string.IsNullOrEmpty(PreviousEmployeeMiddleName) ? $" {PreviousEmployeeMiddleName}" : string.Empty)} ({(string.IsNullOrEmpty(PreviousMobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(PreviousAddress) ? "Address not set" : "Address set and hidden")})] to [{LName}, {FName}{(!string.IsNullOrEmpty(MName) ? $" {MName}" : string.Empty)} ({(string.IsNullOrEmpty(MobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(Address) ? "Address not set" : "Address set and hidden")})]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_EmployeeList SET FirstName='{FName}', MiddleName='{MName}', LastName='{LName}', MobileNumber='{(string.IsNullOrEmpty(MobileNumber) ? "(Mobile number not set)" : MobileNumber)}', EmployeeAddress='{(string.IsNullOrEmpty(Address) ? "(Address not set)" : Address)}', EmployeeDocuments='{CSSimpleFunctions.Convert.Reverse(Documents)}' WHERE EmployeeId='{EmployeeID}'", SQL);

                    Command.ExecuteNonQuery();

                    OkMessageBox("Successfully updated employee details!");
                    GlobalPeopleForm.RefreshEmployees();
                    PreviewEmployeeDialog.RefreshDetails();
                    GlobalActivityRecordForm.RefreshActivities();
                    Close();
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                    DoneButton.Enabled = true;
                    CancelButton.Enabled = true;
                }
            }
            else
            {
                AlertMessageBox(ErrorMessage);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
