using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class AddCustomerVehicleDialog : MaterialForm
    {
        PreviewCustomerDialog PreviewCustomerDialog;
        NewCustomerDialog NewCustomerDialog;
        string CustomerID;

        public AddCustomerVehicleDialog()
        {
            InitializeComponent();
        }

        public AddCustomerVehicleDialog(NewCustomerDialog NewCustomerDialog, string CustomerID)
        {
            InitializeComponent();

            this.NewCustomerDialog = NewCustomerDialog;
            this.CustomerID = CustomerID;
        }

        public AddCustomerVehicleDialog(PreviewCustomerDialog PreviewCustomerDialog, string CustomerID)
        {
            InitializeComponent();

            this.PreviewCustomerDialog = PreviewCustomerDialog;
            this.CustomerID = CustomerID;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            bool NoPlateNumber = false;
            string Suffix = "0";

            string ErrorMessage = "";

            if (Check.HasNumbers(TB_Brand.Text) || Check.HasSymbols(TB_Brand.Text))
            {
                ErrorMessage += "Brand cannot contain numbers nor symbols." + Environment.NewLine;
            }
            if (GetSize().Equals(""))
            {
                ErrorMessage += "Please select a vehicle size." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_Brand.Text))
            {
                ErrorMessage += "Please enter the vehicle's brand." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_Model.Text))
            {
                ErrorMessage += "Please enter the vehicle's model." + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(TB_PlateNumber.Text))
            {
                NoPlateNumber = true;
                //ErrorMessage += "Please enter the vehicle's plate number." + Environment.NewLine;
            }
            List<string> ExistingPlateNumbers = new List<string>();
            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                ExistingPlateNumbers.Add(Vehicle.PlateNumber);
            }
            if (ExistingPlateNumbers.Contains(TB_PlateNumber.Text.ToUpper().Trim()))
            {
                if (!NoPlateNumber)
                {
                    ErrorMessage += "This plate number already exists." + Environment.NewLine;
                }
                else
                {
                    foreach (string PN in ExistingPlateNumbers)
                    {
                        if (PN.StartsWith("."))
                        {
                            Suffix = PN.Split('-')[1];
                        }
                    }
                }
            }

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    string Brand = TB_Brand.Text.ToUpper().Trim();
                    string Model = TB_Model.Text.ToUpper().Trim();
                    string Size = GetSize().ToUpper().Trim();
                    string PlateNumber = NoPlateNumber ? $".NO_PLATE_NUMBER-{int.Parse(Suffix) + 1}" : TB_PlateNumber.Text.ToUpper().Trim();
                    string VehicleID = (GlobalVehicleList.Count + 1).ToString();

                    string CustomerName = string.Empty;
                    string CustomerPlateNumbers = string.Empty;
                    string FinalCustomerPlateNumbers = "";

                    if (!string.IsNullOrEmpty(CustomerID))
                    {
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
                        FinalCustomerPlateNumbers += PlateNumber + "]";
                    }

                    RecordActivity($"Added new vehicle{(string.IsNullOrEmpty(CustomerID) ? "" : " for [Customer ID " + CustomerID)}{(string.IsNullOrEmpty(CustomerName) ? "" : ": " + CustomerName + "]")} - [{Brand}, {Model}, {Size}, {PlateNumber}]");

                    SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_VehicleList(VehicleId, VehicleBrand, VehicleModel, VehicleSize, PlateNumber, CustomerId) VALUES ('{VehicleID}', '{Brand}', '{Model}', '{Size}', '{PlateNumber}', '{(string.IsNullOrEmpty(CustomerID) ? "(None)" : CustomerID)}')", SQL);

                    Command.ExecuteNonQuery();

                    if (!string.IsNullOrEmpty(CustomerID))
                    {
                        SqliteCommand Command1 = new SqliteCommand($"UPDATE AUTOLANDIA_CustomerList SET PlateNumbers='{FinalCustomerPlateNumbers}' WHERE CustomerId='{CustomerID}'", SQL);

                        Command1.ExecuteNonQuery();
                    }

                    OkMessageBox($"Successfully added new vehicle{(string.IsNullOrEmpty(CustomerID) ? "" : " for [Customer ID " + CustomerID)}{(string.IsNullOrEmpty(CustomerName) ? "" : ": " + CustomerName + "]")}!");
                    GlobalPeopleForm.RefreshCustomers();
                    GlobalPeopleForm.RefreshVehicles();
                    if (PreviewCustomerDialog != null)
                    {
                        PreviewCustomerDialog.RefreshDetails();
                    }
                    GlobalActivityRecordForm.RefreshActivities();

                    if (NewCustomerDialog != null)
                    {
                        if (NoticeMessageBox("Add a new order?") == DialogResult.Yes)
                        {
                            new NewTransactionDialog(this, NewCustomerDialog, VehicleID).ShowDialog();
                        }
                        else
                        {
                            NewCustomerDialog.Close();
                            Close();
                        }
                    }
                    else
                    {
                        if (NoticeMessageBox("Add a new customer for this vehicle?") == DialogResult.Yes)
                        {
                            new NewCustomerDialog(this, VehicleID).ShowDialog();
                        }
                        else
                        {
                            Close();
                        }
                    }
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

        string GetSize()
        {
            RadioButton[] RadioButtons = { RB_S, RB_M, RB_L, RB_XL, RB_XXL };
            foreach (RadioButton RadioButton in RadioButtons)
            {
                if (RadioButton.Checked)
                {
                    return RadioButton.Text;
                }
            }
            return "";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (NewCustomerDialog != null)
            {
                NewCustomerDialog.Close();
            }
            Close();
        }

        public void SetVehicleBrand(string Brand)
        {
            TB_Brand.Text = Brand;
            if (Brand.Equals("GENERAL"))
            {
                RB_S.Checked = true;
                RB_XL.Visible = false;
                RB_XXL.Visible = false;
            }
            else
            {
                RB_XL.Visible = true;
                RB_XXL.Visible = true;
            }
        }

        public void SetVehicleModel(string Model)
        {
            TB_Model.Text = Model;
            if (TB_Brand.Text.Equals("GENERAL"))
            {
                if (Model.Equals("TRUCK"))
                {
                    RB_S.Checked = true;
                    RB_S.Text = "Per negotiation";
                    RB_M.Visible = false;
                    RB_L.Visible = false;
                }
                else
                {
                    if (Model.Equals("PUV") || Model.Equals("TRICYCLE"))
                    {
                        RB_S.Text = "M";
                        RB_S.Checked = true;
                        RB_S.Visible = true;
                        RB_M.Visible = false;
                        RB_L.Visible = false;
                    }
                    else
                    {
                        RB_S.Text = "S";
                        RB_S.Visible = true;
                        RB_M.Visible = true;
                        RB_L.Visible = true;
                    }
                }
            }
        }

        private void PickVehicleBrandButton_Click(object sender, EventArgs e)
        {
            new SelectVehicleBrandDialog(this).ShowDialog();
        }

        private void PickVehicleModelButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_Brand.Text))
            {
                new SelectVehicleModelDialog(this, TB_Brand.Text.ToUpper()).ShowDialog();
            }
            else
            {
                AlertMessageBox("Please select a vehicle brand to auto-filter.");
            }
        }
    }
}
