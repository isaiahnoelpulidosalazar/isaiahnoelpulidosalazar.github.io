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
    public partial class TransactionsForm : Form
    {
        Color DefaultBackgroundColor;
        List<TransactionItem> Temp = new List<TransactionItem>(GlobalTransactionList);
        DateTime Global;
        bool CompletedTransactions = false;

        public TransactionsForm()
        {
            InitializeComponent();

            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();
            TransactionList.VerticalScroll.Visible = true;
            TransactionList.HorizontalScroll.Enabled = false;
            TransactionList.HorizontalScroll.Visible = false;

            FilterTransaction.Items.Add("ID");
            FilterTransaction.Items.Add("Employee(s)");
            FilterTransaction.Items.Add("Plate Number");
            FilterTransaction.Items.Add("Status");
            FilterTransaction.Items.Add("Ready");
            FilterTransaction.Items.Add("In progress");
            FilterTransaction.Items.Add("Cancelled");
            FilterTransaction.SelectedIndex = 2;

            Global = DateTime.Now;
            DatePickerButton.Text = Global.ToString("yyyy/MM/dd");
        }

        public void RefreshTransactions()
        {
            RecreateGlobalTransactionList();

            Temp = new List<TransactionItem>(GlobalTransactionList);
            Temp.Reverse();

            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();

            for (int a = 0; a < Temp.Count;)
            {
                if (!DateTime.Parse(Temp[a].DateCreated).Date.ToString("d").Equals(Global.Date.ToString("d")))
                {
                    Temp.Remove(Temp[a]);
                    a = 0;
                }
                else
                {
                    a++;
                }
            }

            foreach (TransactionItem Order in Temp)
            {
                if (CompletedTransactions)
                {
                    if (Order.Status.Equals("Complete"))
                    {
                        RefreshRows(Order);
                    }
                }
                else
                {
                    RefreshRows(Order);
                }
            }
        }

        private void AddNewTransactionButton_Click(object sender, EventArgs e)
        {
            new NewTransactionDialog().ShowDialog();
        }

        private void SearchBarTransaction_TextChanged(object sender, EventArgs e)
        {
            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();

            foreach (TransactionItem Order in Temp)
            {
                if (FilterTransaction.SelectedIndex == 0 || FilterTransaction.SelectedIndex == -1)
                {
                    if (Order.ID.Contains(SearchBarTransaction.Text))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 1)
                {
                    string EmployeeList1 = string.Empty;

                    string[] Split1 = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    foreach (string EmployeeId in Split1)
                    {
                        foreach (EmployeeItem Employee in GlobalEmployeeList)
                        {
                            if (Employee.ID.Equals(EmployeeId.Trim()))
                            {
                                EmployeeList1 += $"({Employee.LastName}, {Employee.FirstName} {Employee.MiddleName})" + ",";
                            }
                        }
                    }
                    EmployeeList1 = EmployeeList1.Substring(0, EmployeeList1.Length - 1);

                    if (EmployeeList1.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 2)
                {
                    string RealPlateNumber1 = string.Empty;

                    foreach (VehicleItem Vehicle in GlobalVehicleList)
                    {
                        if (Vehicle.ID.Equals(Order.VehicleId))
                        {
                            RealPlateNumber1 = Vehicle.PlateNumber;
                        }
                    }

                    if (RealPlateNumber1.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 3)
                {
                    if (Order.Status.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
            }
        }

        private void FilterTransaction_TextChanged(object sender, EventArgs e)
        {
            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();

            foreach (TransactionItem Order in Temp)
            {
                if (FilterTransaction.SelectedIndex == 0 || FilterTransaction.SelectedIndex == -1)
                {
                    if (Order.ID.Contains(SearchBarTransaction.Text))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 1)
                {
                    string EmployeeList1 = string.Empty;

                    string[] Split1 = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    foreach (string EmployeeId in Split1)
                    {
                        foreach (EmployeeItem Employee in GlobalEmployeeList)
                        {
                            if (Employee.ID.Equals(EmployeeId.Trim()))
                            {
                                EmployeeList1 += $"({Employee.LastName}, {Employee.FirstName} {Employee.MiddleName})" + ",";
                            }
                        }
                    }
                    EmployeeList1 = EmployeeList1.Substring(0, EmployeeList1.Length - 1);

                    if (EmployeeList1.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 2)
                {
                    string RealPlateNumber1 = string.Empty;

                    foreach (VehicleItem Vehicle in GlobalVehicleList)
                    {
                        if (Vehicle.ID.Equals(Order.VehicleId))
                        {
                            RealPlateNumber1 = Vehicle.PlateNumber;
                        }
                    }

                    if (RealPlateNumber1.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 3)
                {
                    if (Order.Status.ToUpper().Contains(SearchBarTransaction.Text.ToUpper()))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 4)
                {
                    if (Order.Status.Equals("Ready"))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 5)
                {
                    if (Order.Status.Equals("In progress"))
                    {
                        RefreshRows(Order);
                    }
                }
                if (FilterTransaction.SelectedIndex == 6)
                {
                    if (Order.Status.Equals("Cancelled"))
                    {
                        RefreshRows(Order);
                    }
                }
            }
        }

        void RefreshRows(TransactionItem Order)
        {
            string RealPlateNumber = string.Empty;
            string VehicleSize = string.Empty;
            string EmployeeList = string.Empty;
            int ServiceCount = 0;

            string[] Split = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
            foreach (string EmployeeId in Split)
            {
                foreach (EmployeeItem Employee in GlobalEmployeeList)
                {
                    if (Employee.ID.Equals(EmployeeId.Trim()))
                    {
                        EmployeeList += $"({Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)})" + ",";
                    }
                }
            }
            EmployeeList = EmployeeList.Substring(0, EmployeeList.Length - 1);
            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.ID.Equals(Order.VehicleId))
                {
                    VehicleSize = Vehicle.Size;
                    RealPlateNumber = Vehicle.PlateNumber;
                }
            }
            string ServicePackageDetail = string.Empty;
            foreach (PackageItem Package in GlobalPackageList)
            {
                if (Package.ID.Equals(Order.PackageID))
                {
                    ServicePackageDetail += $"[Package] - {Package.Name}, {VehicleSize} ";
                    break;
                }
            }
            if (!string.IsNullOrEmpty(Order.ServiceIDList))
            {
                string[] ServiceSplit = Order.ServiceIDList.Substring(1, Order.ServiceIDList.Length - 2).Split(',');
                ServiceCount = ServiceSplit.Length;
                ServicePackageDetail += "[Services] - ";
                foreach (ServiceItem Service in GlobalServiceList)
                {
                    if (ServiceSplit.Contains(Service.ID))
                    {
                        ServicePackageDetail += Service.ID + ", ";
                    }
                }
                ServicePackageDetail = ServicePackageDetail.Substring(0, ServicePackageDetail.Length - 2);
            }

            RowStyle Row = new RowStyle(SizeType.Absolute, (55f + (5f * ServiceCount)));
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 7
            };
            Label Id = new Label();
            Label Employees = new Label();
            Label Service_Package = new Label();
            Label PlateNumber = new Label();
            MaterialComboBox Progress = new MaterialComboBox();
            Label LastUpdated = new Label();
            Label DateCreated = new Label();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.Margin = new Padding(0);

            Id.Dock = DockStyle.Fill;
            Id.Text = Order.ID;
            Id.TextAlign = ContentAlignment.MiddleCenter;
            Id.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Id.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Employees.Dock = DockStyle.Fill;
            Employees.Text = EmployeeList;
            Employees.TextAlign = ContentAlignment.MiddleCenter;
            Employees.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Employees.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Service_Package.Dock = DockStyle.Fill;
            Service_Package.Text = ServicePackageDetail;
            Service_Package.TextAlign = ContentAlignment.MiddleCenter;
            Service_Package.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Service_Package.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            PlateNumber.Dock = DockStyle.Fill;
            PlateNumber.Text = RealPlateNumber;
            PlateNumber.TextAlign = ContentAlignment.MiddleCenter;
            PlateNumber.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            PlateNumber.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Progress.Dock = DockStyle.Top;

            if (Order.Status.Equals("Ready"))
            {
                Progress.Items.Add("Ready");
                Progress.Items.Add("Cancelled");
                Progress.Items.Add("In progress");
                Progress.SelectedIndex = 0;
            }
            if (Order.Status.Equals("In progress"))
            {
                Progress.Items.Add("In progress");
                Progress.Items.Add("Complete");
                Progress.SelectedIndex = 0;
            }
            if (Order.Status.Equals("Complete"))
            {
                Progress.Items.Add("Complete");
                Progress.SelectedIndex = 0;
            }
            if (Order.Status.Equals("Cancelled"))
            {
                Progress.Items.Add("Cancelled");
                Progress.SelectedIndex = 0;
            }

            Progress.SelectedIndexChanged += (sndr, evnt) =>
            {
                try
                {
                    DateTime Now = DateTime.Now;

                    RecordActivity($"Changed order reference number [{Order.ID}] progress from [{Order.Status}] to [{Progress.Text}]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_TransactionList SET TransactionStatus='{Progress.Text}', DateUpdated='{$"{Now.ToString("yyyy")}/{Now.ToString("MM")}/{Now.ToString("dd")}" + $" {Now.ToString("HH")}:{Now.ToString("mm")}:{Now.ToString("ss")} {Now.ToString("tt")}"}' WHERE TransactionId='{Order.ID}'", SQL);

                    Command.ExecuteNonQuery();

                    OkMessageBox("Successfully changed transaction status!");
                    RefreshTransactions();
                    GlobalActivityRecordForm.RefreshActivities();

                    if (Progress.Text.Equals("Cancelled"))
                    {
                        SqliteCommand Command1 = new SqliteCommand($"UPDATE AUTOLANDIA_BillingList SET BillingStatus='{Progress.Text}', DateUpdated='{$"{Now.ToString("yyyy")}/{Now.ToString("MM")}/{Now.ToString("dd")}" + $" {Now.ToString("HH")}:{Now.ToString("mm")}:{Now.ToString("ss")} {Now.ToString("tt")}"}' WHERE BillingId='{Order.ID}'", SQL);

                        Command1.ExecuteNonQuery();
                        GlobalBillingForm.RefreshBillings();
                    }
                    if (Progress.Text.Equals("Complete"))
                    {
                        BillingItem Bill = GetBillFromID(Order.ID);
                        if (!Bill.Status.Equals("Paid"))
                        {
                            if (NoticeMessageBox("Proceed to payment?") == DialogResult.Yes)
                            {
                                new PaymentDialog(Order.ID).ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                }
            };

            LastUpdated.Dock = DockStyle.Fill;
            LastUpdated.Text = Order.LastUpdated;
            LastUpdated.TextAlign = ContentAlignment.MiddleCenter;
            LastUpdated.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            LastUpdated.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            DateCreated.Dock = DockStyle.Fill;
            DateCreated.Text = Order.DateCreated;
            DateCreated.TextAlign = ContentAlignment.MiddleCenter;
            DateCreated.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            DateCreated.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Panel.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            Id.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            Employees.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            Service_Package.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            PlateNumber.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            LastUpdated.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };
            DateCreated.Click += (sndr, evnt) =>
            {
                new TransactionDetailDialog(this, Order.ID).ShowDialog();
            };

            TransactionList.RowStyles.Add(Row);
            Panel.Controls.Add(Id, 0, 0);
            Panel.Controls.Add(Employees, 1, 0);
            Panel.Controls.Add(Service_Package, 2, 0);
            Panel.Controls.Add(PlateNumber, 3, 0);
            Panel.Controls.Add(Progress, 4, 0);
            Panel.Controls.Add(LastUpdated, 5, 0);
            Panel.Controls.Add(DateCreated, 6, 0);
            TransactionList.Controls.Add(Panel);
            
            tableLayoutPanel2.Width = TransactionList.Width;
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            RefreshTransactions();
        }

        public void SetDate(DateTime DateTime)
        {
            Global = DateTime;
            DatePickerButton.Text = DateTime.Date.ToString("yyyy/MM/dd");

            TransactionList.Controls.Clear();
            TransactionList.RowStyles.Clear();

            RefreshTransactions();
        }

        private void DatePickerButton_Click(object sender, EventArgs e)
        {
            new DatePickerDialog(this).ShowDialog();
        }

        private void CompletedButton_Click(object sender, EventArgs e)
        {
            if (!CompletedTransactions)
            {
                CompletedTransactions = true;
                CompletedButton.Text = "Switch to all";
                TransactionList.Controls.Clear();
                TransactionList.RowStyles.Clear();

                foreach (TransactionItem Transaction in Temp)
                {
                    if (Transaction.Status.Equals("Complete"))
                    {
                        RefreshRows(Transaction);
                    }
                }
            }
            else
            {
                CompletedTransactions = false;
                CompletedButton.Text = "Switch to completed";
                TransactionList.Controls.Clear();
                TransactionList.RowStyles.Clear();

                foreach (TransactionItem Transaction in Temp)
                {
                    RefreshRows(Transaction);
                }
            }
        }
    }
}
