using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ActivityRecordForm : Form
    {
        Color DefaultBackgroundColor;

        public ActivityRecordForm()
        {
            InitializeComponent();

            ActivityList.Controls.Clear();
            ActivityList.RowStyles.Clear();
            ActivityList.VerticalScroll.Visible = true;
            ActivityList.HorizontalScroll.Enabled = false;
            ActivityList.HorizontalScroll.Visible = false;

            RefreshActivities();
        }

        public void RefreshActivities()
        {
            RecreateGlobalActivityList();

            ActivityList.Controls.Clear();
            ActivityList.RowStyles.Clear();

            foreach (string Activity in GlobalActivityList)
            {
                RefreshRows(Activity);
            }
        }

        void RefreshRows(string Message)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 2
            };
            Label Date = new Label();
            Label Msg = new Label();

            if (DefaultBackgroundColor == null)
            {
                DefaultBackgroundColor = Panel.BackColor;
            }

            Panel.Dock = DockStyle.Fill;
            Panel.ColumnStyles.Clear();
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80f));
            Panel.Margin = new Padding(0);
            Panel.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Panel.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Date.Dock = DockStyle.Fill;
            Date.Text = Message.Split('-')[0].Trim();
            Date.TextAlign = ContentAlignment.MiddleCenter;
            Date.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Date.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Msg.Dock = DockStyle.Fill;
            Msg.Text = Message.Split('-')[1].Trim();
            Msg.TextAlign = ContentAlignment.MiddleCenter;
            Msg.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Msg.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            ActivityList.RowStyles.Add(Row);
            Panel.Controls.Add(Date, 0, 0);
            Panel.Controls.Add(Msg, 1, 0);
            ActivityList.Controls.Add(Panel);

            tableLayoutPanel2.Width = ActivityList.Width;
        }

        private void SearchBarActivity_TextChanged(object sender, System.EventArgs e)
        {
            ActivityList.Controls.Clear();
            ActivityList.RowStyles.Clear();

            foreach (string Activity in GlobalActivityList)
            {
                if (Activity.ToUpper().Contains(SearchBarActivity.Text.ToUpper()))
                {
                    RefreshRows(Activity);
                }
            }
        }
    }
}
