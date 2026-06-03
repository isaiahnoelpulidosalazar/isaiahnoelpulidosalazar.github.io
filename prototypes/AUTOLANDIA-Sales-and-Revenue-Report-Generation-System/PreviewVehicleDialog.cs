using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PreviewVehicleDialog : MaterialForm
    {
        PreviewCustomerDialog PreviewCustomerDialog;
        PeopleForm PeopleForm;
        string VehicleID;

        public PreviewVehicleDialog(PeopleForm PeopleForm, string VehicleID)
        {
            InitializeComponent();

            this.PeopleForm = PeopleForm;
            this.VehicleID = VehicleID;

            RefreshDetails();
        }

        public PreviewVehicleDialog(PeopleForm PeopleForm, string VehicleID, PreviewCustomerDialog PreviewCustomerDialog)
        {
            InitializeComponent();

            this.PeopleForm = PeopleForm;
            this.VehicleID = VehicleID;
            this.PreviewCustomerDialog = PreviewCustomerDialog;

            LinkCustomerButton.Enabled = false;

            RefreshDetails();
        }

        public void RefreshDetails()
        {
            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.ID.Equals(VehicleID))
                {
                    BrandLabel.Text = Vehicle.Brand;
                    ModelLabel.Text = Vehicle.Model;
                    PlateNumberLabel.Text = Vehicle.PlateNumber;
                    SizeLabel.Text = Vehicle.Size;

                    foreach (CustomerItem Customer in GlobalCustomerList)
                    {
                        if (Customer.ID.Equals(Vehicle.CustomerID))
                        {
                            CustomerNameLabel.Text = $"{Customer.LastName}, {Customer.FirstName} {Customer.MiddleName}";
                            LinkCustomerButton.Enabled = false;
                        }
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new EditVehicleDialog(this, VehicleID, PeopleForm, PreviewCustomerDialog != null ? PreviewCustomerDialog : null).ShowDialog();
        }

        private void LinkCustomerButton_Click(object sender, EventArgs e)
        {
            List<VehicleItem> Temp = new List<VehicleItem>(GlobalVehicleList);

            for (int a = 0; a < Temp.Count; a++)
            {
                if (Temp[a].ID.Equals(VehicleID))
                {
                    new LinkCustomerVehicleDialog(this, Temp[a], PeopleForm).ShowDialog();
                }
            }
        }
    }
}
