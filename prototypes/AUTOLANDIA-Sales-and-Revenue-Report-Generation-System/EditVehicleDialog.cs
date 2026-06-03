using CSSimpleFunctions;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditVehicleDialog : MaterialForm
    {
        PreviewVehicleDialog PreviewVehicleDialog;
        PreviewCustomerDialog PreviewCustomerDialog;
        PeopleForm PeopleForm;
        string VehicleID, PreviousBrand, PreviousModel, PreviousSize, PreviousPlateNumber, CustomerID;

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
                MaterialMessageBox.Show("Please select a vehicle brand to auto-filter.", "Alert");
            }
        }

        public void SetVehicleBrand(string Brand)
        {
            TB_Brand.Text = Brand;
        }

        public void SetVehicleModel(string Model)
        {
            TB_Model.Text = Model;
        }

        public EditVehicleDialog(PreviewVehicleDialog PreviewVehicleDialog, string VehicleID, PeopleForm PeopleForm)
        {
            InitializeComponent();

            this.PreviewVehicleDialog = PreviewVehicleDialog;
            this.VehicleID = VehicleID;
            this.PeopleForm = PeopleForm;

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.ID.Equals(VehicleID))
                {
                    TB_Brand.Text = Vehicle.Brand;
                    TB_Model.Text = Vehicle.Model;
                    RadioButton[] RadioButtons = { RB_S, RB_M, RB_L, RB_XL, RB_XXL };
                    string[] Sizes = new string[] { "S", "M", "L", "XL", "XXL" };
                    for (int a = 0; a < Sizes.Length; a++)
                    {
                        if (Sizes[a].Equals(Vehicle.Size))
                        {
                            RadioButtons[a].Checked = true;
                        }
                    }
                    TB_PlateNumber.Text = Vehicle.PlateNumber;

                    PreviousBrand = Vehicle.Brand;
                    PreviousModel = Vehicle.Model;
                    PreviousSize = Vehicle.Size;
                    PreviousPlateNumber = Vehicle.PlateNumber;
                    CustomerID = Vehicle.CustomerID;
                }
            }
        }

        public EditVehicleDialog(PreviewVehicleDialog PreviewVehicleDialog, string VehicleID, PeopleForm PeopleForm, PreviewCustomerDialog PreviewCustomerDialog)
        {
            InitializeComponent();

            this.PreviewVehicleDialog = PreviewVehicleDialog;
            this.PreviewCustomerDialog = PreviewCustomerDialog;
            this.VehicleID = VehicleID;
            this.PeopleForm = PeopleForm;

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.ID.Equals(VehicleID))
                {
                    TB_Brand.Text = Vehicle.Brand;
                    TB_Model.Text = Vehicle.Model;
                    RadioButton[] RadioButtons = { RB_S, RB_M, RB_L, RB_XL, RB_XXL };
                    string[] Sizes = new string[] { "S", "M", "L", "XL", "XXL" };
                    for (int a = 0; a < Sizes.Length; a++)
                    {
                        if (Sizes[a].Equals(Vehicle.Size))
                        {
                            RadioButtons[a].Checked = true;
                        }
                    }
                    TB_PlateNumber.Text = Vehicle.PlateNumber;

                    PreviousBrand = Vehicle.Brand;
                    PreviousModel = Vehicle.Model;
                    PreviousSize = Vehicle.Size;
                    PreviousPlateNumber = Vehicle.PlateNumber;
                    CustomerID = Vehicle.CustomerID;
                }
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
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
                ErrorMessage += "Please enter the vehicle's plate number." + Environment.NewLine;
            }

            if (ErrorMessage.Equals(""))
            {
                try
                {
                    string Brand = TB_Brand.Text.ToUpper();
                    string Model = TB_Model.Text.ToUpper();
                    string Size = GetSize().ToUpper();
                    string PlateNumber = TB_PlateNumber.Text.ToUpper();

                    string CustomerPlateNumbers = string.Empty;
                    string FinalCustomerPlateNumbers = "";

                    if (!CustomerID.Equals("(None)"))
                    {
                        foreach (CustomerItem Customer in GlobalCustomerList)
                        {
                            if (Customer.ID.Equals(CustomerID))
                            {
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
                            if (!string.IsNullOrEmpty(Plate) && !Plate.Equals(PreviousPlateNumber))
                            {
                                FinalCustomerPlateNumbers += Plate + ",";
                            }
                        }
                        FinalCustomerPlateNumbers += PlateNumber + "]";
                    }

                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Updated vehicle from [{PreviousBrand}, {PreviousModel}, {PreviousSize}, {PreviousPlateNumber}] to [{Brand}, {Model}, {Size}, {PlateNumber}]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_VehicleList SET VehicleBrand='{Brand}', VehicleModel='{Model}', VehicleSize='{Size}', PlateNumber='{PlateNumber}' WHERE VehicleId='{VehicleID}'", SQL);

                    Command.ExecuteNonQuery();

                    if (!CustomerID.Equals("(None)"))
                    {
                        SqliteCommand Command1 = new SqliteCommand($"UPDATE AUTOLANDIA_CustomerList SET PlateNumbers='{FinalCustomerPlateNumbers}' WHERE CustomerId='{CustomerID}'", SQL);

                        Command1.ExecuteNonQuery();
                    }

                    MaterialMessageBox.Show($"Successfully updated vehicle details!", "Notice");
                    PeopleForm.RefreshCustomers();
                    PeopleForm.RefreshVehicles();
                    if (PreviewCustomerDialog != null)
                    {
                        PreviewCustomerDialog.RefreshDetails();
                    }
                    PreviewVehicleDialog.RefreshDetails();
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
            Close();
        }
    }
}
