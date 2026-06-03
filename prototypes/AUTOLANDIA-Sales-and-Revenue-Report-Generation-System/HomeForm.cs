using System;
using System.Threading;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class HomeForm : Form
    {
        Thread TimeThread;
        string Time = string.Empty;

        public HomeForm()
        {
            InitializeComponent();

            TimeThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    int Hour = Convert.ToInt32(DateTime.Now.ToString("HH"));
                    Time = Hour >= 12 ? (Hour - 12) < 6 ? "afternoon" : "evening" : Hour < 4 ? "evening" : "morning";
                    try
                    {
                        GreetingLabel.BeginInvoke((MethodInvoker)delegate ()
                        {
                            GreetingLabel.Text = $"Good {Time}, {LoggedAccount.Username}! Today is {DateTime.Now.ToString("D")} - {DateTime.Now.ToString("hh:mm:ss tt")}";
                        });
                    }
                    catch
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }));
            TimeThread.Start();
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimeThread.Abort();
        }

        private void QuickCustomer_Click(object sender, EventArgs e)
        {
            new NewCustomerDialog().ShowDialog();
        }

        private void QuickVehicle_Click(object sender, EventArgs e)
        {
            new AddCustomerVehicleDialog().ShowDialog();
        }

        private void QuickOrder_Click(object sender, EventArgs e)
        {
            new NewTransactionDialog().ShowDialog();
        }

        private void QuickSalary_Click(object sender, EventArgs e)
        {
            new SummaryDialog().ShowDialog();
        }

        private void QuickTimeSchedule_Click(object sender, EventArgs e)
        {
            new EmployeeScheduleDialog().ShowDialog();
        }
    }
}
