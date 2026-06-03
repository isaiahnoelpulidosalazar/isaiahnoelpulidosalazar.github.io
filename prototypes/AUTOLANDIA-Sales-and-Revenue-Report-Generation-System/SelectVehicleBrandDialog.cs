using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectVehicleBrandDialog : MaterialForm
    {
        Color DefaultBackgroundColor;
        AddCustomerVehicleDialog AddCustomerVehicleDialog;
        EditVehicleDialog EditVehicleDialog;
        List<string> Temp = new List<string>();

        public SelectVehicleBrandDialog(AddCustomerVehicleDialog AddCustomerVehicleDialog)
        {
            InitializeComponent();

            this.AddCustomerVehicleDialog = AddCustomerVehicleDialog;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        public SelectVehicleBrandDialog(EditVehicleDialog EditVehicleDialog)
        {
            InitializeComponent();

            this.EditVehicleDialog = EditVehicleDialog;

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
                if (!Temp.Contains(Vehicle.Brand))
                {
                    Temp.Add(Vehicle.Brand);
                }
            }
            Temp.Sort();
            Temp.Insert(0, "GENERAL");

            foreach (string Brand in Temp)
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
                Name.Text = Brand;
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
                        AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                    }
                    if (EditVehicleDialog != null)
                    {
                        EditVehicleDialog.SetVehicleBrand(Brand);
                    }
                    Close();
                };
                Name.Click += (sndr, evnt) =>
                {
                    if (AddCustomerVehicleDialog != null)
                    {
                        AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                    }
                    if (EditVehicleDialog != null)
                    {
                        EditVehicleDialog.SetVehicleBrand(Brand);
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

            foreach (string Brand in Temp)
            {
                if (Brand.Contains(SearchBarVehicle.Text.ToUpper()))
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
                    Name.Text = Brand;
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
                            AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleBrand(Brand);
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleBrand(Brand);
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
                    Name.Text = Brand;
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
                            AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleBrand(Brand);
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (AddCustomerVehicleDialog != null)
                        {
                            AddCustomerVehicleDialog.SetVehicleBrand(Brand);
                        }
                        if (EditVehicleDialog != null)
                        {
                            EditVehicleDialog.SetVehicleBrand(Brand);
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
