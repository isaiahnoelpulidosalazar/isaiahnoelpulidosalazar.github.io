using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectVehicleModelDialog : MaterialForm
    {
        Color DefaultBackgroundColor;
        AddCustomerVehicleDialog AddCustomerVehicleDialog;
        EditVehicleDialog EditVehicleDialog;
        string Brand;
        List<string> Temp = new List<string>();

        public SelectVehicleModelDialog(AddCustomerVehicleDialog AddCustomerVehicleDialog, string Brand)
        {
            InitializeComponent();

            this.AddCustomerVehicleDialog = AddCustomerVehicleDialog;
            this.Brand = Brand;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        public SelectVehicleModelDialog(EditVehicleDialog EditVehicleDialog, string Brand)
        {
            InitializeComponent();

            this.EditVehicleDialog = EditVehicleDialog;
            this.Brand = Brand;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void RefreshVehicles()
        {
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (VehicleModelItem Vehicle in GlobalVehicleModelList)
            {
                if (Vehicle.Brand.Equals(Brand))
                {
                    Temp.Add(Vehicle.Model);
                }
            }
            Temp.Sort();

            foreach (string Model in Temp)
            {
                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                TableLayoutPanel Panel = new TableLayoutPanel
                {
                    ColumnCount = 1
                };
                Label Name = new Label();

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
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                Panel.Margin = new Padding(0);

                Name.Dock = DockStyle.Fill;
                Name.Text = Model;
                Name.TextAlign = ContentAlignment.MiddleLeft;
                Name.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Name.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Panel.Click += (sndr, evnt) =>
                {
                    if (AddCustomerVehicleDialog != null)
                    {
                        AddCustomerVehicleDialog.SetVehicleModel(Model);
                    }
                    if (EditVehicleDialog != null)
                    {
                        EditVehicleDialog.SetVehicleModel(Model);
                    }
                    Close();
                };
                Name.Click += (sndr, evnt) =>
                {
                    if (AddCustomerVehicleDialog != null)
                    {
                        AddCustomerVehicleDialog.SetVehicleModel(Model);
                    }
                    if (EditVehicleDialog != null)
                    {
                        EditVehicleDialog.SetVehicleModel(Model);
                    }
                    Close();
                };

                VehicleList.RowStyles.Add(Row);
                Panel.Controls.Add(Name, 0, 0);
                VehicleList.Controls.Add(Panel);
            }
        }

        private void SearchBarVehicle_TextChanged(object sender, EventArgs e)
        {
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (string Model in Temp)
            {
                if (Model.Contains(SearchBarVehicle.Text.ToUpper()))
                {
                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                    TableLayoutPanel Panel = new TableLayoutPanel
                    {
                        ColumnCount = 1
                    };
                    Label Name = new Label();

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
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                    Panel.Margin = new Padding(0);

                    Name.Dock = DockStyle.Fill;
                    Name.Text = Model;
                    Name.TextAlign = ContentAlignment.MiddleLeft;
                    Name.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Name.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Panel.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleModel(Model);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleModel(Model);
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleModel(Model);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleModel(Model);
                        }
                        Close();
                    };

                    VehicleList.RowStyles.Add(Row);
                    Panel.Controls.Add(Name, 0, 0);
                    VehicleList.Controls.Add(Panel);
                }
                else if (string.IsNullOrEmpty(SearchBarVehicle.Text))
                {
                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                    TableLayoutPanel Panel = new TableLayoutPanel
                    {
                        ColumnCount = 1
                    };
                    Label Name = new Label();

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
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                    Panel.Margin = new Padding(0);

                    Name.Dock = DockStyle.Fill;
                    Name.Text = Model;
                    Name.TextAlign = ContentAlignment.MiddleLeft;
                    Name.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Name.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Panel.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleModel(Model);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleModel(Model);
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleModel(Model);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleModel(Model);
                        }
                        Close();
                    };

                    VehicleList.RowStyles.Add(Row);
                    Panel.Controls.Add(Name, 0, 0);
                    VehicleList.Controls.Add(Panel);
                }
            }
        }
    }
}
