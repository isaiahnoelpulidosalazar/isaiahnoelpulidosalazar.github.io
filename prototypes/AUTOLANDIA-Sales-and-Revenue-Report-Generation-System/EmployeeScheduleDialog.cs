using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EmployeeScheduleDialog : MaterialForm
    {
        Color DefaultBackgroundColor;

        public EmployeeScheduleDialog()
        {
            InitializeComponent();

            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();
            EmployeeList.VerticalScroll.Visible = true;
            EmployeeList.HorizontalScroll.Enabled = false;
            EmployeeList.HorizontalScroll.Visible = false;

            EmployeeCalendar.MaxDate = DateTime.Now.Date;
            EmployeeCalendar.MinDate = DateTime.Now.Date.AddMonths(-2);
        }

        void RefreshEmployees(DateTime DateTime)
        {
            EmployeeList.Controls.Clear();
            EmployeeList.RowStyles.Clear();

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                string TimeInText = "No record";
                string TimeOutText = "No record";

                foreach (EmployeeTimeItem EmployeeTime in GlobalEmployeeTimeList)
                {
                    if (EmployeeTime.ID.Equals(Employee.ID) && DateTime.Parse(EmployeeTime.DateCreated).Date == DateTime.Date)
                    {
                        TimeInText = string.IsNullOrEmpty(EmployeeTime.TimeIn) ? "No record" : EmployeeTime.TimeIn;
                        TimeOutText = string.IsNullOrEmpty(EmployeeTime.TimeOut) ? "No record" : EmployeeTime.TimeOut;
                    }
                }

                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                TableLayoutPanel Panel = new TableLayoutPanel
                {
                    ColumnCount = 3
                };
                Label Name = new Label();
                Label TimeIn = new Label();
                Label TimeOut = new Label();

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
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
                Panel.Margin = new Padding(0);

                Name.Dock = DockStyle.Fill;
                Name.Text = $"{Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
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
                TimeIn.Text = TimeInText;
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
                TimeOut.Text = TimeOutText;
                TimeOut.TextAlign = ContentAlignment.MiddleCenter;
                TimeOut.MouseEnter += (sndr, evnt) =>
                {
                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                };
                TimeOut.MouseLeave += (sndr, evnt) =>
                {
                    Panel.BackColor = DefaultBackgroundColor;
                };

                EmployeeList.RowStyles.Add(Row);
                Panel.Controls.Add(Name, 0, 0);
                Panel.Controls.Add(TimeIn, 1, 0);
                Panel.Controls.Add(TimeOut, 2, 0);
                EmployeeList.Controls.Add(Panel);

                tableLayoutPanel2.Width = EmployeeList.Width;
            }
        }

        private void EmployeeCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            RefreshEmployees(e.Start.Date);
        }

        private void EmployeeScheduleDialog_Load(object sender, EventArgs e)
        {
            RefreshEmployees(EmployeeCalendar.MaxDate);
        }
    }
}
