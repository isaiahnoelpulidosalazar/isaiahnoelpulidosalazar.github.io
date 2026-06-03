using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PreviewCustomerDialog : MaterialForm
    {
        Color DefaultBackgroundColor;
        PeopleForm PeopleForm;
        string CustomerID;

        public PreviewCustomerDialog(PeopleForm PeopleForm, string CustomerID)
        {
            InitializeComponent();

            this.PeopleForm = PeopleForm;
            this.CustomerID = CustomerID;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;
        }

        public void RefreshDetails()
        {
            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                if (Customer.ID.Equals(CustomerID))
                {
                    CustomerNameLabel.Text = $"{Customer.LastName}, {Customer.FirstName} {Customer.MiddleName}";
                    CustomerMobileNumberLabel.Text = Customer.MobileNumber;
                }
            }
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.CustomerID.Equals(CustomerID))
                {
                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                    TableLayoutPanel Panel = new TableLayoutPanel
                    {
                        ColumnCount = 4
                    };
                    Label Brand = new Label();
                    Label Model = new Label();
                    Label Size = new Label();
                    Label PlateNumber = new Label();

                    if (DefaultBackgroundColor == null)
                    {
                        DefaultBackgroundColor = Panel.BackColor;
                    }

                    Panel.Dock = DockStyle.Fill;
                    Panel.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Panel.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };
                    Panel.ColumnStyles.Clear();
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
                    Panel.Margin = new Padding(0);

                    Brand.Dock = DockStyle.Fill;
                    Brand.Text = Vehicle.Brand;
                    Brand.TextAlign = ContentAlignment.MiddleCenter;
                    Brand.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Brand.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Model.Dock = DockStyle.Fill;
                    Model.Text = Vehicle.Model;
                    Model.TextAlign = ContentAlignment.MiddleCenter;
                    Model.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Model.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Size.Dock = DockStyle.Fill;
                    Size.Text = Vehicle.Size;
                    Size.TextAlign = ContentAlignment.MiddleCenter;
                    Size.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Size.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    PlateNumber.Dock = DockStyle.Fill;
                    PlateNumber.Text = Vehicle.PlateNumber;
                    PlateNumber.TextAlign = ContentAlignment.MiddleCenter;
                    PlateNumber.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    PlateNumber.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Panel.Click += (sndr, evnt) =>
                    {
                        new PreviewVehicleDialog(PeopleForm, Vehicle.ID, this).ShowDialog();
                    };
                    Brand.Click += (sndr, evnt) =>
                    {
                        new PreviewVehicleDialog(PeopleForm, Vehicle.ID, this).ShowDialog();
                    };
                    Model.Click += (sndr, evnt) =>
                    {
                        new PreviewVehicleDialog(PeopleForm, Vehicle.ID, this).ShowDialog();
                    };
                    Size.Click += (sndr, evnt) =>
                    {
                        new PreviewVehicleDialog(PeopleForm, Vehicle.ID, this).ShowDialog();
                    };
                    PlateNumber.Click += (sndr, evnt) =>
                    {
                        new PreviewVehicleDialog(PeopleForm, Vehicle.ID, this).ShowDialog();
                    };

                    VehicleList.RowStyles.Add(Row);
                    Panel.Controls.Add(Brand, 0, 0);
                    Panel.Controls.Add(Model, 1, 0);
                    Panel.Controls.Add(Size, 2, 0);
                    Panel.Controls.Add(PlateNumber, 3, 0);
                    VehicleList.Controls.Add(Panel);

                    tableLayoutPanel2.Width = VehicleList.Width;
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddVehicleButton_Click(object sender, EventArgs e)
        {
            new AddCustomerVehicleDialog(this, CustomerID).ShowDialog();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new EditCustomerDialog(this, CustomerID, PeopleForm).ShowDialog();
        }

        private void LinkVehicleButton_Click(object sender, EventArgs e)
        {
            List<CustomerItem> Temp = new List<CustomerItem>(GlobalCustomerList);

            for (int a = 0; a < Temp.Count; a++)
            {
                if (Temp[a].ID.Equals(CustomerID))
                {
                    new LinkCustomerVehicleDialog(this, Temp[a], PeopleForm).ShowDialog();
                }
            }
        }

        private void PreviewCustomerDialog_Load(object sender, EventArgs e)
        {
            RefreshDetails();
        }
    }
}
