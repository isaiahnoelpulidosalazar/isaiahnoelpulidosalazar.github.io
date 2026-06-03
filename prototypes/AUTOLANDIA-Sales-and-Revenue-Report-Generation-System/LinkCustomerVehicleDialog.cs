using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class LinkCustomerVehicleDialog : MaterialForm
    {
        PreviewCustomerDialog PreviewCustomerDialog;
        PreviewVehicleDialog PreviewVehicleDialog;
        PeopleForm PeopleForm;
        CustomerItem Customer;
        VehicleItem Vehicle;

        public LinkCustomerVehicleDialog(PreviewCustomerDialog PreviewCustomerDialog, CustomerItem Customer, PeopleForm PeopleForm)
        {
            InitializeComponent();

            this.PreviewCustomerDialog = PreviewCustomerDialog;
            this.Customer = Customer;
            this.PeopleForm = PeopleForm;

            PickCustomerButton.Enabled = false;

            TB_Name.Text = $"{Customer.ID}: {Customer.LastName}, {Customer.FirstName} {Customer.MiddleName}";
        }

        public LinkCustomerVehicleDialog(PreviewVehicleDialog PreviewVehicleDialog, VehicleItem Vehicle, PeopleForm PeopleForm)
        {
            InitializeComponent();

            this.PreviewVehicleDialog = PreviewVehicleDialog;
            this.Vehicle = Vehicle;
            this.PeopleForm = PeopleForm;

            PickVehicleButton.Enabled = false;

            TB_Vehicle.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
        }

        public void SetCustomerName(string Name)
        {
            TB_Name.Text = Name;
        }

        public void SetUnlinkedVehicle(string Vehicle)
        {
            TB_Vehicle.Text = Vehicle;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_Name.Text) && !string.IsNullOrEmpty(TB_Vehicle.Text))
            {
                try
                {
                    VehicleItem VehicleDetails = null;
                    CustomerItem CustomerDetails = null;
                    foreach (VehicleItem Vehicle in GlobalVehicleList)
                    {
                        if (Vehicle.ID.Equals(TB_Vehicle.Text.Split(':')[0]))
                        {
                            VehicleDetails = Vehicle;
                        }
                    }
                    foreach (CustomerItem Customer in GlobalCustomerList)
                    {
                        if (Customer.ID.Equals(TB_Name.Text.Split(':')[0]))
                        {
                            CustomerDetails = Customer;
                        }
                    }
                    string PlateNumber = VehicleDetails.PlateNumber;
                    string CustomerPlateNumbers = CustomerDetails.PlateNumbers;
                    if (CustomerPlateNumbers.Equals("(None)"))
                    {
                        CustomerPlateNumbers = "[]";
                    }
                    string[] Split = CustomerPlateNumbers.Substring(1, CustomerPlateNumbers.Length - 2).Split(',');
                    string FinalCustomerPlateNumbers = "";
                    FinalCustomerPlateNumbers = "[";
                    foreach (string Plate in Split)
                    {
                        if (!string.IsNullOrEmpty(Plate))
                        {
                            FinalCustomerPlateNumbers += Plate + ",";
                        }
                    }
                    FinalCustomerPlateNumbers += PlateNumber + "]";

                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Added new vehicle for [Customer ID {CustomerDetails.ID}: {CustomerDetails.LastName}, {CustomerDetails.FirstName}{(!string.IsNullOrEmpty(CustomerDetails.MiddleName) ? $" {CustomerDetails.MiddleName}" : string.Empty)}] - [{VehicleDetails.Brand}, {VehicleDetails.Model}, {VehicleDetails.Size}, {VehicleDetails.PlateNumber}]");

                    SqliteCommand Command1 = new SqliteCommand($"UPDATE AUTOLANDIA_CustomerList SET PlateNumbers='{FinalCustomerPlateNumbers}' WHERE CustomerId='{CustomerDetails.ID}'", SQL);
                    SqliteCommand Command2 = new SqliteCommand($"UPDATE AUTOLANDIA_VehicleList SET CustomerId='{CustomerDetails.ID}' WHERE VehicleId='{VehicleDetails.ID}'", SQL);

                    Command1.ExecuteNonQuery();
                    Command2.ExecuteNonQuery();

                    MaterialMessageBox.Show("Successfully assigned customer to a vehicle!", "Notice");
                    PeopleForm.RefreshCustomers();
                    PeopleForm.RefreshVehicles();
                    if (PreviewCustomerDialog != null)
                    {
                        PreviewCustomerDialog.RefreshDetails();
                    }
                    if (PreviewVehicleDialog != null)
                    {
                        PreviewVehicleDialog.RefreshDetails();
                    }
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
                MaterialMessageBox.Show("Please select both a customer and a vehicle.", "Alert");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TB_Vehicle_Click(object sender, EventArgs e)
        {
            if (PickVehicleButton.Enabled)
            {
                new SelectVehicleDialog(this).ShowDialog();
            }
        }

        private void TB_Name_Click(object sender, EventArgs e)
        {
            if (PickCustomerButton.Enabled)
            {
                new SelectCustomerDialog(this).ShowDialog();
            }
        }

        private void PickVehicleButton_Click(object sender, EventArgs e)
        {
            new SelectVehicleDialog(this).ShowDialog();
        }

        private void PickCustomerButton_Click(object sender, EventArgs e)
        {
            new SelectCustomerDialog(this).ShowDialog();
        }
    }
}
