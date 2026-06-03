using MaterialSkin.Controls;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectExportItemDialog : MaterialForm
    {
        DateTime EmployeeSalaryDate, EmployeeWorkBreakdownDate;

        public SelectExportItemDialog()
        {
            InitializeComponent();

            List<string> Temp = new List<string>();
            Temp.Add("Activity List");
            Temp.Add("Billing List");
            Temp.Add("Customer List");
            Temp.Add("Employee List");
            Temp.Add("Employee Salary");
            Temp.Add("Employee Work Breakdown");
            Temp.Add("Order List");
            Temp.Add("Vehicle List");
            Temp.Reverse();

            foreach (string List in Temp)
            {
                if (!InCheckedListBox(List))
                {
                    ItemListCheckBox.Items.Add(List);
                }
            }
        }

        bool InCheckedListBox(string text)
        {
            foreach (MaterialCheckbox Item in ItemListCheckBox.Items)
            {
                if (Item.Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            DoneButton.Enabled = false;
            Timer Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += (s, ev) =>
            {
                Timer.Stop();
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        RecreateAllGlobalData();

                        foreach (MaterialCheckbox Item in ItemListCheckBox.Items)
                        {
                            if (Item.Text == "Activity List" && Item.Checked)
                            {
                                string ActivityData = "<h1>Activity List</h1><table>";
                                foreach (string Message in GlobalActivityList)
                                {
                                    ActivityData += DataToHtml(Message);
                                }
                                ActivityData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(ActivityData, PdfSharp.PageSize.A4);
                                PDF.Save("ActivityList.pdf");
                            }
                            if (Item.Text == "Billing List" && Item.Checked)
                            {
                                string BillingData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Billing List</h1><table>";
                                foreach (BillingItem Billing in GlobalBillingList)
                                {
                                    BillingData += DataToHtml(Billing.ID, Billing.Balance.ToString(), Billing.Discount.ToString(), Billing.Paid.ToString(), Billing.Status, Billing.LastUpdated, Billing.DateCreated);
                                }
                                BillingData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(BillingData, PdfSharp.PageSize.A4);
                                PDF.Save("BillingList.pdf");
                            }
                            if (Item.Text == "Customer List" && Item.Checked)
                            {
                                string CustomerData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Customer List</h1><table>";
                                foreach (CustomerItem Customer in GlobalCustomerList)
                                {
                                    CustomerData += DataToHtml(Customer.ID, $"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : "")}", Customer.MobileNumber, Customer.Address, Customer.PlateNumbers);
                                }
                                CustomerData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(CustomerData, PdfSharp.PageSize.A4);
                                PDF.Save("CustomerList.pdf");
                            }
                            if (Item.Text == "Employee List" && Item.Checked)
                            {
                                string EmployeeData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Employee List</h1><table>";
                                foreach (EmployeeItem Employee in GlobalEmployeeList)
                                {
                                    EmployeeData += DataToHtml(Employee.ID, $"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : "")}", Employee.MobileNumber, Employee.Address, Employee.Documents);
                                }
                                EmployeeData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(EmployeeData, PdfSharp.PageSize.A4);
                                PDF.Save("EmployeeList.pdf");
                            }
                            if (Item.Text == "Employee Salary" && Item.Checked)
                            {
                                new DatePickerDialog(this).ShowDialog();
                                string EmployeeSalaryData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Employee Salary List - " + EmployeeSalaryDate.Date + "</h1><table>";
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

                                        if (DateTime.Parse(Order.DateCreated).Date.Equals(EmployeeSalaryDate.Date) && Split.Contains(Employee.ID.Trim()))
                                        {
                                            foreach (BillingItem Billing in GlobalBillingList)
                                            {
                                                if (Billing.ID.Equals(Order.ID))
                                                {
                                                    TotalSalary += ((Billing.Balance * 0.3) / Split.Count);
                                                }
                                            }
                                        }
                                    }
                                    EmployeeSalaryData += DataToHtml($"{Employee.LastName}, {Employee.FirstName}{(!string.IsNullOrEmpty(Employee.MiddleName) ? $" {Employee.MiddleName}" : "")}", TotalSalary.ToString());
                                }
                                EmployeeSalaryData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(EmployeeSalaryData, PdfSharp.PageSize.A4);
                                PDF.Save("EmployeeSalaryList.pdf");
                            }
                            if (Item.Text == "Employee Work Breakdown" && Item.Checked)
                            {
                                new DatePickerDialog(this, false).ShowDialog();
                                string EmployeeWorkBreakdownData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Employee Work Breakdown List - " + EmployeeSalaryDate.Date + "</h1><table>";
                                foreach (EmployeeItem Employee in GlobalEmployeeList)
                                {
                                    string EmployeeName = string.Empty;
                                    string AllVehiclesWorkedOn = string.Empty;
                                    string AllVehiclePlateNumbers = string.Empty;
                                    string AllAmount = string.Empty;
                                    string ServicePackageDetail = string.Empty;

                                    foreach (TransactionItem Order in GlobalTransactionList)
                                    {
                                        if (DateTime.Parse(Order.DateCreated).Date.Equals(EmployeeWorkBreakdownDate.Date))
                                        {
                                            VehicleItem RealVehicle = null;

                                            string[] Temp = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                                            List<string> Split = new List<string>();
                                            foreach (string S in Temp)
                                            {
                                                Split.Add(S.Trim());
                                            }

                                            foreach (EmployeeItem Employee1 in GlobalEmployeeList)
                                            {
                                                if (Employee1.ID.Equals(Employee.ID))
                                                {
                                                    EmployeeName = $"{Employee1.LastName}, {Employee1.FirstName} {Employee1.MiddleName}";

                                                    foreach (VehicleItem Vehicle in GlobalVehicleList)
                                                    {
                                                        if (Vehicle.ID.Equals(Order.VehicleId))
                                                        {
                                                            RealVehicle = Vehicle;
                                                            AllVehiclePlateNumbers += $"{Vehicle.PlateNumber}\n";
                                                            AllVehiclesWorkedOn += $"{Vehicle.Brand} {Vehicle.Model}\n";
                                                        }
                                                    }

                                                    foreach (BillingItem Billing in GlobalBillingList)
                                                    {
                                                        if (Billing.ID.Equals(Order.ID))
                                                        {
                                                            AllAmount += $"{((Billing.Balance * 0.3) / Split.Count)}\n";
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
                                                        foreach (ServiceItem Service in GlobalServiceList)
                                                        {
                                                            if (ServiceSplit.Contains(Service.ID))
                                                            {
                                                                ServicePackageDetail += "[Services] - ";
                                                                ServicePackageDetail += Service.Name + ", ";
                                                            }
                                                        }
                                                        ServicePackageDetail = ServicePackageDetail.Substring(0, ServicePackageDetail.Length - 2);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    EmployeeWorkBreakdownData += DataToHtml(EmployeeName, AllVehiclePlateNumbers.Substring(0, AllVehiclePlateNumbers.Length - 1), AllVehiclesWorkedOn.Substring(0, AllVehiclesWorkedOn.Length - 1), ServicePackageDetail, AllAmount.Substring(0, AllAmount.Length - 1));
                                }
                                EmployeeWorkBreakdownData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(EmployeeWorkBreakdownData, PdfSharp.PageSize.A4);
                                PDF.Save("EmployeeWorkBreakdownList.pdf");
                            }
                            if (Item.Text == "Order List" && Item.Checked)
                            {
                            }
                            if (Item.Text == "Vehicle List" && Item.Checked)
                            {
                                string VehicleData = "<style>table {width: 100%;} td {text-align: center;}</style><h1>Vehicle List</h1><table>";
                                foreach (VehicleItem Vehicle in GlobalVehicleList)
                                {
                                    string CustomerName = Vehicle.CustomerID;
                                    foreach (CustomerItem Customer in GlobalCustomerList)
                                    {
                                        if (Customer.ID.Equals(Vehicle.CustomerID))
                                        {
                                            CustomerName = $"{Customer.LastName}, {Customer.FirstName}{(!string.IsNullOrEmpty(Customer.MiddleName) ? $" {Customer.MiddleName}" : "")}";
                                        }
                                    }
                                    VehicleData += DataToHtml(Vehicle.ID, Vehicle.Brand, Vehicle.Model, Vehicle.Size, Vehicle.PlateNumber, CustomerName);
                                }
                                VehicleData += "</table>";
                                PdfDocument PDF = PdfGenerator.GeneratePdf(VehicleData, PdfSharp.PageSize.A4);
                                PDF.Save("VehicleList.pdf");
                            }
                        }

                        Invoke(new MethodInvoker(() =>
                        {
                            MaterialMessageBox.Show("Exported as PDF.", "Notice");
                            Close();
                        }));
                    }
                    catch
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            MaterialMessageBox.Show("Failed to export as PDF.", "Alert");
                            Close();
                        }));
                    }
                })).Start();
            };
            Timer.Start();
        }

        public void SetEmployeeSalaryDate(DateTime Date)
        {
            EmployeeSalaryDate = Date;
        }

        public void SetEmployeeWorkBreakdownDate(DateTime Date)
        {
            EmployeeWorkBreakdownDate = Date;
        }

        string DataToHtml(params string[] Data)
        {
            string Temp = "\n<tr>";
            for (int a = 0; a < Data.Length; a++)
            {
                Temp += $"<td>{Data[a]}</td>";
            }
            return Temp + "</tr>";
        }
    }
}
