using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class NewCustomerDialog : MaterialForm
    {
        AddCustomerVehicleDialog AddCustomerVehicleDialog;
        string VehicleID;

        public NewCustomerDialog()
        {
            InitializeComponent();
        }

        public NewCustomerDialog(AddCustomerVehicleDialog AddCustomerVehicleDialog, string VehicleID)
        {
            InitializeComponent();

            this.AddCustomerVehicleDialog = AddCustomerVehicleDialog;
            this.VehicleID = VehicleID;
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
            List<string> LastNames = new List<string>();
            List<string> FirstNames = new List<string>();
            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                LastNames.Add(Customer.LastName);
                FirstNames.Add(Customer.FirstName);
            }

            for (int a = 0; a < LastNames.Count; a++)
            {
                if ($"{LastNames[a].ToUpper().Trim()}:{FirstNames[a].ToUpper().Trim()}".Equals($"{TB_LName.Text.ToUpper().Trim()}{TB_FName.Text.ToUpper().Trim()}"))
                {
                    ErrorMessage += "This customer already exists." + Environment.NewLine;
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
                    string CustomerID = (GlobalCustomerList.Count + 1).ToString().Trim();

                    RecordActivity($"Added new customer: {LName}, {FName}{(!string.IsNullOrEmpty(MName) ? $" {MName}" : string.Empty)} ({(string.IsNullOrEmpty(MobileNumber) ? "Mobile number not set" : "Mobile number set and hidden")} | {(string.IsNullOrEmpty(Address) ? "Address not set" : "Address set and hidden")})");

                    VehicleItem RealVehicle = GetVehicleFromID(VehicleID);

                    if (RealVehicle != null)
                    {
                        string CustomerName = string.Empty;
                        string CustomerPlateNumbers = string.Empty;
                        string FinalCustomerPlateNumbers = "";

                        foreach (CustomerItem Customer in GlobalCustomerList)
                        {
                            if (Customer.ID.Equals(CustomerID))
                            {
                                CustomerName = $"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}";
                                CustomerPlateNumbers = Customer.PlateNumbers;
                            }
                        }

                        if (CustomerPlateNumbers.Equals("(None)"))
                        {
                            CustomerPlateNumbers = "[]";
                        }
                        string[] Split = CustomerPlateNumbers.Substring(1, CustomerPlateNumbers.Length - 2).Split(',');
                        FinalCustomerPlateNumbers = "[";
                        foreach (string Plate in Split)
                        {
                            if (!string.IsNullOrEmpty(Plate))
                            {
                                FinalCustomerPlateNumbers += Plate + ",";
                            }
                        }
                        FinalCustomerPlateNumbers += RealVehicle.PlateNumber + "]";

                        SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_CustomerList(CustomerId, FirstName, PlateNumbers, MobileNumber, CustomerAddress, LastName, MiddleName) VALUES ('{CustomerID}', '{FName}', '{FinalCustomerPlateNumbers}', '{(string.IsNullOrEmpty(MobileNumber) ? "(Mobile number not set)" : MobileNumber)}', '{(string.IsNullOrEmpty(Address) ? "(Address not set)" : Address)}', '{LName}', '{MName}')", SQL);

                        Command.ExecuteNonQuery();
                    }
                    else
                    {
                        SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_CustomerList(CustomerId, FirstName, PlateNumbers, MobileNumber, CustomerAddress, LastName, MiddleName) VALUES ('{CustomerID}', '{FName}', '(None)', '{(string.IsNullOrEmpty(MobileNumber) ? "(Mobile number not set)" : MobileNumber)}', '{(string.IsNullOrEmpty(Address) ? "(Address not set)" : Address)}', '{LName}', '{MName}')", SQL);

                        Command.ExecuteNonQuery();
                    }

                    MaterialMessageBox.Show("Successfully added new customer!", "Notice");
                    GlobalPeopleForm.RefreshCustomers();
                    GlobalActivityRecordForm.RefreshActivities();

                    if (AddCustomerVehicleDialog != null)
                    {
                        DialogResult NewDialogResult = MaterialMessageBox.Show("Add a new order?", "Notice", MessageBoxButtons.YesNo, FlexibleMaterialForm.ButtonsPosition.Right);
                        if (NewDialogResult == DialogResult.Yes)
                        {
                            new NewTransactionDialog(AddCustomerVehicleDialog, this, VehicleID).ShowDialog();
                        }
                        else
                        {
                            AddCustomerVehicleDialog.Close();
                            Close();
                        }
                    }
                    else
                    {
                        DialogResult NewDialogResult = MaterialMessageBox.Show("Add a new vehicle?", "Notice", MessageBoxButtons.YesNo, FlexibleMaterialForm.ButtonsPosition.Right);
                        if (NewDialogResult == DialogResult.Yes)
                        {
                            new AddCustomerVehicleDialog(this, CustomerID).ShowDialog();
                        }
                        else
                        {
                            Close();
                        }
                    }
                }
                catch (Exception exception)
                {
                    MaterialMessageBox.Show(exception.Message, "Alert");
                }
            }
            else
            {
                MaterialMessageBox.Show(ErrorMessage, "Alert");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (AddCustomerVehicleDialog != null)
            {
                AddCustomerVehicleDialog.Close();
            }
            Close();
        }
    }
}
