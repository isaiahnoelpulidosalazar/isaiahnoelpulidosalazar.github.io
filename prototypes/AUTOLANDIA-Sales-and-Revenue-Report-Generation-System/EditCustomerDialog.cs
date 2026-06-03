using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditCustomerDialog : MaterialForm
    {
        PreviewCustomerDialog PreviewCustomerDialog;
        PeopleForm PeopleForm;
        string CustomerID, PreviousCustomerFirstName, PreviousCustomerMiddleName, PreviousCustomerLastName, PreviousAddress, PreviousMobileNumber;

        public EditCustomerDialog(PreviewCustomerDialog PreviewCustomerDialog, string CustomerID, PeopleForm PeopleForm)
        {
            InitializeComponent();

            this.PreviewCustomerDialog = PreviewCustomerDialog;
            this.CustomerID = CustomerID;
            this.PeopleForm = PeopleForm;

            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                if (Customer.ID.Equals(CustomerID))
                {
                    TB_FName.Text = Customer.FirstName;
                    TB_MName.Text = Customer.MiddleName;
                    TB_LName.Text = Customer.LastName;
                    PreviousCustomerFirstName = Customer.FirstName;
                    PreviousCustomerMiddleName = Customer.MiddleName;
                    PreviousCustomerLastName = Customer.LastName;
                    if (!Customer.MobileNumber.Equals("(Mobile number not set)"))
                    {
                        TB_MobileNumber.Text = Customer.MobileNumber;
                        PreviousMobileNumber = Customer.MobileNumber;
                    }
                    if (!Customer.Address.Equals("(Address not set)"))
                    {
                        TB_Address.Text = Customer.Address;
                        PreviousAddress = Customer.Address;
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
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
                ErrorMessage += "Please enter the customer's first name." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_LName.Text))
            {
                ErrorMessage += "Please enter the customer's last name." + Environment.NewLine;
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

                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Updated customer details from [{PreviousCustomerLastName + ", " + PreviousCustomerFirstName + " " + PreviousCustomerMiddleName} ({(string.IsNullOrEmpty(PreviousMobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(PreviousAddress) ? "Address not set" : "Address set and hidden")})] to [{LName + ", " + FName + " " + MName} ({(string.IsNullOrEmpty(MobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(Address) ? "Address not set" : "Address set and hidden")})]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_CustomerList SET FirstName='{FName}', MiddleName='{MName}', LastName='{LName}', MobileNumber='{(string.IsNullOrEmpty(MobileNumber) ? "(Mobile number not set)" : MobileNumber)}', CustomerAddress='{(string.IsNullOrEmpty(Address) ? "(Address not set)" : Address)}' WHERE CustomerId='{CustomerID}'", SQL);

                    Command.ExecuteNonQuery();

                    OkMessageBox("Successfully updated customer details!");
                    PeopleForm.RefreshCustomers();
                    PreviewCustomerDialog.RefreshDetails();
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
    }
}
