using System;
using System.Windows.Forms;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class DatePickerDialog : Form
    {
        SelectExportItemDialog SelectExportItemDialog;
        TransactionsForm TransactionsForm;
        BillingForm BillingForm;
        bool EmployeeSalary = false;

        public DatePickerDialog(TransactionsForm TransactionsForm)
        {
            InitializeComponent();

            this.TransactionsForm = TransactionsForm;

            monthCalendar1.MaxDate = DateTime.Today;
        }

        public DatePickerDialog(BillingForm BillingForm)
        {
            InitializeComponent();

            this.BillingForm = BillingForm;

            monthCalendar1.MaxDate = DateTime.Today;
        }

        public DatePickerDialog(SelectExportItemDialog SelectExportItemDialog, bool EmployeeSalary = true)
        {
            InitializeComponent();

            this.SelectExportItemDialog = SelectExportItemDialog;
            this.EmployeeSalary = EmployeeSalary;

            Text = EmployeeSalary ? "Select Salary Date" : "Select Work Breakdown Date";

            monthCalendar1.MaxDate = DateTime.Today;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (TransactionsForm != null)
            {
                TransactionsForm.SetDate(e.Start.Date);
            }
            if (BillingForm != null)
            {
                BillingForm.SetDate(e.Start.Date);
            }
            if (SelectExportItemDialog != null)
            {
                if (EmployeeSalary)
                {
                    SelectExportItemDialog.SetEmployeeSalaryDate(e.Start.Date);
                }
                else
                {
                    SelectExportItemDialog.SetEmployeeWorkBreakdownDate(e.Start.Date);
                }
            }
            Close();
        }
    }
}
