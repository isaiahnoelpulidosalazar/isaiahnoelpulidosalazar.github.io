using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectVehicleDialog : MaterialForm
    {
        List<VehicleItem> Temp = new List<VehicleItem>();
        Color DefaultBackgroundColor;
        LinkCustomerVehicleDialog LinkCustomerVehicleDialog;
        NewTransactionDialog NewTransactionDialog;
        TransactionDetailDialog EditTransactionDialog;

        public SelectVehicleDialog(LinkCustomerVehicleDialog LinkCustomerVehicleDialog)
        {
            InitializeComponent();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                Temp.Add(Vehicle);
            }

            this.LinkCustomerVehicleDialog = LinkCustomerVehicleDialog;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        public SelectVehicleDialog(NewTransactionDialog NewTransactionDialog)
        {
            InitializeComponent();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                Temp.Add(Vehicle);
            }

            for (int counter = 0; counter < Temp.Count; counter++)
            {
                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    if (Temp[counter].ID.Equals(Order.VehicleId) && (!Order.Status.Equals("Complete") && !Order.Status.Equals("Cancelled")))
                    {
                        Temp.Remove(Temp[counter]);
                        counter--;
                        break;
                    }
                }
            }

            this.NewTransactionDialog = NewTransactionDialog;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        public SelectVehicleDialog(TransactionDetailDialog EditTransactionDialog)
        {
            InitializeComponent();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                Temp.Add(Vehicle);
            }

            for (int counter = 0; counter < Temp.Count; counter++)
            {
                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    if (Temp[counter].ID.Equals(Order.VehicleId) && (!Order.Status.Equals("Complete") && !Order.Status.Equals("Cancelled")))
                    {
                        Temp.Remove(Temp[counter]);
                        counter--;
                        break;
                    }
                }
            }

            this.EditTransactionDialog = EditTransactionDialog;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            RefreshVehicles();
        }

        public void RefreshVehicles()
        {
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (VehicleItem Vehicle in Temp)
            {
                if (NewTransactionDialog != null)
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
                    Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                        if (LinkCustomerVehicleDialog != null)
                        {
                            LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        if (NewTransactionDialog != null)
                        {
                            NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (LinkCustomerVehicleDialog != null)
                        {
                            LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        if (NewTransactionDialog != null)
                        {
                            NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        Close();
                    };

                    VehicleList.RowStyles.Add(Row);
                    Panel.Controls.Add(Name, 0, 0);
                    VehicleList.Controls.Add(Panel);
                }
                else if (EditTransactionDialog != null)
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
                    Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                        if (LinkCustomerVehicleDialog != null)
                        {
                            LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        if (EditTransactionDialog != null)
                        {
                            EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        Close();
                    };
                    Name.Click += (sndr, evnt) =>
                    {
                        if (LinkCustomerVehicleDialog != null)
                        {
                            LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        if (EditTransactionDialog != null)
                        {
                            EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                        }
                        Close();
                    };

                    VehicleList.RowStyles.Add(Row);
                    Panel.Controls.Add(Name, 0, 0);
                    VehicleList.Controls.Add(Panel);
                }
                else
                {
                    if (Vehicle.CustomerID.Equals("(None)"))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchBarVehicle_TextChanged(object sender, EventArgs e)
        {
            if (NewTransactionDialog != null)
            {
                VehicleList.Controls.Clear();
                VehicleList.RowStyles.Clear();

                foreach (VehicleItem Vehicle in Temp)
                {
                    if (Vehicle.PlateNumber.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };

                        VehicleList.RowStyles.Add(Row);
                        Panel.Controls.Add(Name, 0, 0);
                        VehicleList.Controls.Add(Panel);
                    }
                    else if (string.IsNullOrEmpty(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };

                        VehicleList.RowStyles.Add(Row);
                        Panel.Controls.Add(Name, 0, 0);
                        VehicleList.Controls.Add(Panel);
                    }
                }
            }
            else if (EditTransactionDialog != null)
            {
                VehicleList.Controls.Clear();
                VehicleList.RowStyles.Clear();

                foreach (VehicleItem Vehicle in Temp)
                {
                    if (Vehicle.PlateNumber.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };

                        VehicleList.RowStyles.Add(Row);
                        Panel.Controls.Add(Name, 0, 0);
                        VehicleList.Controls.Add(Panel);
                    }
                    else if (string.IsNullOrEmpty(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };

                        VehicleList.RowStyles.Add(Row);
                        Panel.Controls.Add(Name, 0, 0);
                        VehicleList.Controls.Add(Panel);
                    }
                }
            }
            else
            {
                VehicleList.Controls.Clear();
                VehicleList.RowStyles.Clear();

                foreach (VehicleItem Vehicle in Temp)
                {
                    if (Vehicle.PlateNumber.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };

                        VehicleList.RowStyles.Add(Row);
                        Panel.Controls.Add(Name, 0, 0);
                        VehicleList.Controls.Add(Panel);
                    }
                    else if (string.IsNullOrEmpty(SearchBarVehicle.Text.ToUpper()))
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
                        Name.Text = $"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}";
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
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            Close();
                        };
                        Name.Click += (sndr, evnt) =>
                        {
                            if (LinkCustomerVehicleDialog != null)
                            {
                                LinkCustomerVehicleDialog.SetUnlinkedVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (NewTransactionDialog != null)
                            {
                                NewTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
                            }
                            if (EditTransactionDialog != null)
                            {
                                EditTransactionDialog.SetVehicle($"{Vehicle.ID}: {Vehicle.Brand}, {Vehicle.Model}, {Vehicle.PlateNumber}");
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
}
