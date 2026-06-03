using AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.Properties;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ReceiptDialog : MaterialForm
    {
        string ID;
        VehicleItem Vehicle;
        TransactionItem Transaction;

        public ReceiptDialog(string ID, double PaidAmount)
        {
            InitializeComponent();

            this.ID = ID;

            Banner.Image = Resources.autolandia_logo;

            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();
            BillingList.VerticalScroll.Visible = true;
            BillingList.HorizontalScroll.Enabled = false;
            BillingList.HorizontalScroll.Visible = false;
            Transaction = GetTransactionFromID(ID);
            BillingItem Bill = GetBillFromID(ID);

            RefreshRows("--------------------------------------------------------------------------------", "");
            RefreshRows("Date", Bill.LastUpdated);
            RefreshRows("--------------------------------------------------------------------------------", "");
            RefreshRows("Cashier", LoggedAccount.Username);
            RefreshRows("--------------------------------------------------------------------------------", "");
            string[] EmployeeSplit = Transaction.EmployeeIDList.Substring(1, Transaction.EmployeeIDList.Length - 2).Split(',');
            for (int a = 0; a < EmployeeSplit.Length; a++)
            {
                EmployeeItem Employee = GetEmployeeFromID(EmployeeSplit[a]);
                RefreshRows(a == 0 ? "Employee(s)" : "", $"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : "")}");
            }
            RefreshRows("--------------------------------------------------------------------------------", "");
            Vehicle = GetVehicleFromID(Transaction.VehicleId);
            if (!string.IsNullOrEmpty(Transaction.ServiceIDList))
            {
                string[] ServiceSplit = Transaction.ServiceIDList.Substring(1, Transaction.ServiceIDList.Length - 2).Split(',');
                foreach (ServiceItem Service in GlobalServiceList)
                {
                    if (ServiceSplit.Contains(Service.ID))
                    {
                        RefreshRows(Service);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Transaction.PackageID))
            {
                PackageItem Package = GetPackageFromID(Transaction.PackageID);
                RefreshRows(Package);
            }
            RefreshRows("--------------------------------------------------------------------------------", "");
            RefreshRows("Amount due", $"₱{GetBillFromID(ID).Balance.ToString("N2")}");
            RefreshRows("Cash tendered", $"₱{PaidAmount.ToString("N2")}");
            RefreshRows("--------------------------------------------------------------------------------", "");
            RefreshRows("Change", $"₱{(PaidAmount - GetBillFromID(ID).Balance).ToString("N2")}");
        }

        void RefreshRows(string Text, string Subtext)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 20f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 2
            };
            Label Item = new Label();
            Label Price = new Label();

            Panel.Dock = DockStyle.Fill;
            Panel.ColumnStyles.Clear();
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            Panel.Margin = new Padding(0);

            Item.Dock = DockStyle.Fill;
            Item.Text = Text;
            Item.TextAlign = ContentAlignment.MiddleLeft;
            
            Price.Dock = DockStyle.Fill;
            Price.Text = Subtext;
            Price.TextAlign = ContentAlignment.MiddleRight;

            Panel.Click += (sndr, evnt) =>
            {
            };
            Item.Click += (sndr, evnt) =>
            {
            };

            BillingList.RowStyles.Add(Row);
            Panel.Controls.Add(Item, 0, 0);
            Panel.Controls.Add(Price, 1, 0);
            BillingList.Controls.Add(Panel);
        }

        void RefreshRows(ServiceItem Service)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 20f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 2
            };
            Label Item = new Label();
            Label Price = new Label();

            Panel.Dock = DockStyle.Fill;
            Panel.MouseEnter += (sndr, evnt) =>
            Panel.ColumnStyles.Clear();
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            Panel.Margin = new Padding(0);

            Item.Dock = DockStyle.Fill;
            Item.Text = Service.Name;
            Item.TextAlign = ContentAlignment.MiddleLeft;

            Price.Dock = DockStyle.Fill;
            Price.Text = $"₱{GetServicePrice(Service.Name, Vehicle.Size).ToString("N2")}";
            Price.TextAlign = ContentAlignment.MiddleRight;

            Panel.Click += (sndr, evnt) =>
            {
            };
            Item.Click += (sndr, evnt) =>
            {
            };

            BillingList.RowStyles.Add(Row);
            Panel.Controls.Add(Item, 0, 0);
            Panel.Controls.Add(Price, 1, 0);
            BillingList.Controls.Add(Panel);
        }

        void RefreshRows(PackageItem Package)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 20f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 2
            };
            Label Item = new Label();
            Label Price = new Label();

            Panel.Dock = DockStyle.Fill;
            Panel.ColumnStyles.Clear();
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            Panel.Margin = new Padding(0);

            Item.Dock = DockStyle.Fill;
            Item.Text = $"{Package.Name} - {Vehicle.Size}";
            Item.TextAlign = ContentAlignment.MiddleLeft;

            Price.Dock = DockStyle.Fill;
            Price.Text = $"₱{GetPackagePrice(Package.Name, Vehicle.Size).ToString("N2")}";
            Price.TextAlign = ContentAlignment.MiddleRight;

            Panel.Click += (sndr, evnt) =>
            {
            };
            Item.Click += (sndr, evnt) =>
            {
            };

            BillingList.RowStyles.Add(Row);
            Panel.Controls.Add(Item, 0, 0);
            Panel.Controls.Add(Price, 1, 0);
            BillingList.Controls.Add(Panel);
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
