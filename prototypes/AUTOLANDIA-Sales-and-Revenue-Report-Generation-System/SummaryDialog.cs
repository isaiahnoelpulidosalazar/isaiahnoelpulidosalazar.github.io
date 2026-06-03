using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SummaryDialog : MaterialForm
    {
        List<EmployeeItem> Temp = new List<EmployeeItem>(GlobalEmployeeList);
        Color DefaultBackgroundColor;
        bool FirstDateSelected = false;
        DateTime FirstDate, EndDate;

        public SummaryDialog()
        {
            InitializeComponent();

            SummaryText.Text = string.Empty;

            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();
            TransactionList.VerticalScroll.Visible = true;
            TransactionList.HorizontalScroll.Enabled = false;
            TransactionList.HorizontalScroll.Visible = false;

            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();
            EmployeeList.VerticalScroll.Visible = true;
            EmployeeList.HorizontalScroll.Enabled = false;
            EmployeeList.HorizontalScroll.Visible = false;

            SummaryCalendar.MaxDate = DateTime.Now.Date;
            SummaryCalendar.MinDate = DateTime.Now.Date.AddMonths(-2);

            FirstDate = SummaryCalendar.MaxDate;
            EndDate = SummaryCalendar.MaxDate;
        }

        private void SummaryDialog_Load(object sender, EventArgs e)
        {
            Temp.Reverse();

            MaterialCheckbox CheckBoxAll = new MaterialCheckbox();

            if (EndDate != null && FirstDate != null)
            {
                TimeSpan DateRangeTimeSpan = EndDate - FirstDate;
                DateTime[] DateTimeRange = new DateTime[DateRangeTimeSpan.Days + 1];

                for (int a = 0; a < DateTimeRange.Length; a++)
                {
                    if (DateTimeRange[DateTimeRange.Length - 1].CompareTo(EndDate) != 0)
                    {
                        DateTimeRange[a] = FirstDate.AddDays(a);
                    }
                }

                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    string[] Temp1 = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    List<string> Split = new List<string>();

                    foreach (string S in Temp1)
                    {
                        Split.Add(S.Trim());
                    }
                    foreach (EmployeeItem Employee in Temp)
                    {
                        if (DateTimeRange.Contains(DateTime.Parse(Order.DateCreated).Date) && Split.Contains(Employee.ID.Trim()))
                        {
                            MaterialCheckbox CheckBox = new MaterialCheckbox();
                            CheckBox.Text = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}";
                            CheckBox.CheckedChanged += (s, ev) =>
                            {
                                RefreshEmployeeWork(FirstDate.Date, EndDate.Date);
                            };
                            CheckBox.Click += (s, ev) =>
                            {
                                CheckBoxAll.Checked = false;
                            };
                            if (!InCheckedListBox(CheckBox.Text))
                            {
                                EmployeeListCheckBox.Items.Add(CheckBox);
                            }
                        }
                    }
                }
            }

            CheckBoxAll.Text = $"Select All";
            CheckBoxAll.Click += (s, ev) =>
            {
                foreach (MaterialCheckbox CB in EmployeeListCheckBox.Items)
                {
                    CB.Checked = CheckBoxAll.Checked;
                }
                RefreshEmployeeWork(FirstDate.Date, EndDate.Date);
            };
            EmployeeListCheckBox.Items.Add(CheckBoxAll);

            Timer Timer = new Timer();
            Timer.Interval = 1;
            Timer.Tick += (s, evt) =>
            {
                EmployeeListCheckBox.AutoScrollPosition = new Point(0, 0);
                Timer.Stop();
            };
            Timer.Start();

            RefreshEmployeeWork(SummaryCalendar.MaxDate, SummaryCalendar.MaxDate);
            RefreshEmployees(SummaryCalendar.MaxDate, SummaryCalendar.MaxDate);
        }

        bool InCheckedListBox(string text)
        {
            foreach (MaterialCheckbox Item in EmployeeListCheckBox.Items)
            {
                if (Item.Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        void RefreshEmployees(DateTime DateTimeStart, DateTime DateTimeEnd)
        {
            DateRange.Text = $"{DateTimeStart.Date.ToString("d")} - {DateTimeEnd.Date.ToString("d")}";

            TimeSpan DateRangeTimeSpan = DateTimeEnd - DateTimeStart;
            DateTime[] DateTimeRange = new DateTime[DateRangeTimeSpan.Days + 1];

            for (int a = 0; a < DateTimeRange.Length; a++)
            {
                if (DateTimeRange[DateTimeRange.Length - 1].CompareTo(DateTimeEnd) != 0)
                {
                    DateTimeRange[a] = DateTimeStart.AddDays(a);
                }
            }

            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                double TotalSalary = 0;

                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    string[] Temp = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    List<string> Split = new List<string>();

                    foreach (string S in Temp)
                    {
                        Split.Add(S.Trim());
                    }

                    if (DateTimeRange.Contains(DateTime.Parse(Order.DateCreated).Date) && Split.Contains(Employee.ID.Trim()))
                    {
                        foreach (BillingItem Billing in GlobalBillingList)
                        {
                            if (Billing.ID.Equals(Order.ID) && Billing.Status.Equals("Paid"))
                            {
                                TotalSalary += (((Billing.Balance - (Billing.Balance * (Billing.Discount / 100))) * 0.3) / Split.Count);
                            }
                        }
                    }
                }

                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                TableLayoutPanel Panel = new TableLayoutPanel
                {
                    ColumnCount = 2
                };
                Label Name = new Label();
                Label Salary = new Label();

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
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
                Panel.Margin = new Padding(0);

                Name.Dock = DockStyle.Fill;
                Name.Text = $"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}";
                Name.TextAlign = ContentAlignment.MiddleCenter;
                Name.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Name.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Salary.Dock = DockStyle.Fill;
                Salary.Text = $"₱{TotalSalary.ToString("N2")}";
                Salary.TextAlign = ContentAlignment.MiddleCenter;
                Salary.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Salary.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                EmployeeList.RowStyles.Add(Row);
                Panel.Controls.Add(Name, 0, 0);
                Panel.Controls.Add(Salary, 1, 0);
                EmployeeList.Controls.Add(Panel);

                tableLayoutPanel2.Width = EmployeeList.Width;
            }
        }

        void RefreshEmployeeWork(DateTime DateTimeStart, DateTime DateTimeEnd)
        {
            DateRange.Text = $"{DateTimeStart.Date.ToString("d")} - {DateTimeEnd.Date.ToString("d")}";

            TimeSpan DateRangeTimeSpan = DateTimeEnd - DateTimeStart;
            DateTime[] DateTimeRange = new DateTime[DateRangeTimeSpan.Days + 1];

            for (int a = 0; a < DateTimeRange.Length; a++)
            {
                if (DateTimeRange[DateTimeRange.Length - 1].CompareTo(DateTimeEnd) != 0)
                {
                    DateTimeRange[a] = DateTimeStart.AddDays(a);
                }
            }

            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();

            for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
            {
                if (!EmployeeListCheckBox.Items[a].Text.Equals("Select All"))
                {
                    if (EmployeeListCheckBox.Items[a].Checked)
                    {
                        string CheckedID = EmployeeListCheckBox.Items[a].Text.Split(':')[0].Trim();
                        string EmployeeName = string.Empty;
                        string AllVehiclesWorkedOn = string.Empty;
                        string AllVehiclePlateNumbers = string.Empty;
                        string AllAmount = string.Empty;
                        string ServicePackageDetail = string.Empty;
                        int Vehicles = 0;

                        foreach (TransactionItem Order in GlobalTransactionList)
                        {
                            if (DateTimeRange.Contains(DateTime.Parse(Order.DateCreated).Date))
                            {
                                VehicleItem RealVehicle = null;

                                string[] Temp = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                                List<string> Split = new List<string>();
                                foreach (string S in Temp)
                                {
                                    Split.Add(S.Trim());
                                }

                                if (Split.Contains(CheckedID))
                                {
                                    foreach (EmployeeItem Employee in GlobalEmployeeList)
                                    {
                                        if (Employee.ID.Equals(CheckedID))
                                        {
                                            EmployeeName = $"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}";

                                            foreach (VehicleItem Vehicle in GlobalVehicleList)
                                            {
                                                if (Vehicle.ID.Equals(Order.VehicleId))
                                                {
                                                    RealVehicle = Vehicle;
                                                    AllVehiclePlateNumbers += $"{Vehicle.PlateNumber}\n\n";
                                                    AllVehiclesWorkedOn += $"{Vehicle.Brand} {Vehicle.Model}\n\n";
                                                    Vehicles++;
                                                }
                                            }

                                            foreach (BillingItem Billing in GlobalBillingList)
                                            {
                                                if (Billing.ID.Equals(Order.ID))
                                                {
                                                    AllAmount += $"{(Billing.Status.Equals("Paid") ? $"₱{(((Billing.Balance - (Billing.Balance * (Billing.Discount / 100))) * 0.3) / Split.Count).ToString("N2")}" : "Bill still unpaid")}\n\n";
                                                }
                                            }

                                            foreach (PackageItem Package in GlobalPackageList)
                                            {
                                                if (Package.ID.Equals(Order.PackageID))
                                                {
                                                    ServicePackageDetail += $"[{Package.Name}, {RealVehicle.Size}] - {Package.Details} ";
                                                    break;
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(Order.ServiceIDList))
                                            {
                                                string[] ServiceSplit = Order.ServiceIDList.Substring(1, Order.ServiceIDList.Length - 2).Split(',');
                                                ServicePackageDetail += "[Services] - ";
                                                foreach (ServiceItem Service in GlobalServiceList)
                                                {
                                                    if (ServiceSplit.Contains(Service.ID))
                                                    {
                                                        ServicePackageDetail += Service.Name + ", ";
                                                    }
                                                }
                                                ServicePackageDetail = ServicePackageDetail.Substring(0, ServicePackageDetail.Length - 2);
                                                ServicePackageDetail += "\n\n";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        RowStyle Row = new RowStyle(SizeType.Absolute, (100f + (25f * Vehicles)));
                        TableLayoutPanel Panel = new TableLayoutPanel
                        {
                            ColumnCount = 5
                        };
                        Label Employees = new Label();
                        Label PlateNumber = new Label();
                        Label VehicleName = new Label();
                        Label Service_Package = new Label();
                        Label Amount = new Label();

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
                        Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                        Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                        Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                        Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                        Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                        Panel.Margin = new Padding(0);

                        Employees.Dock = DockStyle.Fill;
                        Employees.Text = EmployeeName.Trim();
                        Employees.TextAlign = ContentAlignment.MiddleCenter;
                        Employees.MouseEnter += (sndr, evnt) =>
                        {
                            Panel.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Employees.MouseLeave += (sndr, evnt) =>
                        {
                            Panel.BackColor = DefaultBackgroundColor;
                        };

                        PlateNumber.Dock = DockStyle.Fill;
                        PlateNumber.Text = AllVehiclePlateNumbers.Substring(0, AllVehiclePlateNumbers.Length - 1).Trim();
                        PlateNumber.TextAlign = ContentAlignment.MiddleCenter;
                        PlateNumber.MouseEnter += (sndr, evnt) =>
                        {
                            Panel.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        PlateNumber.MouseLeave += (sndr, evnt) =>
                        {
                            Panel.BackColor = DefaultBackgroundColor;
                        };

                        VehicleName.Dock = DockStyle.Fill;
                        VehicleName.Text = AllVehiclesWorkedOn.Substring(0, AllVehiclesWorkedOn.Length - 1).Trim();
                        VehicleName.TextAlign = ContentAlignment.MiddleCenter;
                        VehicleName.MouseEnter += (sndr, evnt) =>
                        {
                            Panel.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        VehicleName.MouseLeave += (sndr, evnt) =>
                        {
                            Panel.BackColor = DefaultBackgroundColor;
                        };

                        Service_Package.Dock = DockStyle.Fill;
                        Service_Package.Text = ServicePackageDetail.Trim();
                        Service_Package.TextAlign = ContentAlignment.MiddleCenter;
                        Service_Package.MouseEnter += (sndr, evnt) =>
                        {
                            Panel.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Service_Package.MouseLeave += (sndr, evnt) =>
                        {
                            Panel.BackColor = DefaultBackgroundColor;
                        };

                        Amount.Dock = DockStyle.Fill;
                        Amount.Text = AllAmount.Substring(0, AllAmount.Length - 1).Trim();
                        Amount.TextAlign = ContentAlignment.MiddleCenter;
                        Amount.MouseEnter += (sndr, evnt) =>
                        {
                            Panel.BackColor = Color.FromArgb(200, 200, 200);
                        };
                        Amount.MouseLeave += (sndr, evnt) =>
                        {
                            Panel.BackColor = DefaultBackgroundColor;
                        };

                        Panel.Click += (sndr, evnt) =>
                        {
                        };
                        Employees.Click += (sndr, evnt) =>
                        {
                        };
                        PlateNumber.Click += (sndr, evnt) =>
                        {
                        };
                        VehicleName.Click += (sndr, evnt) =>
                        {
                        };
                        Service_Package.Click += (sndr, evnt) =>
                        {
                        };

                        TransactionList.RowStyles.Add(Row);
                        Panel.Controls.Add(Employees, 0, 0);
                        Panel.Controls.Add(PlateNumber, 1, 0);
                        Panel.Controls.Add(VehicleName, 2, 0);
                        Panel.Controls.Add(Service_Package, 3, 0);
                        Panel.Controls.Add(Amount, 4, 0);
                        TransactionList.Controls.Add(Panel);

                        tableLayoutPanel1.Width = TransactionList.Width;
                    }
                }
            }
        }
        private void SummaryCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (e.Start.Date.CompareTo(FirstDate) < 0)
            {
                FirstDateSelected = false;
            }
            if (!FirstDateSelected)
            {
                EmployeeList.Controls.Clear();
                EmployeeList.RowStyles.Clear();
                SummaryText.Text = string.Empty;
                FirstDate = e.Start.Date;
                FirstDateSelected = true;
                DateRange.Text = $"{FirstDate.Date.ToString("d")} - (Select end date)";
            }
            else
            {
                EndDate = e.Start.Date;
                FirstDateSelected = false;
                DateRange.Text = $"{FirstDate.Date.ToString("d")} - {e.Start.Date.ToString("d")}";
                if (EndDate != null && FirstDate != null)
                {
                    TimeSpan DateRangeTimeSpan = EndDate - FirstDate;
                    DateTime[] DateTimeRange = new DateTime[DateRangeTimeSpan.Days + 1];

                    for (int a = 0; a < DateTimeRange.Length; a++)
                    {
                        if (DateTimeRange[DateTimeRange.Length - 1].CompareTo(EndDate) != 0)
                        {
                            DateTimeRange[a] = FirstDate.AddDays(a);
                        }
                    }

                    foreach (TransactionItem Order in GlobalTransactionList)
                    {
                        string[] Temp1 = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                        List<string> Split = new List<string>();

                        foreach (string S in Temp1)
                        {
                            Split.Add(S.Trim());
                        }
                        foreach (EmployeeItem Employee in Temp)
                        {
                            if (DateTimeRange.Contains(DateTime.Parse(Order.DateCreated).Date) && Split.Contains(Employee.ID.Trim()))
                            {
                                MaterialCheckbox CheckBox = new MaterialCheckbox();
                                CheckBox.Text = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}";
                                CheckBox.CheckedChanged += (s, ev) =>
                                {
                                    RefreshEmployeeWork(FirstDate.Date, EndDate.Date);
                                };
                                EmployeeListCheckBox.Items.Add(CheckBox);
                            }
                        }
                    }
                    EmployeeListCheckBox.Items.Clear();
                    EmployeeListCheckBox.Controls.Clear();

                    foreach (TransactionItem Order in GlobalTransactionList)
                    {
                        string[] Temp1 = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                        List<string> Split = new List<string>();

                        foreach (string S in Temp1)
                        {
                            Split.Add(S.Trim());
                        }
                        foreach (EmployeeItem Employee in Temp)
                        {
                            if (DateTimeRange.Contains(DateTime.Parse(Order.DateCreated).Date) && Split.Contains(Employee.ID.Trim()))
                            {
                                MaterialCheckbox CheckBox = new MaterialCheckbox();
                                CheckBox.Text = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}";
                                CheckBox.CheckedChanged += (s, ev) =>
                                {
                                    RefreshEmployeeWork(FirstDate.Date, EndDate.Date);
                                };
                                EmployeeListCheckBox.Items.Add(CheckBox);
                            }
                        }
                    }
                }
                MaterialCheckbox CheckBoxAll = new MaterialCheckbox();
                CheckBoxAll.Text = $"Select All";
                CheckBoxAll.CheckedChanged += (s, ev) =>
                {
                    foreach (MaterialCheckbox CB in EmployeeListCheckBox.Items)
                    {
                        CB.Checked = CheckBoxAll.Checked;
                    }
                    RefreshEmployeeWork(FirstDate.Date, EndDate.Date);
                };
                EmployeeListCheckBox.Items.Add(CheckBoxAll);
                RefreshEmployees(FirstDate.Date, e.Start.Date);
                RefreshEmployeeWork(FirstDate.Date, e.Start.Date);
            }
        }
    }
}
