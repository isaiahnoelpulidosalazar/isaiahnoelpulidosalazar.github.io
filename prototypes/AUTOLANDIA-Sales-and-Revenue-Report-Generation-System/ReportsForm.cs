using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        public void RefreshHome(int PickedTimeframe)
        {
            SalesChart.Titles.Clear();
            SalesChart.Series.Clear();

            DateTime Today = DateTime.Now;

            if (PickedTimeframe == 0)
            {
                SalesChart.Titles.Add("Daily Sales");
                Series DailySales = SalesChart.Series.Add("Daily Sales");
                DailySales.ChartType = SeriesChartType.Line;
                DailySales.Color = Color.Red;

                DateTime[] Days = new DateTime[7];
                DateTime StartOfWeek = Today.AddDays(-(int)Today.DayOfWeek);
                for (int a = 0; a < 7; a++)
                {
                    Days[a] = StartOfWeek.AddDays(a);
                    DailySales.Points.AddXY(Days[a].ToString("d"), 0);
                }

                for (int a = 0; a < Days.Length; a++)
                {
                    double TotalSales = 0;

                    foreach (BillingItem Billing in GlobalBillingList)
                    {
                        if (DateTime.Parse(Billing.LastUpdated).ToString("d").Equals(Days[a].ToString("d")) && Billing.Status.Equals("Paid"))
                        {
                            TotalSales += Convert.ToDouble(Billing.Balance - (Billing.Balance * (Billing.Discount / 100)));
                        }
                    }

                    DailySales.Points[a].SetValueY(TotalSales);
                }
            }
            if (PickedTimeframe == 1)
            {
                SalesChart.Titles.Add("Monthly Sales");
                Series MonthlySales = SalesChart.Series.Add("Monthly Sales");
                MonthlySales.ChartType = SeriesChartType.Line;
                MonthlySales.Color = Color.Red;
                SalesChart.ChartAreas[0].AxisX.Interval = 1;

                DateTime[] Months = new DateTime[12];
                DateTime StartOfYear = DateTime.Parse(Today.AddMonths(-Today.Month + 1).ToString("Y"));
                for (int a = 0; a < 12; a++)
                {
                    Months[a] = StartOfYear.AddMonths(a);
                    MonthlySales.Points.AddXY(Months[a].ToString("MMMM"), 0);
                }


                for (int a = 0; a < Months.Length; a++)
                {
                    double TotalSales = 0;

                    foreach (BillingItem Billing in GlobalBillingList)
                    {
                        if (DateTime.Parse(Billing.LastUpdated).ToString("Y").Equals(Months[a].ToString("Y")) && Billing.Status.Equals("Paid"))
                        {
                            TotalSales += Convert.ToDouble(Billing.Balance - (Billing.Balance * (Billing.Discount / 100)));
                        }
                    }

                    MonthlySales.Points[a].SetValueY(TotalSales);
                }
            }
        }

        private void DailyButton_Click(object sender, EventArgs e)
        {
            RefreshHome(0);
        }

        private void MonthlyButton_Click(object sender, EventArgs e)
        {
            RefreshHome(1);
        }

        private void ExportDataButton_Click(object sender, EventArgs e)
        {
            ExportDataButton.Enabled = false;
            new SelectExportItemDialog().ShowDialog();
            ExportDataButton.Enabled = true;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            RefreshHome(0);
        }

        private void SummaryButton_Click(object sender, EventArgs e)
        {
            new SummaryDialog().ShowDialog();
        }
    }
}
