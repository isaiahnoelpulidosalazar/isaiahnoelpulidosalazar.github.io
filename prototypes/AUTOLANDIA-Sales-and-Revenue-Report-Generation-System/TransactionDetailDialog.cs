using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class TransactionDetailDialog : MaterialForm
    {
        TransactionsForm TransactionsForm;
        public MaterialCheckedListBox.ItemsList ServiceCheckedboxes, EmployeeCheckedboxes, ExtraCheckedboxes;
        Color DefaultBackgroundColor;
        string OrderID;
        double ServicePrice = 0;
        List<string> ExtraList = new List<string>();
        VehicleItem RealVehicle = null;
        bool IsOnLoad;
        MaterialCheckbox Perfume;
        int PerfumeCount = 0;

        public TransactionDetailDialog(TransactionsForm TransactionsForm, string OrderID)
        {
            InitializeComponent();

            this.TransactionsForm = TransactionsForm;
            this.OrderID = OrderID;

            ServiceList.Controls.Clear();
            ServiceList.RowStyles.Clear();
            SelectServicesButton.Visible = true;

            CB_Packages.Items.Add("None");
            foreach (PackageItem Package in GlobalPackageList)
            {
                if (!CB_Packages.Items.Contains(Package.Name) && Package.Status.Equals("Available"))
                {
                    CB_Packages.Items.Add(Package.Name);
                }
            }
            CB_Packages.SelectedIndex = 0;

            ExtraListCheckBox.Controls.Clear();
            ExtraListCheckBox.Items.Clear();
            ExtraList.Clear();
            Perfume = new MaterialCheckbox()
            {
                Text = "Perfume (₱150.00)"
            };
            Perfume.CheckedChanged += (s, e) =>
            {
                if (Perfume.Checked)
                {
                    new PickPerfumeCountDialog(this).ShowDialog();
                }
                else
                {
                    Perfume.Text = "Perfume (₱150.00)";
                    PerfumeCount = 0;
                }
            };
            ExtraList.Add("Car w/ Carrier (₱20.00)");

            ExtraListCheckBox.Items.Add(Perfume);
            foreach (string Item in ExtraList)
            {
                ExtraListCheckBox.Items.Add(Item);
            }
        }

        public void SetEmployees(MaterialCheckedListBox.ItemsList EmployeeCheckboxes)
        {
            TB_Employee.Text = string.Empty;

            EmployeeCheckedboxes = EmployeeCheckboxes;

            for (int a = EmployeeCheckedboxes.Count - 1; a > -1; a--)
            {
                if (EmployeeCheckedboxes[a].Checked)
                {
                    foreach (EmployeeItem Employee in GlobalEmployeeList)
                    {
                        string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                        if (Employee.ID.Equals(EmployeeCheckedboxes[a].Text.Split(':')[0].Trim()))
                        {
                            TB_Employee.Text += EmployeeDetail + ", ";
                            break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(TB_Employee.Text))
            {
                TB_Employee.Text = TB_Employee.Text.Substring(0, TB_Employee.Text.Length - 2);
            }
        }

        public void PickPerfumeCount(int Count)
        {
            PerfumeCount = Count;
            Perfume.Text = $"Perfume (₱150.00) x {PerfumeCount}";
        }

        public void SetServices(MaterialCheckedListBox.ItemsList ServiceCheckboxes)
        {
            ServiceList.Controls.Clear();
            ServiceList.RowStyles.Clear();

            ServicePrice = 0;
            bool HasCheck = false;
            foreach (MaterialCheckbox S in ServiceCheckboxes)
            {
                if (S.Checked)
                {
                    HasCheck = true;
                    break;
                }
            }

            if (HasCheck)
            {
                ServiceCheckedboxes = ServiceCheckboxes;
            }
            else
            {
                ServiceCheckedboxes = null;
            }

            VehicleItem RealVehicle = GetVehicleFromID(TB_Vehicle.Text.Split(':')[0].Trim());

            PackageItem RealPackage = null;

            if (!string.IsNullOrEmpty(CB_Packages.Text))
            {
                RealPackage = GetPackageFromName(CB_Packages.Text);
            }

            if (RealPackage != null)
            {
                if (ServiceCheckedboxes != null)
                {
                    for (int a = ServiceCheckedboxes.Count - 1; a > -1; a--)
                    {
                        foreach (string Item in RealPackage.Details.Split(','))
                        {
                            if (ServiceCheckedboxes[a].Text.ToLower().Contains(Item.ToLower()))
                            {
                                ServiceCheckedboxes[a].Checked = false;
                            }
                        }
                    }
                }
            }

            if (ServiceCheckedboxes != null)
            {
                List<MaterialCheckbox> Temp = ServiceCheckedboxes.ToList();
                Temp.Reverse();

                for (int a = Temp.Count - 1; a > -1; a--)
                {
                    if (Temp[a].Checked)
                    {
                        foreach (ServiceItem Service in GlobalServiceList)
                        {
                            if (Service.Name.Equals(Temp[a].Text))
                            {
                                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                                TableLayoutPanel Panel = new TableLayoutPanel
                                {
                                    ColumnCount = 2
                                };
                                Label Name = new Label();
                                Label Price = new Label();

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
                                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                                Panel.Margin = new Padding(0);

                                Name.Dock = DockStyle.Fill;
                                Name.Text = Service.Name;
                                Name.TextAlign = ContentAlignment.MiddleLeft;
                                Name.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                Name.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };

                                Price.Dock = DockStyle.Fill;
                                Price.Text = $"₱{GetServicePrice(Service.Name, RealVehicle != null ? RealVehicle.Size : "S")}";
                                ServicePrice += GetServicePrice(Service.Name, RealVehicle != null ? RealVehicle.Size : "S");
                                Price.TextAlign = ContentAlignment.MiddleCenter;
                                Price.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                Price.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };

                                ServiceList.RowStyles.Add(Row);
                                Panel.Controls.Add(Name, 0, 0);
                                Panel.Controls.Add(Price, 1, 0);
                                ServiceList.Controls.Add(Panel);

                                tableLayoutPanel2.Width = ServiceList.Width;
                                break;
                            }
                        }
                    }
                }
            }

            if (RealPackage != null)
            {
                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                TableLayoutPanel Panel = new TableLayoutPanel
                {
                    ColumnCount = 2
                };
                Label Name = new Label();
                Label Price = new Label();

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
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                Panel.Margin = new Padding(0);

                Name.Dock = DockStyle.Fill;
                Name.Text = RealPackage.Name;
                Name.TextAlign = ContentAlignment.MiddleLeft;
                Name.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Name.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Price.Dock = DockStyle.Fill;
                Price.Text = $"₱{GetPackagePrice(RealPackage.Name, RealVehicle != null ? RealVehicle.Size : "S")}";
                Price.TextAlign = ContentAlignment.MiddleCenter;
                Price.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Price.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                ServiceList.RowStyles.Add(Row);
                Panel.Controls.Add(Name, 0, 0);
                Panel.Controls.Add(Price, 1, 0);
                ServiceList.Controls.Add(Panel);

                tableLayoutPanel2.Width = ServiceList.Width;

                foreach (string Item in RealPackage.Details.Split(','))
                {
                    RowStyle Row1 = new RowStyle(SizeType.Absolute, 48f);
                    TableLayoutPanel Panel1 = new TableLayoutPanel
                    {
                        ColumnCount = 2
                    };
                    Label Name1 = new Label();
                    Label Price1 = new Label();

                    if (DefaultBackgroundColor == null)
                    {
                        DefaultBackgroundColor = Panel1.BackColor;
                    }

                    Panel1.Dock = DockStyle.Top;
                    Panel1.MouseEnter += (sndr, evnt) =>
                    {
                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Panel1.MouseLeave += (sndr, evnt) =>
                    {
                        Panel1.BackColor = DefaultBackgroundColor;
                    };
                    Panel1.ColumnStyles.Clear();
                    Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                    Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                    Panel1.Margin = new Padding(0);

                    Name1.Dock = DockStyle.Fill;
                    Name1.Text = Item;
                    Name1.TextAlign = ContentAlignment.MiddleLeft;
                    Name1.MouseEnter += (sndr, evnt) =>
                    {
                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Name1.MouseLeave += (sndr, evnt) =>
                    {
                        Panel1.BackColor = DefaultBackgroundColor;
                    };

                    Price1.Dock = DockStyle.Fill;
                    Price1.Text = "";
                    Price1.TextAlign = ContentAlignment.MiddleCenter;
                    Price1.MouseEnter += (sndr, evnt) =>
                    {
                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Price1.MouseLeave += (sndr, evnt) =>
                    {
                        Panel1.BackColor = DefaultBackgroundColor;
                    };

                    ServiceList.RowStyles.Add(Row1);
                    Panel1.Controls.Add(Name1, 0, 0);
                    Panel1.Controls.Add(Price1, 1, 0);
                    ServiceList.Controls.Add(Panel1);

                    tableLayoutPanel2.Width = ServiceList.Width;
                }
            }
        }

        private void TB_Employee_Click(object sender, EventArgs e)
        {
            new SelectEmployeeDialog(this, TB_Employee.Text).ShowDialog();
        }

        private void PickEmployeeButton_Click(object sender, EventArgs e)
        {
            new SelectEmployeeDialog(this, TB_Employee.Text).ShowDialog();
        }

        private void TB_Vehicle_Click(object sender, EventArgs e)
        {
            new SelectVehicleDialog(this).ShowDialog();
        }

        private void PickVehicleButton_Click(object sender, EventArgs e)
        {
            new SelectVehicleDialog(this).ShowDialog();
        }

        private void SelectServicesButton_Click(object sender, EventArgs e)
        {
            new SelectServicesDialog(this, ServiceList.Controls, RealVehicle.Brand, RealVehicle.Model).ShowDialog();
        }

        private void CB_Packages_TextChanged(object sender, EventArgs e)
        {
            if (IsOnLoad)
            {
                IsOnLoad = false;
            }
            else
            {
                ServiceList.Controls.Clear();
                ServiceList.RowStyles.Clear();

                ServicePrice = 0;

                VehicleItem RealVehicle = GetVehicleFromID(TB_Vehicle.Text.Split(',')[0].Split(':')[0].Trim());

                PackageItem RealPackage = null;

                if (!string.IsNullOrEmpty(CB_Packages.Text))
                {
                    RealPackage = GetPackageFromName(CB_Packages.Text);
                }

                if (RealPackage != null)
                {
                    if (ServiceCheckedboxes != null)
                    {
                        for (int a = ServiceCheckedboxes.Count - 1; a > -1; a--)
                        {
                            foreach (string Item in RealPackage.Details.Split(','))
                            {
                                if (ServiceCheckedboxes[a].Text.ToLower().Contains(Item.ToLower()))
                                {
                                    ServiceCheckedboxes[a].Checked = false;
                                }
                            }
                        }
                    }
                }

                if (ServiceCheckedboxes != null)
                {
                    List<MaterialCheckbox> Temp = ServiceCheckedboxes.ToList();
                    Temp.Reverse();

                    for (int a = Temp.Count - 1; a > -1; a--)
                    {
                        if (Temp[a].Checked)
                        {
                            foreach (ServiceItem Service in GlobalServiceList)
                            {
                                if (Service.Name.Equals(Temp[a].Text))
                                {
                                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                                    TableLayoutPanel Panel = new TableLayoutPanel
                                    {
                                        ColumnCount = 2
                                    };
                                    Label Name = new Label();
                                    Label Price = new Label();

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
                                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                                    Panel.Margin = new Padding(0);

                                    Name.Dock = DockStyle.Fill;
                                    Name.Text = Service.Name;
                                    Name.TextAlign = ContentAlignment.MiddleLeft;
                                    Name.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Name.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = DefaultBackgroundColor;
                                    };

                                    Price.Dock = DockStyle.Fill;
                                    Price.Text = $"₱{GetServicePrice(Service.Name, RealVehicle != null ? RealVehicle.Size : "S")}";
                                    ServicePrice += GetServicePrice(Service.Name, RealVehicle != null ? RealVehicle.Size : "S");
                                    Price.TextAlign = ContentAlignment.MiddleCenter;
                                    Price.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Price.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = DefaultBackgroundColor;
                                    };

                                    ServiceList.RowStyles.Add(Row);
                                    Panel.Controls.Add(Name, 0, 0);
                                    Panel.Controls.Add(Price, 1, 0);
                                    ServiceList.Controls.Add(Panel);

                                    tableLayoutPanel2.Width = ServiceList.Width;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (RealPackage != null)
                {
                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                    TableLayoutPanel Panel = new TableLayoutPanel
                    {
                        ColumnCount = 2
                    };
                    Label Name = new Label();
                    Label Price = new Label();

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
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                    Panel.Margin = new Padding(0);

                    Name.Dock = DockStyle.Fill;
                    Name.Text = RealPackage.Name;
                    Name.TextAlign = ContentAlignment.MiddleLeft;
                    Name.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Name.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    Price.Dock = DockStyle.Fill;
                    Price.Text = $"₱{GetPackagePrice(RealPackage.Name, RealVehicle != null ? RealVehicle.Size : "S")}";
                    Price.TextAlign = ContentAlignment.MiddleCenter;
                    Price.MouseEnter += (sndr, evnt) =>
                    {
                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                    };
                    Price.MouseLeave += (sndr, evnt) =>
                    {
                        Panel.BackColor = DefaultBackgroundColor;
                    };

                    ServiceList.RowStyles.Add(Row);
                    Panel.Controls.Add(Name, 0, 0);
                    Panel.Controls.Add(Price, 1, 0);
                    ServiceList.Controls.Add(Panel);

                    tableLayoutPanel2.Width = ServiceList.Width;

                    foreach (string Item in RealPackage.Details.Split(','))
                    {
                        RowStyle Row1 = new RowStyle(SizeType.Absolute, 48f);
                        TableLayoutPanel Panel1 = new TableLayoutPanel
                        {
                            ColumnCount = 2
                        };
                        Label Name1 = new Label();
                        Label Price1 = new Label();

                        if (DefaultBackgroundColor == null)
                        {
                            DefaultBackgroundColor = Panel1.BackColor;
                        }

                        Panel1.Dock = DockStyle.Top;
                        Panel1.MouseEnter += (sndr, evnt) =>
                        {
                            Panel1.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Panel1.MouseLeave += (sndr, evnt) =>
                        {
                            Panel1.BackColor = DefaultBackgroundColor;
                        };
                        Panel1.ColumnStyles.Clear();
                        Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                        Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                        Panel1.Margin = new Padding(0);

                        Name1.Dock = DockStyle.Fill;
                        Name1.Text = Item;
                        Name1.TextAlign = ContentAlignment.MiddleLeft;
                        Name1.MouseEnter += (sndr, evnt) =>
                        {
                            Panel1.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Name1.MouseLeave += (sndr, evnt) =>
                        {
                            Panel1.BackColor = DefaultBackgroundColor;
                        };

                        Price1.Dock = DockStyle.Fill;
                        Price1.Text = "";
                        Price1.TextAlign = ContentAlignment.MiddleCenter;
                        Price1.MouseEnter += (sndr, evnt) =>
                        {
                            Panel1.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Price1.MouseLeave += (sndr, evnt) =>
                        {
                            Panel1.BackColor = DefaultBackgroundColor;
                        };

                        ServiceList.RowStyles.Add(Row1);
                        Panel1.Controls.Add(Name1, 0, 0);
                        Panel1.Controls.Add(Price1, 1, 0);
                        ServiceList.Controls.Add(Panel1);

                        tableLayoutPanel2.Width = ServiceList.Width;
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
            DateTime Now = DateTime.Now;

            string ErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(TB_Employee.Text))
            {
                ErrorMessage += "Please select an employee.\n";
            }
            if (string.IsNullOrEmpty(TB_Vehicle.Text))
            {
                ErrorMessage += "Please select a vehicle.\n";
            }
            if (ServiceList.Controls.Count == 0)
            {
                ErrorMessage += "Please select at least one service or a package.\n";
            }

            if (ErrorMessage.Equals(""))
            {
                double RealPrice = 0;

                VehicleItem RealVehicle = GetVehicleFromID(TB_Vehicle.Text.Split(':')[0].Trim());

                string Extras = string.Empty;
                for (int a = 0; a < ExtraListCheckBox.Items.Count; a++)
                {
                    foreach (string Item in ExtraList)
                    {
                        if (ExtraListCheckBox.Items[a].Text.Equals(Item) && ExtraListCheckBox.Items[a].Checked)
                        {
                            Extras += Item.ToCharArray()[0] == 'P' ? $"{PerfumeCount}{Item.ToCharArray()[0]}" : Item.ToCharArray()[0].ToString();
                            break;
                        }
                    }
                }

                RealPrice += PerfumeCount * 150;
                RealPrice += Extras.Count(c => c == 'C') * 20;

                string ServiceIds = string.Empty;
                for (int a = 0; a < ServiceList.Controls.Count; a++)
                {
                    foreach (ServiceItem Service in GlobalServiceList)
                    {
                        if (Service.Name.Equals(ServiceList.Controls[a].Controls[0].Text) && !string.IsNullOrEmpty(ServiceList.Controls[a].Controls[1].Text))
                        {
                            ServiceIds += Service.ID + ",";
                            break;
                        }
                    }
                }
                if (ServiceIds.Length > 0)
                {
                    ServiceIds = ServiceIds.Substring(0, ServiceIds.Length - 1);
                    if (ServiceIds.Length > 0)
                    {
                        ServiceIds = "[" + ServiceIds + "]";
                    }
                }

                RealPrice += ServicePrice;

                string PackageId = string.Empty;
                string PACKAGESIZE = string.Empty;

                foreach (PackageItem Package in GlobalPackageList)
                {
                    if (Package.Name.Equals(CB_Packages.Text))
                    {
                        PackageId = Package.ID;
                        PACKAGESIZE = RealVehicle.Size;
                        RealPrice += GetPackagePrice(Package.Name, PACKAGESIZE);
                        break;
                    }
                }

                string EmployeeIds = "[";
                string[] SplitComma = TB_Employee.Text.Split(',');
                for (int a = 0; a < SplitComma.Length; a++)
                {
                    foreach (EmployeeItem Employee in GlobalEmployeeList)
                    {
                        if (Employee.ID.Equals(SplitComma[a].Split(':')[0].Trim()))
                        {
                            EmployeeIds += Employee.ID + ",";
                            break;
                        }
                    }
                }
                EmployeeIds = EmployeeIds.Substring(0, EmployeeIds.Length - 1);
                EmployeeIds += "]";

                try
                {
                    DoneButton.Enabled = false;
                    CancelButton.Enabled = false;

                    RecordActivity($"Updated details of transaction with reference number [{OrderID}]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_TransactionList SET EmployeeIdList='{EmployeeIds}', ServiceIdList='{ServiceIds}', PackageId='{PackageId}', VehicleId='{RealVehicle.ID}', DateUpdated='{$"{Now.ToString("yyyy")}/{Now.ToString("MM")}/{Now.ToString("dd")}" + $" {Now.ToString("HH")}:{Now.ToString("mm")}:{Now.ToString("ss")} {Now.ToString("tt")}"}' WHERE TransactionId='{OrderID}'", SQL);
                    SqliteCommand Command1 = new SqliteCommand($"UPDATE AUTOLANDIA_BillingList SET TransactionBalance={RealPrice}, DateUpdated='{$"{Now.ToString("yyyy")}/{Now.ToString("MM")}/{Now.ToString("dd")}" + $" {Now.ToString("HH")}:{Now.ToString("mm")}:{Now.ToString("ss")} {Now.ToString("tt")}"}' WHERE BillingId='{OrderID}'", SQL);

                    Command.ExecuteNonQuery();
                    Command1.ExecuteNonQuery();

                    MaterialMessageBox.Show("Successfully updated transaction details!", "Notice");
                    TransactionsForm.RefreshTransactions();
                    GlobalBillingForm.RefreshBillings();
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

        private void EditTransactionDialog_Load(object sender, EventArgs e)
        {
            foreach (TransactionItem Order in GlobalTransactionList)
            {
                if (Order.ID.Equals(OrderID))
                {
                    TB_ID.Text = Order.ID;
                    TB_LastUpdated.Text = Order.LastUpdated;
                    TB_DateCreated.Text = Order.DateCreated;

                    RealVehicle = GetVehicleFromID(Order.VehicleId);
                    TB_Vehicle.Text = $"{RealVehicle.ID}: {RealVehicle.Brand}, {RealVehicle.Model}, {RealVehicle.PlateNumber}";

                    if (RealVehicle != null)
                    {
                        if (RealVehicle.Brand.Equals("GENERAL"))
                        {
                            ExtraListCheckBox.Controls.Clear();
                            ExtraListCheckBox.Items.Clear();
                            ExtraList.Clear();
                            Perfume = new MaterialCheckbox()
                            {
                                Text = "Perfume (₱150.00)"
                            };
                            Perfume.CheckedChanged += (s, en) =>
                            {
                                if (Perfume.Checked)
                                {
                                    new PickPerfumeCountDialog(this).ShowDialog();
                                }
                                else
                                {
                                    Perfume.Text = "Perfume (₱150.00)";
                                    PerfumeCount = 0;
                                }
                            };

                            ExtraListCheckBox.Items.Add(Perfume);
                            foreach (string Item in ExtraList)
                            {
                                ExtraListCheckBox.Items.Add(Item);
                            }
                        }
                        else
                        {
                            ExtraListCheckBox.Controls.Clear();
                            ExtraListCheckBox.Items.Clear();
                            ExtraList.Clear();
                            Perfume = new MaterialCheckbox()
                            {
                                Text = "Perfume (₱150.00)"
                            };
                            Perfume.CheckedChanged += (s, en) =>
                            {
                                if (Perfume.Checked)
                                {
                                    new PickPerfumeCountDialog(this).ShowDialog();
                                }
                                else
                                {
                                    Perfume.Text = "Perfume (₱150.00)";
                                    PerfumeCount = 0;
                                }
                            };
                            ExtraList.Add("Car w/ Carrier (₱20.00)");

                            ExtraListCheckBox.Items.Add(Perfume);
                            foreach (string Item in ExtraList)
                            {
                                ExtraListCheckBox.Items.Add(Item);
                            }
                        }
                    }

                    if (RealVehicle != null)
                    {
                        if (RealVehicle.Brand.Equals("GENERAL"))
                        {
                            PackageLabel.Visible = false;
                            CB_Packages.Visible = false;
                        }
                        else
                        {
                            PackageLabel.Visible = true;
                            CB_Packages.Visible = true;
                        }
                    }

                    //if (RealVehicle.Brand.Equals("GENERAL"))
                    //{

                    //}
                    //else
                    //{

                    //}

                    if (!string.IsNullOrEmpty(Order.ServiceIDList))
                    {
                        string[] ServiceSplit = Order.ServiceIDList.Substring(1, Order.ServiceIDList.Length - 2).Split(',');
                        foreach (string ServiceID in ServiceSplit)
                        {
                            foreach (ServiceItem Service in GlobalServiceList)
                            {
                                if (Service.ID.Equals(ServiceID))
                                {
                                    RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                                    TableLayoutPanel Panel = new TableLayoutPanel
                                    {
                                        ColumnCount = 2
                                    };
                                    Label Name = new Label();
                                    Label Price = new Label();

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
                                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                                    Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                                    Panel.Margin = new Padding(0);

                                    Name.Dock = DockStyle.Fill;
                                    Name.Text = Service.Name;
                                    Name.TextAlign = ContentAlignment.MiddleLeft;
                                    Name.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Name.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = DefaultBackgroundColor;
                                    };

                                    Price.Dock = DockStyle.Fill;
                                    Price.Text = $"₱{GetServicePrice(Service.Name, RealVehicle != null ? RealVehicle.Size : "S").ToString("N2")}";
                                    Price.TextAlign = ContentAlignment.MiddleCenter;
                                    Price.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Price.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel.BackColor = DefaultBackgroundColor;
                                    };

                                    ServiceList.RowStyles.Add(Row);
                                    Panel.Controls.Add(Name, 0, 0);
                                    Panel.Controls.Add(Price, 1, 0);
                                    ServiceList.Controls.Add(Panel);

                                    tableLayoutPanel2.Width = ServiceList.Width;
                                    break;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Order.PackageID))
                    {
                        foreach (PackageItem Package in GlobalPackageList)
                        {
                            if (Package.ID.Equals(Order.PackageID))
                            {
                                for (int a = 0; a < CB_Packages.Items.Count; a++)
                                {
                                    if (CB_Packages.Items[a].Equals(Package.Name))
                                    {
                                        IsOnLoad = true;
                                        CB_Packages.SelectedIndex = a;
                                        break;
                                    }
                                }

                                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                                TableLayoutPanel Panel = new TableLayoutPanel
                                {
                                    ColumnCount = 2
                                };
                                Label Name = new Label();
                                Label Price = new Label();

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
                                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                                Panel.Margin = new Padding(0);

                                Name.Dock = DockStyle.Fill;
                                Name.Text = Package.Name;
                                Name.TextAlign = ContentAlignment.MiddleLeft;
                                Name.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                Name.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };

                                Price.Dock = DockStyle.Fill;
                                Price.Text = $"₱{GetPackagePrice(Package.Name, RealVehicle != null ? RealVehicle.Size : "S").ToString("N2")}";
                                Price.TextAlign = ContentAlignment.MiddleCenter;
                                Price.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                Price.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };

                                ServiceList.RowStyles.Add(Row);
                                Panel.Controls.Add(Name, 0, 0);
                                Panel.Controls.Add(Price, 1, 0);
                                ServiceList.Controls.Add(Panel);

                                tableLayoutPanel2.Width = ServiceList.Width;

                                foreach (string Item in Package.Details.Split(','))
                                {
                                    RowStyle Row1 = new RowStyle(SizeType.Absolute, 48f);
                                    TableLayoutPanel Panel1 = new TableLayoutPanel
                                    {
                                        ColumnCount = 2
                                    };
                                    Label Name1 = new Label();
                                    Label Price1 = new Label();

                                    if (DefaultBackgroundColor == null)
                                    {
                                        DefaultBackgroundColor = Panel1.BackColor;
                                    }

                                    Panel1.Dock = DockStyle.Top;
                                    Panel1.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Panel1.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = DefaultBackgroundColor;
                                    };
                                    Panel1.ColumnStyles.Clear();
                                    Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f));
                                    Panel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                                    Panel1.Margin = new Padding(0);

                                    Name1.Dock = DockStyle.Fill;
                                    Name1.Text = Item;
                                    Name1.TextAlign = ContentAlignment.MiddleLeft;
                                    Name1.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Name1.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = DefaultBackgroundColor;
                                    };

                                    Price1.Dock = DockStyle.Fill;
                                    Price1.Text = "";
                                    Price1.TextAlign = ContentAlignment.MiddleCenter;
                                    Price1.MouseEnter += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = Color.FromArgb(200, 200, 200);
                                    };
                                    Price1.MouseLeave += (sndr, evnt) =>
                                    {
                                        Panel1.BackColor = DefaultBackgroundColor;
                                    };

                                    ServiceList.RowStyles.Add(Row1);
                                    Panel1.Controls.Add(Name1, 0, 0);
                                    Panel1.Controls.Add(Price1, 1, 0);
                                    ServiceList.Controls.Add(Panel1);

                                    tableLayoutPanel2.Width = ServiceList.Width;
                                }
                                break;
                            }
                        }
                    }

                    foreach (char Item in Order.Extras)
                    {
                        for (int a = 0; a < ExtraListCheckBox.Items.Count; a++)
                        {
                            if (ExtraListCheckBox.Items[a].Text.First().Equals(Item))
                            {
                                ExtraListCheckBox.Items[a].Checked = true;
                            }
                        }
                    }

                    string[] Split = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    foreach (string EmployeeId in Split)
                    {
                        foreach (EmployeeItem Employee in GlobalEmployeeList)
                        {
                            string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                            if (Employee.ID.Equals(EmployeeId.Trim()))
                            {
                                TB_Employee.Text += EmployeeDetail + ", ";
                                break;
                            }
                        }
                    }
                    TB_Employee.Text = TB_Employee.Text.Substring(0, TB_Employee.Text.Length - 2);

                    if (!Order.Status.Equals("Ready"))
                    {
                        TB_Employee.Enabled = false;
                        PickEmployeeButton.Enabled = false;
                        SelectServicesButton.Visible = false;
                        CB_Packages.Visible = false;
                        PackageLabel.Visible = false;
                        materialLabel4.Visible = false;
                        ExtraListCheckBox.Visible = false;
                        DoneButton.Visible = false;
                    }
                }
            }
        }

        public void SetVehicle(string Vehicle)
        {
            TB_Vehicle.Text = Vehicle;
            VehicleItem GetVehicle = GetVehicleFromID(Vehicle.Split(':')[0].Trim());

            if (GetVehicle != null)
            {
                if (GetVehicle.Brand.Equals("GENERAL"))
                {
                    ExtraListCheckBox.Controls.Clear();
                    ExtraListCheckBox.Items.Clear();
                    ExtraList.Clear();
                    Perfume = new MaterialCheckbox()
                    {
                        Text = "Perfume (₱150.00)"
                    };
                    Perfume.CheckedChanged += (s, e) =>
                    {
                        if (Perfume.Checked)
                        {
                            new PickPerfumeCountDialog(this).ShowDialog();
                        }
                        else
                        {
                            Perfume.Text = "Perfume (₱150.00)";
                            PerfumeCount = 0;
                        }
                    };

                    ExtraListCheckBox.Items.Add(Perfume);
                    foreach (string Item in ExtraList)
                    {
                        ExtraListCheckBox.Items.Add(Item);
                    }
                }
                else
                {
                    ExtraListCheckBox.Controls.Clear();
                    ExtraListCheckBox.Items.Clear();
                    ExtraList.Clear();
                    Perfume = new MaterialCheckbox()
                    {
                        Text = "Perfume (₱150.00)"
                    };
                    Perfume.CheckedChanged += (s, e) =>
                    {
                        if (Perfume.Checked)
                        {
                            new PickPerfumeCountDialog(this).ShowDialog();
                        }
                        else
                        {
                            Perfume.Text = "Perfume (₱150.00)";
                            PerfumeCount = 0;
                        }
                    };
                    ExtraList.Add("Car w/ Carrier (₱20.00)");

                    ExtraListCheckBox.Items.Add(Perfume);
                    foreach (string Item in ExtraList)
                    {
                        ExtraListCheckBox.Items.Add(Item);
                    }
                }
            }
        }
    }
}
