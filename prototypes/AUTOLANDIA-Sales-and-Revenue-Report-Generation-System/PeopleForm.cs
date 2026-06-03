using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PeopleForm : Form
    {
        Color DefaultBackgroundColor;

        public PeopleForm()
        {
            InitializeComponent();

            if (!LoggedAccount.IsAdmin)
            {
                tabControl1.TabPages.Remove(UserAccountsTab);
            }

            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();
            EmployeeList.VerticalScroll.Visible = true;
            EmployeeList.HorizontalScroll.Enabled = false;
            EmployeeList.HorizontalScroll.Visible = false;

            CustomerList.Controls.Clear();
            CustomerList.RowStyles.Clear();
            CustomerList.VerticalScroll.Visible = true;
            CustomerList.HorizontalScroll.Enabled = false;
            CustomerList.HorizontalScroll.Visible = false;

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();
            VehicleList.VerticalScroll.Visible = true;
            VehicleList.HorizontalScroll.Enabled = false;
            VehicleList.HorizontalScroll.Visible = false;

            UserList.Controls.Clear();
            UserList.RowStyles.Clear();
            UserList.VerticalScroll.Visible = true;
            UserList.HorizontalScroll.Enabled = false;
            UserList.HorizontalScroll.Visible = false;

            FilterEmployee.Items.Add("ID");
            FilterEmployee.Items.Add("Name");
            FilterEmployee.SelectedIndex = 1;

            FilterCustomer.Items.Add("ID");
            FilterCustomer.Items.Add("Name");
            FilterCustomer.SelectedIndex = 1;

            FilterVehicle.Items.Add("ID");
            FilterVehicle.Items.Add("Brand");
            FilterVehicle.Items.Add("Model");
            FilterVehicle.Items.Add("Size");
            FilterVehicle.Items.Add("Plate Number");
            FilterVehicle.Items.Add("Owner Name");
            FilterVehicle.SelectedIndex = 4;

            FilterUser.Items.Add("Username");
            FilterUser.Items.Add("Type");
            FilterUser.SelectedIndex = 0;
        }

        public void RefreshEmployees()
        {
            RecreateGlobalEmployeeList();
            RecreateGlobalEmployeeTimeList();

            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                RefreshRows(Employee);
            }
        }

        public void RefreshCustomers()
        {
            RecreateGlobalCustomerList();

            CustomerList.Controls.Clear();
            CustomerList.RowStyles.Clear();

            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                RefreshRows(Customer);
            }
        }

        public void RefreshVehicles()
        {
            RecreateGlobalVehicleList();

            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                RefreshRows(Vehicle);
            }
        }

        public void RefreshAccounts()
        {
            RecreateGlobalAccountList();

            UserList.Controls.Clear();
            UserList.RowStyles.Clear();

            foreach (AccountItem Account in GlobalAccountList)
            {
                RefreshRows(Account);
            }
        }

        private void AddNewCustomerButton_Click(object sender, EventArgs e)
        {
            new NewCustomerDialog().ShowDialog();
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = EmployeeList.Width;
            tableLayoutPanel1.Width = CustomerList.Width;
            tableLayoutPanel3.Width = VehicleList.Width;
            tableLayoutPanel4.Width = UserList.Width;
            RefreshEmployees();
            RefreshCustomers();
            RefreshVehicles();
            RefreshAccounts();
        }

        private void AddNewEmployeeButton_Click(object sender, EventArgs e)
        {
            new NewEmployeeDialog().ShowDialog();
        }

        private void EmployeesTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = EmployeeList.Width;
        }

        private void CustomersTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = CustomerList.Width;
        }

        private void AddNewVehicleButton_Click(object sender, EventArgs e)
        {
            new AddCustomerVehicleDialog().ShowDialog();
        }

        private void VehiclesTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel3.Width = VehicleList.Width;
        }

        private void SearchBarEmployee_TextChanged(object sender, EventArgs e)
        {
            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                if (FilterEmployee.SelectedIndex == 0 || FilterEmployee.SelectedIndex == -1)
                {
                    if (Employee.ID.Contains(SearchBarEmployee.Text))
                    {
                        RefreshRows(Employee);
                    }
                }
                if (FilterEmployee.SelectedIndex == 1)
                {
                    if ($"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}".Contains(SearchBarEmployee.Text.ToUpper()))
                    {
                        RefreshRows(Employee);
                    }
                }
            }
        }

        private void SearchBarCustomer_TextChanged(object sender, EventArgs e)
        {
            CustomerList.Controls.Clear();
            CustomerList.RowStyles.Clear();

            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                if (FilterCustomer.SelectedIndex == 0 || FilterCustomer.SelectedIndex == -1)
                {
                    if (Customer.ID.Contains(SearchBarCustomer.Text))
                    {
                        RefreshRows(Customer);
                    }
                }
                if (FilterCustomer.SelectedIndex == 1)
                {
                    if ($"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}".Contains(SearchBarCustomer.Text.ToUpper()))
                    {
                        RefreshRows(Customer);
                    }
                }
            }
        }

        private void SearchBarVehicle_TextChanged(object sender, EventArgs e)
        {
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (FilterVehicle.SelectedIndex == 0 || FilterVehicle.SelectedIndex == -1)
                {
                    if (Vehicle.ID.Contains(SearchBarVehicle.Text))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 1)
                {
                    if (Vehicle.Brand.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 2)
                {
                    if (Vehicle.Model.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 3)
                {
                    if (Vehicle.Size.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 4)
                {
                    if (Vehicle.PlateNumber.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 5)
                {
                    foreach (CustomerItem Customer in GlobalCustomerList)
                    {
                        if (Customer.ID.Equals(Vehicle.CustomerID))
                        {
                            if ($"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}".ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                            {
                                RefreshRows(Vehicle);
                            }
                        }
                    }
                }
            }
        }

        private void FilterEmployee_TextChanged(object sender, EventArgs e)
        {
            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                if (FilterEmployee.SelectedIndex == 0 || FilterEmployee.SelectedIndex == -1)
                {
                    if (Employee.ID.Contains(SearchBarEmployee.Text))
                    {
                        RefreshRows(Employee);
                    }
                }
                if (FilterEmployee.SelectedIndex == 1)
                {
                    if ($"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)}".Contains(SearchBarEmployee.Text.ToUpper()))
                    {
                        RefreshRows(Employee);
                    }
                }
            }
        }

        private void FilterCustomer_TextChanged(object sender, EventArgs e)
        {
            CustomerList.Controls.Clear();
            CustomerList.RowStyles.Clear();

            foreach (CustomerItem Customer in GlobalCustomerList)
            {
                if (FilterCustomer.SelectedIndex == 0 || FilterCustomer.SelectedIndex == -1)
                {
                    if (Customer.ID.Contains(SearchBarCustomer.Text))
                    {
                        RefreshRows(Customer);
                    }
                }
                if (FilterCustomer.SelectedIndex == 1)
                {
                    if ($"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}".Contains(SearchBarCustomer.Text.ToUpper()))
                    {
                        RefreshRows(Customer);
                    }
                }
            }
        }

        private void FilterVehicle_TextChanged(object sender, EventArgs e)
        {
            VehicleList.Controls.Clear();
            VehicleList.RowStyles.Clear();

            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (FilterVehicle.SelectedIndex == 0 || FilterVehicle.SelectedIndex == -1)
                {
                    if (Vehicle.ID.Contains(SearchBarVehicle.Text))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 1)
                {
                    if (Vehicle.Brand.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 2)
                {
                    if (Vehicle.Model.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 3)
                {
                    if (Vehicle.Size.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 4)
                {
                    if (Vehicle.PlateNumber.ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                    {
                        RefreshRows(Vehicle);
                    }
                }
                if (FilterVehicle.SelectedIndex == 5)
                {
                    foreach (CustomerItem Customer in GlobalCustomerList)
                    {
                        if (Customer.ID.Equals(Vehicle.CustomerID))
                        {
                            if ($"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}".ToUpper().Contains(SearchBarVehicle.Text.ToUpper()))
                            {
                                RefreshRows(Vehicle);
                            }
                        }
                    }
                }
            }
        }

        void RefreshRows(EmployeeItem Employee)
        {
            DateTime Today = DateTime.Now;

            string TimedIn = string.Empty;
            string TimedOut = string.Empty;

            foreach (EmployeeTimeItem EmployeeTime in GlobalEmployeeTimeList)
            {
                if (EmployeeTime.ID.Equals(Employee.ID) && DateTime.Parse(EmployeeTime.DateCreated).Date == Today.Date)
                {
                    if (!string.IsNullOrEmpty(EmployeeTime.TimeIn))
                    {
                        TimedIn = EmployeeTime.TimeIn;
                    }
                    if (!string.IsNullOrEmpty(EmployeeTime.TimeOut))
                    {
                        TimedOut = EmployeeTime.TimeOut;
                    }
                }
            }

            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 4
            };
            Label Id = new Label();
            Label Name = new Label();
            Label TimeIn = new Label();
            Label TimeOut = new Label();
            Button TimeInButton = new Button();
            Button TimeOutButton = new Button();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            Panel.Margin = new Padding(0);

            Id.Dock = DockStyle.Fill;
            Id.Text = Employee.ID;
            Id.TextAlign = ContentAlignment.MiddleCenter;
            Id.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Id.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

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

            TimeIn.Dock = DockStyle.Fill;
            TimeIn.Text = TimedIn;
            TimeIn.TextAlign = ContentAlignment.MiddleCenter;
            TimeIn.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            TimeIn.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            TimeOut.Dock = DockStyle.Fill;
            TimeOut.Text = TimedOut;
            TimeOut.TextAlign = ContentAlignment.MiddleCenter;
            TimeOut.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            TimeOut.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            TimeInButton.Text = "Time In";
            TimeInButton.Dock = DockStyle.Fill;
            TimeInButton.ForeColor = Color.White;
            TimeInButton.BackColor = Color.Green;
            TimeInButton.Click += (s, evnt) =>
            {
                try
                {
                    Today = DateTime.Now;

                    RecordActivity($"Employee {Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)} timed in");

                    SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_EmployeeTimeList (EmployeeId, TimeIn, TimeOut, DateCreated) VALUES " +
                        $"('{Employee.ID}', '{$"{Today.ToString("yyyy")}/{Today.ToString("MM")}/{Today.ToString("dd")}" + $" {Today.ToString("HH")}:{Today.ToString("mm")}:{Today.ToString("ss")} {Today.ToString("tt")}"}', '', '{Today.ToString("yyyy")}/{Today.ToString("MM")}/{Today.ToString("dd")}')", SQL);

                    Command.ExecuteNonQuery();

                    RefreshEmployees();
                    GlobalActivityRecordForm.RefreshActivities();
                }
                catch (Exception exception)
                {
                    MaterialMessageBox.Show(exception.Message, "Alert");
                }
            };

            TimeOutButton.Text = "Time Out";
            TimeOutButton.Dock = DockStyle.Fill;
            TimeOutButton.ForeColor = Color.White;
            TimeOutButton.BackColor = Color.FromArgb(200, 0, 0);
            TimeOutButton.Click += (s, evnt) =>
            {
                try
                {
                    Today = DateTime.Now;

                    RecordActivity($"Employee {Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : string.Empty)} timed out");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_EmployeeTimeList SET TimeOut='{$"{Today.ToString("yyyy")}/{Today.ToString("MM")}/{Today.ToString("dd")}" + $" {Today.ToString("HH")}:{Today.ToString("mm")}:{Today.ToString("ss")} {Today.ToString("tt")}"}" +
                        $"' WHERE EmployeeId='{Employee.ID}' AND DateCreated='{Today.ToString("yyyy")}/{Today.ToString("MM")}/{Today.ToString("dd")}'", SQL);

                    Command.ExecuteNonQuery();

                    RefreshEmployees();
                    GlobalActivityRecordForm.RefreshActivities();
                }
                catch (Exception exception)
                {
                    MaterialMessageBox.Show(exception.Message, "Alert");
                }
            };

            Panel.Click += (sndr, evnt) =>
            {
                new PreviewEmployeeDialog(Employee.ID).ShowDialog();
            };
            Id.Click += (sndr, evnt) =>
            {
                new PreviewEmployeeDialog(Employee.ID).ShowDialog();
            };
            Name.Click += (sndr, evnt) =>
            {
                new PreviewEmployeeDialog(Employee.ID).ShowDialog();
            };
            TimeIn.Click += (sndr, evnt) =>
            {
                new PreviewEmployeeDialog(Employee.ID).ShowDialog();
            };
            TimeOut.Click += (sndr, evnt) =>
            {
                new PreviewEmployeeDialog(Employee.ID).ShowDialog();
            };

            EmployeeList.RowStyles.Add(Row);
            Panel.Controls.Add(Id, 0, 0);
            Panel.Controls.Add(Name, 1, 0);
            if (string.IsNullOrEmpty(TimedIn))
            {
                Panel.Controls.Add(TimeInButton, 2, 0);
                Panel.Controls.Add(TimeOut, 3, 0);
            }
            else
            {
                if (string.IsNullOrEmpty(TimedOut))
                {
                    Panel.Controls.Add(TimeIn, 2, 0);
                    Panel.Controls.Add(TimeOutButton, 3, 0);
                }
                else
                {
                    Panel.Controls.Add(TimeIn, 2, 0);
                    Panel.Controls.Add(TimeOut, 3, 0);
                }
            }
            EmployeeList.Controls.Add(Panel);

            tableLayoutPanel2.Width = EmployeeList.Width;
        }

        void RefreshRows(CustomerItem Customer)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 3
            };
            Label Id = new Label();
            Label Name = new Label();
            Label PlateNumbers = new Label();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42f));
            Panel.Margin = new Padding(0);

            Id.Dock = DockStyle.Fill;
            Id.Text = Customer.ID;
            Id.TextAlign = ContentAlignment.MiddleCenter;
            Id.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Id.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Name.Dock = DockStyle.Fill;
            Name.Text = $"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : string.Empty)}";
            Name.TextAlign = ContentAlignment.MiddleCenter;
            Name.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Name.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            PlateNumbers.Dock = DockStyle.Fill;
            PlateNumbers.Text = Customer.PlateNumbers;
            PlateNumbers.TextAlign = ContentAlignment.MiddleCenter;
            PlateNumbers.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            PlateNumbers.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Panel.Click += (sndr, evnt) =>
            {
                new PreviewCustomerDialog(this, Customer.ID).ShowDialog();
            };
            Id.Click += (sndr, evnt) =>
            {
                new PreviewCustomerDialog(this, Customer.ID).ShowDialog();
            };
            Name.Click += (sndr, evnt) =>
            {
                new PreviewCustomerDialog(this, Customer.ID).ShowDialog();
            };
            PlateNumbers.Click += (sndr, evnt) =>
            {
                new PreviewCustomerDialog(this, Customer.ID).ShowDialog();
            };

            CustomerList.RowStyles.Add(Row);
            Panel.Controls.Add(Id, 0, 0);
            Panel.Controls.Add(Name, 1, 0);
            Panel.Controls.Add(PlateNumbers, 2, 0);
            CustomerList.Controls.Add(Panel);

            tableLayoutPanel1.Width = CustomerList.Width;
        }

        void RefreshRows(VehicleItem Vehicle)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 6
            };
            Label Id = new Label();
            Label Brand = new Label();
            Label Model = new Label();
            Label Size = new Label();
            Label PlateNumber = new Label();
            Label CustomerName = new Label();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.Margin = new Padding(0);

            Id.Dock = DockStyle.Fill;
            Id.Text = Vehicle.ID;
            Id.TextAlign = ContentAlignment.MiddleCenter;
            Id.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Id.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

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

            string RealCustomerName = "(None)";

            foreach (CustomerItem Customer1 in GlobalCustomerList)
            {
                if (Customer1.ID.Equals(Vehicle.CustomerID))
                {
                    RealCustomerName = $"{Customer1.LastName}, {Customer1.FirstName}{(!string.IsNullOrEmpty(Customer1.MiddleName) ? $" {Customer1.MiddleName}" : string.Empty)}";
                }
            }

            CustomerName.Dock = DockStyle.Fill;
            CustomerName.Text = RealCustomerName;
            CustomerName.TextAlign = ContentAlignment.MiddleCenter;
            CustomerName.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            CustomerName.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Panel.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            Id.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            Brand.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            Model.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            Size.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            PlateNumber.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };
            CustomerName.Click += (sndr, evnt) =>
            {
                new PreviewVehicleDialog(this, Vehicle.ID).ShowDialog();
            };

            VehicleList.RowStyles.Add(Row);
            Panel.Controls.Add(Id, 0, 0);
            Panel.Controls.Add(Brand, 1, 0);
            Panel.Controls.Add(Model, 2, 0);
            Panel.Controls.Add(Size, 3, 0);
            Panel.Controls.Add(PlateNumber, 4, 0);
            Panel.Controls.Add(CustomerName, 5, 0);
            VehicleList.Controls.Add(Panel);

            tableLayoutPanel3.Width = VehicleList.Width;
        }

        void RefreshRows(AccountItem Account)
        {
            if (!LoggedAccount.Username.Equals(Account.Username))
            {
                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                TableLayoutPanel Panel = new TableLayoutPanel
                {
                    ColumnCount = 3
                };
                Label Username = new Label();
                Label Password = new Label();
                Label Type = new Label();

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
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                Panel.Margin = new Padding(0);

                Username.Dock = DockStyle.Fill;
                Username.Text = Account.Username;
                Username.TextAlign = ContentAlignment.MiddleCenter;
                Username.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Username.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Password.Dock = DockStyle.Fill;
                Password.Text = "***";
                Password.TextAlign = ContentAlignment.MiddleCenter;
                Password.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Password.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Type.Dock = DockStyle.Fill;
                Type.Text = Account.IsAdmin ? "Admin" : "User";
                Type.TextAlign = ContentAlignment.MiddleCenter;
                Type.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                Type.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                Panel.Click += (sndr, evnt) =>
                {
                    new EditUserDialog(Account.Username, Account.Password, Account.IsAdmin ? 0 : 1).ShowDialog();
                };
                Username.Click += (sndr, evnt) =>
                {
                    new EditUserDialog(Account.Username, Account.Password, Account.IsAdmin ? 0 : 1).ShowDialog();
                };
                Password.Click += (sndr, evnt) =>
                {
                    new EditUserDialog(Account.Username, Account.Password, Account.IsAdmin ? 0 : 1).ShowDialog();
                };
                Type.Click += (sndr, evnt) =>
                {
                    new EditUserDialog(Account.Username, Account.Password, Account.IsAdmin ? 0 : 1).ShowDialog();
                };

                UserList.RowStyles.Add(Row);
                Panel.Controls.Add(Username, 0, 0);
                Panel.Controls.Add(Password, 1, 0);
                Panel.Controls.Add(Type, 2, 0);
                UserList.Controls.Add(Panel);

                tableLayoutPanel4.Width = VehicleList.Width;
            }
        }

        private void EmployeeScheduleButton_Click(object sender, EventArgs e)
        {
            new EmployeeScheduleDialog().ShowDialog();
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            new NewUserDialog().ShowDialog();
        }

        private void SearchUser_TextChanged(object sender, EventArgs e)
        {
            UserList.Controls.Clear();
            UserList.RowStyles.Clear();
            
            foreach (AccountItem Account in GlobalAccountList)
            {
                if (FilterUser.SelectedIndex == 0 || FilterUser.SelectedIndex == -1)
                {
                    if (Account.Username.ToUpper().Contains(SearchUser.Text.ToUpper()))
                    {
                        RefreshRows(Account);
                    }
                }
                if (FilterUser.SelectedIndex == 1)
                {
                    if ("ADMIN".Contains(SearchUser.Text.ToUpper()))
                    {
                        if (Account.IsAdmin)
                        {
                            RefreshRows(Account);
                        }
                    }
                    if ("USER".Contains(SearchUser.Text.ToUpper()))
                    {
                        if (!Account.IsAdmin)
                        {
                            RefreshRows(Account);
                        }
                    }
                }
            }
        }

        private void UserAccountsTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel4.Width = UserList.Width;
        }
    }
}
