using AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.Properties;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class MainForm : MaterialForm
    {
        bool TabMove = false;
        Point PanelLocation, CursorPosition;
        List<Form> Forms = new List<Form>()
        {
            new HomeForm(),
            new PeopleForm(),
            new ServicesAndPackagesForm(),
            new TransactionsForm(),
            new BillingForm(),
            new ReportsForm(),
            new ActivityRecordForm()

        };
        List<MaterialButton> TabButtons = new List<MaterialButton>();
        LoginForm LoginForm;
        bool Logout = false;

        public MainForm(LoginForm LoginForm)
        {
            InitializeComponent();

            this.LoginForm = LoginForm;
            this.LoginForm.Hide();

            Logo.Image = Resources.autolandia_logo;
            Logo.BackColor = Color.Black;
            TabPanel.BackColor = Color.Black;

            TabButtons.Add(HomeTab);
            TabButtons.Add(PeopleTab);
            TabButtons.Add(ServicesAndPackagesTab);
            TabButtons.Add(TransactionsTab);
            TabButtons.Add(BillingTab);
            TabButtons.Add(ReportsTab);
            TabButtons.Add(ActivityRecordTab);

            GlobalHomeForm = (HomeForm)Forms[0];
            GlobalPeopleForm = (PeopleForm)Forms[1];
            GlobalServicesAndPackagesForm = (ServicesAndPackagesForm)Forms[2];
            GlobalTransactionsForm = (TransactionsForm)Forms[3];
            GlobalBillingForm = (BillingForm)Forms[4];
            GlobalReportsForm = (ReportsForm)Forms[5];
            GlobalActivityRecordForm = (ActivityRecordForm)Forms[6];

            SelectTab(0);

            System.Windows.Forms.Timer FocusTimer = new System.Windows.Forms.Timer();
            FocusTimer.Tick += (s, e) =>
            {
                HomeTab.Focus();
                FocusTimer.Stop();
            };
            FocusTimer.Start();
        }

        void SelectTab(int Index)
        {
            foreach (Form Form in Forms)
            {
                Form.SendToBack();
            }
            Forms[Index].MdiParent = this;
            Forms[Index].Dock = DockStyle.Fill;
            if (Forms[Index].Visible)
            {
                Forms[Index].BringToFront();
            }
            else
            {
                Forms[Index].Show();
            }

            TabButtons[Index].Focus();
        }

        private void HomeTab_Click(object sender, EventArgs e)
        {
            SelectTab(0);
        }

        private void TransactionsTab_Click(object sender, EventArgs e)
        {
            SelectTab(3);
        }

        private void BillingTab_Click(object sender, EventArgs e)
        {
            SelectTab(4);
        }

        private void PeopleTab_Click(object sender, EventArgs e)
        {
            SelectTab(1);
        }

        private void ActivityRecordTab_Click(object sender, EventArgs e)
        {
            SelectTab(6);
        }

        private void ServicesAndPackagesTab_Click(object sender, EventArgs e)
        {
            SelectTab(2);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Logout)
            {
                Application.Exit();
            }
        }

        private void Logo_MouseDown(object sender, MouseEventArgs e)
        {
            TabMove = true;
            PanelLocation = Location;
            CursorPosition = Cursor.Position;
        }

        private void Logo_MouseMove(object sender, MouseEventArgs e)
        {
            if (TabMove)
            {
                Location = new Point(MousePosition.X - (CursorPosition.X - PanelLocation.X), MousePosition.Y - (CursorPosition.Y - PanelLocation.Y));
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            new SettingsDialog().ShowDialog();
        }

        private void ReportsTab_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalReportsForm.RefreshHome(0);
            }
            catch { }
            SelectTab(5);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            LoggedAccount = null;
            Logout = true;
            Close();
            LoginForm.Show();
        }

        private void Logo_MouseUp(object sender, MouseEventArgs e)
        {
            TabMove = false;
        }
    }
}
